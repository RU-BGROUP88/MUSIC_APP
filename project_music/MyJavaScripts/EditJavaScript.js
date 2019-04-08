
// will run when the document is ready
$(document).ready(function () {
    loadlocalstorage();
    GetTopArtists();
    GetTopTracks();
    GetTopAlbums();

    $('#buttonUpload').on('click', function () {
        var data = new FormData();
        var files = $("#files").get(0).files;

        // Add the uploaded file to the form data collection
        if (files.length > 0) {
            for (f = 0; f < files.length; f++) {
                data.append("UploadedImage", files[f]);
            }
            data.append("name", "yaniv"); // aopend what ever data you want to send along with the files. See how you extract it in the controller.
        }

        // Ajax upload
        $.ajax({
            type: "POST",
            url: "../Api/FileUpload",
            contentType: false,
            processData: false,
            data: data,
            success: showImages,
            error: error
        });

        return false;
    });

});

function showImages(data) {

    var imgStr = "";

    if (Array.isArray(data)) {

        for (var i = 0; i < data.length; i++) {
            imgStr += "<img src='../" + data[i] + "'/>";
        }
    }

    else {
        imgStr = "<img src='../" + data + "'/>";

    }

    document.getElementById("ph").innerHTML = imgStr;
}

function error(err) {
    swal("show Images going wrong!"+err, "", "error");
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
    Password = JSON.parse(localStorage["Password"]);
    Image = JSON.parse(localStorage["Image"]);
    musictype = JSON.parse(localStorage["MusicType"]);
    str1 = "";
    str1 += " <div class='w3-container'>"
    str1 += " <h4 class='w3-center'>My Profile</h4>"
    str1 += " <p class='w3-center' id = 'image'>" + Image + "</p>"

    str1 += "   <hr>"
    str1 += "  <p><i class='fa fa-pencil fa-fw w3-margin-right w3-text-theme'></i> " + Name + " " + FamilyName + "</p>"
    str1 += " <p><i class='fa fa-home fa-fw w3-margin-right w3-text-theme'></i> " + Address + "</p>"
    str1 += " </div>"
    $("#profile").empty();
    $("#profile").append(str1);
    $("#Name").val(Name);
    $("#FamilyName").val(FamilyName);
    $("#Gender").val(Gender);
    $("#Age").val(Age);
    $("#Address").val(Address);
    $("#Email").val(Email);
    $("#password").val(Password);
    $("#MusicType").val(musictype);

}

function SaveChange() {

    user = { // Note that the name of the fields must be identical to the names of the properties of the object in the server

        UserId: UserId,
        Name: $("#Name").val(),
        FamilyName: $("#FamilyName").val(),
        Gender: $("#Gender").val(),
        Age: parseFloat($("#Age").val()),
        Image: document.getElementById("ph").innerHTML == "" ? document.getElementById("image").innerHTML : document.getElementById("ph").innerHTML,
        Address: $("#Address").val(),
        Email: $("#Email").val(),
        Password: $("#password").val(),
        MusicType: $("#MusicType").val(),
    }
    ajaxCall("Put", "../api/Users", JSON.stringify(user), updateUserSuccess, errorUserUpdate);
}

function updateUserSuccess() {
    swal("Update Successfuly!", "", "success");
    UpdateUserInfo();

}

// this function is activated in case of a failure
function errorUserUpdate(err) {
    swal("Update got wrong!" + err, "", "error");
}
function UpdateUserInfo() {


    Email = $("#Email").val(),
        Password = $("#password").val(),

        ajaxCall("GET", "../api/Users/?Email=" + Email + "&Password=" + Password, "", successGetUserInfo, errorGetUserInfo);

}

function successGetUserInfo(data) {
    localStorage["UserId"] = JSON.stringify(data.UserId);;
    localStorage["Name"] = JSON.stringify(data.Name);
    localStorage["FamilyName"] = JSON.stringify(data.FamilyName)
    localStorage["Gender"] = JSON.stringify(data.Gender);
    localStorage["Age"] = JSON.stringify(data.Age);
    localStorage["Address"] = JSON.stringify(data.Address);
    localStorage["Email"] = JSON.stringify(data.Email);
    localStorage["MusicType"] = JSON.stringify(data.MusicType);
    localStorage["Password"] = JSON.stringify(data.Password);
    localStorage["Image"] = JSON.stringify(data.Image);
    str1 = "";
    str1 += " <div class='w3-container'>"
    str1 += " <h4 class='w3-center'>My Profile</h4>"
    str1 += " <p class='w3-center' id = 'image'>" + data.Image + "</p>"
    str1 += "   <hr>"
    str1 += "  <p><i class='fa fa-pencil fa-fw w3-margin-right w3-text-theme'></i> " + data.Name + " " + data.FamilyName + "</p>"
    str1 += " <p><i class='fa fa-home fa-fw w3-margin-right w3-text-theme'></i> " + data.Address + "</p>"
    //str +=" <p><i class='fa fa-birthday-cake fa-fw w3-margin-right w3-text-theme'></i> April 1, 1988</p>"
    str1 += " </div>"
    $("#profile").empty();
    $("#profile").append(str1);
    $("#Name").val(data.Name);
    $("#FamilyName").val(data.FamilyName);
    $("#Gender").val(data.Gender);
    $("#Age").val(data.Age);
    $("#Address").val(data.Address);
    $("#Email").val(data.Email);
    $("#password").val(data.Password);
    $("#MusicType").val(data.MusicType);
    musictype = data.MusicType;
    GetTopArtists();
    GetTopTracks();
    GetTopAlbums();
}

function errorGetUserInfo(err) {
    swal("Get User Info got wrong!"+err, " ", "success");

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