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

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//返回app主界面
function BackToApp() {
    let message = { 'id': '1' }
    let u = navigator.userAgent
    let isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1
    if (isAndroid === true) {
        window.android.backClick('1')
    } else {
        window.webkit.messageHandlers.popToRoot.postMessage(message)
    }
}

//遮罩层，显示与隐藏
function Cover(flat) {
    var id = "cover";
    if (flat) {
        var str_html = '<div id="' + id + '"></div>';
        var e = $(str_html);
        e.css("position", "fixed");
        e.css("top", "0");
        e.css("right", "0");
        e.css("bottom", "0");
        e.css("left", "0");
        e.css("z-index", "9");
        e.css("background-color", " #000");
        e.css("opacity", "0");
        e.animate({ "opacity": "0.5" });
        $("body").append(e);
    } else {
        $("#" + id).remove();
    }
}

//显示加载中时的遮罩层
function CoverLoading(flat) {
    var id = "cover_loading";
    if (flat) {
        var str_html = '<div id="' + id + '"></div>';
        var e = $(str_html);
        e.css("position", "fixed");
        e.css("top", "0");
        e.css("right", "0");
        e.css("bottom", "0");
        e.css("left", "0");
        e.css("z-index", "19");
        e.css("background-color", " #000");
        e.css("opacity", "0");
        window.setTimeout(function () {
            e.animate({ "opacity": "0.5" });
        }, 500);
        $("body").append(e);
        return e;
    } else {
        $("#" + id).remove();
    }
}

//显示加载中
function ShowLoading(str_message) {
    var e = CoverLoading(true);

    var id = "message_loading";
    if (!str_message) {
        str_message = "正在加载中..";
    }
    var str_html = '<span id="' + id + '">' + str_message + '</span>';
    var e = $(str_html);

    e.css("z-index", "20");
    e.css("position", "absolute");
    e.css("top", "50%");
    e.css("left", "50%");
    e.css("transform", "translate(-50%,-50%)");
    e.css("background-color", "#fff");
    e.css("color", "#666");
    e.css("padding", "15px 25px");
    e.css("font-size", "20px");
    e.css("border-radius", "5px");
    e.css("opacity", "0");
    $("body").append(e);
    window.setTimeout(function () {
        e.animate({ "opacity": "1" });
    }, 500);
}

//隐藏加载中
function HideLoading() {
    CoverLoading(false);
    var id = "message_loading";
    $("#" + id).remove();
}

//弹出确认框
function ConfirmBox(obj) {
    //obj.message 提示消息
    //obj.title  提示标题
    //obj.success  成功时执行的函数
    //obj.cancle 取消时执行的函数

    var this_control = this;
    if (obj.title == undefined) {
        obj.title = "提示";
    }

    var confirm_box = $('<div class="ConfirmBox"></div>');
    var title = $('<div class="title">' + obj.title + '</div>');
    var message = $('<div class="message">' + obj.message + '</div>');
    var button = $('<div class="button"></div>');
    confirm_box.append(title);
    confirm_box.append(message);
    confirm_box.append(button);

    var btn_cancle = $('<span class="btn_cancle">取消</span>');
    var btn_yes = $('<span class="btn_yes">确实</span>');
    button.append(btn_cancle);
    button.append(btn_yes);

    btn_cancle.click(function () {
        this_control.Hide();
        confirm_box.remove();

        if (obj.cancle) {
            obj.cancle();
        }
    })

    btn_yes.click(function () {
        this_control.Hide();
        confirm_box.remove();
        if (obj.success) {
            obj.success();
        }

    })

    this.Show = function () {
        Cover(true);
        $("body").append(confirm_box);
        confirm_box.removeClass("hide");
    }

    this.Hide = function () {
        Cover(false);
        confirm_box.addClass("hide");
    }
}

