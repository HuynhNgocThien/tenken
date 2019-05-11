const app = document.getElementById('menu-list')

const container = document.createElement('li')
container.setAttribute('class', 'active')

app.appendChild(container)

const link = document.createElement('a')
link.setAttribute('href', 'http://localhost:57384/home/index')
link.text = 'Home'
container.appendChild(link)

var request = new XMLHttpRequest()
request.open('GET', 'http://localhost:57384/CategoryAPI/getAllCategory', true)
request.onload = function () {
    // Begin accessing JSON data here
    var data = JSON.parse(this.response)
    if (request.status >= 200 && request.status < 400) {
        data.forEach(category => {
            const container = document.createElement('li')
            app.appendChild(container)

            const link = document.createElement('a')
            link.setAttribute('href', 'http://localhost:57384/category/index?categoryID=' + category.CategoryID)
            link.text = category.CategoryName
            container.appendChild(link)
        })
    } else {
        console.log(request.status);
    }
}

request.send()