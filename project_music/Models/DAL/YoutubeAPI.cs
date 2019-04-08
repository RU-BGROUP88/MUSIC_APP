using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Linq;
using System.Text;
using LinqToTwitter;

namespace project_music.Models
{
    public class YoutubeAPI
    {
        public YoutubeAPI()
        {

        }
        public async Task<List<Video>> Run(string musictype)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBlZ1dFjZGI7easyZu7oicVDq43OPGzp3s",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = musictype; // Replace with your search term.
            searchListRequest.MaxResults = 10;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<Video> videos = new List<Video>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();
            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        Video v = new Video();
                        v.VideoId =  searchResult.Id.VideoId;
                        v.Title = searchResult.Snippet.Title;
                        v.Description = searchResult.Snippet.Description;
                        v.PublishedAt = searchResult.Snippet.PublishedAt.ToString() ;
                        videos.Add(v);
                        break;

               
                }
            }
            return videos;
        }



    }
}