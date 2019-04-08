using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_music.Models
{
    public class Tweet
    {
        
        public string Username { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
       public int TweetID { get; set; }


        public Tweet()
        {

        }

        public Tweet(string _username, string _text, string _date, string _url, int _UserId)
        {
            Username = _username;
            Text = _text;
            Date = _date;
            Url = _url;
            UserId = _UserId;
        }


        public List<Tweet> GetTweetsFromAPI(string musictype)
        {
            TwitterAPI t = new TwitterAPI();

            return t.GetTwitterFeeds(musictype);

        }

        public List<Tweet> GetSearchTweetsFromAPI(string query)
        {
            TwitterAPI t = new TwitterAPI();

            return t.GetSearchTweetsFromAPI(query);

        }

        public int AddTweets()
        {
            NewDBservices dbs = new NewDBservices();
            int numAffected = dbs.InsertTweets(this);

            return numAffected;
        }

        public List<Tweet> GetTweetsFromDB(int UserID)
        {
            NewDBservices dbs = new NewDBservices();

            return dbs.GetTweetsFromDB("ConnectionString", "Tweets", UserID);

        }

        public int RemoveTweetFromDB(int TweetID)
        {
            NewDBservices dbs = new NewDBservices();
            return dbs.RemoveTweetFromDB("ConnectionString", "Tweets", TweetID);
        }
    }
}