using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_music.Models
{
    public class BestTracks
    {
        public string NameTrack { get; set; }
        public string NameArtist { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }


        public BestTracks()
        {

        }

        public BestTracks(string _nameTrack, string _nameArtist, string _image, string _url)
        {
            NameTrack = _nameTrack;
            NameArtist = _nameArtist;
            Image = _image;
            Url = _url;
        }
    }
}