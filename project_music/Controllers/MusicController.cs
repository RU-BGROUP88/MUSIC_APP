using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project_music.Models;
using Hammock.Serialization;

namespace project_music.Controllers
{
    public class MusicController : ApiController
    {

        //----------הבאת רשימת הזמרים המובחרים לפי סגנון המוזיקה הנבחר----------------//

        [HttpGet]
        [Route("api/Music/TopArtists")]
        public List<BestArtist> GetTopArtists(string musictype)
        {

            MusicDataAPI mu = new MusicDataAPI();

            return mu.GetTopArtists(musictype);
        }

        //----------הבאת רשימת השירים המובחרים לפי סגנון המוזיקה הנבחר----------------//

        [HttpGet]
        [Route("api/Music/TopTracks")]
        public List<BestTracks> GetTopTracks(string musictype)
        {

            MusicDataAPI mu = new MusicDataAPI();

            return mu.GetTopTracks(musictype);
        }

        //----------הבאת רשימת האלבומים המובחרים לפי סגנון המוזיקה הנבחר----------------//

        [HttpGet]
        [Route("api/Music/TopAlbums")]
        public List<BestAlbums> GetTopAlbums(string musictype)
        {

            MusicDataAPI mu = new MusicDataAPI();

            return mu.GetTopAlbums(musictype);
        }

        //----------הבאת המידע על הזמר לפי שמו----------------//

        [HttpGet]
        [Route("api/Music/ArtistInfo")]
        public ArtistInformation GetArtistInfo(string ArtistName)
        {

            MusicDataAPI mu = new MusicDataAPI();

            return mu.GetArtistInfo(ArtistName);
        }


    }
}