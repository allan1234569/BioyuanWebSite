$(function () {
    var addNewsEditor = CKEDITOR.replace("AddNewsTextArea");

    var modifyNewsEditor = CKEDITOR.replace("ModifyNewsTextArea");

    $('#add_news_form')
    .bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            title: {
                validators: {
                    notEmpty: {
                        message: '标题不能为空'
                    }
                }
            },
            author: {
                validators: {
                    notEmpty: {
                        message: '作者不能为空'
                    }
                }
            },
            keyword: {
                validators: {
                    notEmpty: {
                        message: '关键词不能为空'
                    }
                }
            },
            content: {
                validators: {
                    notEmpty: {
                        message: '新闻内容不能为空'
                    }
                }
            }
        }
    })

    addNewsEditor.on('change', function () {
        
    })


    //发布新闻
    $('.release_news_btn').on('click', function () {
        var obj = $(this);
        var val = $(this).parent().next().text();
        val = val.trim();
        
        $.ajax({
            type: "post",
            url: "/Admin/Home/NewsManage/EnableNews",
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


    //删除新闻弹框并设置ID
    $('.delete_news').on('click', function () {
        var val = $(this).parent().next().text();

        val = val.trim();

        $('#delete_news_id').text(val);
    })
    //删除新闻
    $('#delete_news_btn').on('click', function () {

        var id = $('#delete_news_id').text();

        $.ajax({
            type: "post",
            url: "DeleteNews",
            data: { "id": id },
            datatype: "text",
            success: function (data) {
                window.location.href = "/Admin/Home/News";
            }
        })

    })


    $('#addNewsModel').on('hidden.bs.modal', function (e) {
        $("#addNews_title").val("");
        $("#addNews_author").val("");
        $("#addNews_keyword").val("");
        addNewsEditor.setData("");
        $("#addNewsy_remark").val("");
    })

    $('#modifyNewsModel').on('hidden.bs.modal', function (e) {
        $("#modifyNews_title").val("");
        $("#modifyNews_author").val("");
        $("#modifyNews_keyword").val("");
        modifyNewsEditor.setData("");
        $("#modifyNewsy_remark").val("");
    })


    //$.ajax({
    //    type: "post",
    //    url: "/Admin/Home/NewsCategoryManage/GetAllNewsCategorys",
    //    data: { "id": val }, //以键/值对的形式
    //    async: false,
    //    dataType: "text",
    //    success: function (data) {
    //        if (data == "1") {
    //            obj.parent().prev().find("span").text("启用");
    //        } else if (data == "0") {
    //            obj.parent().prev().find("span").text("禁用");
    //        }
    //    },
    //    error: function () {

    //    },
    //    complete: function () {

    //    }
    //});


    var setModifyNewsFormData = function (data) {
        //{"author":"22","content":"<p>22<\/p>\u000d\u000a","dateTime":"\/Date(1522218227000+0800)\/","f_id":"","f_name":"哈哈","id":"beedfb50-cb07-4e0c-ae45-8af5d021c3ff","keyword":"22","remark":"22","state":0,"title":"22"}
        
        var jsonObj = JSON.parse(data);
        $("#modifyNews_title").val(jsonObj.title);
        
        $("#modifyNews_author").val(jsonObj.author);
        $("#modifyNews_keyword").val(jsonObj.keyword);
        
        modifyNewsEditor.setData(jsonObj.content);

        $("#modifyNews_remark").val(jsonObj.remark);
        

    };


    //显示数据列表、分页处理
    $(function () {
        
        var dataLength = 0;

        $.ajax({
            url: "/Admin/Home/NewsManage/GetNewsByTitleName",
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

        var element = $('#news_pagination');
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
                return "/Admin/Home/NewsManage/GetNewsByTitleName?page=" + page;
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
                        
                        //显示列表
                        var table = $("#news_table");
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
                                } else if (j == 1) {//新闻分类
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": jsonObj[i].f_name
                                    })

                                    td.append(span);
                                } else if (j == 2) {//新闻标题
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": jsonObj[i].title
                                    })

                                    td.append(span);
                                    } else if (j == 3) {//作者
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": jsonObj[i].author
                                    })

                                    td.append(span);
                                } else if (j == 4) {//日期
                                    td = $("<td/>", {
                                    })

                                    var span = $("<span/>", {
                                        "text": formatDateBoxFull(jsonObj[i].dateTime)
                                    })

                                    td.append(span);
                                } else if (j == 5) {//状态
                                    td = $("<td/>", {
                                        "class": "td-status"
                                    })

                                    if (jsonObj[i].state == 0) {
                                        var btn = $("<a/>", {
                                            "class": "release_news_btn",
                                            "text": "未发布",
                                            "click": function () {

                                                var obj = $(this);
                                                var val = $(this).parent().next().next().text();

                                                val = val.trim();
                                                $.ajax({
                                                    type: "post",
                                                    url: "/Admin/Home/NewsManage/ReleaseNews",
                                                    data: { "id": val }, //以键/值对的形式
                                                    async: false,
                                                    dataType: "text",
                                                    success: function (data) {
                                                        if (data == "1") {
                                                            obj.text("已发布");
                                                            obj.removeAttr('href');//去掉a标签中的href属性
                                                            obj.removeAttr('onclick');//去掉a标签中的onclick事件
                                                            obj.addClass("released");
                                                        }
                                                    },
                                                    error: function () {

                                                    },
                                                    complete: function () {

                                                    }
                                                });
                                            }
                                        })
                                        td.append(btn);
                                    } else {
                                        var btn = $("<a/>", {
                                            "class": "release_news_btn released",
                                            "text": "已发布"
                                        })
                                        td.append(btn);
                                    }
                                    
                                } else if (j == 6) {//操作
                                    td = $("<td/>", {
                                        "class": "td-manage"
                                    })

                                    var a1 = $("<a/>", {
                                        "class": "modify_news",
                                        "data-toggle":"modal",
                                        "title": "修改",
                                        "href": "javascript:;",
                                        "click": function () {//获取待修改的新闻信息
                                            var id = $(this).parent().next().text().trim();
                                            
                                            $.ajax({
                                                type: "post",
                                                url: "/Admin/Home/NewsManage/GetNewsDetail",
                                                data: { "id": id },
                                                async: false,
                                                dataType: "text",
                                                success: function (data) {
                                                    if (data != "null") {
                                                        setModifyNewsFormData(data);
                                                        $("#modifyNewsModel").modal('show');

                                                    } else {
                                                        alert("数据加载失败");
                                                    }
                                                },
                                     
                                            })
                                        }
                                    })
                                    var a2 = $("<a/>", {
                                        "class": "delete_news",
                                        "data-toggle": "modal",
                                        "data-target": "#deleteNewsModal",
                                        "title": "删除",
                                        "href": "javascript:;",
                                        "click": function () {
                                            var val = $(this).parent().next().text();

                                            val = val.trim();

                                            $('#delete_news_id').text(val);
                                        }
                                    })

                                    var span1 = $("<span/>", {
                                        "class": "glyphicon glyphicon-pencil",
                                        "value": ''
                                    })
                                    var span2 = $("<span/>", {
                                        "class": "glyphicon glyphicon-trash",
                                        "value": ''
                                    })

                                    a1.append(span1);
                                    a2.append(span2);

                                    td.append(a1);
                                    td.append(a2);
                                } else if (j == 7) {
                                    td = $("<td/>", {
                                        "class": "hidden",
                                        "text": jsonObj[i].id
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
        $("#news_pagination li:first a").trigger("click");
    });

})
