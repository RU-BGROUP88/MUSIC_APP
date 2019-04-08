using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_music.Models
{


    public class TopArtist
    {
        public Topartists topartists { get; set; }


        public class Topartists
        {
            public artists[] artist { get; set; }
            public Attr @attr { get; set; }
        }

        public class Attr
        {
            public string tag { get; set; }
            public string page { get; set; }
            public string perPage { get; set; }
            public string totalPages { get; set; }
            public string total { get; set; }
        }

        public class artists
        {
            public string name { get; set; }
            public string mbid { get; set; }
            public string url { get; set; }
            public string streamable { get; set; }
            public Image[] image { get; set; }
            public Attr1 attr { get; set; }
        }

        public class Attr1
        {
            public string rank { get; set; }
        }

        public class Image
        {
            public string text { get; set; }
            public string size { get; set; }
        }

    }
    
}