$(document).ready(function () {

    $('.navbar-toggle').click(function () {
        $('.collapse-menu').toggle();
        $('.overlay-menu').toggle();
    });

    $('.close-menu').click(function () {
        $('.navbar-toggle').trigger('click');
    });

    $('.overlay-menu').click(function () {
        $('.navbar-toggle').trigger('click');
    });

    $('[data-toggle="tooltip"]').tooltip();

});