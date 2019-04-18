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
            $.get(`${action}`, { id: id, PostId: postId, parentCommentId: parentCommentId })
                .done(function (data) {
                    $("#edit-container").empty().html(data);
                    $(".edit-modal").addClass("active");
                });
        }
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