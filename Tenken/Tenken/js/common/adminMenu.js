confirmResetPassword = function (userID, userName) {
    if (confirm("Reset password will change password of this user to default. Are you sure?")) {
        var requestMenu = new XMLHttpRequest()
        requestMenu.open('POST', '/UserAPI/ResetPassword', true)
        requestMenu.onload = function () {
            var data = JSON.parse(this.response)
            // Begin accessing JSON data here
            if (requestMenu.status >= 200 && requestMenu.status < 400) {
                if (data == true) {
                    arlet("Success reset password for user: "+ userName)
                } else {
                    arlet("Something wrong, please try again")
                }
            } else {
                arlet("Something wrong, please try again")
            }
        }
        var params = "userID=" + userID;
        requestMenu.send(params)
    }
}

var modal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on the button, open the modal 
var showdescription = function () {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}