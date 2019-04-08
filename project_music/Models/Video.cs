using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using WebApplication2.Models;

namespace project_music.Models
{
    public class Video
    {
        public string VideoId { get; set; }
        public string PublishedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int YoutubeID { get; set; }

        
        public Video()
        {

        }

        public Video(string _videoId, string _publishedAt, string _title, string _description, int _userId, int _youtubeID)
        {
            VideoId = _videoId;
            PublishedAt = _publishedAt;
            Title = _title;
            Description = _description;
            UserId = _userId;
            YoutubeID = _youtubeID;
        }

    
        public async System.Threading.Tasks.Task<List<Video>> GetVideosAsync(string musictype)
        {
            YoutubeAPI yo = new YoutubeAPI();
            List<Video> videos = new List<Video>();
            videos = await yo.Run(musictype);
            return videos;
        }

         public int AddYoutubes()
        {
            NewDBservices dbs = new NewDBservices();
            int numAffected = dbs.InsertYoutubes(this);

            return numAffected;
        }

        public List<Video> GetYoutubes(int UserID)
        {
            NewDBservices dbs = new NewDBservices();

            return dbs.GetYoutubesFromDB("ConnectionString", "Youtubes", UserID);

        }
        public int RemoveYoutubeFromDB(string VideoId)
        {
            NewDBservices dbs = new NewDBservices();
            return dbs.RemoveYoutubeFromDB("ConnectionString", "Youtubes", VideoId);
        }
    }
}