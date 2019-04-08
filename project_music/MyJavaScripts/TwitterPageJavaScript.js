
$(document).ready(function () {
    str = "<div class='loader'></div><p id='loader'>Loading Tweets</p>"
    $("#phloader").append(str);
    $("#mainstory").hide()
    loadlocalstorage();
    GetTopArtists();
    GetTopTracks();
    GetTopAlbums();
    $("#TwitterForm").submit(LoadTweets);


});

function AddTweetToDB(ObjectNumber) {


    tweet = { // Note that the name of the fields must be identical to the names of the properties of the object in the server

        Username: tweets[ObjectNumber].Username,
        Text: tweets[ObjectNumber].Text,
        Date: tweets[ObjectNumber].Date,
        ProfileImageUrl: tweets[ObjectNumber].ProfileImageUrl,
        Url: tweets[ObjectNumber].Url,
        UserId: UserId,

    }
    ajaxCall("POST", "../api/Twitter", JSON.stringify(tweet), successTweet, errorTweet);

}


function successTweet(data) {
    swal("Added Successfuly!", "App Music Added The Tweet To Tweets List ", "success");


}

function errorTweet(err) {
    swal("function AddTweetToDB got wrong!" + err, "", "error");
}

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

    ajaxCall("GET", "../api/Twitter/Tweets?musictype=" + musictype, "", successGetTweets, errorGetTweets);



}


function LoadTweets() {
    GetTweets();
    return false; // the return false will prevent the form from being submitted
    // hence the page will not reload
}
function GetTweets() {


    query = $("#search").val(),
        ajaxCall("GET", "../api/Twitter/Tweets?query=" + query, "", successGetTweets, errorGetTweets);

}
function successGetTweets(data) {
    $("#mainstory").show()

    tweets = data;
    $("#container").empty();
    str = "";
    for (var i = 0; i < data.length; i++) {
        str += "  <div class='w3-container w3-card w3-white w3-round w3-margin' >"
        str += "  <br>"
        str += " <img src=" + data[i].ProfileImageUrl + " alt='Avatar' class='w3-left w3-circle w3-margin-right' style='width:60px'>"
        str += "  <span class='w3-right w3-opacity'></span>"
        str += " <h4>" + data[i].Username + "</h4><br>"
        str += " <hr class='w3-clear'>"
        str += " <p>" + data[i].Text + "</p>"
        if (data[i].Url != null) {
            //str += "< a href=\"" + data[i].Url + "\">" + data[i].Url+"</a>"
            str += "<a href=" + data[i].Url + ">" + data[i].Url + "</a>"
        }
        str += "<p>" + data[i].Date + "</p>";

        str += "<button type=submit onclick='AddTweetToDB(" + i + ")'  value= " + i + ">Save</button>"
        str += "</div>"


    }

    $("#phloader").empty();

    $("#container").append(str);



}

function errorGetTweets(err) {
    swal("function GetTweets going wrong!" + err, "", "error");
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
