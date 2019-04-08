using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_music.Models
{
    public class ArtistInformation
    {
        public string NameArtist { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }



        public ArtistInformation()
        {

        }

        public ArtistInformation( string _nameArtist, string _url, string _summary, string _content)
        {
            NameArtist = _nameArtist;
            Url = _url;
            Summary = _summary;
            Content = _content;


        }
    }
}