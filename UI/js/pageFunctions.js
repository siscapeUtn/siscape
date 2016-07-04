function slideshow() {
    $(function () {
        $('.slider').cycle({
            fx: 'fade',
            speed: 'fast',
            timeout: 4000,
            slideExpr: 'img'
        });
    });
}

function accordion() {
    $(function () {
        $("#contactUs-accordion").accordion({
            heightStyle: "content",
            collapsible : true,
            active : 'none',
            icons: { "header": "ui-icon-plus", "activeHeader": "ui-icon-minus" }
        });
    });
}