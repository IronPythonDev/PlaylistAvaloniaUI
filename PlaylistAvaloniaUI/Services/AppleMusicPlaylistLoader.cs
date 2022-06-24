using HtmlAgilityPack;
using PlaylistAvaloniaUI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlaylistAvaloniaUI.Services
{
    public class AppleMusicPlaylistLoader : IPlaylistLoader
    {
        public string ConvertInnerTextToString(string inputString) => string.Join(' ', inputString.Replace("\n", "").Split(" ").Where(s => !string.IsNullOrEmpty(s)));

        public HtmlDocument GetPlaylistHtmlDocument(string url) => new HtmlWeb().Load(url);
        public string GetPlaylistNameFromHtmlDocument(HtmlDocument htmlDocument) => ConvertInnerTextToString(htmlDocument.DocumentNode.SelectSingleNode(@"//*[@id=""page-container__first-linked-element""]").InnerText);
        public string GetPlaylistDescriptionFromHtmlDocument(HtmlDocument htmlDocument) => htmlDocument.DocumentNode.SelectSingleNode(@"//*[@id=""web-main""]/div[3]/div/div[1]/div/div[1]/div[3]/div/div[3]/div/div/span/p").InnerText;
        public string GetPlaylistAvatarUrlFromHtmlDocument(HtmlDocument htmlDocument) => htmlDocument.DocumentNode.SelectSingleNode(@"/html/body/div[2]/div/div[3]/div/div[1]/div/div[1]/div[2]/div/div/picture/source[1]").Attributes.First(p => p.Name == "srcset").Value.Split(',').First().Split(' ').First();
        public SongModel GetSongFromHtmlNode(HtmlNode htmlNode)
        {
            var songName = ConvertInnerTextToString(htmlNode.ChildNodes.First(p => p.HasClass("songs-list__col--song")).ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText);
            var artist = ConvertInnerTextToString(htmlNode.ChildNodes.First(p => p.HasClass("songs-list__col--artist")).ChildNodes[1].ChildNodes[1].ChildNodes[0].InnerText);
            var duration = ConvertInnerTextToString(htmlNode.ChildNodes.First(p => p.HasClass("songs-list__col--time")).ChildNodes[1].ChildNodes[3].InnerText);
            var album = ConvertInnerTextToString(htmlNode.ChildNodes.First(p => p.HasClass("songs-list__col--album")).ChildNodes[1].ChildNodes[1].InnerText);

            return new SongModel
            {
                Name = songName,
                Artist = artist,
                Duration = duration,
                Album = album,
            };
        }
        public HtmlNode GetSongsContainerFromHtmlDocument(HtmlDocument htmlDocument) => htmlDocument.DocumentNode.SelectSingleNode(@"//*[@id=""web-main""]/div[3]/div/div[1]/div/div[2]");
        public IList<HtmlNode> GetSongRowsFromContainer(HtmlNode containerNode) => containerNode.ChildNodes.Where(p => p.GetAttributeValue("role", null) == "row").ToList();
        public IList<SongModel> GetPlaylistSongsFromHtmlDocument(HtmlDocument htmlDocument)
        {
            var songs = new List<SongModel>();

            foreach (var row in GetSongRowsFromContainer(GetSongsContainerFromHtmlDocument(htmlDocument))) songs.Add(GetSongFromHtmlNode(row));

            return songs;
        }

        public PlaylistModel GetPlaylistFromUrl(string url)
        {
            var htmlDocument = GetPlaylistHtmlDocument(url);

            return new PlaylistModel
            {
                Name = GetPlaylistNameFromHtmlDocument(htmlDocument),
                Description = GetPlaylistDescriptionFromHtmlDocument(htmlDocument),
                AvatarUrl = GetPlaylistAvatarUrlFromHtmlDocument(htmlDocument),
                Songs = new System.Collections.ObjectModel.ObservableCollection<SongModel>(GetPlaylistSongsFromHtmlDocument(htmlDocument))
            };
        }
    }
}
