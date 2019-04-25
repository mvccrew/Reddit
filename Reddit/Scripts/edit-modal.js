function editErrors() {

    const modal = document.getElementsByClassName('edit-modal');

    if (modal.length > 0) {
        var form = $(".edit-modal");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

        modal[0].classList.add('active');
    }
    else {
        window.location.reload();
    }

}

var triggerEditModal = () => {
    $(".edit-btn").click(function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var action = $(this).data('action');
        if ($(this).hasClass('edit-comment')) {
            var postId = $(this).data('post-id');
            var parentCommentId = $(this).data('parent-id');
            $.get(`${action}`, { id: id, PostId: postId, parentCommentId: parentCommentId, SubRedditId: subRedditId })
                .done(function (data) {
                    $("#edit-container").empty().html(data);
                    $(".edit-modal").addClass("active");
                });
        }
        // tova lichno IVILIN GO E PISAL DA SE ZNAE
        else if ($(this).hasClass('edit-rule'))
        {
            var subRedditId = $(this).data('sub-id');
            $.get(`${action}`, { id: id, SubRedditId: subRedditId })
                .done(function (data) {
                    $("#edit-container").empty().html(data);
                    $(".edit-modal").addClass("active");
                });
        }
            // DO tuka e IVILIN
        else {
            $.get(`${action}`, { id: id })
                .done(function (data) {
                    $("#edit-container").empty().html(data);
                    $(".edit-modal").addClass("active");
                });
        }
    });

    $(document).on('click', '.edit-modal-close', function () {
        $(".edit-modal").removeClass("active");
    });
}

window.addEventListener('DOMContentLoaded', triggerEditModal, false);
window.addEventListener('DOMNodeInserted', triggerEditModal, false);