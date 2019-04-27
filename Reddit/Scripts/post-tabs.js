function onTabClick(event) {
    let activeTabs = document.querySelectorAll('.active');
    let clickedTab = event.target.id;

    activeTabs.forEach(function (tab) {
        tab.className = tab.className.replace('active', '');
    });

    event.target.parentElement.className += ' active';

    if (clickedTab == 'text') {
        document.querySelector('#PostTypeId').value = "1";
        document.querySelector('.content').innerHTML =
            '<textarea class="form-input" cols="20" id="Content" name="Content" placeholder="Text (optional)" rows="3" type=""></textarea>';
    }
    else if (clickedTab == 'link') {
        document.querySelector('#PostTypeId').value = "3";
        document.querySelector('.content').innerHTML =
            '<textarea class="form-input" cols="20" id="Content" name="Content" placeholder="Url" rows="3" type=""></textarea>';
    }
    else {
        document.querySelector('#PostTypeId').value = "2";
        document.querySelector('.content').innerHTML =
            '<input class="form-input" name="image" type="file"/>';
    }
}

const element = document.getElementById('post-tab');
element.addEventListener('click', onTabClick, false);