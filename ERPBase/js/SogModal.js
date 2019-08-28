//显示UI
function ShowUI(control_id) {
    //1显示遮罩层
    Cover(true);

    //2动画显示目标
    $("#" + control_id).css("display", "inline-block");
    $("#" + control_id).css("margin-left", "-300px;");
    var top = 30 + $(document).scrollTop();//需要计算页面滚动后的情况
    $("#" + control_id).animate({ marginTop: top + "px" }, 150);

    //自动监控关闭样式
    $(".CoverClose").click(function () {
        HideUI(control_id);
    })

    //初始移动的操作
    var Is_move = false;
    var ori_x = 0;
    var ori_y = 0;
    var last_x = -300;
    var last_y = 30;
    var move_x = 0;
    var move_y = 0;

    $(".modal_title").mousedown(function (e) {
        ori_x = e.clientX;
        ori_y = e.clientY;
        Is_move = true;
    });

    $("body").mouseup(function (e) {
        last_x = last_x + (move_x);
        last_y = last_y + (move_y);
        Is_move = false;
    })

    $("body").mousemove(function (e) {
        if (Is_move) {
            move_x = e.clientX - ori_x;
            move_y = e.clientY - ori_y
            var x = last_x + (move_x);
            var y = last_y + (move_y);
            $("#" + control_id).css("margin-left", x);
            $("#" + control_id).css("margin-top", y);
        }
    });
}

//隐藏UI
function HideUI(control_id) {
    //1动画显示目标
    $("#" + control_id).animate({ marginTop: "-700px" }, 150);
    window.setTimeout(function () {
        //2删除遮罩层,隐藏目标
        $("#" + control_id).css("display", "none");
        Cover(false);
    }, 150);
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
    e.css("padding", "30px 50px");
    e.css("font-size", "25px");
    e.css("border-radius", "10px");
    e.css("opacity", "0");
    $("body").append(e);
    window.setTimeout(function () {
        e.animate({ "opacity": "1" });
    }, 500);
}

function HideLoading() {
    CoverLoading(false);
    var id = "message_loading";
    $("#" + id).remove();
}