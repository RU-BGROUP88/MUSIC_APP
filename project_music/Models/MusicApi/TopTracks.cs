using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_music.Models
{

    public class TopTracks
    {
        public Tracks tracks { get; set; }


        public class Tracks
        {
            public Track[] track { get; set; }
            public Attr attr { get; set; }
        }

        public class Attr
        {
            public string tag { get; set; }
            public string page { get; set; }
            public string perPage { get; set; }
            public string totalPages { get; set; }
            public string total { get; set; }
        }

        public class Track
        {
            public string name { get; set; }
            public string duration { get; set; }
            public string mbid { get; set; }
            public string url { get; set; }
            public Streamable streamable { get; set; }
            public Artist artist { get; set; }
            public Image[] image { get; set; }
            public Attr1 attr { get; set; }
        }

        public class Streamable
        {
            public string text { get; set; }
            public string fulltrack { get; set; }
        }

        public class Artist
        {
            public string name { get; set; }
            public string mbid { get; set; }
            public string url { get; set; }
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