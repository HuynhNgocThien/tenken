
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

let updateCart = function (productId, cartID) {
    if (cartID === 0) {
        confirm("Please login first")
    }
    else {
        // Add cart
        addToCart(productId, cartID);
        // Get cart value
        getCartValue(cartID);
    }
}

let addToCart = function (productID, cartID) {
    if (cartID !== null && cartID != undefined) {
        var request = new XMLHttpRequest()
        request.open('POST', '/CartAPI/AddCart', true)
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.onload = function () {
            var data = JSON.parse(this.response)
            console.log(data);
        }
        var params = 'ProductID=' + productID + '&Quantity=1&CartID=' + cartID;
        request.send(params);
    }
}
