function checkForErrors() {

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

var triggerModal = (event) => {
    const login = document.getElementById("login");
    const closeLinks = document.querySelectorAll("[href='#close']");
    const loginModal = document.getElementById("loginModal");

    login.addEventListener('click', () => {
        loginModal.classList.add('active');
    });

    if (closeLinks.length > 0) {
        closeLinks.forEach(el => {
            el.addEventListener('click', () => {
                loginModal.classList.remove('active');
            });
        });
    }

    if (window.location.hash == '#notloggedin') {
        loginModal.classList.add('active');
    }
}

window.addEventListener('DOMContentLoaded', triggerModal, false);
window.addEventListener('DOMNodeInserted', triggerModal, false);