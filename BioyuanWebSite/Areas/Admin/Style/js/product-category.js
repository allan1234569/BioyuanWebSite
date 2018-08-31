﻿//产品分类管理
$(function () {

    $('#addProductCategoryModel').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $('#modifyProductCategoryModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    //$('#deleteUserModal').modal({
    //    backdrop: 'static',
    //    keyboard: false,
    //    show: false
    //});


    //启用分类
    $('.enable_product_category').on('click', function () {
        var obj = $(this);
        var id = $(this).parent().next().text().trim();

        $.ajax({
            type: 'post',
            url: '/Admin/Home/ProductCategoryManage/EnableProductCategory',
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
    $('.modify_product_category').on('click', function () {

        var id = $(this).parent().next().text().trim();
        
        $.ajax({
            type: "post",
            url: "/Admin/Home/ProductCategoryManage/GetProductCategoryDetail",
            data: { "id": id },
            async: false,
            dataType: "text",
            success: function (data) {
 
                if (data != "null") {
                    //{"CategoryId":"9afc684b-ebc9-4e16-a4ac-087502a435c1","CreateTime":"\/Date(1516587316000+0800)\/","ModifyTime":"\/Date(1516587316000+0800)\/","State":1,"CategoryName":"肿瘤","Description":""}
                    var jsonObj = JSON.parse(data);
                    $("#modifyProductCategory_CategoryId").val(jsonObj.CategoryId)
                    $("#modifyProductCategory_CategoryName").val(jsonObj.CategoryName); 
                    $("#modifyProductCategory_Description").val(jsonObj.Description);
                    $("#modifyProductCategoryModal").modal('show');

                } else {
                    alert("数据加载失败");
                }
            }
        })
    })  

    //删除分类设置ID弹框
    $('.delete_product_category').on('click', function () {
        var val = $(this).parent().next().text();
        val = val.trim();

        $('#delete_product_category_id').text(val);
    })
    //删除分类/*
    $('#delete_product_category_btn').on('click', function () {

        var id = $('#delete_product_category_id').text();

        $.ajax({
            type: "post",
            url: "/Admin/Home/ProductCategoryManage/DeleteProductCategory",
            data: { "id": id },
            datatype: "text",
            success: function (data) {

                if (data == "True") {
                    alert("True");
                } else if (data == "False") {
                    alert("False");
                }

                window.location.href = "/Admin/Home/ProductCategoryManage/ProductCategory";
            }
        })
    })
})