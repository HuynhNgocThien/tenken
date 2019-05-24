var rating = 0;

minus = function () {
    var input = document.getElementById("quantity");
    if (parseInt(input.value) > 1) {
        input.value -= 1;
    }
}
plus = function () {
    var input = document.getElementById("quantity");
    input.value = parseInt(input.value) + 1;
}

ratingStar = function (id) {
    var star = document.getElementById(id);
    star.removeAttribute("class");
    star.setAttribute("class", "icon-star-full");
}

var rating = function (data) {
    switch (data) {
        case 1:
            rating = 1;
            ratingStar("star-1");
            break;
        case 2:
            rating = 2;
            ratingStar("star-1");
            ratingStar("star-2");
            break;
        case 3:
            rating = 3;
            ratingStar("star-1");
            ratingStar("star-2");
            ratingStar("star-3");
            break;
        case 4:
            rating = 4;
            ratingStar("star-1");
            ratingStar("star-2");
            ratingStar("star-3");
            ratingStar("star-4");
            break;
        case 5:
            rating = 5;
            ratingStar("star-1");
            ratingStar("star-2");
            ratingStar("star-3");
            ratingStar("star-4");
            ratingStar("star-5");
            break;
    }
}


comment = function (content, productid, userid) {

    var request = new XMLHttpRequest()
    request.open('POST', '/CommentAPI/commentMerge', true)
    request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    request.onload = function () {
        var data = JSON.parse(this.response)
        // Begin accessing JSON data here
        if (request.status >= 200 && request.status < 400) {
            window.location.href = "/Product/ProductDetail?productID=" + productid;
        } else {
            arlet("Some thing wrong, please try again!")
        }
    }
    var params = 'content=' + content + '&productID=' + productid + '&userID=' + userid + '&rating=' + rating + '&reply=0';
    request.send(params)
}