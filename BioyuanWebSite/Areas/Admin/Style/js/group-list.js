function GroupList(inputId) {
    
    var obj = new Object();
    if (inputId == null || inputId == "") {
        alert("初始化失败，请检查参数！");
        return;
    }
    obj.inputId = inputId;
    //初始化
    obj = (function (obj) {
        obj.listValue = "";
        obj.isDisable = false;
        obj.id = inputId;
        return obj;
    })(obj);

    //初始化界面
    obj.initView = function () {
        var inputObj = $("#" + this.inputId);
        var inputId = this.inputId;
        var placeholder = inputObj.attr("placeholder");
        
        inputObj.css("display", "none");
        var appendStr = '';
        appendStr += '<div class="itemsContaine" id="' + inputId + '_itemcontaine">';
        appendStr += '<input type="text" class="itemInput form-control col-sm-10" placeholder="' + placeholder + '"/><div class="itemList"></div>';
        appendStr += '</div>';
        inputObj.after(appendStr);
        var itemInput = $("#" + inputId + "_itemcontaine .itemInput");

        if (!this.isDisable) {
            
            $("#" + inputId + "_itemcontaine").attr("ds", "1");
            itemInput.keydown(function (event) {
                if (event.keyCode == 13) {
                    var inputValue = $(this).val();
                    groupListTake.setListItemValue(inputId, inputValue);
                    $(this).val("");
                }
            });

        } else {
            $("#" + inputId + "_itemcontaine").attr("ds", "0");
            itemInput.remove();
        }
        if (this.listValue != null && this.listValue != "") {
            groupListTake.setListItemValue(inputId, this.listValue);
            if (this.isDisable) {
                $("#" + inputId + "_itemcontaine .itemList .delete").remove();
            }
        }

    }

    obj.setValue = function (inputValue) {
        $("#" + this.id + "_itemcontaine .itemList .item-div").remove();
        $("#" + this.id).val("");
        groupListTake.setListItemValue(this.id, inputValue);
    }

    return obj;
}


var groupListTake = {
    
    "setListItemValue": function (inputId, inputValue) {
        if (inputValue == null || inputValue == "") {
            return;
        }
        var itemListContaine = $("#" + inputId + "_itemcontaine .itemList");

        inputValue = inputValue.replace(/，/g, ",");
        var inputValueArray = inputValue.split("|");
        for (var i = 0; i < inputValueArray.length; i++) {
            var valueItem = $.trim(inputValueArray[i]);
            if (valueItem != "") {
                var appendListItemObj = groupListTake.getListItemModel(valueItem);
                itemListContaine.append(appendListItemObj);
            }
        }
        groupListTake.resetListItemValue(inputId);
       
    },
    "resetListItemValue": function (inputId) {
        var tags = $("#" + inputId + "_itemcontaine .itemList .item-div");
        var tagsStr = "";
        for (var i = 0; i < tags.length; i++) {
            tagsStr += tags.eq(i).find("span").text() + "|";
        }
        
        tagsStr = tagsStr.substr(0, tagsStr.length - 1);
        $("#" + inputId).val(tagsStr);
    },
    "getListItemModel": function (valueStr) {

        var item_div = $("<div />", {
            class: "item-div"
        });

        var list_group_item_span = $("<span />", {
            class: "list-group-item",
            text: valueStr,
        });


        var delete_item_span = $("<span />", {
            class: "glyphicon glyphicon-remove delete-item",
            "click": function () {
                var contatieObjId = $(this).parent().parent().parent().parent(".itemsContaine").attr("id");
                var inputId = "";

                if (contatieObjId.indexOf("_itemcontaine") > 0) {
                    inputId = contatieObjId.replace("_itemcontaine", "")
                }

                $(this).parent().parent().remove();

                groupListTake.resetListItemValue(inputId);
            }
        });

        list_group_item_span.dblclick(function () {//注册双击事件,额外注册，在定义对象时定义该事件不生效，应该是个BUG
            var listItemObj = $(this);

            listItemObj.css("display", "none");

            var updateInputObj = $("<input />", {
                type: "text",
                class: "form-control",
                style: "margin-top:5px;",
                value: listItemObj.text()
            })
            updateInputObj.css("height", listItemObj.css("height"));
            updateInputObj.insertAfter(this);
            updateInputObj.focus();

            updateInputObj.blur(function () { //焦点离开事件
                var inputValue = $(this).val();
                if (inputValue != null && inputValue != "") {
                    listItemObj.text(inputValue);
                    listItemObj.append(groupListTake.getDeleteItemModel());
                    listItemObj.css("display", "block");
                } else {
                    listItemObj.remove();
                }
                updateInputObj.remove();
                groupListTake.resetListItemValue(inputId);
            });
			
            updateInputObj.keydown(function (event) {//Enter按键事件

                if (event.keyCode == 13) {
                    var inputValue = $(this).val();
                    if (inputValue != null && inputValue != "") {
                        listItemObj.text(inputValue);
                        listItemObj.append(groupListTake.getDeleteItemModel());
                        listItemObj.css("display", "block");
                    } else {
                        listItemObj.remove();
                    }
                    updateInputObj.remove();
                    groupListTake.resetListItemValue(inputId);
                }
            });
        })
        list_group_item_span.css("display", "block");//显示调用显示块,没有这句无法正常显示
        list_group_item_span.append(delete_item_span);
        item_div.append(list_group_item_span);

        var obj = item_div;

        return obj;
    },
    "getDeleteItemModel": function () {
        var deleteItemObj = $("<span />", {
            class: "glyphicon glyphicon-remove delete-item"
        })

        deleteItemObj.click(function () {//鼠标点击删除事件
            

            var contatieObjId = $(this).parent().parent().parent().parent(".itemsContaine").attr("id");
            var inputId = "";

            if (contatieObjId.indexOf("_itemcontaine") > 0) {
                inputId = contatieObjId.replace("_itemcontaine", "")
            }

            $(this).parent().parent().remove();

            groupListTake.resetListItemValue(inputId);
        })

        return deleteItemObj;
    }
}