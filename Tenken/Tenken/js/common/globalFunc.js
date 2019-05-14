
var cartID = parseInt(sessionStorage.getItem('cartID'));

let getCartValue = function (cartID) {
    if (cartID !== null && cartID != undefined) {
        var request = new XMLHttpRequest()
        request.open('GET', '/CartAPI/getCartValue?cartID=' + cartID, true)
        request.onload = function () {
            var data = JSON.parse(this.response)
            if (request.status >= 200 && request.status < 400) {
                sessionStorage.setItem('cartValue', data);
                document.getElementById('cartValue').textContent = 'Cart[' + sessionStorage.getItem('cartValue') + ']';
            }
        }
        request.send()
    } else {
        sessionStorage.setItem('cartValue', 0);
        document.getElementById('cartValue').textContent = 'Cart[' + sessionStorage.getItem('cartValue') + ']';
    }
}
getCartValue();

let updateCart = function (data) {
    // Add cart

    // Get cart value
    cartID = parseInt(sessionStorage.getItem('cartID'));
    getCartValue(cartID);
}   
