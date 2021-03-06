﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="help" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/H5/css/font-awesome4.7/css/font-awesome.min.css" rel="stylesheet" />
    <script src="/H5/js/jquery-1.11.0.min.js"></script>
    <style>
        .disabled {
            pointer-events: none;
            cursor: default;
            /*opacity: 0.6;*/
        }

        body, html, form, .container {
            padding: 0px;
            margin: 0px;
        }

        body, html, form, .container {
            position: relative;
            width: 100%;
            height: 100%;
        }

            .container .item_img {
                width: 100%;
                height: 95%;
                position: absolute;
                transition: left 0.5s;
                left: 100%;
                top: 41px;
                transform: scaleY(50px);
            }

        /*头部提示*/
        .top_tips {
            position: fixed;
            top: 0px;
            left: 100%;
            background-color: rgba(33,33,33,0.7);
            color: #fff;
            width: 100%;
            padding: 10px;
            font-size: 16px;
        }

            .top_tips .info_icon {
                padding: 0px 15px;
                font-size: 20px;
            }

        /*点击指示器*/
        @keyframes point_tip {
            0% {
                opacity: 1;
                transform: scale(1, 1);
            }

            100% {
                opacity: 0.1;
                transform: scale(1.3, 1.3);
            }
        }

        .mouse_tip {
            position: fixed;
            top: 100px;
            left: 100px;
            background-color: rgba(33,33,33,1);
            width: 50px;
            height: 50px;
            border-radius: 50%;
            z-index: 2;
            animation: point_tip 1.5s linear infinite;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
        </div>
    </form>
    <script>
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



        function show_top_tips(message) {
            var html = '';
            html += '<div class="top_tips">';
            html += ' <i class="fa fa-info-circle info_icon" aria-hidden="true"></i>';
            html += message;
            html += '</div>';
            var $html = $(html);
            $("body").append($html);
            $html.css("left", "0%");
        }


        function hide_top_tips() {
            $(".top_tips").remove();
        }

        function show_mouse_tip(x, y, callback) {
            var html = '<div class="mouse_tip"></div>';
            var $html = $(html);
            $html.css("top", y);
            $html.css("left", x);
            if (callback) {
                $html.click(callback);
            }
            $("body").append($html);
        }

        function hide_mouse_tip() {
            $(".mouse_tip").removeClass();
        }

        function show_image(url) {
            var html = '<img class="item_img" src="' + url + '" />';
            var $html = $(html);
            $(".container").append($html);

            if (window.Index == 0) {
                $html.css("left", "0px");
            } else {
                $html.animate({ left: "0px" });
            }
            $html.siblings().animate({ left: "-100%" });
            window.setTimeout(function () {
                $html.siblings().remove();
            }, 1000);
        }


        var arr = [];

        var item1 = {};
        item1.url = "1.jpg";
        item1.text = "点击移动办公图标设置显示菜单";
        item1.x = "80%";
        item1.y = "42%";
        arr.push(item1);

        var item2 = {};
        item2.url = "2.jpg";
        item2.text = "勾选菜单";
        item2.x = "6%";
        item2.y = "63%";
        arr.push(item2);

        var item3 = {};
        item3.url = "3.jpg";
        item3.text = "勾选菜单";
        item3.x = "32%";
        item3.y = "63%";
        arr.push(item3);

        var item4 = {};
        item4.url = "4.jpg";
        item4.text = "勾选菜单";
        item4.x = "57%";
        item4.y = "63%";
        arr.push(item4);

        var item5 = {};
        item5.url = "5.jpg";
        item5.text = "点击保存按钮";
        item5.x = "85%";
        item5.y = "8%";
        arr.push(item5);


        var item6 = {};
        item6.url = "6.jpg";
        item6.text = "点击机票,进入机票申请";
        item6.x = "31%";
        item6.y = "82%";
        arr.push(item6);

        var item61 = {};
        item61.url = "61.jpg";
        item61.text = "输入需购买机票信息";
        item61.x = "60%";
        item61.y = "16%";
        arr.push(item61);


        var item62 = {};
        item62.url = "61.jpg";
        item62.text = "选择审批人";
        item62.x = "3%";
        item62.y = "70%";
        arr.push(item62);


        var item63 = {};
        item63.url = "61.jpg";
        item63.text = "点击保存按钮,发起申请,等待相关人员审批";
        item63.x = "45%";
        item63.y = "93%";
        arr.push(item63);


        var item64 = {};
        item64.url = "61.jpg";
        item64.text = "点击由上角查看机票申请列表";
        item64.x = "86%";
        item64.y = "10%";
        arr.push(item64);

        var item65 = {};
        item65.url = "62.jpg";
        item65.text = "点击具体机票信息查看明细";
        item65.x = "40%";
        item65.y = "15%";
        arr.push(item65);

        var item66 = {};
        item66.url = "63.jpg";
        item66.text = "确实信息,点击返回列表";
        item66.x = "0%";
        item66.y = "8%";
        arr.push(item66);


        var item67 = {};
        item67.url = "62.jpg";
        item67.text = "点击返回申请页";
        item67.x = "0%";
        item67.y = "8%";
        arr.push(item67);

        var item67 = {};
        item67.url = "61.jpg";
        item67.text = "点击返回主页";
        item67.x = "0%";
        item67.y = "8%";
        arr.push(item67);


        var item7 = {};
        item7.url = "6.jpg";
        item7.text = "点击采购,进入采购申请";
        item7.x = "56%";
        item7.y = "82%";
        arr.push(item7);


        var item71 = {};
        item71.url = "61.jpg";
        item71.text = "与机票一样,不重复说明，点击返回主页";
        item71.x = "0%";
        item71.y = "8%";
        arr.push(item71);


        var item8 = {};
        item8.url = "6.jpg";
        item8.text = "点击行政审批,进行审批机票与采购申请";
        item8.x = "6%";
        item8.y = "82%";
        arr.push(item8);

        var item81 = {};
        item81.url = "81.jpg";
        item81.text = "点击需审批的条目";
        item81.x = "45%";
        item81.y = "23%";
        arr.push(item81);


        var item82 = {};
        item82.url = "82.jpg";
        item82.text = "输入审批建议,可以没有";
        item82.x = "6%";
        item82.y = "54%";
        arr.push(item82);

        var item83 = {};
        item83.url = "82.jpg";
        item83.text = "点击底部按钮进行审批操作";
        item83.x = "45%";
        item83.y = "90%";
        arr.push(item83);


        var item9 = {};
        item9.url = "9.jpg";
        item9.text = "点击退出教程";
        item9.x = "50%";
        item9.y = "50%";
        arr.push(item9);

        function ImageTips(arr_json) {
            window.Index = window.Index == undefined ? -1 : window.Index;
            var arr = arr_json;
            var that = this;

            this.next = function () {
                window.Index++;
                if (arr.length <= window.Index) {
                    BackToApp();
                } else {
                    action();
                }
            }

            this.pre = function () {
                window.Index--;
                action();
            }

            function action() {
                var item_data = arr[window.Index];
                show_image("/H5/image/" + item_data.url);

                window.setTimeout(function () {
                    hide_mouse_tip();
                    hide_top_tips();
                    show_top_tips(item_data.text);
                    show_mouse_tip(item_data.x, item_data.y, that.next);
                }, 500)

            }
        }

        var img = new ImageTips(arr);
        img.next();
    </script>
</body>
</html>
