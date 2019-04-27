function onTabClick(event) {
    let activeTabs = document.querySelectorAll('li.active');
    let clickedTab = event.target.id;

    activeTabs.forEach(function (tab) {
        tab.className = tab.className.replace('active', '');
    });

    event.target.parentElement.className += ' active';

    if (clickedTab == 'text') {
        document.querySelector('#PostTypeId').value = "1";
        document.querySelector('.post-content').innerHTML =
            '<textarea class="form-input" cols="20" id="Content" name="Content" placeholder="Text (optional)" rows="3" type=""></textarea>';
    }
    else if (clickedTab == 'link') {
        document.querySelector('#PostTypeId').value = "3";
        document.querySelector('.post-content').innerHTML =
            '<textarea class="form-input" cols="20" id="Content" name="Content" placeholder="Url" rows="3" type=""></textarea>';
    }
    else {
        document.querySelector('#PostTypeId').value = "2";
        document.querySelector('.post-content').innerHTML =
            '<input class="form-input" name="image" type="file"/>';
    }
}

$(document).on('click', '#post-tab', onTabClick);