using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlaylistAvaloniaUI.Models
{
    public class PlaylistModel : ReactiveObject
    {
        private string name = default!;
        private string description = default!;
        private string avatarUrl = default!;
        private Avalonia.Media.Imaging.Bitmap avatar = default!;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                this.RaisePropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                this.RaisePropertyChanged(nameof(Description));
            }
        }
        public string AvatarUrl
        {
            get => avatarUrl;
            set
            {
                avatarUrl = value;
                DownloadAvatar();
                this.RaisePropertyChanged(nameof(AvatarUrl));
            }
        }
        public Avalonia.Media.Imaging.Bitmap Avatar
        {
            get => avatar;
            set
            {
                avatar = value;
                this.RaisePropertyChanged(nameof(Avatar));
            }
        }

        public ObservableCollection<SongModel> Songs { get; set; } = new();

        public async Task DownloadAvatar()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(AvatarUrl);
                response.EnsureSuccessStatusCode();
                Avatar = new Avalonia.Media.Imaging.Bitmap(response.Content.ReadAsStream());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Exception] {ex.Message}");
                Avatar = null;
            }
        }
    }
}
