using PlaylistAvaloniaUI.Models;

namespace PlaylistAvaloniaUI.Services
{
    public interface IPlaylistLoader
    {
        PlaylistModel GetPlaylistFromUrl(string url);
    }
}
