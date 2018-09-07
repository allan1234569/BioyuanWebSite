//室间质评品管理
$(function () {

    //禁止点击背景或按esc键关闭模态框
    $('#addinterroomqualitycontrolmodel').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });
    $('#addinterroomqualitycontrolmodel').on('hidden.bs.modal', function (e) {
        clearAddInterroomQualityControlFormData();
    })

    $('#modifyInterroomQualityControlModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });
    $('#modifyInterroomQualityControlModal').on('hidden.bs.modal', function (e) {
        clearModifyInterroomQualityControlFormData();
    })


    //初始化室间的分析物标签
    /* 添加质控品模块 */
    var add_tagInterroomAnalyte = new Tag("add_interroomAnalyte");
    //tag2.tagValue = "浙江,绍兴,上海,山东,北京,四川";     //初始化标签
    add_tagInterroomAnalyte.initView();

    var add_interroomGroupListInterroomFeature = new GroupList("add_interroomFeatureValue");
    add_interroomGroupListInterroomFeature.initView();

    var add_interroomGroupListInterroomSpecification = new GroupList("add_interroomSpecificationValue");
    add_interroomGroupListInterroomSpecification.initView();

    var add_interroomGroupListInterroomStorageCondition = new GroupList("add_interroomStorageConditionValue");
    add_interroomGroupListInterroomStorageCondition.initView();

    /* 修改质控品模块 */
    var modify_tagInterroomAnalyte = new Tag("modifyInterroom_InterroomAnalyte");
    //tag2.tagValue = "浙江,绍兴,上海,山东,北京,四川";     //初始化标签
    modify_tagInterroomAnalyte.initView();

    var modify_interroomGroupListInterroomFeature = new GroupList("modifyInterroom_Feature");
    modify_interroomGroupListInterroomFeature.initView();

    var modify_interroomGroupListInterroomSpecification = new GroupList("modifyInterroom_Specification");
    modify_interroomGroupListInterroomSpecification.initView();

    var modify_interroomGroupListInterroomStorageCondition = new GroupList("modifyInterroom_StorageCondition");
    modify_interroomGroupListInterroomStorageCondition.initView();



    //启用用户
    $('.enable_interroomControl').on('click', function () {

        var obj = $(this);
        var val = $(this).parent().next().text();
        val = val.trim();

        $.ajax({
            type: "post",
            url: "/Admin/Home/ProductsManage/EnableInterroomQualityControl",
            data: { "id": val }, //以键/值对的形式
            async: false,
            dataType: "text",
            success: function (data) {
                if (data == "1") {
                    obj.parent().prev().find("span").text("启用");
                } else if (data == "0") {
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

    //清除添加室间质评品的表单数据
    function clearAddInterroomQualityControlFormData() {
        $("#addInterroom_ProductName").val("");
        $("#addInterroom_Description").val("");
        //$("#add_lab_Img").val(jsonObj.Img);
        $("#addInterroom_Preview").attr('src', '');
        //$("#addInterroom_CategoryName").find("option[value='" + "--请选择专业--" + "']").attr("selected", true);
        $("#addInterroom_Constitute").val("");
        $("#addInterroom_SingleSpecification").val("");
        add_tagInterroomAnalyte.setValue("");
        add_interroomGroupListInterroomFeature.setValue("");
        add_interroomGroupListInterroomSpecification.setValue("");
        add_interroomGroupListInterroomStorageCondition.setValue("");
        $("#addInterroom_UsefulLife").val("");
        $("#addInterroom_Annotation").val("");
    }

    //清除修改室间质评品的表单数据
    function clearModifyInterroomQualityControlFormData() {
        $("#modifyInterroom_InterroomQualityControlId").val("");
        $("#modifyInterroom_ProductName").val("");
        $("#modifyInterroom_Description").val("");
        //$("#modify_lab_Img").val(jsonObj.Img);
        $("#modifyInterroom_Preview").attr('src', '');
        //$("#modifyInterroom_CategoryName").find("option[value='" + "--请选择专业--" + "']").attr("selected", true);
        $("#modifyInterroom_Constitute").val("");
        $("#modifyInterroom_SingleSpecification").val("");
        modify_tagInterroomAnalyte.setValue("");
        modify_interroomGroupListInterroomFeature.setValue("");
        modify_interroomGroupListInterroomSpecification.setValue("");
        modify_interroomGroupListInterroomStorageCondition.setValue("");
        $("#modifyInterroom_UsefulLife").val("");
        $("#modifyInterroom_Annotation").val("");
    }


    //获取待修改室内质控品数据
    $('.modify_interroomControl').on('click', function () {

        var id = $(this).parent().next().text().trim();

        $.ajax({
            type: "post",
            url: "/Admin/Home/ProductsManage/GetInterroomQualityControlDetail",
            data: { "id": id },
            async: false,
            dataType: "text",
            success: function (data) {

                if (data != "null") {
                    var jsonObj = JSON.parse(data);
                    //{"Analyte":"","Annotation":"","CertificateNo":"","Constitute":"","CreateTime":"\/Date(1514274389000+0800)\/","Description":"","Feature":"","Img":"","InterroomQualityControlId":"fbe1da68-b903-45bd-b998-431520b6d9bc","ModifyTime":"\/Date(1514274389000+0800)\/","ProductName":"室间质评品名称1","SingleSpecification":"","Specification":"","State":0,"StorageCondition":"","UsefulLife":""}

                    $("#modifyInterroom_InterroomQualityControlId").val(jsonObj.InterroomQualityControlId);
                    $("#modifyInterroom_ProductName").val(jsonObj.ProductName);
                    $("#modifyInterroom_Description").val(jsonObj.Description);

                    //$("#modify_lab_Img").val(jsonObj.Img);

                    $("#modifyInterroom_Preview").attr('src', '/Admin/Home/ProductsManage/ShowImage?id=' + jsonObj.Img);

                    $("#modifyInterroom_CategoryName").find("option[value='" + jsonObj.CategoryName + "']").attr("selected", true);

                    $("#modifyInterroom_Constitute").val(jsonObj.Constitute);
                    $("#modifyInterroom_SingleSpecification").val(jsonObj.SingleSpecification);

                    modify_tagInterroomAnalyte.setValue(jsonObj.Analyte);
                    modify_interroomGroupListInterroomFeature.setValue(jsonObj.Feature);
                    modify_interroomGroupListInterroomSpecification.setValue(jsonObj.Specification);
                    modify_interroomGroupListInterroomStorageCondition.setValue(jsonObj.StorageCondition);

                    $("#modifyInterroom_UsefulLife").val(jsonObj.UsefulLife);
                    
                    $("#modifyInterroom_Annotation").val(jsonObj.Annotation);
                    
                    $("#modifyInterroomQualityControlModal").modal('show');

                } else {
                    alert("数据加载失败");
                }

            }
        })

    })

    //删除室间质评品弹框并设置ID
    $('.delete_interroomControl').on('click', function () {
        var val = $(this).parent().next().text();
        val = val.trim();

        $('#delete_interroomControl_id').text(val);
    })
    //删除室间质评品
    $('#delete_interroomControl_btn').on('click', function () {

        var id = $('#delete_interroomControl_id').text();

        $.ajax({
            type: "post",
            url: "DeleteInterroomQualityControl",
            data: { "ProductId": id },
            datatype: "text",
            success: function (data) {

                window.location.href = "/Admin/Home/InterroomQualityControl";

                if (data == "True") {
                    alert("True");
                } else if (data == "False") {
                    alert("False");
                }
            }
        })

    })

    //添加质评品，修改图片的事件
    $('#addInterroom_Img').on('change', function () {

        if (window.FileReader) {
            var reader = new FileReader();
        } else {
            alert("您的设备不支持图片预览功能，如需该功能请升级您的设备！");
        }

        var file = this.files[0];
        var imageType = /^image\//;
        if (!imageType.test(file.type)) {
            alert("请选择图片！");
            return;
        }

        if (this.files && file) {
            reader = new FileReader();
            reader.onload = function (e) {
                $("#addInterroom_Preview").attr("src", e.target.result);
            }
            reader.readAsDataURL(file);
        }

    })

    //修定质评品，修改图片的事件
    $('#modifyInterroom_Img').on('change', function () {

        if (window.FileReader) {
            var reader = new FileReader();
        } else {
            alert("您的设备不支持图片预览功能，如需该功能请升级您的设备！");
        }

        var file = this.files[0];
        var imageType = /^image\//;
        if (!imageType.test(file.type)) {
            alert("请选择图片！");
            return;
        }

        if (this.files && file) {
            reader = new FileReader();
            reader.onload = function (e) {
                $("#modifyInterroom_Preview").attr("src", e.target.result);
            }
            reader.readAsDataURL(file);
        }

    })


    //表格分页处理
    $(function () {
        var dataLength = 0;

        $.ajax({
            url: "/Admin/Home/ProductsManage/GetInterroomQualityControlByName",
            type: "POST",
            async: false,
            dataType: "json",
            success: function (data) {
                var jsonObj = data;
                dataLength = jsonObj.length;
            }
        });


        var i_numberOfPage;
        var i_totalPages;

        if (dataLength > 0) {
            i_totalPages = Math.ceil(dataLength / 6);//获取总页数
        } else {
            i_totalPages = 0;
        }

        if (i_totalPages >= 5) {
            i_numberOfPage = 5;
        } else {
            i_numberOfPage = i_totalPages;
        }

        var element = $('#iqc_pagination');
        var options = {
            size: "small",//分页控件的大小，参数分别是small，normal，large
            bootstrapMajorVersion: 3,//bootstrap的版本，告诉它如何来渲染页面
            itemTexts: function (type, page, current) {
                switch (type) {
                    case "first":
                        return "首页";
                    case "prev":
                        return "上一页";
                    case "next":
                        return "下一页";
                    case "last":
                        return "尾页";
                    case "page":
                        return page;
                }
            },//自定义首页，下一页，上一页，尾页的显示文字
            //itemContainerClass: function (type, page, current) {
            //    return (page === current) ? "active" : "pointer-cursor";
            //},//是否在鼠标悬浮时显示辅助标签
            shouldShowPage: function (type, page, current) {
                switch (type) {
                    case "first":
                    case "last":
                        return true;
                    default:
                        return true;
                }
            },//是否显示“第一页”和“最后一页”
            useBootstrapTooltip: true,
            alignment: "left",//对齐方式，可选参数分别是left，center，right
            currentPage: 1,//当前页
            numberOfPages: i_numberOfPage,//显示多少个分页按钮
            totalPages: i_totalPages,
            pageUrl: function (type, page, current) {
                return "/Admin/Home/ProductsManage/GetInterroomQualityControlByName?page=" + page;
            },
            onPageClicked: function (event, originalEvent, type, page) {
                originalEvent.preventDefault();
                originalEvent.stopPropagation();
                $.ajax({
                    url: originalEvent.target.href,
                    type: "POST",
                    dataType: "json",
                    success: function (data) {

                        var jsonObj = data;

                        console.log(jsonObj.length);
                        console.log(data + "                   currentPage=" + page);
                        $(data).each(function () {//更新列表显示

                        });

                        var table = $("#iqc_table");
                        table.empty("");

                        var maxCount = jsonObj.length;
                        var startIndex = (page - 1) * 6;

                        for (var i = (page - 1) * 6; i < Math.min(maxCount, eval(6 * page)) ; i++) {

                            var tr = $("<tr/>", {
                            })

                            for (var j = 0; j < 8; j++) {
                                var td;

                                if (j == 0) {//序号
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": i + 1
                                    })

                                    td.append(span);
                                } else if (j == 1) {//质控品名称
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": jsonObj[i].ProductName
                                    })

                                    td.append(span);
                                } else if (j == 2) {//样品图片
                                    td = $("<td/>", {
                                    })

                                    var src = "";
                                    if (jsonObj[i].Img == "") {
                                        src = "";
                                    } else {
                                        src = '/Admin/Home/ProductsManage/ShowImage?id=' + jsonObj[i].Img;
                                    }
                                    var img = $("<img/>", {
                                        "src": src,
                                        "height": 60,
                                        "width": 90
                                    })

                                    td.append(img);
                                } else if (j == 3) {//创建时间
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": formatDateBoxFull(jsonObj[i].CreateTime)
                                    })

                                    td.append(span);
                                } else if (j == 4) {//修改时间
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": formatDateBoxFull(jsonObj[i].ModifyTime)
                                    })

                                    td.append(span);
                                } else if (j == 5) {//状态
                                    td = $("<td/>", {
                                        "class": "td-status"
                                    })
                                    var span = $("<span/>", {
                                        "text": jsonObj[i].State == 1 ? "启用" : "禁用"
                                    })

                                    td.append(span);
                                } else if (j == 6) {//操作
                                    td = $("<td/>", {
                                        "class": "td-manage"
                                    })

                                    var a1 = $("<a/>", {
                                        "class": "enable_interroomControl",
                                        "title": "启用",
                                        "href": "javascript:;",
                                        "click": function () {//启用/禁用室内质控品
                                            var obj = $(this);
                                            var val = $(this).parent().next().text();
                                            val = val.trim();

                                            $.ajax({
                                                type: "post",
                                                url: "/Admin/Home/ProductsManage/EnableInterroomQualityControl",
                                                data: { "id": val }, //以键/值对的形式
                                                async: false,
                                                dataType: "text",
                                                success: function (data) {
                                                    if (data == "1") {
                                                        obj.parent().prev().find("span").text("启用");
                                                    } else if (data == "0") {
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
                                        }
                                    })
                                    var a2 = $("<a/>", {
                                        "class": "modify_interroomControl",
                                        //"data-toggle":"modal",
                                        "title": "修改",
                                        "href": "javascript:;",
                                        "click": function () {//获取待修改室内质控品数据
                                            var id = $(this).parent().next().text().trim();

                                            $.ajax({
                                                type: "post",
                                                url: "/Admin/Home/ProductsManage/GetInterroomQualityControlDetail",
                                                data: { "id": id },
                                                async: false,
                                                dataType: "text",
                                                success: function (data) {

                                                    if (data != "null") {
                                                        var jsonObj = JSON.parse(data);
                                                        //{"Analyte":"","Annotation":"","CertificateNo":"","Constitute":"","CreateTime":"\/Date(1514274389000+0800)\/","Description":"","Feature":"","Img":"","InterroomQualityControlId":"fbe1da68-b903-45bd-b998-431520b6d9bc","ModifyTime":"\/Date(1514274389000+0800)\/","ProductName":"室间质评品名称1","SingleSpecification":"","Specification":"","State":0,"StorageCondition":"","UsefulLife":""}

                                                        $("#modifyInterroom_InterroomQualityControlId").val(jsonObj.InterroomQualityControlId);
                                                        $("#modifyInterroom_ProductName").val(jsonObj.ProductName);
                                                        $("#modifyInterroom_Description").val(jsonObj.Description);

                                                        //$("#modify_lab_Img").val(jsonObj.Img);

                                                        $("#modifyInterroom_Preview").attr('src', '/Admin/Home/ProductsManage/ShowImage?id=' + jsonObj.Img);

                                                        $("#modifyInterroom_CategoryName").find("option[value='" + jsonObj.CategoryName + "']").attr("selected", true);

                                                        $("#modifyInterroom_Constitute").val(jsonObj.Constitute);
                                                        $("#modifyInterroom_SingleSpecification").val(jsonObj.SingleSpecification);

                                                        modify_tagInterroomAnalyte.setValue(jsonObj.Analyte);
                                                        modify_interroomGroupListInterroomFeature.setValue(jsonObj.Feature);
                                                        modify_interroomGroupListInterroomSpecification.setValue(jsonObj.Specification);
                                                        modify_interroomGroupListInterroomStorageCondition.setValue(jsonObj.StorageCondition);

                                                        $("#modifyInterroom_UsefulLife").val(jsonObj.UsefulLife);

                                                        $("#modifyInterroom_Annotation").val(jsonObj.Annotation);

                                                        $("#modifyInterroomQualityControlModal").modal('show');

                                                    } else {
                                                        alert("数据加载失败");
                                                    }

                                                }
                                            })
                                        }
                                    })
                                    var a3 = $("<a/>", {
                                        "class": "delete_interroomControl",
                                        "data-toggle": "modal",
                                        "data-target": "#deleteInterroomQualityControlModal",
                                        "title": "删除",
                                        "href": "javascript:;",
                                        "click": function () {
                                            var val = $(this).parent().next().text();
                                            val = val.trim();

                                            $('#delete_interroomControl_id').text(val);
                                        }
                                    })

                                    var span1 = $("<span/>", {
                                        "class": "glyphicon glyphicon-download"
                                    })
                                    var span2 = $("<span/>", {
                                        "class": "glyphicon glyphicon-pencil"
                                    })
                                    var span3 = $("<span/>", {
                                        "class": "glyphicon glyphicon-trash"
                                    })

                                    a1.append(span1);
                                    a2.append(span2);
                                    a3.append(span3);

                                    td.append(a1);
                                    td.append(a2);
                                    td.append(a3);
                                } else if (j == 7) {
                                    td = $("<td/>", {
                                        "class": "hidden",
                                        "text": jsonObj[i].InterroomQualityControlId
                                    })
                                }

                                tr.append(td);
                            }

                            table.append(tr);
                        }

                    },
                    error: function (error) {
                        alert("error");
                    }
                });
            }
        }
        element.bootstrapPaginator(options);
        $("#iqc_pagination li:first a").trigger("click");
    });

   
})
