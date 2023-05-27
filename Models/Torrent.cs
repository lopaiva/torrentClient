using ArbdTorrentClient.Utils;

namespace ArbdTorrentClient.Models
{
    public class Torrent : NotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChange(nameof(Name)); }
        }

        private double progress;
        public double Progress
        {
            get { return progress; }
            set { progress = value; RaisePropertyChange(nameof(Progress)); }
        }

        private long downloadSpeed;
        public long DownloadSpeed
        {
            get { return downloadSpeed; }
            set { downloadSpeed = value; RaisePropertyChange(nameof(DownloadSpeed)); }
        }

        private long dataBytesDownloaded;
        public long DataBytesDownloaded
        {
            get { return dataBytesDownloaded; }
            set { dataBytesDownloaded = value; RaisePropertyChange(nameof(DataBytesDownloaded)); }
        }

        private long uploadSpeed;
        public long UploadSpeed
        {
            get { return uploadSpeed; }
            set { uploadSpeed = value; RaisePropertyChange(nameof(UploadSpeed)); }
        }

        private long dataBytesUploaded;
        public long DataBytesUploaded
        {
            get { return dataBytesUploaded; }
            set { dataBytesUploaded = value; RaisePropertyChange(nameof(DataBytesUploaded)); }
        }

    }
}
