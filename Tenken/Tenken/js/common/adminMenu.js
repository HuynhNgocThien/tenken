confirmResetPassword = function (userID) {
    if (confirm("Reset password will change password of this user to default. Are you sure?")) {
        var request = new XMLHttpRequest()
        request.open('POST', '/UserAPI/ResetPassword', true)
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
        request.onload = function () {
            var data = JSON.parse(this.response)
            // Begin accessing JSON data here
            if (request.status >= 200 && request.status < 400) {
                if (data == true) {
                    confirm("Success reset password!")
                } else {
                    confirm("Something wrong, please try again")
                }
            } else {
                confirm("Something wrong, please try again")
            }
        }
        var params = "userID=" + userID;
        request.send(params)
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
if (span !== undefined) {
    span.onclick = function () {
        modal.style.display = "none";
    }
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}


let arletRemoveProduct = function (productID) {
    if (confirm("Delete Product! Are you sure?")) {
        var request = new XMLHttpRequest()
        request.open('POST', '/ProductAPI/productDelete', true)
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
        request.onload = function () {
            var data = JSON.parse(this.response)
            // Begin accessing JSON data here
            if (request.status >= 200 && request.status < 400) {
                if (data.Result == true) {
                    window.location.href = '/Admin/Menu?TypeMenu=Product'
                } else {
                    confirm("Something wrong, please try again")
                }
            } else {
                confirm("Something wrong, please try again")
            }
        }
        var params = "productID=" + productID;
        request.send(params)
    }
};

let arletRemoveCategory = function (categoryID) {
    if (confirm("Delete Category! Are you sure?")) {
        var request = new XMLHttpRequest()
        request.open('POST', '/CategoryAPI/categoryDelete', true)
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
        request.onload = function () {
            var data = JSON.parse(this.response)
            // Begin accessing JSON data here
            if (request.status >= 200 && request.status < 400) {
                if (data.Result == true) {
                    window.location.href = '/Admin/Menu?TypeMenu=Category'
                } else {
                    confirm("Something wrong, please try again")
                }
            } else {
                confirm("Something wrong, please try again")
            }
        }
        var params = "categoryID=" + categoryID;
        request.send(params)
    }
};