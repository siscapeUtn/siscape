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

function uploadImage() {
        
    $(function () {
        $('#ContentPlaceHolder1_flLoadImage').on('change', function () {
            document.getElementById("ContentPlaceHolder1_uploadFile").value = this.value.substring(this.value.lastIndexOf("\\") +1);
        });
    });

}