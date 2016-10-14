$(function () {
    $('.ui.accordion')
        .accordion()
    ;

    $('.ui.dropdown')
        .dropdown()
    ;

    $('.login-modal')
        .modal('attach events', '.login-button')
    ;
});