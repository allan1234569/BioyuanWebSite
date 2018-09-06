/// <reference path="bootstrapValidator.js" />

//表单验证
$(document).ready(function () {
    $('#sign_in_form').bootstrapValidator({
        //container: 'tooltip',
        //        trigger: 'blur',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            LoginName: {
                validators: {
                    stringLength: {
                        min: 4,
                        max: 30,
                        message: '登录名长度不能小于6位或超过30位'
                    },
                    notEmpty: {
                        message: '登录名必填不能为空'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_\.]+$/,
                        message: '用户名只能由字母、数字、点和下划线组成。'
                    }
                }
            },
            LoginPwd: {
                validators: {
                    stringLength: {
                        min: 8,
                        max: 16,
                        message: '密码不能少于8个或超过16个字符'
                    },
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    regexp: {
                        regexp: /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,16}$/,
                        message: '必须是字母和数字的组合，不能使用特殊字符，长度在8-16之间'
                    }
                }
            },
            EnValidateCode: {
                validators: {
                    identical: {
                        field: "ValidateCode",
                        message:"验证码不正确"
                    }
                }
            }
        }
    })
    .on('success.form.bv', function (e) {
        // Prevent form submission
        e.preventDefault();
        
        // Get the form instance
        var $form = $(e.target);

        // Get the BootstrapValidator instance
        var bv = $form.data('bootstrapValidator');


        // Use Ajax to submit form data
        $.ajax({
      
            type: "post",
            url: "/Home/Login",
            data: $form.serialize(),

            success: function (data) {
                if (data == '-1') {

                    alert("用户不存在")

                } else if (data == '0') {

                    alert("密码错误")

                } else {

                    $("#my-modal-login").modal('hide')
                    $(".loginOutLi").removeClass('hidden')
                }

                button.removeAttribute('disabled');
            }
        })
    });

    $('#sign_up_form').bootstrapValidator({
        //container: 'tooltip',
        //        trigger: 'blur',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            NickName: {
                validators: {
                    notEmpty: {
                        message: '昵称不能为空'
                    }
                }
            },
            LoginName: {
                validators: {
                    stringLength: {
                        min: 4,
                        max: 30,
                        message: '登录名长度不能小于4位或超过30位'
                    },
                    threshold: 4, //有4字符以上才发送ajax请求，（input中输入一个字符，插件会向服务器发送一次，设置限制，6字符以上才开始）
                    remote: {//ajax验证。server result:{"valid",true or false} 向服务发送当前input name值，获得一个json数据。例表示正确：{"valid",true}  
                        url: '/User/UserExists',//验证地址
                        message: '此用户名已经被使用',//提示消息
                        delay: 50,//每输入一个字符，就发ajax请求，服务器压力还是太大，设置2秒发送一次ajax（默认输入一个字符，提交一次，服务器压力太大）
                        type: 'POST',//请求方式
                        /**自定义提交数据，默认值提交当前input value
                         *  data: function(validator) {
                              return {
                                  password: $('[name="passwordNameAttributeInYourForm"]').val(),
                                  whatever: $('[name="whateverNameAttributeInYourForm"]').val()
                              };
                           }
                         */
                        success: function (data) {
                            alert(data);
                        }
                    },
                    notEmpty: {
                        message: '登录名必填不能为空'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_\.]+$/,
                        message: '用户名只能由字母、数字、点和下划线组成。'
                    }
                }
            },
            LoginPwd: {
                validators: {
                    stringLength: {
                        min: 8,
                        max:16,
                        message: '密码不能少于8个或超过16个字符'
                    },
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    regexp: {
                        regexp: /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,16}$/,
                        message: '必须是字母和数字的组合，不能使用特殊字符，长度在8-10之间'
                    },
                    different: {
                        field: 'LoginName',
                        message: '用户名和密码不能相同'
                    }
                }
            },
            EnPassword: {
                validators: {
                    stringLength: {
                        min: 8,
                        max: 16,
                        message: '密码不能少于8个或超过16个字符'
                    },
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    regexp: {
                        regexp: /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,16}$/,
                        message: '必须是字母和数字的组合，不能使用特殊字符，长度在8-16之间'
                    },
                    identical: {
                        field: 'LoginPwd',
                        message: '密码和确认密码不相同'
                    },
                }
            },
            UserEmail: {
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    emailAddress: {
                        message: '输入的邮箱地址无效'
                    },
                    threshold: 4, //有4字符以上才发送ajax请求，（input中输入一个字符，插件会向服务器发送一次，设置限制，6字符以上才开始）
                    remote: {//ajax验证。server result:{"valid",true or false} 向服务发送当前input name值，获得一个json数据。例表示正确：{"valid",true}  
                        url: '/User/EmailExists',
                        message: '此邮箱已经被注册',
                        delay: 50,//每输入一个字符，就发ajax请求，服务器压力还是太大，设置2秒发送一次ajax（默认输入一个字符，提交一次，服务器压力太大）
                        type: 'POST'//请求方式
                        /**自定义提交数据，默认值提交当前input value
                         *  data: function(validator) {
                              return {
                                  password: $('[name="passwordNameAttributeInYourForm"]').val(),
                                  whatever: $('[name="whateverNameAttributeInYourForm"]').val()
                              };
                           }
                         */
                    }
                }
            }
        }
    })
    .on('success.form.bv', function (e) {
        // Prevent form submission
        e.preventDefault();

        // Get the form instance
        var $form = $(e.target);

        // Get the BootstrapValidator instance
        var bv = $form.data('bootstrapValidator');

        $.ajax({

            type: "post",
            url:"/Home/"

        })


        // Use Ajax to submit form data
        $.ajax({

            type: "post",
            url: "/Home/Register",
            data: $form.serialize(),

            success: function (data) {

                alert(data);

                if (data == '') {
                    alert("用户不存在")
                } else if (data == '0') {
                    alert("密码错误")
                } else {

                    $("#my-modal-login").modal('hide')
                    $(".loginOutLi").removeClass('hidden')
                }

                button.removeAttribute('disabled');
            }
        })
    });

    $('#getpwd_form').bootstrapValidator({
        //container: 'tooltip',
        //        trigger: 'blur',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            LoginName: {
                validators: {
                    stringLength: {
                        min: 4,
                        max: 30,
                        message: '登录名长度不能小于4位或超过30位'
                    },
                    notEmpty: {
                        message: '登录名必填不能为空'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z0-9_\.]+$/,
                        message: '用户名只能由字母、数字、点和下划线组成。'
                    }
                }
            },
            UserEmail: {
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    emailAddress: {
                        message: '输入的邮箱地址无效'
                    }
                }
            }
        }
    })
    .on('success.form.bv', function (e) {
        // Prevent form submission
        e.preventDefault();

        // Get the form instance
        var $form = $(e.target);

        // Get the BootstrapValidator instance
        var bv = $form.data('bootstrapValidator');

        // Use Ajax to submit form data
        $.ajax({

            type: "post",
            url: "/User/ForgetPassword",
            data: $form.serialize(),

            success: function (data) {
                if (data == '-1') {
                    alert("用户不存在")
                } else if (data == '0') {
                    alert("密码错误")
                } else {

                    $("#my-modal-login").modal('hide')
                    $(".loginOutLi").removeClass('hidden')
                }

                button.removeAttribute('disabled');
            }
        })
    });

});

