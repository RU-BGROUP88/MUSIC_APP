using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_music.Models
{
    public class BestAlbums
    {
        public string NameAlbum { get; set; }
        public string NameArtist { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }


        public BestAlbums()
        {

        }

        public BestAlbums(string _nameAlbum, string _nameArtist, string _image, string _url)
        {
            NameAlbum = _nameAlbum;
            NameArtist = _nameArtist;
            Image = _image;
            Url = _url;
        }
    }
}