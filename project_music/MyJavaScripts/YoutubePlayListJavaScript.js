
$(document).ready(function () {
    $("#container").hide();
    str = "<div class='loader'></div><p id='loader'>Loading Videos</p>"
    $("#phloader").append(str);
    loadlocalstorage();
    GetTopArtists();
    GetTopTracks();
    GetTopAlbums();
    GetYoutubesfromDB();
    $("#YoutubeForm").submit(f1);


});

function AddYoutubeToDB(ObjectNumber) {

    Youtube = { // Note that the name of the fields must be identical to the names of the properties of the object in the server

        VideoId: Youtubes[ObjectNumber].VideoId,
        PublishedAt: Youtubes[ObjectNumber].PublishedAt,
        Title: Youtubes[ObjectNumber].Title,
        Description: Youtubes[ObjectNumber].Description,

    }
    ajaxCall("POST", "../api/Youtube", JSON.stringify(Youtube), successYoutube, errorYoutube);

}

function successYoutube(data) {
    swal("Added Successfuly!", "App Music Added The Video To Youtube List ", "success");


}

function errorYoutube(err) {
    swal("function AddYoutubeToDB got wrong!" + err, "", "error");
}

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
    GetVideos();
    return false; // the return false will prevent the form from being submitted
    // hence the page will not reload
}


function GetVideos() {


    musictype = $("#search").val(),
        ajaxCall("GET", "../api/Youtube/?musictype=" + musictype, "", successGetVideos, errorGetVideos);

}
function successGetVideos(data) {
    $("#container").show();

    Youtubes = data;
    $("#container").empty();
    str = "";
    for (var i = 0; i < data.length; i++) {
        str += "  <div class='w3-container w3-card w3-white w3-round w3-margin' >"
        str += "  <br>"
        str += "<h1>" + data[i].Title + "</h1>";
        str += "<iframe frameborder='0' style='min-width:99%;min-height:400px;' src='https://www.youtube.com/embed/" + data[i].VideoId + "'" + "> </iframe> ";
        str += "<p>" + data[i].Description + "</p>";
        str += "<p>" + data[i].PublishedAt + "</p>";
        str += "<button type=submit onclick='AddYoutubeToDB(" + i + ")'  value= " + i + ">Save</button>"

        str += "</div>"
    }

    $("#container").append(str);
    $("#phloader").empty();

}

function errorGetVideos(err) {
    swal("GetVideos got wrong!"+err, "", "error");
}

function GetTopArtists() {
    ajaxCall("GET", "../api/Music/TopArtists?musictype=" + musictype, "", successGetTopArtists, errorGetTopArtists);


}

function successGetTopArtists(data) {
    $("#Demo1").empty();
    $("#Interests").empty();
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
function GetYoutubesfromDB() {


    ajaxCall("GET", "../api/Youtube/?UserId=" + UserId, "", successGetYoutubes, errorGetYoutubes);

}
function successGetYoutubes(data) {
    Youtubes = data;
    $("#container").show();

    $("#container").empty();
    str = "";
    for (var i = 0; i < data.length; i++) {
        str += "  <div class='w3-container w3-card w3-white w3-round w3-margin' >"
        str += "  <br>"
        str += "<h1>" + data[i].Title + "</h1>";
        str += "<iframe frameborder='0' style='min-width:99%;min-height:400px;' src='https://www.youtube.com/embed/" + data[i].VideoId + "'" + "> </iframe> ";
        str += "<p>" + data[i].Description + "</p>";
        str += "<p>" + data[i].PublishedAt + "</p>";
        str += "<button type=submit onclick='RemoveYoutubeFromDB(" + i + ")'  value= " + i + ">Remove</button>"

        str += "</div>"
    }
    $("#phloader").empty();

    $("#container").append(str);

}

function errorGetYoutubes(err) {
    swal("function GetYoutubes got wrong!" + err, "", "error");
}
function RemoveYoutubeFromDB(ObjectNumber) {
    VideoId = Youtubes[ObjectNumber].VideoId,

        ajaxCall("Put", "../api/Youtube/?VideoId=" + VideoId, "", successRemoveYoutubes, errorRemoveYoutubes);


}
function successRemoveYoutubes() {
    swal("Remove Successfuly from playlist!", "", "success");
    GetYoutubesfromDB();


}

function errorRemoveYoutubes(err) {
    swal("function Remove from playlist going wrong"+err, " ", "error");


}

function GetVideos() {


    query = $("#search").val(),
        ajaxCall("GET", "../api/Youtube/Videos?query=" + query, "", successGetYoutubes, errorGetYoutubes);

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

