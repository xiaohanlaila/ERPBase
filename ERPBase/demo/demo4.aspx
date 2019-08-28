<%@ Page Language="C#" AutoEventWireup="true" CodeFile="demo4.aspx.cs" Inherits="demo4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/css/font-awesome4.7/css/font-awesome.min.css" />
    <link href="/css/maintenance.css?v=11" rel="stylesheet" />
    <link href="/css/jquery.toast.css" rel="stylesheet" />

    <script src="/js/jquery-1.11.0.min.js"></script>
    <script src="/js/SogModal.js?v=4"></script>
    <script src="/js/jquery.toast.js"></script>
    <script src="/js/maintenance.js?v=1" type="text/javascript"></script>

    <style type="text/css">
        /*文件样式*/
        .SogFileUpload {
            font-size: 14px;
            color: #666;
            border: 1px solid #ccc;
            width: 400px;
            height: 36px;
            padding: 5px 12px;
            vertical-align: middle;
        }

        .file_icon, .folder_icon {
            color: #3bb9ef;
            font-size: 25px;
            cursor: pointer;
            margin-right: 20px;
            float: left;
        }

            .file_icon:hover, .folder_icon:hover {
                color: #0d82ae;
            }


        .file_url, .down_file, .folder_url, .view_folder {
            display: inline-block;
            text-decoration: none;
            color: #555;
            cursor: pointer;
        }

            .file_url:hover {
                display: inline-block;
                text-decoration: none;
                color: #3bb9ef;
            }

            .file_url:link {
                color: #3bb9ef;
            }

        .file_precent {
            float: right;
            background-color: red;
            display: inline-block;
            border-radius: 50%;
            width: 20px;
            height: 20px;
            font-size: 12px;
            color: #fff;
            padding-top: 2px;
            text-align: center;
            font-weight: 400;
        }

        .file_clear, .folder_clear {
            float: right;
            font-size: 15px;
            cursor: pointer;
            /*margin-right: 20px;*/
            padding: 5px;
        }

            .file_clear:hover, .folder_clear:hover {
                background-color: #eee;
            }
    </style>

    <style>
        .file_progress {
            border: 1px solid #ccc;
            position: fixed;
            bottom: -700px;
            right: 0px;
            background-color: #fff;
            width: 500px;
            border-radius: 5px 5px 0px 0px;
            transition: bottom 0.5s;
            z-index: 20;
        }

        .progress_title {
            border-bottom: 1px solid #ccc;
            height: 40px;
            line-height: 40px;
            font-weight: 400;
        }

        .progress_close {
            float: right;
            font-size: 18px;
            cursor: pointer;
            padding: 0px;
            color: #444;
            padding: 10px;
        }

            .progress_close:hover {
                background-color: #eee;
            }

        .progress_text {
            color: #333;
            font-size: 14px;
            padding: 0px 10px;
        }

        .progress_line_o {
            width: 130px;
            height: 10px;
            border-radius: 5px;
            background-color: #eee;
            display: inline-block;
            margin: 0px 10px;
        }

        .progress_line_o_total {
            width: 325px;
            height: 10px;
            border-radius: 5px;
            background-color: #eee;
            display: inline-block;
            margin: 0px 10px;
        }

        .progress_line_f {
            background-color: #3bb9ef;
            width: 66%;
            height: 10px;
            border-radius: 5px;
        }

        .progress_content {
            min-height: 300px;
            max-height: 600px;
            overflow: auto;
        }

        .progress_item {
            padding: 10px;
            border-bottom: 1px solid #eee;
            cursor: pointer;
            color: #333;
            font-size: 14px;
        }

            .progress_item:hover {
                background-color: #FFFAFA;
            }

            .progress_item:active {
                background-color: #ccc;
            }

        .progress_icon {
            font-size: 18px;
            padding: 0px 5px;
        }

        .progress_filename {
            width: 220px;
            display: inline-block;
            overflow: hidden;
            padding-left: 10px;
            white-space: nowrap;
        }

        .progress_size {
            font-size: 12px;
        }

        .file_delete_icon {
            float: right;
            font-size: 18px;
            cursor: pointer;
            padding: 0px;
            color: #444;
            padding-top: 2px;
        }

            .file_delete_icon:hover {
                color: red;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    </form>

    <script>
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

        //筛选条件缩放
        $(".handler .small_btn,.close").click(function () {
            $(".SogCondition .open").slideToggle();
            $(".SogCondition .close").slideToggle();
            if ($(this).hasClass("fa-chevron-down")) {
                $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
            } else {
                $(this).addClass("fa-chevron-down").removeClass("fa-chevron-up");
            }
        })

        //头部导航条件
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

        //表格全选
        $('.SogContent').on('click', '.check_all', function () {
            $(".check_one").prop("checked", this.checked);
        });

        //右上角新增按钮事件
        $(".add_botton").click(function () {
            ShowUI("div_add");
        });

        //点击编辑按钮
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

        //新增时保存按钮
        $("#btn_add_save").click(function () {
            Add();
        });

        //编辑时保存按钮
        $("#btn_edit_save").click(function () {
            Edit();
        });

        //删除按钮
        $("#btn_delete").click(function () {
            Delete();
        });

        //页面加载完成后，加载列表
        $(function () {
            Search();
        });

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

        var SOGWarming = "SOGWarming";

        //异步请求数据
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

        //鼠标列表检查表单事件
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
        }).click(function () {
            var type = $(this).attr("type");
            if (type != "file") {
                $(this).removeClass(SOGWarming);
                $(this).tips();
            }
        });

        //新增函数
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

        //判断是否空或者空字符串
        function IsNullOrEmpty(obj) {
            if (obj == undefined) {
                return true;
            }

            if (obj == null) {
                return true;
            }

            if (obj == "") {
                return true;
            }
            return false;
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
    </script>


    <%--文件逻辑--%>
    <script type="text/javascript">
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

        //上传文件的ajax核心方法
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

        //点击表格下载文件
        $('.SogContent').on('click', '.down_file', function () {
            var FL_ID = $(this).attr("value");
            Download(FL_ID);
        });

    </script>

    <script>
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
    </script>

</body>
</html>
<%--表单10万条数据，查询在1秒以内，安排性能测试--%>
<%--编辑的弹出与查看的弹出有冲突--%>
<%--预览文件--%>
<%--文件信息:大小
图片：直接显示小图,移到图片上面显示中图
pdf: 在页面又面显示，点击查看详情
excel：在页面又面显示，点击查看详情
doc：在页面又面显示，点击查看详情
ppt：在页面又面显示，点击查看详情
mp3(音频): 显示音频播放，点击可以直接播放
mp4(视频):显示视频播放，点击可以直接播放
rar.zip(暂时无法预览)

查询表格无数据时--%>


<%--tip控件思路
1可以指定方向，上，下，左，右  标签开始位置
2不指定方向：方向计算，标签开始（左中右）
tips($e) $e=jq元素--%>

<%--后台获取文件与文件夹描述效率非常慢，需要优化--%>
<%--安排js与css放到后台,特别是文件夹与文件部分--%>



