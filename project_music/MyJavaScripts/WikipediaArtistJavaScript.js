
$(document).ready(function () {
    $("#container").hide();

    loadlocalstorage();
    GetTopArtists();
    GetTopTracks();
    GetTopAlbums();
    $("#WikepediaAristForm").submit(f1);


});



$('input[type=checkbox]').each(function () {
    var sThisVal = (this.checked ? $(this).val() : "");
});
//-------------upload data content from local store
function loadlocalstorage() {


    UserId = JSON.parse(localStorage["UserId"]);
    Name = JSON.parse(localStorage["Name"]);
    FamilyName = JSON.parse(localStorage["FamilyName"]);
    Gender = JSON.parse(localStorage["Gender"]);
    Age = JSON.parse(localStorage["Age"]);
    Address = JSON.parse(localStorage["Address"]);
    Email = JSON.parse(localStorage["Email"]);
    musictype = JSON.parse(localStorage["MusicType"]);
    Password = JSON.parse(localStorage["Password"]);
    Image = JSON.parse(localStorage["Image"]);
    str1 = "";
    str1 += " <div class='w3-container'>"
    str1 += " <h4 class='w3-center'>My Profile</h4>"
    str1 += " <p class='w3-center' id = 'image'>" + Image + "</p>"
    str1 += "   <hr>"
    str1 += "  <p><i class='fa fa-pencil fa-fw w3-margin-right w3-text-theme'></i> " + Name + " " + FamilyName + "</p>"
    str1 += " <p><i class='fa fa-home fa-fw w3-margin-right w3-text-theme'></i> " + Address + "</p>"
    //str +=" <p><i class='fa fa-birthday-cake fa-fw w3-margin-right w3-text-theme'></i> April 1, 1988</p>"
    str1 += " </div>"
    $("#profile").append(str1);


}
function f1() {
    GetArtistInfo();
    return false; // the return false will prevent the form from being submitted
    // hence the page will not reload
}


function GetArtistInfo() {


    ArtistName = $("#search").val();
    ajaxCall("GET", "../api/Music/ArtistInfo?ArtistName=" + ArtistName, "", successGetArtistInfo, errorGetArtistInfo);

}
function successGetArtistInfo(data) {
    $("#container").show();

    ArtistInformation = data;

    $("#container").empty();
    str = "";

    str += "  <div class='w3-container w3-card w3-white w3-round w3-margin' >"
    str += "  <br>"
    str += "<h1>Artist Name: " + data.NameArtist + "</h1></br>";
    str += "<a href=" + data.Url + ">" + data.Url + "</a></br></br>"
    str += "<h1>Summary </h1></br>";
    str += "<p>" + data.Summary + "</p>";
    str += "<h1>Content </h1></br>";
    str += "<p>" + data.Content + "</p>";

    str += "</div>"


    $("#container").append(str);



}

function errorGetArtistInfo(err)
{
    swal("function GetArtistInfo got wrong!" + err, "", "error");
}

function GetTopArtists() {
    ajaxCall("GET", "../api/Music/TopArtists?musictype=" + musictype, "", successGetTopArtists, errorGetTopArtists);


}
function successGetTopArtists(data) {
    $("#Interests").empty();
    $("#Demo1").empty();
    string1 = "";
    interests = "";
    for (var i = 0; i < data.length; i++) {
        string1 += "<p>" + data[i].Name + "</p>";
        string1 += "<a href=" + data[i].Url + ">" + data[i].Url + "</a><br><br>";
        interests += "<span class='w3-tag w3-small w3-theme-d5'>" + data[i].Name + "</span>"

    }
    interests += "<span class='w3-tag w3-small w3-theme-d5'>" + musictype + "</span>"
    $("#Interests").append(interests);
    $("#Demo1").append(string1);
}
function errorGetTopArtists(err) {
    $("#Demo1").empty();
    $("#Demo1").append("not found Top Artists");
}
function GetTopTracks() {
    ajaxCall("GET", "../api/Music/TopTracks?musictype=" + musictype, "", successGetTopTracks, errorGetTopTracks);


}

function successGetTopTracks(data) {
    $("#Demo2").empty();
    string2 = "";
    for (var i = 0; i < data.length; i++) {
        string2 += "<p>" + data[i].NameArtist + "-" + data[i].NameTrack + "</p>";
        string2 += "<a href=" + data[i].Url + ">" + data[i].Url + "</a><br><br>";

    }
    $("#Demo2").append(string2);
}

function errorGetTopTracks(err) {
    $("#Demo2").empty();
    $("#Demo2").append("not found Top Tracks");
}

function GetTopAlbums() {
    ajaxCall("GET", "../api/Music/TopAlbums?musictype=" + musictype, "", successGetTopAlbums, errorGetTopAlbums);


}

function successGetTopAlbums(data) {
    $("#Demo3").empty();
    string3 = "";
    for (var i = 0; i < data.length; i++) {
        string3 += "<p>" + data[i].NameArtist + "-" + data[i].NameAlbum + "</p>";
        string3 += "<a href=" + data[i].Url + ">" + data[i].Url + "</a><br><br>";

    }
    $("#Demo3").append(string3);
}

function errorGetTopAlbums(err) {
    $("#Demo3").empty();
    $("#Demo3").append("not found Top Albums");
}


    // Accordion
    function myFunction(id) {
        var x = document.getElementById(id);
        if (x.className.indexOf("w3-show") == -1) {
            x.className += " w3-show";
            x.previousElementSibling.className += " w3-theme-d1";

        } else {
            x.className = x.className.replace("w3-show", "");
            x.previousElementSibling.className =
                x.previousElementSibling.className.replace(" w3-theme-d1", "");

        }
    }

    // Used to toggle the menu on smaller screens when clicking on the menu button
    function openNav() {
        var x = document.getElementById("navDemo");
        if (x.className.indexOf("w3-show") == -1) {
            x.className += " w3-show";
        } else {
            x.className = x.className.replace(" w3-show", "");
        }
    }
   