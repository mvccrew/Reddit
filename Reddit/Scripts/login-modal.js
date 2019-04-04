document.addEventListener('DOMContentLoaded', () => {

    const login = document.getElementById("login");
    const closeLinks = document.querySelectorAll("[href='#close']");

    login.addEventListener('click', () => {
        const loginModal = document.getElementById("loginModal");
        loginModal.classList.add('active');
    });

    if (closeLinks.length > 0) {
        closeLinks.forEach(el => {
            el.addEventListener('click', () => {
                const loginModal = document.getElementById("loginModal");
                loginModal.classList.remove('active');
            });
        });
    }

});