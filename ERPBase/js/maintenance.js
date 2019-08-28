//全局变量
var SOGWarming = "SOGWarming";

$.fn.extend({
    tips: function (msg) {
        var id = "tips";
        if (msg) {
            $(this).mouseover(function () {
                var str_html = '<div id="' + id + '" class="tips" >' + msg + '</div>';
                var e = $(str_html);
                var middle = (this.getBoundingClientRect().top + this.getBoundingClientRect().bottom) / 2
                e.css("left", this.getBoundingClientRect().right + 10);
                e.css("top", middle - 20);
                $("body").append(e);
            });
            $(this).mouseout(function () {
                $("#" + id).remove();
            });
        } else {
            $(this).unbind("mouseover");
            $("#" + id).remove();
        }
    }
});


//依赖项，JQ，jquery.toast.js
//成功提示
function SuccessBox(str_message, fn) {
    var time = 2500;
    $.toast({
        heading: 'Success',
        text: str_message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'success',
        hideAfter: time
    });

    if (fn) {
        window.setTimeout(fn, time);
    }
}

//失败提示
function ErrorBox(str_message, fn) {
    var time = 2500;
    $.toast({
        heading: 'Error',
        text: str_message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'error',
        hideAfter: time
    });

    if (fn) {
        window.setTimeout(fn, time);
    }
}

//检查控件数组
function CheckControl(arr_cto) {
    var IsAllTrue = true;
    arr_cto.each(function () {
        if (!CheckOneControl(this)) {
            IsAllTrue = false;
        }
    });
    return IsAllTrue;
}

//检查一个控件
function CheckOneControl(_this) {
    var rule = $(_this).attr("rule");
    if (!IsNullOrEmpty(rule)) {
        re_rule = new RegExp(eval(rule))
        var value = GetOneControlValue(_this);
        if (!re_rule.test(value)) {
            $(_this).addClass(SOGWarming);
            $(_this).tips($(_this).attr("rule_desc"));
            return false;
        } else {
            $(_this).removeClass(SOGWarming);
            $(_this).tips();
            return true;
        }
    } else {
        return true;
    }
}

//获取控件数组的值
function GetControlValue(arr_control, json) {
    arr_control.each(function () {
        var target = $(this).attr("target");
        var value = $(this).attr("value");
        if (IsNullOrEmpty(value)) {
            value = "";
        }
        if (target) {
            json[target] = value;
        }
    });
}

//获取一个控件的值
function GetOneControlValue(_this) {
    var value = $(_this).val() ? $(_this).val() : $(_this).attr("value");
    if (value == undefined) {
        value = "";
    }
    return value
}

//获取控件数组的值2
function GetControlFormValue(arr_control, json) {
    arr_control.each(function () {
        var target = $(this).attr("target");
        var value = GetOneControlValue(this);
        if (IsNullOrEmpty(value)) {
            value = "";
        }
        if (target) {
            json[target] = value;
        }
    });
}

//设置一个控件的值
function SetOneControlValue(_this, value) {
    if ($(_this).hasClass("SogFileUpload")) {
        $(_this).attr("value", value);
        SogFileUploadInit(_this, value);
    } else if ($(_this).hasClass("SogFolderUpload")) {
        $(_this).attr("value", value);
        SogFolderUploadInit(_this, value);
    }
    else {
        $(_this).val(value);
    }
}

//清除控件数组
function ClearControl(arr_cto) {
    arr_cto.each(function () {
        ClearOneControl(this)
    })
}

//清除一个控件
function ClearOneControl(cto) {
    cto = $(cto);
    //单文件上传
    if (cto.hasClass("SogFileUpload")) {
        cto.attr("value", "");
        cto.find(".file_url").html("");
        cto.find(".file_input").val("");
        cto.find(".file_clear").addClass("hide");
    }

    //文件夹上传
    if (cto.hasClass("SogFolderUpload")) {
        cto.attr("value", "");
        cto.find(".folder_url").html("");
        cto.find(".folder_input").val("");
        cto.find(".folder_clear").addClass("hide");
    }

    //其他非特殊控件
    $(cto).val("");
}


//异步请求方法
function Ajax(url, post_data, callback) {
    console.log(post_data);
    var async = false;
    if (callback) {
        async = true
    }
    var json_result = null;
    $.ajax({
        type: 'post',
        url: url,
        async: async,
        data: post_data,
        datatype: "json",
        success: function (result) {
            json_result = $.parseJSON(result);
            if (callback) {
                callback(json_result);
            }
        },
        error: function (result) {
            json_result = $.parseJSON(result);
            if (callback) {
                callback(json_result);
            }
        }
    });
    return json_result;
}

