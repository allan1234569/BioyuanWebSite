//[室内质控品管理]

$(function () {

    //表单验证
    $('#add_lqc_form')
    .bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            ProductName: {
                message: '质控品名称无效',
                validators: {
                    notEmpty: {
                        message: '质控品不能为空'
                    }
                },
                remote: {
                    url: ''
                }
            },
            CategoryName: {
                validators: {
                    notEmpty: {
                        message: '专业不能为空'
                    }
                }
            }
        }
    })
    .on('click', '#add_lab_item_no', function () {
        
        var tableId = $(this).parent().next().children("div").next().children("div").children("div").children("table").attr("id");

        var Table = document.getElementById(tableId);   //获取表格

        var columnLength = Table.rows.item(0).cells.length;

        var NewRow = Table.insertRow();         //插入行

        var proObj = $('#table-project');

        //NewRow.insertCell().innerHTML = '<input class="form-control hidden" name=""></input>';//项目ID
        NewRow.insertCell().innerHTML = '<input class="form-control" name="ProductCode"></input>';//项目号
        NewRow.insertCell().innerHTML = '<input class="form-control" name="Concentration"></input>';//浓度
        NewRow.insertCell().innerHTML = '<input class="form-control" name="Specification"></input>';//规格
        NewRow.insertCell().innerHTML = '<input class="form-control" name="CertificateNo"></input>';//证书编号
        NewRow.insertCell().innerHTML = '<a data-toggle="modal" data-target="" title="删除" href="javascript:;"><span class="glyphicon glyphicon-trash deleteRow"></span></a>';   //操作

        $("#" + tableId + " tr:last a .deleteRow").on('click', function () {
            var obj = $(this);
            obj.parent().parent().parent().remove();

            var tab = $('.nav-tabs li.active a');

            var proName = tab.text();
            var rowCount = Table.rows.length - 1;
            $("[name=" + proName + "]").val(rowCount);
        })

        var proName = tab.text();
        var rowCount = Table.rows.length - 1;
        $("[name=" + proName + "]").val(rowCount);



        $option = $(this).find('[name="option[]"]');

    })
    .on('click', '.deleteRow', function () {
        alert("这一行将被删除")
    })

    

    $('#modify_lqc_form')
    .bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            ProductName: {
                message: '室内质控品名称无效',
                validators: {
                    notEmpty: {
                        message: '室内质控品不能为空'
                    }
                },
                remote: {
                    url: ''
                }
            }
        }
    })
    .on('click', '#modify_lab_item_no', function () {
        var tableId = $(this).parent().next().children("div").next().children("div").children("div").children("table").attr("id");

        var Table = document.getElementById(tableId);   //获取表格

        var columnLength = Table.rows.item(0).cells.length;

        var NewRow = Table.insertRow();         //插入行

        //NewRow.insertCell().innerHTML = '<input class="form-control hidden" name="SpecificationId"></input>';//项目ID
        NewRow.insertCell().innerHTML = '<input class="form-control" name="ProductCode"></input>';//项目号
        NewRow.insertCell().innerHTML = '<input class="form-control" name="Concentration"></input>';//浓度
        NewRow.insertCell().innerHTML = '<input class="form-control" name="Specification"></input>';//规格
        NewRow.insertCell().innerHTML = '<input class="form-control" name="CertificateNo"></input>';//证书编号
        NewRow.insertCell().innerHTML = '<a data-toggle="modal" data-target="" title="删除" href="javascript:;"><span class="glyphicon glyphicon-trash deleteRow"></span></a>';   //操作

        $("#" + tableId + " tr:last a .deleteRow").on('click', function () {
            var obj = $(this);
            obj.parent().parent().parent().remove();
        })
    })



    //添加质控品
    $('#addLabQualityControlModel').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });
    $('#addLabQualityControlModel').on('hidden.bs.modal', function (e) {
        clearAddLaboratoryQualityControlFormData();
    })

    $('#modifyLabQualityControlModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });
    $('#modifyLabQualityControlModal').on('hidden.bs.modal', function (e) {
        clearModifyLaboratoryQualityControlFormData();
    })



    /* 添加质控品模块 */
    //初始化分析物标签
    var add_tagAnalyte = new Tag("add_labAnalyte");
    add_tagAnalyte.initView();

    var add_GroupListStability = new GroupList("add_labStabilityValue");
    add_GroupListStability.initView();

    var add_GroupListFeature = new GroupList("add_labFeatureValue");
    add_GroupListFeature.initView();


    /* 添加质控品模块 */
    var modify_tagAnalyte = new Tag("modify_Analyte");
    modify_tagAnalyte.initView();

    var modify_GroupListStability = new GroupList("modify_Stability");
    modify_GroupListStability.initView();

    var modify_GroupListFeature = new GroupList("modify_Feature");
    modify_GroupListFeature.initView();

    ////启用室内质控品
    //$('.enable_labQualityControl').on('click', function () {
    //    var obj = $(this);
    //    var val = $(this).parent().next().text();
    //    val = val.trim();
    //    $.ajax({
    //        type: "post",
    //        url: "/Admin/Home/ProductsManage/EnableLaboratoryQualityControl",
    //        data: { "id": val },
    //        async: false,
    //        dataType: "text",
    //        success: function (data) {
    //            if (data == "1") {
    //                obj.parent().prev().find("span").text("启用");
    //            } else if (data == "0") {
    //                obj.parent().prev().find("span").text("禁用");
    //            }
    //        },
    //        error: function () {

    //        },
    //        complete: function () {

    //        }
    //    });

    //    if (obj.attr("title") == "启用") {
    //        obj.attr("title", "禁用");
    //    } else{
    //        obj.attr("title", "启用");
    //    }
    //})


    ////获取待修改室内质控品数据
    //$('.modify_labQualityControl').on('click', function () {

    //    var id = $(this).parent().next().text().trim();
        
    //    $.ajax({
    //        type: "post",
    //        url: "/Admin/Home/ProductsManage/GetLaboratoryQualityControlDetail",
    //        data: { "id": id },
    //        async: false,
    //        dataType: "text",
    //        success: function (data) {

    //            if (data != "null") {
    //                var jsonObj = JSON.parse(data);
    
    //                //{"Analyte":"分析物,asad","Annotation":"备注","CertificateNo":null,"CreateTime":"\/Date(1514883601000+0800)\/","Description":"描述","Feature":"产品特征","Img":"","LaboratoryQualityControlId":"820f6d7d-c085-4a76-834b-57c559f6b842","ModifyTime":"\/Date(1514884141000+0800)\/","ProductName":"质控品名称111","Specifications":[{"CertificateNo":"","Concentration":"","LaboratoryQualityControlId":"820f6d7d-c085-4a76-834b-57c559f6b842","ProductCode":"","Specification":"","SpecificationId":"804d25b4-88a2-4c8d-b2ef-4955dc2b8948"}],"Stability":"稳定性","State":0}

    //                $("#modify_LaboratoryQualityId").val(jsonObj.LaboratoryQualityControlId);
    //                $("#modify_ProductName").val(jsonObj.ProductName);
    //                $("#modify_Description").val(jsonObj.Description);
         
    //                //$("#modify_lab_Img").val(jsonObj.Img);
    //                $("#modify_lab_preview").attr('src', '/Admin/Home/ProductsManage/ShowImage?id=' + jsonObj.Img);

    //                $("#modify_lab_CategoryName").find("option[value='" + jsonObj.CategoryName + "']").attr("selected", true);

    //                var tableId = "modify_tabs_default";

    //                var table = document.getElementById(tableId);

    //                for (var i = 0; i < jsonObj.Specifications.length; ++i) {
                        
    //                    var NewRow = table.insertRow();//插入行
                        
    //                    NewRow.insertCell().innerHTML = '<input class="form-control" name="ProductCode" value="' + jsonObj.Specifications[i].ProductCode + '"></input>';//项目号
    //                    NewRow.insertCell().innerHTML = '<input class="form-control" name="Concentration" value="' + jsonObj.Specifications[i].Concentration + '"></input>';//浓度
    //                    NewRow.insertCell().innerHTML = '<input class="form-control" name="Specification" value="' + jsonObj.Specifications[i].Specification + '"></ input>';//规格
    //                    NewRow.insertCell().innerHTML = '<input class="form-control" name="CertificateNo" value="' + jsonObj.Specifications[i].CertificateNo + '"></input>';//证书编号
    //                    NewRow.insertCell().innerHTML = '<a data-toggle="modal" data-target="" title="删除" href="javascript:;"><span class="glyphicon glyphicon-trash deleteRow"></span></a>';//操作

    //                    $("#" + tableId + " tr:last a .deleteRow").on('click', function () {
    //                        var obj = $(this);
    //                        obj.parent().parent().parent().remove();
    //                    })
    //                }
                    
    //                modify_tagAnalyte.setValue(jsonObj.Analyte);
    //                modify_GroupListStability.setValue(jsonObj.Stability)
    //                modify_GroupListFeature.setValue(jsonObj.Feature);
    //                $("#modify_Annotation").val(jsonObj.Annotation);
                    
    //                $("#modifyLabQualityControlModal").modal('show');
                    
    //            } else {
    //                alert("数据加载失败");
    //            }

    //        }
    //    })

    //})

    //清除添加室内质控品的表单数据
    function clearAddLaboratoryQualityControlFormData() {
        $("#add_ProductName").val("");
        $("#add_Description").val("");
        //$("#add_lab_Img").val(jsonObj.Img);
        $("#add_lab_preview").attr('src', '');
        $("#add_lab_CategoryName").find("option[value='" + "--请选择专业--" + "']").attr("selected", true);
        add_tagAnalyte.setValue("");
        add_GroupListStability.setValue("")
        add_GroupListFeature.setValue("");
        $("#add_Annotation").val("");
        clearTable("");
    }

    //清除修改室内质控品的表单数据
    function clearModifyLaboratoryQualityControlFormData() {
        $("#modify_LaboratoryQualityId").val("");
        $("#modify_ProductName").val("");
        $("#modify_Description").val("");
        //$("#modify_lab_Img").val(jsonObj.Img);
        $("#modify_lab_preview").attr('src', '');
        $("#modify_lab_CategoryName").find("option[value='" + "--请选择专业--" + "']").attr("selected", true);
        modify_tagAnalyte.setValue("");
        modify_GroupListStability.setValue("")
        modify_GroupListFeature.setValue("");
        $("#modify_Annotation").val("");
        clearTable("");
    }


    //删除室内质控品
    $('.delete_labQualityControl').on('click', function () {
        var val = $(this).parent().next().text();
        val = val.trim();

        $('#delete_labControl_id').text(val);
    })
    //删除室内质控品
    $('#delete_labControl_btn').on('click', function () {

        var id = $('#delete_labControl_id').text();

        $.ajax({
            type: "post",
            url: "DeleteLaboratoryQuailtyControl",
            data: { "ProductId": id },
            datatype: "text",
            success: function (data) {
                window.location.href = "/Admin/Home/LaboratoryQualityControl";
            }
        })

    })


    //定义修改图片的事件
    $('#add_lab_img').on('change', function () {

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
                $("#add_lab_preview").attr("src", e.target.result);
            }
            reader.readAsDataURL(file);
        }

    })
    

    $('#modify_lab_Img').on('change', function () {

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
                $("#modify_lab_preview").attr("src", e.target.result);
            }
            reader.readAsDataURL(file);
        }
    })


    //新增质控品-添加货号
    //$('#add_lab_item_no').on('click', function () {

    //    var tableId = $(this).parent().next().children("div").next().children("div").children("div").children("table").attr("id");

    //    var Table = document.getElementById(tableId);   //获取表格

    //    var columnLength = Table.rows.item(0).cells.length;

    //    var NewRow = Table.insertRow();         //插入行

    //    var proObj = $('#table-project');
        
    //    //NewRow.insertCell().innerHTML = '<input class="form-control hidden" name=""></input>';//项目ID
    //    NewRow.insertCell().innerHTML = '<input class="form-control" name="ProductCode"></input>';//项目号
    //    NewRow.insertCell().innerHTML = '<input class="form-control" name="Concentration"></input>';//浓度
    //    NewRow.insertCell().innerHTML = '<input class="form-control" name="Specification"></input>';//规格
    //    NewRow.insertCell().innerHTML = '<input class="form-control" name="CertificateNo"></input>';//证书编号
    //    NewRow.insertCell().innerHTML = '<a data-toggle="modal" data-target="" title="删除" href="javascript:;><span class="glyphicon glyphicon-trash deleteRow"></span></a>';   //操作

    //    $("#" + tableId + " tr:last a .deleteRow").on('click', function () {
    //        var obj = $(this);
    //        obj.parent().parent().parent().remove();

    //        var tab = $('.nav-tabs li.active a');

    //        var proName = tab.text();
    //        var rowCount = Table.rows.length - 1;
    //        $("[name=" + proName + "]").val(rowCount);
    //    })

    //    var proName = tab.text();
    //    var rowCount = Table.rows.length - 1;
    //    $("[name=" + proName + "]").val(rowCount);
    //});


    //修改质控品-添加货号
    $('#modify_lab_item_no').on('click', function () {
        
        var tableId = $(this).parent().next().children("div").next().children("div").children("div").children("table").attr("id");

        var Table = document.getElementById(tableId);   //获取表格

        var columnLength = Table.rows.item(0).cells.length;

        var NewRow = Table.insertRow();         //插入行

        //NewRow.insertCell().innerHTML = '<input class="form-control hidden" name="SpecificationId"></input>';//项目ID
        NewRow.insertCell().innerHTML = '<input class="form-control" name="ProductCode"></input>';//项目号
        NewRow.insertCell().innerHTML = '<input class="form-control" name="Concentration"></input>';//浓度
        NewRow.insertCell().innerHTML = '<input class="form-control" name="Specification"></input>';//规格
        NewRow.insertCell().innerHTML = '<input class="form-control" name="CertificateNo"></input>';//证书编号
        NewRow.insertCell().innerHTML = '<a data-toggle="modal" data-target="" title="删除" href="javascript:;"><span class="glyphicon glyphicon-trash deleteRow"></span></a>';   //操作

        $("#" + tableId + " tr:last a .deleteRow").on('click', function () {
            var obj = $(this);
            obj.parent().parent().parent().remove();
        })
    });


    function ShowImage(ImgName){

        $.ajax({
            type:"post",
            url:"/Admin/Home/ShowImage/" + ImgName,
            datatype: "text",
            success: function (data) {
                return data;
            }
        })

        return new FileReader();
    }


    //表格分页处理
    $(function () {
        var dataLength = 0;

        $.ajax({
            url: "/Admin/Home/ProductsManage/GetLaboratoryQualityControlByName",
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

        var element = $('#lqc_pagination');
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
                return "/Admin/Home/ProductsManage/GetLaboratoryQualityControlByName?page=" + page;
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

                        var table = $("#lqc_table");
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
                                        "class": "enable_labQualityControl",
                                        "title": "启用",
                                        "href": "javascript:;",
                                        "click": function () {//启用/禁用室内质控品
                                            var obj = $(this);
                                            var val = $(this).parent().next().text();
                                            val = val.trim();
                                            $.ajax({
                                                type: "post",
                                                url: "/Admin/Home/ProductsManage/EnableLaboratoryQualityControl",
                                                data: { "id": val },
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
                                        "class": "modify_labQualityControl",
                                        //"data-toggle":"modal",
                                        "title": "修改",
                                        "href": "javascript:;",
                                        "click": function () {//获取待修改室内质控品数据
                                            var id = $(this).parent().next().text().trim();

                                            $.ajax({
                                                type: "post",
                                                url: "/Admin/Home/ProductsManage/GetLaboratoryQualityControlDetail",
                                                data: { "id": id },
                                                async: false,
                                                dataType: "text",
                                                success: function (data) {

                                                    if (data != "null") {
                                                        var jsonObj = JSON.parse(data);

                                                        //{"Analyte":"分析物,asad","Annotation":"备注","CertificateNo":null,"CreateTime":"\/Date(1514883601000+0800)\/","Description":"描述","Feature":"产品特征","Img":"","LaboratoryQualityControlId":"820f6d7d-c085-4a76-834b-57c559f6b842","ModifyTime":"\/Date(1514884141000+0800)\/","ProductName":"质控品名称111","Specifications":[{"CertificateNo":"","Concentration":"","LaboratoryQualityControlId":"820f6d7d-c085-4a76-834b-57c559f6b842","ProductCode":"","Specification":"","SpecificationId":"804d25b4-88a2-4c8d-b2ef-4955dc2b8948"}],"Stability":"稳定性","State":0}

                                                        $("#modify_LaboratoryQualityId").val(jsonObj.LaboratoryQualityControlId);
                                                        $("#modify_ProductName").val(jsonObj.ProductName);
                                                        $("#modify_Description").val(jsonObj.Description);

                                                        //$("#modify_lab_Img").val(jsonObj.Img);
                                                        $("#modify_lab_preview").attr('src', '/Admin/Home/ProductsManage/ShowImage?id=' + jsonObj.Img);
                                                        

                                                        $("#modify_lab_CategoryName").find("option[value='" + jsonObj.CategoryName + "']").attr("selected", true);

                                                        var tableId = "modify_tabs_default";

                                                        var table = document.getElementById(tableId);

                                                        for (var i = 0; i < jsonObj.Specifications.length; ++i) {

                                                            var NewRow = table.insertRow();//插入行

                                                            NewRow.insertCell().innerHTML = '<input class="form-control" name="ProductCode" value="' + jsonObj.Specifications[i].ProductCode + '"></input>';//项目号
                                                            NewRow.insertCell().innerHTML = '<input class="form-control" name="Concentration" value="' + jsonObj.Specifications[i].Concentration + '"></input>';//浓度
                                                            NewRow.insertCell().innerHTML = '<input class="form-control" name="Specification" value="' + jsonObj.Specifications[i].Specification + '"></ input>';//规格
                                                            NewRow.insertCell().innerHTML = '<input class="form-control" name="CertificateNo" value="' + jsonObj.Specifications[i].CertificateNo + '"></input>';//证书编号
                                                            NewRow.insertCell().innerHTML = '<a data-toggle="modal" data-target="" title="删除" href="javascript:;"><span class="glyphicon glyphicon-trash deleteRow"></span></a>';//操作

                                                            $("#" + tableId + " tr:last a .deleteRow").on('click', function () {
                                                                var obj = $(this);
                                                                obj.parent().parent().parent().remove();
                                                            })
                                                        }

                                                        modify_tagAnalyte.setValue(jsonObj.Analyte);
                                                        modify_GroupListStability.setValue(jsonObj.Stability)
                                                        modify_GroupListFeature.setValue(jsonObj.Feature);
                                                        $("#modify_Annotation").val(jsonObj.Annotation);

                                                        $("#modifyLabQualityControlModal").modal('show');

                                                    } else {
                                                        alert("数据加载失败");
                                                    }

                                                }
                                            })
                                        }
                                    })
                                    var a3 = $("<a/>", {
                                        "class": "delete_labQualityControl",
                                        "data-toggle": "modal",
                                        "data-target": "#deleteLabQualityControlModal",
                                        "title": "删除",
                                        "href": "javascript:;",
                                        "click": function () {
                                            var val = $(this).parent().next().text();
                                            val = val.trim();

                                            $('#delete_labControl_id').text(val);
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
                                        "text": jsonObj[i].LaboratoryQualityControlId
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
        $("#lqc_pagination li:first a").trigger("click");
    });
    
})


function clearTable(tableId) {
    $("#" + tableId + " tbody tr:not(:first)").empty("");
}