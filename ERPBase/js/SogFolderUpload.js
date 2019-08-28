//文件盒子全局变量
var file_progress_box = null;

//获取文件图标
function GetIcon(type) {
    var obj = {};
    obj["doc"] = "fa-file-word-o";
    obj["docx"] = "fa-file-word-o";

    obj["xls"] = "fa-file-excel-o";
    obj["xlsx"] = "fa-file-excel-o";

    obj["ppt"] = "file-powerpoint-o";
    obj["pptx"] = "file-powerpoint-o";

    obj["pdf"] = "fa-file-pdf-o";

    obj["mp4"] = "fa fa-file-video-o";
    obj["avi"] = "fa fa-file-video-o";
    obj["wmv"] = "fa fa-file-video-o";

    obj["mp3"] = "file-audio-o";

    obj["rar"] = "fa-file-archive-o";
    obj["zip"] = "fa-file-archive-o";

    obj["jpg"] = "fa-file-image-o";
    obj["png"] = "fa-file-image-o";
    obj["gif"] = "fa-file-image-o";

    obj["txt"] = "fa-file-text-o";

    var value = obj[type];
    if (value == undefined) {
        value = "fa-file-o"
    }
    return value;
}

//获取文件容量描述
function GetSizeText(size) {
    var KBsize = size / 1024;

    if (KBsize > 1 && KBsize < 1024) {
        return KBsize.toFixed(2) + "KB";
    }

    var MBsize = size / (1024 * 1024);
    if (MBsize > 1 && MBsize < 1024) {
        return MBsize.toFixed(2) + "MB";
    }

    var GBsize = size / (1024 * 1024 * 1024);
    if (GBsize > 1 && GBsize < 1024) {
        return GBsize.toFixed(2) + "GB";
    }

    var TBsize = size / (1024 * 1024 * 1024 * 1024);
    if (TBsize > 1 && TBsize < 1024) {
        return TBsize.toFixed(2) + "TB";
    }
    return "";
}

//获取文件扩展名
function GetFileExtension(filename) {
    var index = filename.lastIndexOf(".");
    var extension = filename.substring(index + 1);
    return extension;
}

//进度条控件obj是一个jq元素
function Progress(obj) {
    var is_first = true;
    this.Control = obj;
    this.Width = 100;
    this.Height = 10;
    this.EmptyColor = "#eee";
    this.FullColor = "#3bb9ef";
    this.Son = undefined;
    this.Show = function (num) {
        if (is_first) {
            this.Control.css("width", this.Width + "px");
            this.Control.css("height", this.Height + "px");
            this.Control.css("background-color", this.EmptyColor);
            this.Control.css("border-radius", (this.Height / 2) + "px");
            this.Control.css("display", "inline-block");
            is_first = false;
        }

        if (this.Son == undefined) {
            var son = $("<div></div>");
            son.css("background-color", this.FullColor);
            son.css("transition", "width 0.3s");
            son.css("height", this.Height + "px");
            son.css("border-radius", (this.Height / 2) + "px");
            this.Control.append(son);
            this.Son = son;
        }
        num = num == undefined ? 0 : num;
        num = num > 100 ? 100 : num;
        num = num < 0 ? 0 : num;
        this.Son.css("width", num + "%");
        if (num == 100) {
            this.Son.css("background-color", "#56c32a");
        }
        return this;
    }
};

