using PlaylistAvaloniaUI.Models;
using PlaylistAvaloniaUI.Services;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace PlaylistAvaloniaUI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        string[] playlists = new string[]
        {
            "https://music.apple.com/ua/playlist/a-list-pop/pl.5ee8333dbe944d9f9151e97d92d1ead9",
            "https://music.apple.com/ua/playlist/rap-life/pl.abe8ba42278f4ef490e3a9fc5ec8e8c5"
        };

        private PlaylistModel _selectedPlaylist;
        public ObservableCollection<PlaylistModel> Playlists { get; set; } = new ObservableCollection<PlaylistModel>();
        public PlaylistModel SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                _selectedPlaylist = value;

                this.RaisePropertyChanged(nameof(SelectedPlaylist));
            }
        }

        public MainWindowViewModel()
        {
            var loader = new AppleMusicPlaylistLoader();

            foreach (var url in playlists) Playlists.Add(loader.GetPlaylistFromUrl(url));
        }
    }
}
