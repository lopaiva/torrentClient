using ArbdTorrentClient.Models;
using ArbdTorrentClient.Utils;
using MonoTorrent.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace ArbdTorrentClient
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private CancellationTokenSource cancellation;

        public Command StartDownloadCommand { get; set; }
        public Command StopDownloadCommand { get; set; }

        private StandardDownloader downloader;

        private ClientEngine engine;
        public ClientEngine Engine 
        {
            get { return engine; }
            set { engine = value; RaisePropertyChange(nameof(Engine)); }
        }

        private ObservableCollection<TorrentManager> managers;
        public ObservableCollection<TorrentManager> Managers
        {
            get { return managers; }
            set { managers = value; RaisePropertyChange(nameof(Managers)); }
        }

        private int torrentsCount;
        public int TorrentsCount
        {
            get { return torrentsCount; }
            set { torrentsCount = value; RaisePropertyChange(nameof(TorrentsCount)); }
        }

        private ObservableCollection<Torrent> torrents;
        public ObservableCollection<Torrent> Torrents
        {
            get { return torrents; }
            set { torrents = value; RaisePropertyChange(nameof(Torrents)); }
        }

        private async void UpdateTorrents()
        {
            await Task.Run(() => 
            { 
                while (true)
                {
                    Thread.Sleep(500);
                    App.Current.Dispatcher.Invoke(() => 
                    {
                        if (Engine != null && Engine.Torrents != null)
                        {
                            TorrentsCount = Engine.Torrents.Count;
                            foreach (var t in Engine.Torrents)
                            {
                                if (Torrents.Count(o => t.Torrent.Name.Equals(o.Name)) == 0)
                                {
                                    var torrent = new Torrent();
                                    SetTorrentValue(torrent, t);
                                    Torrents.Add(torrent);
                                }
                                else
                                {
                                    var torrent = Torrents.FirstOrDefault(o => o.Name.Equals(t.Torrent.Name));
                                    if (torrent != null)
                                    {
                                        SetTorrentValue(torrent, t);
                                    }
                                }
                            }

                        }
                    });
                }
            });
        }

        private void SetTorrentValue(Torrent torrent, TorrentManager t)
        {
            torrent.Name = t.Torrent.Name;
            torrent.Progress = t.Progress;
            torrent.DownloadSpeed = t.Monitor.DownloadSpeed;
            torrent.DataBytesDownloaded = t.Monitor.DataBytesDownloaded;
            torrent.UploadSpeed = t.Monitor.UploadSpeed;
            torrent.DataBytesUploaded = t.Monitor.DataBytesUploaded;
        }

        public MainViewModel()
        {
            cancellation = new CancellationTokenSource();
            Engine = CreateClientEngine();
            downloader = new StandardDownloader(Engine);

            StartDownloadCommand = new Command(p => StartDownload());
            StopDownloadCommand = new Command(p => StopDownload());

            Torrents = new ObservableCollection<Torrent>();

            UpdateTorrents();
        }

        private async void StartDownload()
        {
            await downloader.DownloadAsync(cancellation.Token);
        }

        private void StopDownload()
        {
            cancellation.Cancel();
        }
        
        public ClientEngine CreateClientEngine()
        {
            // Give an example of how settings can be modified for the engine.
            var settingBuilder = new EngineSettingsBuilder
            {
                // Allow the engine to automatically forward ports using upnp/nat-pmp (if a compatible router is available)
                AllowPortForwarding = true,

                // Automatically save a cache of the DHT table when all torrents are stopped.
                AutoSaveLoadDhtCache = true,

                // Automatically save 'FastResume' data when TorrentManager.StopAsync is invoked, automatically load it
                // before hash checking the torrent. Fast Resume data will be loaded as part of 'engine.AddAsync' if
                // torrent metadata is available. Otherwise, if a magnetlink is used to download a torrent, fast resume
                // data will be loaded after the metadata has been downloaded. 
                AutoSaveLoadFastResume = true,

                // If a MagnetLink is used to download a torrent, the engine will try to load a copy of the metadata
                // it's cache directory. Otherwise the metadata will be downloaded and stored in the cache directory
                // so it can be reloaded later.
                AutoSaveLoadMagnetLinkMetadata = true,

                // Use a fixed port to accept incoming connections from other peers.
                //ListenPort = 55123,
                ListenPort = 41170,

                // Use a random port for DHT communications.
                DhtPort = 55120,
            };
            return new ClientEngine(settingBuilder.ToSettings());
        }

    }
}