//生成进度盒子控件
function ProgressBox(arr, is_remove) {
    //arr(id type,name,size 数组) is_remove 是否存在右上角删除按钮
    var TotalSize = 0;//总容量
    var TotalNumber = arr.length;//总文件数量
    var total_progress = 0;//累计完成容量
    var total_progress_number = 0;//累计完成文件数量

    var file_progress = $('<div class="file_progress"></div>');
    var progress_title = $('<div class="progress_title"></div>');
    var progress_content = $('<div class="progress_content"></div>');
    file_progress.append(progress_title);
    file_progress.append(progress_content);

    var progress_text1 = $('<span class="progress_text">总进度</span>');
    var progress_line_o_total = $('<div></div>');
    var j_total = new Progress(progress_line_o_total);
    j_total.Control.css("margin", "0px 10px");
    j_total.Width = 310;
    j_total.Show(0);

    var t = "0/" + TotalNumber;
    var progress_text2 = $('<span class="progress_text">' + t + '</span>');

    var progress_close = $('<i class="fa fa-minus progress_close" aria-hidden="true"></i>');
    if (is_remove) {
        progress_close = $('<i class="fa fa-times progress_close" aria-hidden="true"></i>');
    }
    progress_title.append(progress_text1);
    progress_title.append(progress_line_o_total);
    progress_title.append(progress_text2);
    progress_title.append(progress_close);

    for (var i = 0; i < arr.length; i++) {
        var item = arr[i];
        var value_html = "";
        if (!IsNullOrEmpty(item.id)) {
            value_html = ' value="' + item.id + '" ';
        }

        var progress_item = $('<div class="progress_item" ' + value_html + ' ></div>');
        var icon = GetIcon(item.type);
        var progress_icon = $('<i class="fa ' + icon + ' progress_icon" aria-hidden="true"></i>');
        var progress_filename = $('<span class="progress_filename">' + item.name + '</span>');
        var progress_line_o = $('<div class="progress_line_o"><div class="progress_line_f"></div></div>');
        var progress_line_o = $('<div></div>');
        var j = new Progress(progress_line_o);
        j.Control.css("margin", "0px 10px");
        j.Width = 110;
        j.Show(0);
        item.progress = j;
        var size_text = GetSizeText(item.size);
        var progress_size = $('<span class="progress_size">' + size_text + '</span>');
        if (!IsNullOrEmpty(item.id)) {
            var value_html = ' value="' + item.id + '" ';
            var file_delete_icon = $('<i class="fa fa-times file_delete_icon" aria-hidden="true" ' + value_html + ' ></i>');
            file_delete_icon.click(function () {
                var FL_ID = $(this).attr("value");
                var json_result = DeleteFile(FL_ID);
                if (json_result.status == "1") {
                    $(this).parent().remove();
                }
            })
        }

        progress_item.append(progress_icon);
        progress_item.append(progress_filename);
        progress_item.append(progress_line_o);
        progress_item.append(progress_size);
        progress_item.append(file_delete_icon);

        progress_item.click(function () {
            var FL_ID = $(this).attr("value");
            if (!IsNullOrEmpty(FL_ID)) {
                Download(FL_ID);
            }
        });

        progress_content.append(progress_item);

        TotalSize += item.size;
    }
    $("body").append(file_progress);

    this.Show = function () {
        file_progress.animate({ bottom: "0px" });
        if (progress_close.hasClass("fa-window-maximize")) {
            progress_close.removeClass("fa-window-maximize").addClass("fa-minus");
        }
    }

    this.Hide = function () {
        var content_height = progress_content.height();
        file_progress.css("bottom", "-" + content_height + "px");
        if (progress_close.hasClass("fa-minus")) {
            progress_close.removeClass("fa-minus").addClass("fa-window-maximize");
        }
    }

    this.Remove = function () {
        this.Hide();
        window.setTimeout(function () { file_progress.remove(); }, 1000);
    }

    this.progress = function (i, size) {
        //获取当前文件大小，计算百分百,显示进度条
        var item_percent = size / arr[i].size;
        arr[i].progress.Show(item_percent * 100);

        //总进度数量
        if (item_percent >= 1) {
            if (!arr[i].success) {
                total_progress_number += 1;
                arr[i].success = true;
                progress_text2.text(total_progress_number + "/" + TotalNumber);
            }
        }

        //计算累计大小，计算百分百,显示进度条
        var progress_size = arr[i].progress_size;
        if (progress_size == undefined) {
            progress_size = 0;
        }
        total_progress = total_progress - progress_size + size;
        arr[i].progress_size = size;
        var total_percent = total_progress / TotalSize;
        j_total.Show(total_percent * 100);
    }

    var parent = this;

    progress_close.click(function () {

        if ($(this).hasClass("fa-minus")) {
            parent.Hide();
            return true;
        }

        if ($(this).hasClass("fa-window-maximize")) {
            parent.Show();
            return true;
        }

        if ($(this).hasClass("fa-times")) {
            parent.Remove();
            return true;
        }
    })
}

//新建文件夹
function CreateFolder() {
    var url = "/sys/File.aspx?action=CreateFolder";
    var json = {};
    var json_result = Ajax(url, json);
    return json_result.message;
};

//计算文件夹文件的个数与容量
function UpdateFolder(FD_ID) {
    var url = "/sys/File.aspx?action=UpdateFolder&FD_ID=" + FD_ID;
    var json = {};
    var json_result = Ajax(url, json);
};

