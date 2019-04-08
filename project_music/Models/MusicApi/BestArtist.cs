using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_music.Models
{
    public class BestArtist
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }


        public BestArtist()
        {

        }

        public BestArtist(string _name, string _image, string _url)
        {
            Name = _name;
            Image = _image;
            Url = _url;
        }
    }
}