//新增方法
function Add() {
    var target = "div_add";
    var json = {};

    var arr_cto = $("#" + target + " .SogControl");
    var IsAllTrue = CheckControl(arr_cto);
    if (!IsAllTrue)//检查控件全部通过
    {
        return false;
    }
    //上传文件夹
    UploadFolder($("#" + target + " .SogFolderUpload"));

    //获取控件的值组成json
    GetControlFormValue(arr_cto, json);

    var url = "/sys/AjaxData.aspx?action=Add";
    ShowLoading();
    window.setTimeout(function () {
        HideUI(target); //关闭弹窗 
    }, 1000);
    Ajax(url, json, function (json) {
        if (json.status == 1) {
            Search();//替换表格
            HideUI(target); //关闭弹窗 
            //arr_cto.val("")
            ClearControl(arr_cto)//清空数据
            SuccessBox(json.message);//提示用户
        } else {
            ErrorBox(json.message);
        }
        HideLoading();
    });

}

//编辑方法
function Edit() {
    var target = "div_edit";
    var json = {};

    var arr_cto = $("#" + target + " .SogControl");
    var IsAllTrue = CheckControl(arr_cto);
    if (!IsAllTrue)//检查控件全部通过
    {
        return false;
    }

    //上传文件夹
    UploadFolder($("#" + target + " .SogFolderUpload"));

    //获取控件的值组成json
    GetControlFormValue(arr_cto, json);
    json["data_id"] = window.data_id;
    console.log(json);

    var url = "/sys/AjaxData.aspx?action=Edit";
    ShowLoading();
    window.setTimeout(function () {
        HideUI(target); //关闭弹窗 
    }, 1000);
    Ajax(url, json, function (json) {
        HideLoading();
        if (json.status == 1) {
            Search();//替换表格
            HideUI(target); //关闭弹窗 
            ClearControl(arr_cto)//清空数据
            SuccessBox(json.message);//提示用户
        } else {
            ErrorBox(json.message);
        }
    });
}

//删除方法
function Delete() {
    if (confirm('删除后不可恢复,继续吗？')) {
        var ids = new Array();
        $(".check_one").each(function () {
            if (this.checked) {
                ids.push($(this).attr("data_id"));
            }
        });
        if (ids.length == 0) {
            alert("请选择你需要删除的内容!");
        } else {
            var json = {};
            var str_ids = ids.join(",");
            json["data_id"] = str_ids;
            var url = "/sys/AjaxData.aspx?action=Delete";
            ShowLoading("正在删除");

            Ajax(url, json, function (res) {
                HideLoading();
                if (res.status == 1) {
                    Search();//替换表格
                    SuccessBox(res.message);
                } else {
                    ErrorBox(res.message);
                }
            });
        }
    }
}

//查询数据
function Search(IsPageing) {
    var json = {};

    //导航
    GetControlValue($(".SogNavigate"), json);


    //条件查询
    GetControlValue($(".condition_item"), json);


    $(".SogText").each(function () {
        var target = $(this).attr("target");
        var value = $(this).val();
        if (!value) {
            value = "";
        }
        if (target) {
            json[target] = value;
        }
    });


    if (IsPageing) {
        //分页
        GetControlValue($(".SogPages"), json);
    }


    //功能ID
    var SO_ID = $("#SO_ID").val();
    json["SO_ID"] = SO_ID;

    console.log(json);

    function GetHtml(url, json) {
        var str_html = "";
        $.ajax({
            type: "POST",
            data: json,
            async: false,
            url: url,
            success: function (result) {
                str_html = result;
            }
        });
        return str_html;
    }

    var str_html = GetHtml("/sys/AjaxData.aspx?action=GetTable", json);
    $(".SogContent").html(str_html);
}

//页面加载完成后，加载列表
$(function () {
    Search();
});

//筛选条件缩放事件
$(".handler .small_btn,.close").click(function () {
    $(".SogCondition .open").slideToggle();
    $(".SogCondition .close").slideToggle();
    if ($(this).hasClass("fa-chevron-down")) {
        $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
    } else {
        $(this).addClass("fa-chevron-down").removeClass("fa-chevron-up");
    }
});

//头部导航条件事件
$(".SogNavigate a").click(function () {
    $(this).addClass("active").siblings().removeClass("active");
    $(this).parent().attr("value", $(this).attr("value"));
    Search();
});