//页面切换
$(document).ready(function () {

    $(".login-dialog .forget-pwd").click(function () {

        $(".login-dialog .modal-content").addClass("hidden");
        $(".login-dialog .modal-content:last").removeClass("hidden");

        $("img.SecurityCode").click();
    })


    $(".login-dialog .register-new").click(function () {

        $(".login-dialog .modal-content").removeClass("hidden");
        $(".login-dialog .modal-content:first").addClass("hidden");
        $(".login-dialog .modal-content:last").addClass("hidden");

        $("img.SecurityCode").click();
    })

    $(".login-dialog .existing-account").click(function () {

        $(".login-dialog .modal-content").addClass("hidden");
        $(".login-dialog .modal-content:first").removeClass("hidden");

        $("img.SecurityCode").click();
    })

    $(".login-dialog .turn-to-login").click(function () {

        $(".login-dialog .modal-content").addClass("hidden");
        $(".login-dialog .modal-content:first").removeClass("hidden");

        $("img.SecurityCode").click();
    })
});


$(function () {

    ////阻止表单提交
    //document.querySelector('#goToLogin').addEventListener('click', function () {
    //    var button = this;

    //    button.setAttribute('disabled', true);

    //    //仍然会阻止表单提交
    //    //setTimeout(function () {
    //    //    button.removeAttribute('disabled');
    //    //}, 0);

    //}, false);


    //document.querySelector('#goToRegister').addEventListener('click', function () {
    //    var button = this;

    //    button.setAttribute('disabled', true);

    //    //仍然会阻止表单提交
    //    //setTimeout(function () {
    //    //    button.removeAttribute('disabled');
    //    //}, 0);

    //}, false);

    //document.querySelector('#goToGetpass').addEventListener('click', function () {
    //    var button = this;

    //    button.setAttribute('disabled', true);

    //    //仍然会阻止表单提交
    //    setTimeout(function () {
    //        button.removeAttribute('disabled');
    //    }, 0);

    //}, false);

    $("#Login_SecurityCode").on('click', function () {
        this.src = this.src + '?';

        var cookie_val = getCookie('yzmcode');
        alert(cookie_val);

        $.ajax({

            type: "post",
            url: "/Home/GetSecurityCode",

            success: function (data) {
                alert(data)
            }
        })
    })
    $("#Register_SecurityCode").on('click', function () {
        this.src = this.src + '?';

        var cookie_val = getCookie('yzmcode');
        alert(cookie_val);

        $.ajax({

            type: "post",
            url: "/Home/GetSecurityCode",

            success: function (data) {
                alert(data)
            }
        })
    })
    $("#Forget_SecurityCode").on('click', function () {
        this.src = this.src + '?';

        var cookie_val = getCookie('yzmcode');
        alert(cookie_val);                     

        $.ajax({

            type: "post",
            url: "/Home/GetSecurityCode",

            success: function (data) {
                alert(data)
            }
        })
    })


    $('#my-modal-login').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $("#login_btn").click(function () {
        $("#my-modal-login").modal("toggle");
        //$(".modal-backdrop").remove();//删除class值为modal-backdrop的标签，可去除阴影
    });

    $("#login-close-btn").click(function () {
        $('#my-modal-login').modal('hide')
    })


    //$("#goToLogin").click(function () {
    //    var button = this;

    //    var data = { "loginname": $.trim($("#LoginName").val()), "loginpwd": $.trim($("#LoginPwd").val()) }

    //    $.ajax({
    //        type: "post",
    //        url: "/Home/Login",
    //        data: data,

    //        success: function (data) {
    //            if (data == '-1') {
    //                alert("用户不存在")
    //            } else if (data == '0') {
    //                alert("密码错误")
    //            } else {

    //                $("#my-modal-login").modal('hide')
    //                $(".loginOutLi").removeClass('hidden')
    //            }

    //            button.removeAttribute('disabled');
    //        }
    //    })

    //})

    //$("#goToRegister").on('click', function () {
    //    var button = this;

    //    var data = { "nickname": $.trim($("#NickName").val()), "loginname": $.trim($("#Register_LoginName").val()), "loginpwd": $.trim($("#Register_LoginPwd").val()), "email": $.trim($("#Register_UserEmail").val()) }

    //    $.ajax({
    //        type: "post",
    //        url: "/Home/Register",
    //        data: data,

    //        success: function (data) {

    //            if (data == '-1') {

    //                //var span = $("</span>", {
    //                //    "text": "用户不存在",
    //                //    "style": "color:red;"
    //                //})

    //                //button.insertBefore(span);

    //            } else if (data == '0') {

    //                //var span = $("</span>", {
    //                //    "text": "密码错误",
    //                //    "style": "color:red;"
    //                //})

    //                //button.insertBefore(span);
    //            } else {

    //                $("#my-modal-login").modal('hide')
    //                $(".loginOutLi").removeClass('hidden')
    //            }

    //            button.removeAttribute('disabled');
    //        }
    //    })
    //})

    //$("#goToGetpass").on('click', function () {
        

    //    var loginName = $("#LoginName").val()
    //    var emailAdress = $("#UserEmail").val()

    //    alert(loginName +"," + emailAdress)

    //    var data = { "loginname": $.trim($("#LoginName").val()), "email": $.trim($("#UserEmail").val()) }

    //    $.ajax({
    //        url: "Home/ForgetPassword",
    //        type: "post",
    //        data: data,

    //        success: function (data) {
    //            console.log(data);

    //            alert("邮件发送成功,去邮箱查看您的密码！");
    //            $("#my-modal-login").modal('hide');
                
    //        },

    //        complete: function () { },

    //        error: function (ex) {
    //            alert("错误", "出错啦!");
    //            console.log(ex);
    //        }
    //    });
    //})

})

$(function () {

    //获取cookie值
    var allcookies = document.cookie;

    //定义一个函数，用来读取特定的cookie值。
    function getCookie(cookie_name) {
        var allcookies = document.cookie;
        var cookie_pos = allcookies.indexOf(cookie_name);   //索引的长度
        // 如果找到了索引，就代表cookie存在，

        // 反之，就说明不存在。

        if (cookie_pos != -1) {
            // 把cookie_pos放在值的开始，只要给值加1即可。
            cookie_pos += cookie_name.length + 1;      //这里我自己试过，容易出问题，所以请大家参考的时候自己好好研究一下。。。
            var cookie_end = allcookies.indexOf(";", cookie_pos);
            if (cookie_end == -1) {
                cookie_end = allcookies.length;
            }
            var value = unescape(allcookies.substring(cookie_pos, cookie_end)); //这里就可以得到你想要的cookie的值了。。。
        }
        return value;
    }


})

$(function () {

    $("#Register_LoginName").on("blur", function () {
        

        
    })

})
