//左侧栏滑动
$(function () {

    $('.top_header div .left_open i').click(function (event) {
        if ($('.left-nav').css('left') == '0px') {
            $('.left-nav').animate({ left: '-220px' }, 100);
            $('.page-content').animate({ left: '0px' }, 100);
            $('.page-content-bg').hide();
        } else {
            $('.left-nav').animate({ left: '0px' }, 100);
            $('.page-content').animate({ left: '220px' }, 100);
            if ($(window).width() < 768) {
                $('.page-content-bg').show();
            }
        }
    });

    $('.page-content-bg').click(function (event) {
        $('.left-nav').animate({ left: '-220px' }, 100);
        $('.page-content').animate({ left: '0px' }, 100);
        $(this).hide();
    });





    //$("div.collapse a").on('click', function () {

    //    var collapseValue = $(this).parent().attr('id');
    //    alert(collapseValue);
    //    //setCookie("collapseId", collapseValue, "h1");
    //    delCookie('collapseId');
    //    setCookie('collapseId', collapseValue, 30)
    //})


    //var collapseId = getCookie("collapseId");
    //alert(collapseId);
    //$("#" + collapseId).addClass("in");

})


//写cookies  
function setCookie(c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString());
}

//读取cookies  
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

    if (arr = document.cookie.match(reg))

        return (arr[2]);
    else
        return null;
}

//删除cookies  
function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) {
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    }
        
}



/**
 *
 * @param $imgURL//加载图片的地址
 * @param callback//加载完成后腰进行的动作
 */
function checkImgLoaded($id, $imgURL, callback) {
    var $img = $($id);//创建Image()对象
    $img.src = $imgURL;//赋值
    if (!!window.ActiveXObject) {//判断是否为IE浏览器
        $img.onreadystatechange = function () {//使用ActiveX控件
            if (this.readystate == "complete" || this.readystate == "loaded") {
                callback();
            }
        }
    }
    else {
        $img.onload = function () {
            callback();
        }
    }
}









