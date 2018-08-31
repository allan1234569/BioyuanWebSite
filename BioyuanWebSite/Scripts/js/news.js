$(document).ready(function (e) {
    $(".tab li").click(function () {
        $(".tab li").eq($(this).index()).addClass("activ").siblings().removeClass("activ");
        $(".tabCon div").hide().eq($(this).index()).show();
    })
});

