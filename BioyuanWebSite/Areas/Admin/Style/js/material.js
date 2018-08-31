//标准物质管理
$(function () {

    //禁止点击背景或按ESC键关闭模态框
    $('#addMaterialModel').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $('#addMaterialModel').on('hidden.bs.modal', function (e) {
        clearAddMaterialFormData();
    })

    $('#modifyMateriallModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $('#modifyMateriallModal').on('hidden.bs.modal', function (e) {
        clearModifyMaterialFormData();
    })

    //初始标准物的标签
    /*添加标准物*/
    var add_materialGroupListConcentrantion = new GroupList("addMaterial_Concentrantion");
    add_materialGroupListConcentrantion.initView();

    var add_materialGroupListStability = new GroupList("addMaterial_Stability");
    add_materialGroupListStability.initView();

    var add_materialGroupListFeature = new GroupList("addMaterial_Feature");
    add_materialGroupListFeature.initView();

    var add_Table = new CustomTalbe("addMaterial_MyTable");
    add_Table.initTable();


    /*修改标准物*/
    var modify_materialGroupListConcentrantion = new GroupList("modifyMaterial_Concentrantion");
    modify_materialGroupListConcentrantion.initView();

    var modify_materialGroupListStability = new GroupList("modifyMaterial_Stability");
    modify_materialGroupListStability.initView();

    var modify_materialGroupListFeature = new GroupList("modifyMaterial_Feature");
    modify_materialGroupListFeature.initView();

    var modify_Table = new CustomTalbe("modifyMaterial_MyTable");
    modify_Table.initTable();

    ////定义删除元素事件
    //$('.delete-item').on('click', function () {
    //    var obj = $(this).parent();
    //    obj.remove();
    //})

    //启用标准物质
    $('.enable_material').on('click', function () {
        var obj = $(this);
        var val = $(this).parent().next().text();
        val = val.trim();

        $.ajax({
            type: "post",
            url: "/Admin/Home/ProductsManage/EnableMaterial",
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

    function setModifyMaterialFormData(data) {

        //{
        //    "Annotation":"对，我就是备注",
        //    "CategoryId":"",
        //    "CategoryName":"血筛四项",
        //    "CreateTime":"\/Date(1517905538000+0800)\/",
        //    "Description":"我是标准物，有什么问题吗",
        //    "Feature":"产品特征|产品特征|产品特征",
        //    "Img":"cba8c7b0-7924-4c87-85b7-b80c990effca.jpg",
        //    "MaterialId":"8c0bfa99-be51-44e2-ae3f-40988ce39863",
        //    "ModifyTime":"\/Date(1517905538000+0800)\/",
        //    "ProductName":"标准物名称",
        //    "RecommendedConcentration":"推荐浓度|推荐浓度|推荐浓度",
        //    "Stability":"稳定性|稳定性|稳定性",
        //    "State":1,
        //    "materialProjects":
        //    [
        //        {
        //            "materialSpecifications":
        //            [
        //                {
        //                    "CertificateNo":"11111",
        //                    "MaterialProjectId":"cb8a7ceb-d907-4a7a-a856-cef214037557",
        //                    "ProductCode":"1",
        //                    "Specification":"1",
        //                    "SpecificationId":"0e018cf5-4cf4-4f47-9b8d-fe7fc5b57914",
        //                    "StardardUncertairty":"1"
        //                },
        //                {
        //                    "CertificateNo":"22222",
        //                    "MaterialProjectId":"cb8a7ceb-d907-4a7a-a856-cef214037557",
        //                    "ProductCode":"2",
        //                    "Specification":"2",
        //                    "SpecificationId":"0abbf19f-35d2-4e3d-8a41-6036b49a7ead",
        //                    "StardardUncertairty":"2"
        //                },
        //                {
        //                    "CertificateNo":"33333",
        //                    "MaterialProjectId":"cb8a7ceb-d907-4a7a-a856-cef214037557",
        //                    "ProductCode":"3",
        //                    "Specification":"3",
        //                    "SpecificationId":"146a7f81-503a-46a7-9a68-0e7577251e78",
        //                    "StardardUncertairty":"3"
        //                }
        //            ],
        //            "materialId":"8c0bfa99-be51-44e2-ae3f-40988ce39863",
        //            "materialProjectId":"cb8a7ceb-d907-4a7a-a856-cef214037557",
        //            "materialProjectName":"",
        //            "unit":"111mg\/L"
        //        }
        //    ]
        //}

        var jsonObj = JSON.parse(data);
        $("#modifyMaterial_MaterialId").val(jsonObj.MaterialId);
        $("#modifyMaterial_ProductName").val(jsonObj.ProductName);
        $("#modifyMaterial_Description").val(jsonObj.Description);
        $("#modifyMaterial_Preview").attr('src', '/Admin/Home/ProductsManage/ShowImage?id=' + jsonObj.Img);
        $("#modifyMaterial_CategoryName").find("option[value='" + jsonObj.CategoryName + "']").attr("selected", true);
        modify_materialGroupListConcentrantion.setValue(jsonObj.RecommendedConcentration);

        if (jsonObj.materialProjects.length == 1 && jsonObj.materialProjects[0].materialProjectName == "") {
            
            for (var i = 0; i < jsonObj.materialProjects[0].materialSpecifications.length; ++i) {
                
                var materailSpecification = jsonObj.materialProjects[0].materialSpecifications[i];
                fun_addItemNo1("modifyMaterial", materailSpecification);
            }
        }
        else {
            for (var i = 0; i < jsonObj.materialProjects.length; ++i) {

                fun_addProject1("modifyMaterial", jsonObj.materialProjects[i].materialProjectName, jsonObj.materialProjects[i].unit);

                for (var j = 0; j < jsonObj.materialProjects[i].materialSpecifications.length; ++j) {

                    var materailSpecification = jsonObj.materialProjects[i].materialSpecifications[j];
                    fun_addItemNo1("modifyMaterial", materailSpecification);
                }
            }
        }
        
        modify_materialGroupListStability.setValue(jsonObj.Stability);
        modify_materialGroupListFeature.setValue(jsonObj.Feature);
        $("#modifyMaterial_Annotation").val(jsonObj.Annotation);
        
    }

    //清除添加标物的表单数据
    function clearAddMaterialFormData() {
        $("#addMaterial_ProductName").val("");
        $("#addMaterial_Description").val("");
        $("#addMaterial_Preview").attr('src', '');
        //$("#addMaterial_CategoryName").find("option[value='" + "--请选择专业--" + "']").attr("selected", true);
        add_materialGroupListConcentrantion.setValue("");
        clearTable("addMaterial_MyTable");
        $("#addMaterial_Stability").val("");
        $("#addMaterial_Feature").val("");
        $("#addMaterial_Annotation").val("");
    }

    //清除修改标物的表单数据
    function clearModifyMaterialFormData() {
        $("#modifyMaterial_MaterialId").val("");
        $("#modifyMaterial_ProductName").val("");
        $("#modifyMaterial_Description").val("");
        $("#modifyMaterial_Preview").attr('src', '');
        //$("#modifyMaterial_CategoryName").find("option[value='" + "--请选择专业--" + "']").attr("selected", true);
        $("#modifyMaterial_Concentrantion").val("");
        clearTable("modifyMaterial_MyTable");
        modify_materialGroupListStability.setValue("");
        modify_materialGroupListFeature.setValue("");
        $("#modifyMaterial_Annotation").val("");
    }

    //获取待修改室内质控品数据
    $('.modify_material').on('click', function () {

        var id = $(this).parent().next().text().trim();

        $.ajax({
            type: "post",
            url: "/Admin/Home/ProductsManage/GetMaterialDetail",
            data: { "id": id },
            async: false,
            dataType: "text",
            success: function (data) {

                if (data != "null") {

                    setModifyMaterialFormData(data);

                    $("#modifyMateriallModal").modal('show');

                } else {
                    alert("数据加载失败");
                }

            }
        })

    })



    //删除标准物质弹框并设置ID
    $('.delete_material').on('click', function () {
        var val = $(this).parent().next().text();

        val = val.trim();

        $('#delete_material_id').text(val);
    })
    //删除标准物质
    $('#delete_material_btn').on('click', function () {

        var id = $('#delete_material_id').text();

        $.ajax({
            type: "post",
            url: "DeleteMaterial",
            data: { "MaterialId": id },
            datatype: "text",
            success: function (data) {
                window.location.href = "/Admin/Home/Material";
            }
        })

    })

    //添加标准物质，定义修改图片的事件
    $('#addMaterial_Img').on('change', function () {

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
                $("#addMaterial_Preview").attr("src", e.target.result);
            }
            reader.readAsDataURL(file);
        }

    })

    //修改标准物质，定义修改图片的事件
    $('#modifyMaterial_Img').on('change', function () {

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
                $("#modifyMaterial_Preview").attr("src", e.target.result);
            }
            reader.readAsDataURL(file);
        }

    })


    $(".deleteRow").on('click', function () { //页面加载完成时为所有的删控件添加事件
        var obj = $(this);
        obj.parent().parent().parent().remove();

        var tab = $('.nav-tabs li.active a');
        var rowCountobj = $("[name=" + tab.text() + "]");
        var rowCount = Table.rows.length - 1;
        rowCountobj.text(rowCount);
    })

    //添加货号函数
    function fun_addItemNo(keyWord) {
        var tab = $('#' + keyWord + '_MyTable ul.nav-tabs li.active a');

        var tableId = tab.attr('href');
        
        if (tableId == undefined) {
            tableId = keyWord + "_MyTable_defaultTable";
        } else {
            if (tableId.indexOf("#") >= 0) {
                tableId = tableId.replace("#", "");
            }
        }

        tableId = tableId + "_table";

        var Table = document.getElementById(tableId);   //取得自定义的表对象

        var columnLength = Table.rows.item(0).cells.length;

        var NewRow = Table.insertRow();//添加行

        var proObj = $('#table-project');

        NewRow.insertCell().innerHTML = '<input class="form-control" name="ProductCode"></input>';//货号
        NewRow.insertCell().innerHTML = '<input class="form-control" name="StandardUncertairty"></input>';//标准及不确定度
        NewRow.insertCell().innerHTML = '<input class="form-control" name="Specification"></input>';//规格
        NewRow.insertCell().innerHTML = '<input class="form-control" name="CertificateNo"></input>';//标物物质编号
        NewRow.insertCell().innerHTML = '<a data-toggle="modal" data-target="" title="删除" href="javascript:;"><span class="glyphicon glyphicon-trash deleteRow"></span></a>';   //操作

        $("#" + tableId + " tr:last a .deleteRow").on('click', function () { //为最新的一行添加事件
            var obj = $(this);
            obj.parent().parent().parent().remove();

            var tab = $('#' + keyWord + '_MyTable ul.nav-tabs li.active a');

            var proName = tab.text();
            var rowCount = Table.rows.length - 1;
            $("[name=" + proName + "]").val(rowCount);
        })

        var proName = tab.text();
        var rowCount = Table.rows.length - 1;
        $("[name=" + proName + "]").val(rowCount);
    }


    function fun_addItemNo1(keyWord, specification) {

        var tab = $('#' + keyWord + '_MyTable ul.nav-tabs li.active a');

        var tableId = tab.attr('href');
        
        if (tableId == undefined) {
            tableId = keyWord + "_MyTable_defaultTable";
        } else {
            if (tableId.indexOf("#") >= 0) {
                tableId = tableId.replace("#", "");
            }
        }

        tableId = tableId + "_table";

        var Table = document.getElementById(tableId);   //取得自定义的表对象

        var columnLength = Table.rows.item(0).cells.length;

        var NewRow = Table.insertRow();//添加行

        var proObj = $('#table-project');

        NewRow.insertCell().innerHTML = '<input class="form-control" name="ProductCode" value="' + specification.ProductCode + '"></input>';//货号
        NewRow.insertCell().innerHTML = '<input class="form-control" name="StandardUncertairty" value="' + specification.StardardUncertairty + '"></input>';//标准及不确定度
        NewRow.insertCell().innerHTML = '<input class="form-control" name="Specification" value="' + specification.Specification + '"></input>';//规格
        NewRow.insertCell().innerHTML = '<input class="form-control" name="CertificateNo" value="' + specification.CertificateNo + '"></input>';//标物物质编号
        NewRow.insertCell().innerHTML = '<a data-toggle="modal" data-target="" title="删除" href="javascript:;"><span class="glyphicon glyphicon-trash deleteRow"></span></a>';   //操作
        
        
        $("#" + tableId + " tr:last a .deleteRow").on('click', function () { //为最新的一行添加事件
            var obj = $(this);
            obj.parent().parent().parent().remove();

            var tab = $('#' + keyWord + '_MyTable ul.nav-tabs li.active a');

            var proName = tab.text();
            var rowCount = Table.rows.length - 1;
            $("[name=" + proName + "]").val(rowCount);
        })
        
        if (tab != undefined) {
            var proName = tab.text();
            var rowCount = Table.rows.length - 1;
            
            //$("[name=" + proName + "]").val(rowCount);    //这句执行会失败
        }
    }


    function fun_addProject(keyWord) {

        var tabs = $("ul.nav-tabs");

        var tabUl = $("ul.table-rowcount");

        var count = $(".nav-tabs li").length;

        var inputObj = $("#" + keyWord + "_addProject").next();
        
        var titleName = inputObj.val().trim();
        
        if (titleName != null && titleName != "") {

            var id =  keyWord + "_MyTable" + count;
            tabs.addTabs({
                "tabContentId": keyWord + "_MyTable",
                "id": id,
                "title": titleName,
                "content": '<table id="' + id + '" class="table table-bordered" style="margin-left:0px;">' +
                            '<tr>' +
                            '<td>货号</td>' +
                            '<td style="width:350px;"><span class="col-sm-6">标准及不确定度</span><input class="col-sm-6" name="Unit" placeholder="单位" style="width:100px;" /></td>' +
                            '<td>规格</td>' +
                            '<td>标物物质编号</td>' +
                            '<td>操作</td>' +
                            '</tr>' +
                            '</table>'
            });

            tabUl.addTableCountTab({
                "name": titleName
            });

        } else {
            alert("项目名不能为空！");
        }

        var liCount = $(".nav-tabs li").length;

        if (liCount > 0) {
            $("#" + keyWord + "_MyTable ul.nav-tabs").css("display", "block");
            var tableid = $("#" + keyWord + "_MyTable_defaultTable").children().first().attr("id");
            clearTable(tableid);

        } else {
            $("#" + keyWord + "_MyTable ul.nav-tabs").css("display", "none");
        }

        inputObj.val("");
        inputObj.focus();
    }

    function fun_addProject1(keyWord, proName, unit) {

        var tabs = $("ul.nav-tabs");

        var tabUl = $("ul.table-rowcount");

        var count = $(".nav-tabs li").length;

        var inputObj = $("#" + keyWord + "_addProject").next();

        var titleName = proName;

        if (titleName != null && titleName != "") {

            var id = keyWord + "_MyTable" + count;
            tabs.addTabs({
                "tabContentId": keyWord + "_MyTable",
                "id": id,
                "title": titleName,
                "content": '<table id="' + id + '" class="table table-bordered" style="margin-left:0px;">' +
                            '<tr>' +
                            '<td>货号</td>' +
                            '<td style="width:350px;"><span class="col-sm-6">标准及不确定度</span><input class="col-sm-6" name="Unit" placeholder="单位" style="width:100px;" value="'+ unit +'"/></td>' +
                            '<td>规格</td>' +
                            '<td>标物物质编号</td>' +
                            '<td>操作</td>' +
                            '</tr>' +
                            '</table>'
            });

            tabUl.addTableCountTab({
                "name": titleName
            });

        } else {
            alert("项目名不能为空！");
        }

        var liCount = $(".nav-tabs li").length;

        if (liCount > 0) {
            $("#" + keyWord + "_MyTable ul.nav-tabs").css("display", "block");
            var tableid = $("#" + keyWord + "_MyTable_defaultTable").children().first().attr("id");
            clearTable(tableid);

        } else {
            $("#" + keyWord + "_MyTable ul.nav-tabs").css("display", "none");
        }

        inputObj.val("");
        inputObj.focus();
    }


    //添加货号
    $('#addMaterial_addItemNo').on('click', function () {
        fun_addItemNo("addMaterial");
    });
    //添加项目
    $("#addMaterial_addProject").on("click", function () {
        fun_addProject("addMaterial");
    });


    //添加货号
    $('#modifyMaterial_addItemNo').on('click', function () {
        fun_addItemNo("modifyMaterial");
    });
    //添加项目
    $("#modifyMaterial_addProject").on("click", function () {
        fun_addProject("modifyMaterial");
    });


    Array.prototype.remove = function (dx) {
        if (isNaN(dx) || dx > this.length) { return false; }
        for (var i = 0, n = 0; i < this.length; i++) {
            if (this[i] != this[dx]) {
                this[n++] = this[i]
            }
        }
        this.length -= 1
    }


    ////添加标准物表单验证
    //$("#add-material-form").validate();

    ////产品名称
    //$("#addMaterial_ProductName").rules('add', {
    //    required: true,
    //    minlength: 1,
    //    maxlength: 40,
    //    messages: {
    //        required: '请输入产品名称！',
    //        minlength: '帐号不能小于{0}位！',
    //        maxlength: '帐号不能小于{0}位！'
    //    }
    //});



    ////修改标准物表单验证
    //$("#modify-material-form").validate();

    ////产品名称
    //$("#modifyMaterial_ProductName").rules('add', {
    //    required: true,
    //    minlength: 1,
    //    maxlength: 40,
    //    messages: {
    //        required: '请输入产品名称！',
    //        minlength: '帐号不能小于{0}位！',
    //        maxlength: '帐号不能小于{0}位！'
    //    }
    //});




    //显示数据列表、分页处理
    $(function () {

        var dataLength = 0;

        $.ajax({
            url: "/Admin/Home/ProductsManage/GetMaterialsByName",
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

        var element = $('#m_pagination');
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
                return "/Admin/Home/ProductsManage/GetMaterialsByName?page=" + page;
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
                        console.log(data + "                   currentPage=" + page);
                        $(data).each(function () {//更新列表显示

                        });

                        var table = $("#m_table");
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
                                } else if (j == 1) {//标准物名称
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": jsonObj[i].ProductName
                                    })

                                    td.append(span);
                                } else if (j == 2) {//样品图片
                                    td = $("<td/>", {
                                    })

                                    var img = $("<img/>", {
                                        "src": '@Url.Action("ShowImage", "Home", new { id = ' + jsonObj[i].Img + '})',
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
                                        "class": "enable_material",
                                        "title": "启用",
                                        "href": "javascript:;",
                                        "click": function () {//启用/禁用室内质控品
                                            var obj = $(this);
                                            var val = $(this).parent().next().text();
                                            val = val.trim();

                                            $.ajax({
                                                type: "post",
                                                url: "/Admin/Home/ProductsManage/EnableMaterial",
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
                                        "class": "modify_material",
                                        //"data-toggle":"modal",
                                        "title": "修改",
                                        "href": "javascript:;",
                                        "click": function () {//获取待修改室内质控品数据
                                            var id = $(this).parent().next().text().trim();

                                            $.ajax({
                                                type: "post",
                                                url: "/Admin/Home/ProductsManage/GetMaterialDetail",
                                                data: { "id": id },
                                                async: false,
                                                dataType: "text",
                                                success: function (data) {

                                                    if (data != "null") {

                                                        setModifyMaterialFormData(data);

                                                        $("#modifyMateriallModal").modal('show');

                                                    } else {
                                                        alert("数据加载失败");
                                                    }

                                                }
                                            })
                                        }
                                    })
                                    var a3 = $("<a/>", {
                                        "class": "delete_material",
                                        "data-toggle": "modal",
                                        "data-target": "#deleteMaterialModal",
                                        "title": "删除",
                                        "href": "javascript:;",
                                        "click": function () {
                                            var val = $(this).parent().next().text();

                                            val = val.trim();

                                            $('#delete_material_id').text(val);
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
                                        "text": jsonObj[i].MaterialId
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
        $("#m_pagination li:first a").trigger("click");
    });

    
});

function clearTable(tableId) {
    $("#" + tableId + " tbody tr:not(:first)").empty("");
}

