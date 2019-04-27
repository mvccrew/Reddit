function checkForErrors(modalId) {

    const modal = document.getElementById(modalId);

    if (modal) {
        var form = $('#' + modalId);
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

        modal.classList.add('active');
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
    const redirectToRegister = document.getElementById("redirectToRegister");
    const closeLinks = document.querySelectorAll(".authenticate-close");

    login.addEventListener('click', () => {
        loginModal.classList.add('active');
    });

    register.addEventListener('click', () => {
        registerModal.classList.add('active');
    });

    redirectToRegister.addEventListener('click', () => {
        loginModal.classList.remove('active');
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