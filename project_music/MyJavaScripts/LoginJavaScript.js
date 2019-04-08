$(document).ready(function () {
   
    $("#loginForm").submit(f1);
    $("#SignUpForm").submit(f2);

    $('#buttonUpload').on('click', function () {
        str = "<div class='loader'></div><p id='loader2'>Loading Image</p>"
        $("#phloader").append(str);
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
    console.log(data);

    var imgStr = "";

    if (Array.isArray(data)) {

        for (var i = 0; i < data.length; i++) {
            imgStr += "<img src='../" + data[i] + "'/>";
        }
    }

    else {
        imgStr = "<img src='../" + data + "'/>";
    }
    $("#phloader").empty();

    document.getElementById("ph").innerHTML = imgStr;
}

function error(data) {
    console.log(data);
}
function f2() {
    str = "<div class='loader'></div><p id='loaderSignUp'>Loading Sign Up User</p>"
    $("#phloader").append(str);
    AddUser();

    return false; // the return false will prevent the form from being submitted
    // hence the page will not reload
}

function AddUser() {


    User = { // Note that the name of the fields must be identical to the names of the properties of the object in the server

        Name: $("#name").val(),
        FamilyName: $("#familyName").val(),
        Gender: $("#gender").val(),
        Age: parseFloat($("#age").val()),
        Address: $("#address").val(),
        Image: document.getElementById("ph").innerHTML,
        Email: $("#email").val(),
        Password: $("#password").val(),
        MusicType: $("#MusicType").val(),

    }
    ajaxCall("POST", "../api/Users", JSON.stringify(User), successSignUp, errorSignUp);

}

function successSignUp(data) {
    $("#phloader").empty();

    swal("Added Successfuly!", "App Music Thank you for registering ", "success");


}

function errorSignUp(err) {
    alert("error");
}
function f1() {
    str = "<div class='loader'></div><p id='loader'>Loading Music App Home Page</p>"
    $("#phloader").append(str);
    CheckUser();
    return false; // the return false will prevent the form from being submitted
    // hence the page will not reload
}
function CheckUser() {


    Email = $("#user").val(),
        Password = $("#pass").val(),

        ajaxCall("GET", "../api/Users/?Email=" + Email + "&Password=" + Password, "", successLogIn, errorLogIn);

}

function successLogIn(Userdata) {
    if (Userdata.UserId == 0 && Userdata.Address == null) {
        $("#phloader").empty(str);

        swal("user name is not correct!", "", "error");


    }
    else if (Userdata.UserId == 1 && Userdata.Address == null) {
        $("#phloader").empty(str);

        swal("password is not correct!", "", "error");


    }
    //if (Persondata.Address == null) { window.location = 'insert.html'; }
    else {
        localStorage["UserId"] = JSON.stringify(Userdata.UserId);;
        localStorage["Name"] = JSON.stringify(Userdata.Name);
        localStorage["FamilyName"] = JSON.stringify(Userdata.FamilyName)
        localStorage["Gender"] = JSON.stringify(Userdata.Gender);
        localStorage["Age"] = JSON.stringify(Userdata.Age);
        localStorage["Address"] = JSON.stringify(Userdata.Address);
        localStorage["Email"] = JSON.stringify(Userdata.Email);
        localStorage["MusicType"] = JSON.stringify(Userdata.MusicType);
        localStorage["Password"] = JSON.stringify(Userdata.Password);
        localStorage["Image"] = JSON.stringify(Userdata.Image);
        $("#phloader").empty(str);
        window.location = 'YoutubePage.html';

    }

}


function errorLogIn(err) {
    alert("error");
}