//用户管理
$(function () {

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




    //添加用户表单验证
    $("#add_user_form").validate();

    //昵称
    $("#userName").rules('add', {
        required: true,
        minlength: 1,
        maxlength: 20,
        messages: {
            required: '请输入昵称！',
            minlength: '帐号不能小于{0}位！',
            maxlength: '帐号不能小于{0}位！'
        }
    });

    //登录名
    $("#loginName").rules('add', {
        required: true,
        minlength: 4,
        maxlength: 20,
        messages: {
            required: '请输入登录名！',
            minlength: '帐号不能小于{0}位！',
            maxlength: '帐号不能小于{0}位！'
        }
    });

    //密码
    $("#password").rules('add', {
        required: true,
        minlength: 6,
        maxlength: 20,
        pass: true,
        messages: {
            required: '请输入6-20位密码，数字和字母！',
            minlength: '密码不能小于{0}位！',
            maxlength: '密码不能超过{0}位！',
            pass: '密码不能含数字和字母以外的符号！'
        }
    });
    //确认密码
    $("#notpass").rules('add', {
        required: true,
        equalTo: '#password',
        messages: {
            required: '请再次输入密码',
            equalTo: '密码输入不一致',
        }
    });



    //修改密码表单验证
    $("#modify_password_form").validate();

    //原始密码
    $("#origin-password").rules('add', {
        required: true,
        minlength: 6,
        maxlength: 20,
        pass: true,
        messages: {
            required: '请输入6-20位密码，数字和字母！',
            minlength: '密码不能小于{0}位！',
            maxlength: '密码不能超过{0}位！',
            pass: '密码不能含数字和字母以外的符号！'
        }
    });

    //新密码
    $("#new-password").rules('add', {
        required: true,
        minlength: 6,
        maxlength: 20,
        pass: true,
        messages: {
            required: '请输入6-20位密码，数字和字母！',
            minlength: '密码不能小于{0}位！',
            maxlength: '密码不能超过{0}位！',
            pass: '密码不能含数字和字母以外的符号！'
        }
    });

    //确认密码
    $("#new-notpass").rules('add', {
        required: true,
        equalTo: '#new-password',
        messages: {
            required: '请再次输入密码',
            equalTo: '密码输入不一致',
        }
    });

    //密码验证规则
    $.validator.addMethod('pass', function (value, element) {
        var pass = /^[\w]+$/
        return this.optional(element) || (pass.test(value));
    });
})