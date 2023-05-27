using MonoTorrent.Client;
using MonoTorrent.Client.PortForwarding;
using MonoTorrent.Client.Tracker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbdTorrentClient.Models
{
    public class TorrentLife
    {

        public PeerConnectedEventArgs PeerConnectedEventArgs { get; set; }

        public ConnectionAttemptFailedEventArgs ConnectionAttemptFailedEventArgs { get; set; }

        public PieceHashedEventArgs PieceHashedEventArgs { get; set; }

        public TorrentStateChangedEventArgs TorrentStateChangedEventArgs { get; set; }

        public AnnounceResponseEventArgs AnnounceResponseEventArgs { get; set; }

        public ClientEngine Engine { get; set; }

        public ObservableCollection<TorrentManager> Managers { get; set; }

        public Mapping Mapping { get; set; }



        public object? Sender { get; set; }



    }
}
