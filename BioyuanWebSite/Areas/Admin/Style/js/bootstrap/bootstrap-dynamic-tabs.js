function CustomTalbe(divId) {

    var obj = new Object();
    if (divId == null || divId == "") {
        alert("初始化失败，请检查参数！");
        return;
    }
    
    obj.initTable = function () {
        var divObj = $("#" + divId);

        var contentDiv = tableTake.getTableContentModel(divId);
        var tableDiv = tableTake.getTableModel(divId+"_defaultTable");
        contentDiv.append(tableDiv);

        divObj.append(tableTake.getTabModel());
        divObj.append(contentDiv);
    }

    return obj;
}

var tableTake = {

    "getTabModel": function () {
        var ul = $("<ul />", {
            "class": "nav nav-tabs"
        });

        ul.css("display", "none");

        return ul;
    },
    "getTableContentModel": function (divid) {
        var objDiv = $("<div />", {
            "id": divid + "-tabContent",
            "class":"tab-content"
        })

        return objDiv;
    },
    "getTableModel": function (tableid) {
        
        var div = $("<div />", {
            "id": tableid,
            "class": "tab-pane fade active in",
        });

        var table = $("<table />", {
            "id": tableid + "_table",
            "class": "table table-bordered"
        });
        table.css("margin-left", "0px");

        var tr = $("<tr />");
        
        var td1 = $("<td />", {
            "text": "货号"
        });
        var td2 = $("<td />", {
        });
        td2.css("width", "300px");
        var span = $("<span />", {
            "text": "标准及不确定度"
        });
        var input = $("<input />", {
            "name": "unit",
            "placeholder": "单位"
        });
        input.css("width:20px;");
        input.css("margin-left", "10px");

        td2.append(span);
        td2.append(input);

        var td3 = $("<td />", {
            "text": "规格"
        });
        var td4 = $("<td />", {
            "text": "标物物质编号"
        });
        var td5 = $("<td />", {
            "text": "操作"
        });

        tr.append(td1);
        tr.append(td2);
        tr.append(td3);
        tr.append(td4);
        tr.append(td5);

        table.append(tr);

        div.append(table)

        return div;
    }
}

$.fn.addTabs = function (options) {
    //判断是否已存在指定ID的tab
    if ($("#" + options.id).length > 0) {
        throw "当前ID的Tab已存在．";
    }

    //构建li元素
    var li = $("<li />", {
    });

    //构建a元素
    var a = $("<a />", {
        "href": "#" + options.id,
        "text": options.title,
        "name":"MaterialProjectName",
        "data-toggle": "tab",
        "click": function () {
            $(this).tab("show");
        }
    });

    var span = $("<span />", {
        "class": "glyphicon glyphicon-remove tab-delete",
        "click": function () {
            
            var tableid = $(this).parent().attr("href");
            
            var li = $(this).parent().parent();

            var nextLi = li.siblings().last();
            
            var count = li.siblings().length;

            li.siblings().removeClass("active")
            nextLi.addClass("active");

            var nextTable = $(nextLi.children("a").attr("href"));

            nextTable.siblings().removeClass("active in");
            nextTable.addClass("active in");

            var defaultTable = $(tableid).prev();

            $(tableid).remove();
            li.remove();
            if (count <= 0) {
                defaultTable.addClass("active in");
            }
        }
    })

    a.append(span);
    
    var input = $("<input />", {
        "name": "MaterialProjectName",
        "class":"hidden",
        "value": options.title,
    })

    var input1 = $("<input />", {
        "name":options.title,
        "class": "hidden"
    })

    //合并li和input元素
    li.append(a);
    li.append(input);
    li.append(input1);

    var ul = $(this);
    //合并ul和li元素
    ul.append(li);

    //添加完成显示当前li
    $(li).tab("show");

    //构建table
    var div = tableTake.getTableModel(options.id);

    var container = $("#" + options.tabContentId + "-tabContent");
    container.append(div);
    
    //添加完成后显示div
    $(div).siblings().removeClass("active");
    
}


$.fn.addTableCountTab = function (options) {

    //构建li元素
    var li = $("<li />");

    //构建input元素
    var input = $("<input />", {
        "value": "0",
        "name":options.name
    });

    //合并li和a元素
    li.append(input);

    var ul = $(this);
    //合并ul和li元素
    ul.append(li);

    //添加完成显示当前li
    $(li).tab("show");


}
