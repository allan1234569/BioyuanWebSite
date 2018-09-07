//新闻分类管理
$(function () {

    $('#addNewsCategoryModel').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $('#modifyNewsCategoryModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $('#add_news_category_form')
    .bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            CategoryName: {
                validators: {
                    notEmpty: {
                        message: '分类名不能为空'
                    },
                    remote: {//ajax验证。server result:{"valid",true or false} 向服务发送当前input name值，获得一个json数据。例表示正确：{"valid",true}  
                        url: '/Admin/Home/NewsCategoryExists',//验证地址
                        message: '此分类已经添加',//提示消息
                        delay: 50,//每输入一个字符，就发ajax请求，服务器压力还是太大，设置2秒发送一次ajax（默认输入一个字符，提交一次，服务器压力太大）
                        type: 'POST',//请求方式
                        success: function (data) {
                            alert(data);
                        }
                    }
                }
            }
        }
    })
    
    $('#modify_news_category_form')
    .bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            CategoryName: {
                validators: {
                    notEmpty: {
                        message: '分类名不能为空'
                    },
                    remote: {//ajax验证。server result:{"valid",true or false} 向服务发送当前input name值，获得一个json数据。例表示正确：{"valid",true}  
                        url: '/Admin/Home/ProductCategoryExists',//验证地址
                        message: '此分类已经添加',//提示消息
                        delay: 50,//每输入一个字符，就发ajax请求，服务器压力还是太大，设置2秒发送一次ajax（默认输入一个字符，提交一次，服务器压力太大）
                        type: 'POST',//请求方式
                        success: function (data) {
                            alert(data);
                        }
                    }
                }
            }
        }
    })


    //启用分类
    $('.enable_news_category').on('click', function () {
        var obj = $(this);
        var id = $(this).parent().next().text().trim();

        $.ajax({
            type: 'post',
            url: '/Admin/Home/NewsCategoryManage/EnableNewsCategory',
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


    //修改分类
    $('.modify_news_category').on('click', function () {

        var id = $(this).parent().next().text().trim();

        $.ajax({
            type: "post",
            url: "/Admin/Home/NewsCategoryManage/GetNewsCategoryDetail",
            data: { "id": id },
            async: false,
            dataType: "text",
            success: function (data) {

                if (data != "null") {

                    //{"CategoryId":"9c2213ff-f7e9-4960-a2ec-c6fa7b618587","CategoryName":"111","CreateTime":"\/Date(1515493193000+0800)\/","ModifyTime":"\/Date(1515493193000+0800)\/","State":0}

                    var jsonObj = JSON.parse(data);

                    $("#modifyNewsCategory_CategoryId").val(jsonObj.CategoryId)
                    $("#modifyNewsCategory_CategoryName").val(jsonObj.CategoryName);
                    $("#modifyNewsCategory_Description").val(jsonObj.Description);
                    $("#modifyNewsCategoryModal").modal('show');

                } else {
                    alert("数据加载失败");
                }

            }
        })

    })

    //删除分类设置ID弹框
    $('.delete_news_category').on('click', function () {
        var val = $(this).parent().next().text();
        val = val.trim();

        $('#delete_news_category_id').text(val);
    })
    //删除分类/*
    $('#delete_news_category_btn').on('click', function () {

        var id = $('#delete_news_category_id').text();

        $.ajax({
            type: "post",
            url: "/Admin/Home/NewsCategoryManage/DeleteNewsCategory",
            data: { "id": id },
            datatype: "text",
            success: function (data) {

                if (data == "True") {
                    alert("True");
                } else if (data == "False") {
                    alert("False");
                }

                window.location.href = "/Admin/Home/NewsCategoryManage/NewsCategory";
            }
        })

    })
})