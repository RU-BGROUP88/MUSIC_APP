using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using WebApplication2.Models;
//using project_music.AppCode;
using project_music.Models;
using Hammock.Serialization;

namespace project_music.Controllers
{
    public class YoutubeController : ApiController
    {
        //----------קבלת הסרטונים לפי סגנון המוזיקה הנבחר של המשתמש----------------//

        [HttpGet]
        [Route("api/Youtube")]
        public async System.Threading.Tasks.Task<List<Video>> Get(string musictype)
        {
            
            Video v = new Video();

            return await v.GetVideosAsync(musictype);
        }

        //----------הבאת הסרטונים לפלייליסט של המשתמש לפי המספר המזהה שלו----------------//

        [HttpGet]
        [Route("api/Youtube")]
        public List<Video> Get(int UserId)
        {
            Video vi = new Video();

            return vi.GetYoutubes(UserId);

        }
        //----------הוספת הסרטון ליוטיוב פלייליסט עבור המשתמש שנשמר בדאטה בייס----------------//

        [HttpPost]
        [Route("api/Youtube")]
        public void Post([FromBody]Video vid)
        {
            vid.AddYoutubes();
        }

        //----------מחיקת הסרטון מהיוטיוב פלייליסט של המשתמש----------------//

        [HttpPut]
        [Route("api/Youtube")]
        public int RemoveYoutubeFromDB(string VideoId)
        {
            Video v = new Video();
            return v.RemoveYoutubeFromDB(VideoId);
        }

    }
}