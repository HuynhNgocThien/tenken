let checkPassword = function () {
    var password = document.getElementById('sso_login_form_password').value;
    var repassword = document.getElementById('sso_login_form_re_password').value;

    var passwordDiv = document.getElementById('password-div');
    var arletPassword = document.getElementById('arlet-password');
    if (repassword !== '') {
        if (password !== repassword && arletPassword === null) {
            var arletPassword = document.createElement('p');
            arletPassword.setAttribute('class', 'arlet');
            arletPassword.setAttribute('id', 'arlet-password');
            arletPassword.textContent = 'Re-Password is not match';
            passwordDiv.appendChild(arletPassword);
        } else if (password === repassword && arletPassword !== null) {
            {
                arletPassword.remove();
            }
        }
    }
    if (password === repassword && password !== '') {
        return true;
    } else {
        return false;
    }
}


let checkEmail = function (data) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var accountdDiv = document.getElementById('account-div');
    var arletEmail = document.getElementById('arlet-email');

    if (!re.test(String(data).toLowerCase()) && arletEmail === null) {
        var arletEmail = document.createElement('p');
        arletEmail.setAttribute('class', 'arlet');
        arletEmail.setAttribute('id', 'arlet-email');
        arletEmail.textContent = 'Email format is in-valid';
        accountdDiv.appendChild(arletEmail);
    } else if (re.test(String(data).toLowerCase()) && arletEmail !== null) {
        arletEmail.remove();
    }
    if (re.test(String(data).toLowerCase())) {
        return true;
    } else {
        return false
    }
}
let register = function (e) {
    userInfo = '@ViewBag.UserInfo';
    console.log(userInfo);
}