//加载显示文件夹
function LoadFile(FD_ID) {
    if (window.box != undefined) {
        window.box.Remove();
    }
    var url = "/sys/File.aspx?action=GetFileByFolder&FD_ID=" + FD_ID;
    var json = {};
    var json_result = Ajax(url, json);
    var arr = [];
    for (var i = 0; i < json_result.items.length; i++) {
        var item = json_result.items[i]
        var obj = {};
        obj.id = item.FL_ID;
        obj.name = item.FL_NAME;
        obj.size = item.FL_SIZE;
        obj.type = item.FL_EXTENSION;
        arr.push(obj);
    }
    window.box = new ProgressBox(arr, true);
    for (var i = 0; i < arr.length; i++) {
        box.progress(i, arr[i].size);
    }

    window.box.Show();
};

//删除文件
function DeleteFile(FL_ID) {
    var url = "/sys/File.aspx?action=DeleteFile&FL_ID=" + FL_ID;
    var json = {};
    var json_result = Ajax(url, json);
    return json_result
};

//点击文件夹图标，弹出选择文件夹
$(".folder_icon").click(function () {
    $(this).parent().find(".folder_input").click();
    $(this).parent().removeClass(SOGWarming);
});

//完成文件夹选择，计算文件数量与产生弹出窗口
$(".folder_input").change(function () {
    if (file_progress_box) {
        file_progress_box.Remove();
    }
    var parent = $(this).parent();
    parent.find(".folder_url").text("选择" + this.files.length + "文件");
    var arr = [];
    for (var i = 0; i < this.files.length; i++) {
        var file = this.files[i];
        var obj = {};
        obj.id = 0;
        obj.name = file.name;
        obj.size = file.size;
        obj.type = GetFileExtension(file.name);
        arr.push(obj);
    }
    file_progress_box = new ProgressBox(arr);

    parent.find(".folder_clear").removeClass("hide").click(function () {
        ClearOneControl(parent);
        if (file_progress_box) {
            file_progress_box.Remove();
        }
    });

    //点击文件数量，显示文件盒子
    parent.find(".folder_url").click(function () {
        file_progress_box.Show();
    })
});

//点击表格查看文件夹文件
$('.SogContent').on('click', '.view_folder', function () {
    var FD_ID = $(this).attr("value");
    LoadFile(FD_ID);
});

//上传文件夹
function UploadFolder(folders) {

    folders.each(function () {
        var folder = $(this).find(".folder_input")
        //提交同步ajax，获取文件夹ID，设置好控件提交
        if (folder[0].files.length > 0) {
            var value = $(this).attr("value")
            if (value == undefined || value == "") {
                folder_id = CreateFolder();
                $(this).attr("value", folder_id);
            }
            UploadOneFolder(folder[0], $(this).attr("value"));
        }
    });
};

//上传一个文件夹
function UploadOneFolder(folder, folder_id) {
    file_progress_box.Show();
    UploadFile(folder.files, folder_id, function () {
        file_progress_box.Remove();
    });
};

//递归上传文件(核心)
function UploadFile(files, folder_id, fn) {
    if (window.Index == undefined) {
        window.Index = 0;
    }
    var file = files[window.Index];
    var xhr = new XMLHttpRequest();
    var fd = new FormData();
    fd.append("files", file);
    xhr.upload.addEventListener("progress", function (evt) {
        if (evt.lengthComputable) {
            file_progress_box.progress(window.Index, evt.loaded);
        }
    }, false);
    xhr.addEventListener("load", function (evt) {
        var data = evt.target.responseText;
        window.Index = window.Index + 1;
        if (window.Index < files.length) {
            UploadFile(files, folder_id, fn);
        } else {
            UpdateFolder(folder_id);
            fn();
            window.Index = undefined;
        }
    }, false);
    //发送文件和表单自定义参数
    xhr.open("POST", "/sys/File.aspx?action=Upload&FD_ID=" + folder_id, true);
    xhr.send(fd);
};

//上传文件夹控件初始化
function SogFolderUploadInit(_this, value) {
    if (value == "" || value == "null" || value == null) {
        $(_this).parent().find(".file_url").text("");
        return;
    }
    var url = "/sys/File.aspx?action=GetOneFolder&FD_ID=" + value;
    var json_data = {};
    Ajax(url, json_data, function (json_result) {
        if (json_result.status == 1) {
            var folder_url = $(_this).parent().find(".folder_url");
            folder_url.text("文件" + json_result.items.FD_FILE_COUNT + "个");
            folder_url.click(function () {
                LoadFile(value);
            })
        }
    });
};