using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project_music.Models;
using Hammock.Serialization;
using LinqToTwitter;

namespace project_music.Controllers
{
    public class TwitterController : ApiController
    {



        //---------הבאת טוויטים לפי הגדרת המוזיקה הנבחרת של המשתמש--------//
        [HttpGet]
        [Route("api/Twitter/Tweets")]
        public List<Tweet> GetTweets(string musictype)
        {

            Tweet twe = new Tweet();

            return twe.GetTweetsFromAPI(musictype);
        }

        //--------- הבאת טוויטים לפי ערך החיפוש הרצוי--------//

        [HttpGet]
        [Route("api/Twitter/Tweets")]
        public List<Tweet> GetSearchTweets(string query)
        {

            Tweet twe = new Tweet();

            return twe.GetSearchTweetsFromAPI(query);
        }

        //--------- הכנסת הטוויט לרשימת טוויטר פלייליסט בדאטה בייס--------//

        [HttpPost]
        [Route("api/Twitter")]
        public void Post([FromBody]Tweet tweet)
        {
            tweet.AddTweets();
        }
        //--------- הבאת רשימת הטוויטים לטוויטר פלייליסט לפי המספר המזהה של המשתמש--------//

        [HttpGet]
        [Route("api/Twitter")]
        public List<Tweet> Get(int UserId)
        {

            Tweet twe = new Tweet();

            return twe.GetTweetsFromDB(UserId);
        }

        //--------- מחיקת הטוויט של המשמתמש לפי המספר המזהה של הטוויט--------//

        [HttpPut]
        [Route("api/Twitter")]
        public int RemovetweetFromDB(int TweetID)
        {
            Tweet t = new Tweet();
            return t.RemoveTweetFromDB(TweetID);
        }
    }
}
