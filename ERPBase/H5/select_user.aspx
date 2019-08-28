<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_user.aspx.cs" Inherits="select_user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <link href="../css/font-awesome4.7/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        .hide {
            display: none;
        }

        .icon_back {
            float: left;
            display: inline-block;
            border: 1px solid #fff;
            width: 10px;
            height: 10px;
            transform: rotate(45deg) translateY(10px);
            border-top: 0px;
            border-right: 0px;
        }

        .icon_more {
            float: right;
            line-height: 30px;
            position: relative;
        }

        body, html {
            padding: 0px;
            margin: 0px;
            overflow: hidden;
        }

        .head {
            color: #fff;
            background-color: rgba(96,199,146,1);
            text-align: center;
            padding: 15px 20px 0px 20px;
            font-size: 18px;
            line-height: 30px;
        }

        .container {
            background-color: #eee;
        }
    </style>

    <style>
        html, form, body, .container {
            height: 100%;
        }

        .search {
            position: relative;
            padding: 5px 10px;
            width: 100%;
            box-sizing: border-box;
        }

        .search_box {
            width: 100%;
            height: 30px;
            border-radius: 5px;
            /*margin: 5px 10px;*/
            border: 0px;
            border-style: none;
            outline-style: none;
            position: relative;
            padding-left: 30px;
            box-sizing: border-box;
        }

        .search_box_icon {
            color: #ccc;
            position: absolute;
            top: 14px;
            left: 16px;
            z-index: 1;
        }

        .department_list {
            background-color: #fff;
            overflow-y: auto;
            height: calc(100% - 86.6px);
        }

            .department_list .item {
                border-bottom: 1px solid #eee;
                background-color: #fff;
            }

                .department_list .item:active {
                    background-color: #eee;
                }

                .department_list .item:after {
                    content: '';
                    float: right;
                    display: inline-block;
                    border: 1px solid #ccc;
                    width: 10px;
                    height: 10px;
                    transform: rotate(-135deg) translateY(-28px) translateX(-8px);
                    border-top: 0px;
                    border-right: 0px;
                }

            .department_list .list_head {
                display: inline-block;
                width: 25px;
                height: 25px;
                line-height: 25px;
                margin: 10px;
                padding: 10px;
                background-color: rgba(66,169,116,1);
                text-align: center;
                color: #fff;
                border-radius: 3px;
            }

        .user_list {
            background-color: #fff;
            overflow-y: auto;
            height: calc(100% - 86.6px);
        }

            .user_list .item {
                border-bottom: 1px solid #eee;
                font-size: 12px;
                position: relative;
            }

                .user_list .item:active {
                    background-color: #eee;
                }

                .user_list .item:after {
                    content: '';
                    float: right;
                    display: inline-block;
                    border: 1px solid #ccc;
                    width: 10px;
                    height: 10px;
                    transform: rotate(-135deg) translateY(-28px) translateX(-8px);
                    border-top: 0px;
                    border-right: 0px;
                }

            .user_list .head_image {
                width: 45px;
                height: 45px;
                margin: 5px 10px;
            }

            .user_list .item .content {
                display: inline-block;
                position: absolute;
                top: 8px;
            }

            .user_list .name {
                margin-bottom: 5px;
            }

        .Iback {
            background-color: red;
            position: fixed;
            width: 90px;
            height: 45px;
            left: 0;
            top: 0px;
            z-index: 1;
            opacity: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="head">
                <span class="icon_back"></span>
                <span class="title">审批人</span>
            </div>

            <div class="search">
                <i class="fa fa-search search_box_icon " aria-hidden="true"></i>
                <input type="text" placeholder="搜索姓名/手机号码" class="search_box" />
            </div>

            <%--用户列表--%>
            <%--<div class="user_list">
                <div class="item">
                    <img class="head_image" src="http://usercenter.zhongliko.com/headimage/39cdf7892c634303be57675aca994a30.png" />
                    <div class="content">
                        <div class="name">赵泽辉</div>
                        <div class="department">商业智能</div>
                    </div>
                </div>
            </div>--%>

            <%--部门列表--%>
            <%--<div class="department_list">
                <div class="item">
                    <span class="list_head">财</span>
                    <span class="list_content">财务部门</span>
                </div>
            </div>--%>
        </div>
        <div class="Iback"></div>
    </form>
    <script>
        function MessageBox(content) {
            var tip_message = $('<div class="tip_message">' + content + '</div>');
            $("body").append(tip_message);
            tip_message.css("bottom", "50px");
            window.setTimeout(function () {
                tip_message.css("opacity", "0");
                tip_message.remove();
            }, 1500);
        }

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

        $('.search_box').bind('input propertychange', function () {
            if ($(this).val() == "") {
                //恢复原状
                $(".department_list").removeClass("hide");
                $(".user_list").remove();

            } else {
                render_user(undefined, $(this).val());
            }
        });

        function get_user() {
            //ajax 获取数据缓存起来,每天一次
            //id,name,image_url,dp_id
            var url = "/H5/work_follow/ajax.aspx?action=get_user";
            var data = Ajax(url, {});
            return data;
        }

        function clear_cache(search) {
            for (i = 0; i < localStorage.length; i++) {
                var key = localStorage.key(i);
                if (key.indexOf(search) >= 0) {
                    localStorage.removeItem(key);
                }
            }
        }

        function get_user_cache() {
            var d = new Date();
            var key = "get_user" + d.getFullYear() + d.getMonth() + d.getDay();


            var st = localStorage.getItem(key);
            if (st == undefined) {
                clear_cache("get_user");
                var data = get_user();
                if (data.status == "1") {
                    localStorage.setItem(key, JSON.stringify(data));
                }
                return data;
            } else {
                //console.log(JSON.parse(st));
                return JSON.parse(st);
            }
        }

        function get_department_cache() {
            var d = new Date();
            var key = "get_department" + d.getFullYear() + d.getMonth() + d.getDay();
            var st = localStorage.getItem(key);
            if (st == undefined) {
                clear_cache("get_department");
                var data = get_department();
                if (data.status == "1") {
                    localStorage.setItem(key, JSON.stringify(data));
                }
                return data;
            } else {
                return JSON.parse(st);
            }
        }

        function get_department() {
            var url = "/H5/work_follow/ajax.aspx?action=get_department";
            var data = Ajax(url, {});
            return data;
            //ajax 获取数据缓存起来,每天一次 id,name,name1
        }

        function render_department() {
            var data = get_department_cache();
            var department_list = $('<div class="department_list"> </div>');
            if (data.status == "1") {
                for (var i = 0; i < data.items.length; i++) {
                    var item = data.items[i];
                    var item_control = $('<div class="item Idepartment" value="' + item.DP_ID + '"> <span class="list_head">' + item.DP_NAME + '</span> <span class="list_content">' + item.DP_DEPARTMENT_NAME + '</span> </div>');
                    department_list.append(item_control);
                }
            }
            $(".department_list").remove();
            $(".container").append(department_list);
        }

        render_department();

        function render_user(dp_id, search_content) {
            var data = get_user_cache();

            var user_list = $('<div class="user_list"> </div>');
            if (data.status == "1") {
                for (var i = 0; i < data.items.length; i++) {
                    var item = data.items[i];
                    var is_in = false;
                    //部门搜索
                    if (dp_id != undefined) {
                        if (item.UR_DEPARTMENT_ID == dp_id) {
                            is_in = true;
                        }
                    }

                    //内容搜索
                    if (search_content != undefined) {
                        if (item.content.indexOf(search_content) >= 0) {
                            is_in = true;
                        }
                    }

                    if (is_in) {
                        var item_control = $('<div class="item Iuser" value="' + item.UR_USER_ID + '" url="' + item.UR_HEAD_IMAGE + '" name="' + item.UR_NAME + '" ></div>');
                        var head_image = $('<img class="head_image" src="' + item.UR_HEAD_IMAGE + '" />')
                        var content = $('<div class="content"><div class="name">' + item.UR_NAME + '</div><div class="department">' + item.DP_DEPARTMENT_NAME + '</div></div>');
                        item_control.append(head_image);
                        item_control.append(content);
                        user_list.append(item_control);
                    }
                }
            }
            $(".department_list").addClass("hide");
            $(".user_list").remove();
            $(".container").append(user_list);
        }

        $(".container").on("click", ".Idepartment", function () {
            render_user($(this).attr("value"));
        });

        $(".container").on("click", ".Iuser", function () {
            //恢复原状
            $(".department_list").removeClass("hide");
            $(".user_list").remove();
            $(".search_box").val("");
            //返回父亲
            window.parent.user_hide()
            window.parent.user_select($(this).attr("value"), $(this).attr("url"), $(this).attr("name"));
        });

        $(".Iback").click(function () {
            if ($(".department_list").hasClass("hide")) {
                $(".department_list").removeClass("hide");
                $(".user_list").remove();
            } else {
                $(".search_box").val("");
                window.parent.user_hide()
                window.parent.user_select(undefined, undefined);
            }
        });
    </script>
</body>
</html>
