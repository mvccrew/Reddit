function checkLoginForErrors() {

    const loginModal = document.getElementById("loginModal");

    if (loginModal) {
        var form = $('#loginModal');
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

        loginModal.classList.add('active');
    }
    else {
        window.location.reload();
    }

}

function checkRegisterForErrors() {

    const registerModal = document.getElementById("registerModal");

    if (registerModal) {
        var form = $('#registerModal');
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

        registerModal.classList.add('active');
    }
    else {
        window.location.reload();
    }

}

var triggerModal = (event) => {
    const login = document.getElementById("login");
    const register = document.getElementById("register");
    const loginModal = document.getElementById("loginModal");
    const registerModal = document.getElementById("registerModal");
    const closeLinks = document.querySelectorAll("[href='#close']");

    login.addEventListener('click', () => {
        loginModal.classList.add('active');
    });

    register.addEventListener('click', () => {
        registerModal.classList.add('active');
    });

    if (closeLinks.length > 0) {
        closeLinks.forEach(el => {
            el.addEventListener('click', () => {
                loginModal.classList.remove('active');
                registerModal.classList.remove('active');
            });
        });
    }

    if (window.location.hash == '#notloggedin') {
        loginModal.classList.add('active');
    }
}

window.addEventListener('DOMContentLoaded', triggerModal, false);
window.addEventListener('DOMNodeInserted', triggerModal, false);