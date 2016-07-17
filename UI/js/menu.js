jQuery(document).ready(function ($) {
   
    $('#menu-bar').click(function () {
        if ($('#menu-bar').hasClass('is_close')) {
            $('#menu-bar').removeClass('is_close');
            $('#menu-bar').addClass('is_open');
            $('header .header-container nav').animate({
                right: '0'
            });
        } else {
            $('#menu-bar').removeClass('is_open');
            $('#menu-bar').addClass('is_close');
            $('header .header-container nav').animate({
                right: '-100%'
            });
        }
    });


    $('.content').click(function () {
        $('#menu-bar').removeClass('is_open');
        $('#menu-bar').addClass('is_close');
        $('header .header-container nav').animate({
            right: '-100%'
        });
    });

});