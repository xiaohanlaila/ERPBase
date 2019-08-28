<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="ERPBase.H5.work_follow.detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=0" />
    <title></title>
    <link href="/H5/css/font-awesome4.7/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/H5/css/work_follow.css?id=14" rel="stylesheet" />

    <script src="/H5/js/jquery-1.11.0.min.js"></script>
    <script src="/H5/js/work_follow.js?id=15"></script>
    <style>
        html, body, form, .container {
            height: 100%;
        }

        .work_follow_content {
            height: calc(100% - 45px);
            overflow-y: auto;
            margin-bottom: 44px;
        }

        .work_follow_empty {
            height: 60px;
        }
    </style>

    <style>
        .bottom_function {
            position: fixed;
            bottom: 0;
            width: 100%;
            font-size: 0px;
        }

        .b {
            color: #fff;
            text-align: center;
            width: 33.33%;
            display: inline-block;
            font-size: 18px;
            padding: 10px 0px;
        }

        .b0 {
            background-color: rgba(251,184,103,1);
        }

        .b1 {
            background-color: rgba(96,199,146,1);
        }

        .b2 {
            background-color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <script>
        $(".Iback").click(function () {
            window.parent.hide_detail();
        })

        function handle_work_follow(WF_STATUS, WF_TO_USER, callback) {
            var json_post = {};
            $(".Idata").each(function () {
                json_post[$(this).attr("target")] = GetControlValue(this);
            });
            var function_code = $(".container").attr("value");
            var id = getUrlParam("id");
            json_post["function_code"] = function_code;
            json_post["id"] = id;
            json_post["WF_STATUS"] = WF_STATUS;
            json_post["WF_TO_USER"] = WF_TO_USER;

            var url = "/H5/work_follow/ajax.aspx?action=handle_work_follow";
            return data = Ajax(url, json_post, callback);
        }


        //选择人员后，显示（是否发送给赵泽辉审批，确认后发送请求）
        //保存后刷新页面

        //终审按钮
        $(".btn_end_approve").click(function () {
            var cb = {};
            cb.message = "确认到终审吗?";
            cb.success = function () {
                ShowLoading("正在处理...");
                handle_work_follow("1", "", function (data) {
                    if (data.status == "1") {
                        window.parent.update_status(data.primary_key, data.message);
                        location.reload();
                    } else {
                        MessageBox(data.message);
                    }
                });
            }
            var cb_box = new ConfirmBox(cb);
            cb_box.Show();
        })

        //驳回按钮
        $(".btn_back").click(function () {
            var cb = {};
            cb.message = "确认驳回审批吗?";
            cb.success = function () {
                ShowLoading("正在处理...");
                handle_work_follow("2", "", function (data) {
                    if (data.status == "1") {
                        window.parent.update_status(data.primary_key, data.message);
                        location.reload();
                    } else {
                        MessageBox(data.message);
                    }
                });
            }
            var cb_box = new ConfirmBox(cb);
            cb_box.Show();
        })

        //同意按钮
        $(".btn_approve").click(function () {
            show_approve_user();
        });

        //撤回按钮
        $(".cancle").click(function () {
            var cb = {};
            cb.message = "确认撤回吗?";
            cb.success = function () {
                ShowLoading("正在处理...");
                handle_work_follow("4", "", function (data) {
                    if (data.status == "1") {
                        window.parent.update_status(data.primary_key, data.message);
                        location.reload();
                    } else {
                        MessageBox(data.message);
                    }
                });
            }
            var cb_box = new ConfirmBox(cb);
            cb_box.Show();
        })

        function show_approve_user() {
            var iframe = $('<iframe src="/H5/select_user.aspx" class="iframe"></iframe>');
            $("body").append(iframe);
            window.setTimeout(function () {
                iframe.css("left", "0px");
            }, 200);
        }

        function user_hide() {
            $(".iframe").css("left", "100%")
            window.setTimeout(function () {
                $(".iframe").remove();
            }, 200);
        }

        function user_select(userid, user_image_url, name) {
            if (userid != undefined) {
                var cb = {};
                cb.message = "是否发送给" + name + "审批?";
                cb.success = function () {
                    ShowLoading("正在处理...");
                    handle_work_follow("1", userid, function (data) {
                        if (data.status == "1") {
                            window.parent.update_status(data.primary_key, data.message);
                            location.reload();
                        } else {
                            MessageBox(data.message);
                        }
                    });
                }
                var cb_box = new ConfirmBox(cb);
                cb_box.Show();
            }
        }

    </script>
</body>
</html>