//筛选条件点击事件
$(".condition_item a").click(function () {
    $(this).addClass("active").siblings().removeClass("active");
    $(this).parent().attr("value", $(this).attr("value"));
    var id = $(this).parent().attr("id");
    var text = $(this).text();
    $("." + id).text(text);
    Search();
});

//转换分页点击事件
$('.SogContent').on('click', '.SogPages a', function () {
    $(this).addClass("active").siblings().removeClass("active");
    $(this).parent().attr("value", $(this).attr("value"));
    Search(true);
});

//表格全选事件
$('.SogContent').on('click', '.check_all', function () {
    $(".check_one").prop("checked", this.checked);
});

//右上角新增按钮事件
$(".add_botton").click(function () {
    ShowUI("div_add");
});

//点击编辑按钮事件
$('.SogContent').on('click', '.Iedit', function () {
    //初始化控件
    var target_control = "div_edit";
    var arr_cto = $("#" + target_control + " .SogControl");
    arr_cto.val("").removeClass(SOGWarming);

    //查询数据,加入控件
    var url = "/sys/AjaxData.aspx?action=GetOne";
    Ajax(url, { "data_id": $(this).attr("data_id") }, function (json) {
        if (json.status == "1") {
            arr_cto.each(function () {
                var one_row = json.items[0]
                var target = $(this).attr("target");
                SetOneControlValue(this, one_row[target])
            });
            //弹出窗口
            ShowUI(target_control);
        } else {
            ErrorBox(json.message);
        }
    });
    //当前编辑数据的主键
    window.data_id = $(this).attr("data_id");
});


//查询按钮事件
$("#btn_search").click(function () {
    Search();
});

//新增时保存按钮事件
$("#btn_add_save").click(function () {
    Add();
});

//编辑时保存按钮事件
$("#btn_edit_save").click(function () {
    Edit();
});

//删除按钮
$("#btn_delete").click(function () {
    Delete();
});

//鼠标离开检查表单事件
$(":input").blur(function () {
    var rule = $(this).attr("rule");
    var type = $(this).attr("type");
    if (!IsNullOrEmpty(rule) && type != "file") {
        re_rule = new RegExp(eval(rule))
        if (!re_rule.test($(this).val())) {
            $(this).addClass(SOGWarming);
            $(this).tips($(this).attr("rule_desc"));
        } else {
            $(this).removeClass(SOGWarming);
            $(this).tips();
            return true;
        }
    } else {
        return true;
    }
});
$(":input").click(function () {
    var type = $(this).attr("type");
    if (type != "file") {
        $(this).removeClass(SOGWarming);
        $(this).tips();
    }
});



/*图片文件预览 开始*/
function delete_tips() {
    var id = "tips_image";
    $("#" + id).remove();
}

function hide_tips() {
    var id = "tips_image";
    $("#" + id).animate({ opacity: "0" });
}

function show_tips(image) {
    if (image) {
        var id = "tips_image";
        delete_tips();
        var img = '<img src="' + image + '" style="width: 100%; height: 100%; border-radius: 5px;" />';
        var str_html = '<div id="' + id + '" class="item_tips right_tips" >' + img + '</div>';
        var e = $(str_html);
        var middle = (this.getBoundingClientRect().top + this.getBoundingClientRect().bottom) / 2
        e.css("left", this.getBoundingClientRect().left - 310);
        $("body").append(e);
        window.setTimeout(function () {
            console.log("e.height()=" + (e.height()));
            e.css("top", middle - (e.height() / 2) - 9);
        }, 20);
    }
}

$(function () {
    $("body").on("mouseenter", ".progress_item,.down_file", function (e) {
        var url = "/sys/File.aspx?action=PreViewFile";
        var post_data = {};
        post_data["FL_ID"] = $(this).attr("value");
        var data = Ajax(url, post_data);
        if (data.status == "1") {
            show_tips.call(this, data.message);
        }
        e.stopPropagation();
    });

    $("body").on("mouseleave", ".progress_item,.down_file", function (e) {
        hide_tips();
        e.stopPropagation();
    });
});
/*图片文件预览 结束*/

/*日期控件 开始*/
$(".SogDate").each(function () {
    var id = $(this).attr("id");
    zaneDate({
        elem: '#' + id,
        type: 'day',
        format: 'yyyy-MM-dd',
        //done: function (d) {
        //    console.log(d, 'done')
        //}
    })
});

$(".SogDateTime").each(function () {
    var id = $(this).attr("id");
    zaneDate({
        elem: '#' + id,
        format: 'yyyy-MM-dd HH:mm',
        showtime: true,
        showsecond: false,
        //done: function (d) {
        //    console.log(d, 'done')
        //}
    })
});
/*日期控件 结束*/

