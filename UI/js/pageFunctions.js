function menuResponsive(){
    
}

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
        $("#accordion").accordion({
            collapsible: true
        });
    });
}