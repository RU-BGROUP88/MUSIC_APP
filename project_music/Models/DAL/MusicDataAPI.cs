using System.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using project_music.Models;
using System.Xml.XPath;

namespace project_music.Models
{
    public class MusicDataAPI
    {
        public MusicDataAPI() { }


        public List<BestArtist> GetTopArtists(string musictype)
        {
            List<BestArtist> results = new List<BestArtist>();
            BestArtist BestArtist = new BestArtist();
            string s = "ws.audioscrobbler.com/2.0/?method=tag.gettopartists&tag=" + musictype + "&api_key=ab32aa2c2265e2ebd1c8fe914f9e4d82&format=json";
            string json = new WebClient().DownloadString("http://" + s);
            var result = JsonConvert.DeserializeObject<TopArtist>(json);

            for (int i = 0; i < 10; i++)
            {
                BestArtist Newartist = new BestArtist();
                Newartist.Name = result.topartists.artist[i].name.ToString();
                Newartist.Url = result.topartists.artist[i].url.ToString();
                Newartist.Image = result.topartists.artist[i].image[2].ToString();

                results.Add(Newartist);
            }
           
            return results;
        }

        public List<BestTracks> GetTopTracks(string musictype)
        {
            List <BestTracks> results = new List<BestTracks>();
            BestTracks BestArtist = new BestTracks();
            string s = "ws.audioscrobbler.com/2.0/?method=tag.gettoptracks&tag="+ musictype +"&api_key=ab32aa2c2265e2ebd1c8fe914f9e4d82&format=json";
            string json = new WebClient().DownloadString("http://" + s);
            var result = JsonConvert.DeserializeObject<TopTracks>(json);

            for (int i = 0; i < 10; i++)
            {
                BestTracks Newtrack = new BestTracks();
                Newtrack.NameTrack = result.tracks.track[i].name;
                Newtrack.NameArtist = result.tracks.track[i].artist.name;
                Newtrack.Url = result.tracks.track[i].url;
                Newtrack.Image = result.tracks.track[i].image[2].text;

                results.Add(Newtrack);
            }

            return results;
        }


        public List<BestAlbums> GetTopAlbums(string musictype)
        {
            List<BestAlbums> results = new List<BestAlbums>();
            string s = "ws.audioscrobbler.com/2.0/?method=tag.gettopalbums&tag="+ musictype +"&api_key=ab32aa2c2265e2ebd1c8fe914f9e4d82&format=json";
            string json = new WebClient().DownloadString("http://" + s);
            var result = JsonConvert.DeserializeObject<TopAlbums>(json);

            for (int i = 0; i < 10; i++)
            {
                BestAlbums Newalbum = new BestAlbums();
                Newalbum.NameAlbum = result.albums.album[i].name;
                Newalbum.NameArtist = result.albums.album[i].artist.name;
                Newalbum.Url = result.albums.album[i].url;
                Newalbum.Image = result.albums.album[i].image[2].text;

                results.Add(Newalbum);
            }

            return results;
        }

     

        public ArtistInformation GetArtistInfo(string ArtistName)
        {
            ArtistInformation artist = new ArtistInformation();

            try
            {
                string s = "ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=" + ArtistName + "&api_key=ab32aa2c2265e2ebd1c8fe914f9e4d82&format=json";
                string json = new WebClient().DownloadString("http://" + s);
                var result = JsonConvert.DeserializeObject<ArtistInfo>(json);
                artist.NameArtist = result.artist.name;
                artist.Url = result.artist.url;
                artist.Summary = result.artist.bio.summary;
                artist.Content = result.artist.bio.content;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
          
           

            return artist;




        }

    }

}
