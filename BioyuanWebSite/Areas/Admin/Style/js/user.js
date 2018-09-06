//用户管理
$(function () {

    //表单验证
    $('#add_user_form')
    .bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            UserName: {
                validators: {
                    notEmpty: {
                        message: '昵称不能为空'
                    }
                },
                remote: {
                    url: ''
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
            LoginPwdConfirm: {
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
            }
        }
    })


    $('#modify_password_form')
    .bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            OriginLoginPwd: {
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
            LoginPwdConfirm: {
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
            }
        }
    })


    $('#addUserModel').on('hidden.bs.modal', function (e) {
        $("#add_user_form").data("bootstrapValidator").resetForm();
    })

    $('#modifyUserModal').on('hidden.bs.modal', function (e) {
        $("#modify_password_form").data("bootstrapValidator").resetForm();
    })

    $('#addUserModel').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $('#modifyUserModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    //$('#deleteUserModal').modal({
    //    backdrop: 'static',
    //    keyboard: false,
    //    show: false
    //});


    //启用用户
    $('.enable_user').on('click', function () {
        var obj = $(this);
        var id = $(this).parent().next().text();
        id = id.trim();

        $.ajax({
            type: 'post',
            url: '/Admin/Home/UserManage/EnableUser',
            data: { "id": id },
            async: false,
            dataType: 'text',
            success: function (data) {
                if (data == '1') {
                    obj.parent().prev().find("span").text("启用");
                } else if (data == '0') {
                    obj.parent().prev().find("span").text("禁用");
                }
            },
            error: function () {

            },
            complete: function () {

            }
        });

        if (obj.attr("title") == "启用") {
            obj.attr("title", "禁用");
        } else {
            obj.attr("title", "启用");
        }
    })


    //修改密码
    $('.modify_user').on('click', function () {

        var id = $(this).parent().next().text().trim();

        $.ajax({
            type: "post",
            url: "/Admin/Home/ProductsManage/GetAdminById",
            data: { "id": id },
            async: false,
            dataType: "text",
            success: function (data) {

                if (data != "null") {

                    //{\"UserId\":\"1384d86f-141d-4231-b261-a651362be8d8\",\"UserName\":\"超级用户\",\"CreateTime\":\"\\/Date(1514168344000+0800)\\/\",\"Limit\":0,\"LoginName\":\"admin\",\"LoginPwd\":\"admin123456\",\"ModifyTime\":\"\\/Date(1514168344000+0800)\\/\",\"State\":1}

                    var jsonObj = JSON.parse(data);

                    $("#modifyUser_UserId").val(jsonObj.UserId)
                    $("#modifyUser_UserName").val(jsonObj.UserName);
                    $("#modifyUser_LoginName").val(jsonObj.LoginName);

                    $("#modifyUserModal").modal('show');

                } else {
                    alert("数据加载失败");
                }

            }
        })

    })

    //删除用户设置ID弹框
    $('.delete_user').on('click', function () {
        var val = $(this).parent().next().text();
        val = val.trim();

        $('#delete_user_id').text(val);
    })
    //删除用户/*
    $('#delete_user_btn').on('click', function () {
        var id = $('#delete_user_id').text();

        $.ajax({
            type: "post",
            url: "DeleteUser",
            data: { "id": id },
            datatype: "text",
            success: function (data) {

                if (data == "True") {
                    alert("True");
                } else if (data == "False") {
                    alert("False");
                }

                window.location.href = "/Admin/Home/UserManage/User";
            }
        })

    })


})