//圆形效果
function Round(x, y) {
    var abc = $('<div></div>');
    $("body").append(abc)
    abc.css("position", "fixed");
    abc.css("height", "40px");
    abc.css("width", "40px");
    abc.css("border-radius", "50%");
    abc.css("background", "radial-gradient(circle, #333, #666, #eee)");
    abc.css("opacity", "0.05");
    abc.css("transition", "transform 0.3s, opacity 0.5s");
    abc.css("left", x - 20);
    abc.css("top", y - 20);

    window.setTimeout(function () {
        abc.css("opacity", "0.3");
        abc.css("transform", "scale(1.2, 1.2)");
    }, 10);

    window.setTimeout(function () {
        abc.css("opacity", "0");
        abc.remove();
    }, 400);
}

//兼容IOS的样式[点击是IOS出发css的active]
document.addEventListener("touchstart", function () {
    // do nothing
}, false);

function user_show() {
    $(".iframe_user").css("left", "0px");
}

function list_show() {
    var function_code = getUrlParam("function_code");
    var iframe_list = $('<iframe src="/H5/work_follow/list.aspx?function_code=' + function_code + '" class="iframe_list"></iframe>');
    $("body").append(iframe_list);
    window.setTimeout(function () {
        iframe_list.css("left", "0px");
    }, 100);
}

function user_hide() {
    $(".iframe_user").css("left", "100%");
}

function list_hide() {
    $(".iframe_list").css("left", "100%");
    window.setTimeout(function () {
        $(".iframe_list").remove();
    }, 200);
}

function user_select(userid, user_image_url) {
    if (userid != undefined) {
        $(".person_select").attr("value", userid);
        $(".img_person").attr("src", user_image_url);
    }
}

//提示框
function MessageBox(content) {
    var tip_message = $('<div class="tip_message">' + content + '</div>');
    $("body").append(tip_message);
    tip_message.css("bottom", "50px");
    window.setTimeout(function () {
        tip_message.css("opacity", "0");
        tip_message.remove();
    }, 1500);
}

//空字符串
function IsNullOrEmpty(str_string) {
    if (str_string == '' || str_string == undefined || str_string == null) {
        return true;
    } else {
        return false;
    }
}

//两点间的动画函数
function animateTwoPoint(startPoint, endPoint, fn_callback) {
    var _body = document.body
    var E = document.createElement('div')
    var is_end = false

    //加入元素与C3动画
    E.style.cssText = 'width: 15px; height: 15px; background-color: rgb(23, 187, 103); border-radius: 50%;position: absolute;transition: left 1s ease-in, top 1s cubic-bezier(.26,.84,.51,.98), transform 1s ease-in'
    E.style.left = (startPoint.x - 7.5) + 'px'
    E.style.top = (startPoint.y - 7.5) + 'px'
    _body.appendChild(E)

    //改变元素的位置，自动触发C3动画
    setTimeout(function () {
        E.style.left = endPoint.x + 'px'
        E.style.top = endPoint.y + 'px'
        E.style.transform = 'scale(0.5)'
    }, 20)

    //监听动画完成，执行回调函数
    E.addEventListener('webkitTransitionEnd', function () {
        if (!is_end) {
            _body.removeChild(E)
            if (fn_callback) {
                fn_callback();
            }
            is_end = true
        }
    })
}

//锁定控件
function LockControl(_this, time) {
    $(_this).addClass("disabled");
    if (time == undefined) {
        time = 5000;
    }
    window.setTimeout(function () {
        $(_this).removeClass("disabled")
    }, time);
}

//解锁控件
function OpenControl(_this) {
    $(_this).removeClass("disabled");
}

//检查控件
function CheckControl(_this) {
    var rule = $(_this).attr("rule");
    if (!IsNullOrEmpty(rule)) {
        re_rule = new RegExp(eval(rule))
        if (!re_rule.test($(_this).val())) {
            MessageBox(($(_this).attr("rule_desc")));
            return false;
        } else {
            return true;
        }
    } else {
        return true;
    }
}

//获取控件的值
function GetControlValue(_this) {
    return $(_this).val();
}

//清除控件的值
function ClearControlValue(_this) {
    return $(_this).val("");
}

//点击选择审批人
$(".person_select").click(function () {
    if ($(this).attr("value") == undefined) {
        user_show();
    } else {
        $(this).removeAttr("value");
        var url = '/H5/image/add.jpg';
        $(".img_person").attr("src", url);
    }
});

$(".Iback").click(function (e) {
    Round(e.clientX, e.clientY);
})