//生成随机数
function Random(i) {
    if (!i) {
        i = 1;
    }
    return parseInt(Math.random() * i);
}

//点击上传图标，弹出选择文件框
$(".file_icon").click(function () {
    $(this).parent().find(".file_input").click();
    $(this).parent().removeClass(SOGWarming);
})

//完成文件选择，ajax提交文件到服务器
$(".file_input").change(function () {
    var filepath = $(this).val();
    var extStart = filepath.lastIndexOf(".");
    var current_ext = filepath.substring(extStart, filepath.length).toLowerCase();
    var parent = $(this).parent();
    parent.find(".file_url").text(this.files[0].name);
    CheckOneControl(this);
    UploadOneFile(
        this.files[0],
        function (res) {
            $(".file_precent").remove();
            var json_res = $.parseJSON(res);
            if (json_res.status == "1") {
                parent.attr("value", json_res.message);
            }
        },
    function (present) {
        $(".file_precent").remove();
        parent.append("<span class='file_precent'>" + present + "</a>");
    }
    );

    parent.find(".file_clear").removeClass("hide").click(function () {
        ClearOneControl(parent);
    });
})

//点击文件链接,下载文件
$(".file_url").click(function () {
    var parent = $(this).parent();
    Download(parent.attr("value"));
})

//点击表格下载文件事件
$('.SogContent').on('click', '.down_file', function () {
    var FL_ID = $(this).attr("value");
    Download(FL_ID);
});

//上传文件的核心方法
function UploadOneFile(file, fn_callback, fn_progress) {
    var present = 0;
    var xhr = new XMLHttpRequest();
    var fd = new FormData();
    fd.append("files", file);
    xhr.upload.addEventListener("progress", function (evt) {
        if (fn_progress) {
            if (evt.lengthComputable) {
                present = parseInt((evt.loaded / file.size) * 90);
                if (present == 90) {
                    present += Random(10);
                }
                fn_progress(present);
            }
        }

    }, false);
    xhr.addEventListener("load", function (evt) {
        var data = evt.target.responseText;
        if (fn_callback) {
            fn_callback(data);
        }
    }, false);
    //发送文件和表单自定义参数
    xhr.open("POST", "/sys/File.aspx?action=Upload", true);
    xhr.send(fd);
}

//下载文件的核心方法
function Download(FL_ID) {
    var url = "/sys/File.aspx?action=DownLoad&FL_ID=" + FL_ID;
    var html = "<iframe src='" + url + "' style='display: none;'></iframe>";
    $("body").append(html);
}

//上传控件初始化
function SogFileUploadInit(_this, value) {
    if (value == "" || value == "null" || value == null) {
        $(_this).parent().find(".file_url").text("");
        return;
    }
    var url = "/sys/File.aspx?action=GetOneFile&FL_ID=" + value;
    var json_data = {};
    Ajax(url, json_data, function (json_result) {
        if (json_result.status == 1) {
            $(_this).parent().find(".file_url").text(json_result.items.FL_NAME);
        }
    });
}

