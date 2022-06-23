using ReactiveUI;

namespace PlaylistAvaloniaUI.Models
{
    public class SongModel : ReactiveObject
    {
        private string name;
        private string artist;
        private string album;
        private string duration;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                this.RaisePropertyChanged(nameof(Name));
            }
        }
        public string Artist
        {
            get => artist;
            set
            {
                artist = value;
                this.RaisePropertyChanged(nameof(Artist));
            }
        }
        public string Album
        {
            get => album;
            set
            {
                album = value;
                this.RaisePropertyChanged(nameof(Album));
            }
        }
        public string Duration
        {
            get => duration;
            set
            {
                duration = value;
                this.RaisePropertyChanged(nameof(Duration));
            }
        }
    }
}
