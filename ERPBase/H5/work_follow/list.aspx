<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="ERPBase.H5.work_follow.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=0" />
    <title></title>
    <link href="/H5/css/font-awesome4.7/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/H5/css/work_follow.css?id=15" rel="stylesheet" />
    <script src="/H5/js/jquery-1.11.0.min.js"></script>
    <script src="/H5/js/work_follow.js?v=15"></script>
    <style>
        .container {
            background: #fff;
        }

        html, body, form, .container {
            height: 100%;
        }

        .content {
            height: calc(100% - 45px);
            overflow-y: auto;
            position: relative;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%--<div class="container">
            <div class="content">
                <div class="item1">
                    <img src="http://usercenter.zhongliko.com/headimage/39cdf7892c634303be57675aca994a30.png" class="head_image" />
                    <div class="message">
                        <div class="top">
                            <span class="name">赵泽辉外勤</span>
                            <span class="stauts c2">待审核</span>
                        </div>

                        <div class="bottom">2018-09-09 12:45</div>
                    </div>
                </div>
            </div>
        </div>--%>
    </form>

    <script>
        $(".Iback").click(function () {
            window.parent.get_work_form_waitting()
            window.parent.list_hide()
        });

        function get_work_form_list(page) {
            var url = "/H5/work_follow/ajax.aspx?action=get_work_form_list";
            var json_post = {};
            json_post["function_code"] = $(".container").attr("value");
            json_post["page"] = page;
            return Ajax(url, json_post);
        }

        function render_work_form_list() {
            var page = $(".content_scroll").attr("page");
            page = (page == undefined) ? 0 : page;
            var data = get_work_form_list(page);
            if (data.status == "1") {
                for (var i = 0; i < data.items.length; i++) {
                    var item_data = data.items[i];
                    var item1 = $('<div class="item1 WaterWave" value="' + item_data.id + '" > </div>');
                    var head_image = $('<img src="' + item_data.user_image + '" class="head_image" />')
                    var message = $('<div class="message"> </div>');
                    item1.append(head_image);
                    item1.append(message);

                    var top = $('<div class="top"></div>');
                    var bottom = $('<div class="bottom">' + item_data.date + '</div>');
                    message.append(top);
                    message.append(bottom);

                    var user_name = $('<span class="name">' + item_data.user_name + '【' + item_data.master + '】' + '</span>');
                    var status_name = $('<span class="stauts ' + item_data.status_class + '">' + item_data.status_name + '</span>');
                    top.append(user_name);
                    top.append(status_name);

                    $(".content_scroll").append(item1);
                }
                $(".content_scroll").attr("page", ++page);
                return true;
            } else {
                //alert(page);
                if (page == 0) {
                    var no_data = $('<span class="no_data">暂无数据<span>')
                    $(".content_scroll").append(no_data);
                }
                return false;
            }
        }
        render_work_form_list();

        window.is_continue = true;
        $(".content").scroll(function () {
            var scroH = $(".content").scrollTop();  //滚动高度
            var viewH = $(window).height();  //可见高度 
            var contentH = $(".content_scroll").height();  //内容高度
            if (scroH > 100) {
                //距离顶部大于100px时
                console.log("大于100px");
            }
            if (contentH - (scroH + viewH) <= 100) {
                //距离底部高度小于100px
                if (window.is_continue) {
                    var b = render_work_form_list();
                    if (!b) {
                        window.is_continue = false;
                    }
                }
            }
        });

        $(".content").on("click", ".item1", function () {
            LockControl(this, 200);
            var id = $(this).attr("value");
            var function_code = getUrlParam("function_code");
            var iframe = $('<iframe src="/H5/work_follow/detail.aspx?id=' + id + '&function_code=' + function_code + '" class="iframe"></iframe>');
            $("body").append(iframe);
            window.setTimeout(function () {
                iframe.css("left", "0px");
            }, 500);
            window.CurrentItem = this;
        })

        function hide_detail() {
            $(".iframe").css("left", "100%")
            window.setTimeout(function () {
                $(".iframe").remove();
            }, 200);
        }

        function update_status(status, stauts_text) {
            var ct = $(window.CurrentItem).find(".stauts");
            for (var i = 0; i < 5; i++) {
                ct.removeClass("c" + i);
            }
            ct.addClass("c" + status);
            ct.text(stauts_text);
        }
    </script>
</body>
</html>
