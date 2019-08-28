<%@ Page Language="C#" AutoEventWireup="true" CodeFile="approve.aspx.cs" Inherits="approve" %>

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
        html, body, form, .container {
            height: 100%;
        }

        .navgite {
            margin: 10px 0px;
            background: #fff;
            font-size: 0px;
            position: relative;
        }

            .navgite .item {
                display: inline-block;
                width: 50%;
                font-size: 14px;
                text-align: center;
                padding: 10px 0px;
            }

            .navgite .active {
                color: rgba(96,199,146,1);
                transition: color 0.2s;
            }

            .navgite .line {
                position: absolute;
                bottom: 0px;
                left: 12%;
                width: 25%;
                height: 3px;
                background-color: rgba(96,199,146,1);
                transition: left 0.2s;
            }

            .navgite .right {
                left: 63%;
                transition: left 0.2s;
            }

        .content {
            background-color: #fff;
            position: relative;
            height: calc(100% - 109px);
        }

        .content_scroll {
            height: 100%;
            overflow-y: auto;
            overflow-x: hidden;
        }

        .content .me {
            width: 100%;
            /*background: green;*/
            position: absolute;
            left: 100%;
            transition: left 0.2s;
        }

        .content .other {
            width: 100%;
            /*background-color: red;*/
            position: absolute;
            left: 0%;
            transition: left 0.2s;
        }

        .active .me {
            left: 0% !important;
            transition: left 0.2s;
        }

        .active .other {
            left: -100% !important;
            transition: left 0.2s;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    </form>
    <script>
        $(".item").click(function () {
            $(this).addClass("active")
            $(this).siblings().removeClass("active");

            if ($(this).hasClass("other")) {
                $(".line").removeClass("right");
                $(".content").removeClass("active");
            } else {
                $(".line").addClass("right");
                $(".content").addClass("active");
            }
        })
    </script>
    <script>
        $(".Iback").click(function () {
            BackToApp();
        })

        function get_work_form_list(page, type) {
            var url = "/H5/work_follow/ajax.aspx?action=get_approve_work_form_list";
            var json_post = {};
            json_post["function_code_list"] = $(".container").attr("value");
            json_post["page"] = page;
            json_post["type"] = type;
            return Ajax(url, json_post);
        }

        function render_work_form_list() {
            var page = $(this).attr("page");
            page = (page == undefined) ? 0 : page;
            var type = $(this).attr("type");
            var data = get_work_form_list(page, type);
            if (data.status == "1") {
                for (var i = 0; i < data.items.length; i++) {
                    var item_data = data.items[i];
                    var item_class = "";
                    if (item_data.status == "0" || item_data.status == "3") {
                        item_class = 'WaterWave item1 WF_STATUS' + item_data.WF_STATUS;
                    } else {
                        item_class = " WaterWave item1"
                    }

                    var item1 = $('<div class="' + item_class + '" value="' + item_data.id + '" business="' + item_data.business + '" > </div>');
                    var head_image = $('<img src="' + item_data.user_image + '" class="head_image" />')
                    var message = $('<div class="message"> </div>');
                    item1.append(head_image);
                    item1.append(message);

                    var top = $('<div class="top"></div>');
                    var bottom = $('<div class="bottom">' + item_data.date + '</div>');
                    message.append(top);
                    message.append(bottom);

                    var user_name = $('<span class="name">' + item_data.user_name + item_data.business_text + '【' + item_data.master + '】' + '</span>');
                    var status_name = $('<span class="stauts ' + item_data.status_class + '">' + item_data.status_name + '</span>');
                    top.append(user_name);
                    top.append(status_name);

                    $(this).append(item1);
                }
                $(this).attr("page", ++page);
                return true;
            } else {
                if (page == 0) {
                    var no_data = $('<span class="no_data">暂无数据<span>')
                    $(this).append(no_data);
                }
                return false;
            }
        }

        $(".content_scroll").each(function () {
            render_work_form_list.call(this);
        })

        window.is_continue = true;
        $(".content_scroll").scroll(function () {
            var contentH = this.scrollHeight;//内容高度
            var scroH = $(".content_scroll").scrollTop();  //滚动高度
            var viewH = $(window).height();  //可见高度 

            if (scroH > 100) {
                //距离顶部大于100px时
                console.log("大于100px");
            }
            if (contentH - (scroH + viewH) <= 100) {
                //距离底部高度小于100px
                if (window.is_continue) {
                    var b = render_work_form_list.call(this);
                    if (!b) {
                        window.is_continue = false;
                    }
                }
            }
        })

        $(".content").on("click", ".item1", function () {
            LockControl(this, 200);
            var id = $(this).attr("value");
            var function_code = $(this).attr("business");
            var iframe = $('<iframe src="/H5/work_follow/detail.aspx?id=' + id + '&function_code=' + function_code + '" class="iframe"></iframe>');
            $("body").append(iframe);
            window.setTimeout(function () {
                iframe.css("left", "0px");
            }, 300);
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
            for (var i = 0; i < 2; i++) {
                $(window.CurrentItem).removeClass("WF_STATUS" + i);
            }
        }

    </script>
</body>
</html>
