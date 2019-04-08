using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetSharp;
using Newtonsoft.Json;
using System.Text;
using project_music.Models;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using LinqToTwitter;
using System.Configuration;

namespace project_music.Models
{
    public class TwitterAPI
    {
        string _consumerKey = "ACA3Mxln5AKOHsE3V80o6r7cZ";
        string _consumerSecret = "FwSClntH6Yzz2JvR2Sa82eVb1HP98JRJRLwxkl3nPA7gsAXjAj";
        string _accessToken = "1097821566866472962-V78CCo2kjL231JNLdBFQ88LL1tB0Dq";
        string _accessTokenSecret = "inJ8TvRGtGhzVwd4rwBAXBzjNU85O5u56FktaPMko7Db9";
        private string status;
        private List<Tweet> statuses;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public List<Tweet> Statuses
        {
            get { return statuses; }
            set { statuses = value; }
        }

        public TwitterAPI()
        {

        }


        public List<Tweet> GetTwitterFeeds(string musictype)
        {
            if (musictype == "Classical" || musictype == "blues" || musictype == "acoustic" || musictype == "rock" || musictype == "metal" || musictype == "funk")
            {
                musictype = musictype + "music";
            }
            if(musictype == "classic rock")
            {
                musictype = "classicrock";
            }
            var auth = new SingleUserAuthorizer
            {

                CredentialStore = new InMemoryCredentialStore()
                {

                    ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                    OAuthToken = ConfigurationManager.AppSettings["accessToken"],
                    OAuthTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]

                }

            };
            var twitterCtx = new TwitterContext(auth);
            var ownTweets = new List<Status>();

            var searchResults =
               (from search in twitterCtx.Search
                where search.Type == SearchType.Search &&
                      search.Query == musictype

                select search.Statuses)
               .SingleOrDefault();

            List<Tweet> result = new List<Tweet>();
            foreach (var tweet in searchResults)
            {
                Tweet newTweet = new Tweet();
                newTweet.Username = tweet.User.Name.ToString();
                newTweet.Text = tweet.Text.ToString();
                newTweet.Date = tweet.CreatedAt.ToString();
                newTweet.ProfileImageUrl = tweet.User.ProfileImageUrl.ToString();
                if (tweet.User.Url != null)
                {
                    newTweet.Url = tweet.User.Url.ToString();
                }
                result.Add(newTweet);
            }
            return result;


        }

        public List<Tweet> GetSearchTweetsFromAPI(string query)
        {
          
            var auth = new SingleUserAuthorizer
            {

                CredentialStore = new InMemoryCredentialStore()
                {

                    ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"],
                    OAuthToken = ConfigurationManager.AppSettings["accessToken"],
                    OAuthTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]

                }

            };
            var twitterCtx = new TwitterContext(auth);
            var ownTweets = new List<Status>();

            var searchResults =
               (from search in twitterCtx.Search
                where search.Type == SearchType.Search &&
                      search.Query == query

                select search.Statuses)
               .SingleOrDefault();

            List<Tweet> result = new List<Tweet>();
            foreach (var tweet in searchResults)
            {
                Tweet newTweet = new Tweet();
                newTweet.Username = tweet.User.Name.ToString();
                newTweet.Text = tweet.Text.ToString();
                newTweet.Date = tweet.CreatedAt.ToString();
                newTweet.ProfileImageUrl = tweet.User.ProfileImageUrl.ToString();
                if (tweet.User.Url != null)
                {
                    newTweet.Url = tweet.User.Url.ToString();
                }
                result.Add(newTweet);
            }
            return result;


        }
    }
}