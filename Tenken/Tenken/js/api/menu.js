const menuList = document.getElementById('menu-list')

const listMenu = document.createElement('li')
listMenu.setAttribute('class', 'active')

menuList.appendChild(listMenu)

const link = document.createElement('a')
link.setAttribute('href', '/home/index')
link.text = 'Home'
listMenu.appendChild(link)

var islogin = localStorage.getItem('userName') === null ? false : true;


var a = document.getElementById('cartValue')
if (islogin) {
    a.setAttribute('href', '/Cart/Cart?cartID=' + parseInt(localStorage.getItem('cartID')));
} else {
    a.setAttribute('href', '/Home/Login');
}

var requestMenu = new XMLHttpRequest()
requestMenu.open('GET', '/CategoryAPI/getAllCategory', true)
requestMenu.onload = function () {
    var data = JSON.parse(this.response)
    // Begin accessing JSON data here
    if (requestMenu.status >= 200 && requestMenu.status < 400) {
        data.forEach(category => {
            const container = document.createElement('li')
            menuList.appendChild(container)

            const link = document.createElement('a')
            link.setAttribute('href', '/Product/Category?categoryID=' + category.CategoryID)
            link.text = category.CategoryName
            container.appendChild(link)
        })
    } else {
        console.log(requestMenu.status);
    }
}

requestMenu.send()