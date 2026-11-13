$(document).ready(function () {

    var boxWidth = $(".hello").width();
    $("#lblComapnay").hover(function () {
        $(".hello").animate({
            width: 0,
        });
    });
    $(".DivCompany").hover(function () {
        $(".hello").animate({

            width: boxWidth
        });
    });
});