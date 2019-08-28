<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo1.aspx.cs" Inherits="ERPBase.demo.demo1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/css/font-awesome4.7/css/font-awesome.min.css" />
    <script src="/js/jquery-1.11.0.min.js"></script>

    <!--公共区-->
    <style>
        body, html {
            padding: 0px;
            margin: 5px;
            font-family: "Microsoft Yahei";
        }

        input, select，button {
            outline: none;
            box-sizing: border-box;
        }

        button:focus {
            outline: none;
        }
    </style>

    <!--按钮区-->
    <style>
        /*新增按钮*/
        .add_botton {
            color: #fff;
            height: 25px;
            width: 25px;
            font-size: 18px;
            text-align: center;
            display: inline-block;
            background-color: #3BB9EF;
            font-weight: bold;
            border-radius: 50%;
            margin: 5px;
            cursor: pointer;
            transition: background-color ease-in-out 0.5s,box-shadow ease-in-out 0.5s;
        }

            .add_botton:hover {
                background-color: #0d82ae;
            }



        .btn {
            color: #fff;
            font-weight: 400;
            border: 1px solid transparent;
            text-align: center;
            display: inline-block;
            font-size: 12px;
            padding: 3px 12px;
            margin: 3px;
            cursor: pointer;
        }

        .green_btn {
            background-color: #56c32a;
        }

            .green_btn:hover {
                background-color: #439921;
            }


        .SogControl {
            width: 400px;
            height: 36px;
            padding: 5px 12px;
            border: 1px solid #ccc;
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            border-radius: 2px;
            font-size: 14px;
            color: #555;
        }

            .SogControl:active, .SogControl:hover, SogControl:focus {
                border-color: #10a6de;
            }
    </style>

    <!--头部区域-->
    <style>
        .active {
        }

        .SogHead {
            width: 100%;
            max-width: 100%;
            padding-bottom: 10px;
        }

        .SogTitle {
            font-size: 20px;
            padding: 5px;
            font-weight: bold;
            margin-right: 50px;
        }

        /*导航*/
        .SogNavigate {
            display: inline-block;
            font-size: 0px;
        }

            .SogNavigate a, .SogNavigate span {
                padding: 5px 15px;
                border: 1px solid #cccccc;
                border-left: 0px;
                font-size: 12px;
            }

                .SogNavigate a:first-child, .SogNavigate span:first-child {
                    border: 1px solid #ccc;
                }


            .SogNavigate a {
                cursor: pointer;
                color: #0f83b5;
                background: #ffffff;
                transition: background-color ease-in-out 0.5s,box-shadow ease-in-out 0.5s;
            }

                .SogNavigate a:hover {
                    background-color: #eee;
                }

                .SogNavigate a.active {
                    background: #3BB9EF;
                    color: #ffffff;
                    transition: background-color ease-in-out 0.5s,box-shadow ease-in-out 0.5s;
                }

                    .SogNavigate a.active:hover {
                        background-color: #0d82ae;
                    }

        .SogRight {
            display: inline-block;
            float: right;
        }
    </style>

    <!--条件区域-->
    <style>
        .SogCondition {
            width: 100%;
            max-width: 100%;
        }

        .condition_item {
            border: 1px solid #ddd;
            padding: 0px;
            border-bottom: 0px;
        }

            .condition_item:last-child {
                border-bottom: 1px solid #ccc;
            }

        .condition_head {
            display: inline-block;
            color: #333;
            font-size: 12px;
            margin-right: 30px;
            margin-left: 11px;
            font-weight: bold;
        }

        .condition_item a {
            padding: 6px;
            font-size: 12px;
            margin: 7px 5px;
            display: inline-block;
        }

        .condition_item a {
            color: #333;
            cursor: pointer;
            transition: background-color ease-in-out 0.5s,box-shadow ease-in-out 0.5s;
        }

            .condition_item a:hover {
                background-color: #eee;
            }

            .condition_item a.active {
                color: #FFF;
                background-color: #3BB9EF;
                transition: background-color ease-in-out 0.5s,box-shadow ease-in-out 0.5s;
            }

        .close {
            border: 1px solid #ddd;
            padding: 5px 10px;
            font-size: 0px;
        }

        .condition_desc {
            display: inline-block;
            margin-left: 10px;
            border-radius: 2px;
            background-color: #3BB9EF;
        }

            .condition_desc span {
                display: inline-block;
                font-size: 12px;
                color: #fff;
                padding: 5px;
            }

        .SogCondition .open {
            background: #FBFCFD;
        }

        .SogCondition .close {
            background: #FBFCFD;
        }


        .SogCondition .handler {
            text-align: center;
            font-size: 0px;
        }

        .small_btn {
            display: inline-block;
            width: 33px;
            height: 17px;
            cursor: pointer;
            background-color: #3bb9ef;
            color: #fff;
            background-color: #3bb9ef;
            font-size: 14px;
            padding-top: 3px;
            transition: background-color ease-in-out 0.5s,box-shadow ease-in-out 0.5s;
        }

            .small_btn:hover {
                background-color: #0d82ae;
            }
    </style>

    <!--文本搜索区-->
    <style>
        .SogTextSearch {
            font-size: 0px;
            margin: 8px 0px;
        }

            .SogTextSearch button {
                background: #3BB9EF;
                font-size: 14px;
                padding: 0px 15px;
                color: #ffffff;
                display: inline-block;
                height: 34px;
                text-align: center;
                line-height: 34px;
                vertical-align: top;
                border: none;
                cursor: pointer;
                transition: background-color ease-in-out 0.5s,box-shadow ease-in-out 0.5s;
            }

                .SogTextSearch button:hover {
                    background-color: #0d82ae;
                }

            .SogTextSearch input {
                height: 34px;
                border: 1px solid #ddd;
                font-size: 12px;
                padding: 3px 12px;
                margin: 0px;
                width: 220px;
                transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
                vertical-align: top;
            }

                .SogTextSearch input:active, .SogTextSearch input:focus, .SogTextSearch input:hover {
                    border-color: #3BB9EF;
                }

            .SogTextSearch i {
                margin-right: 5px;
            }
    </style>

    <!--功能区域-->
    <style type="text/css">
        .SogFunction {
            font-size: 14px;
            padding-left: 10px;
        }

            .SogFunction .number {
                color: #3BB9EF;
                padding: 0 5px;
            }
    </style>

    <!--表格区域-->
    <style>
        .SogTable {
            width: 100%;
            max-width: 100%;
            border-collapse: collapse;
        }

            .SogTable tr:nth-child(odd) {
                background-color: #F6FBFD;
            }

            .SogTable tr:nth-child(even) {
                background-color: #FFFFFF;
            }

            .SogTable tr:hover {
                background-color: rgba(218,242,252,1);
            }

            .SogTable th, .SogTable td {
                border: 1px solid #ddd;
                padding: 8px;
                font-size: 12px;
                font-family: "Microsoft Yahei";
                text-align: left;
            }

        .SogTdCheck {
            width: 20px;
        }
    </style>

    <!--分页区域-->
    <style>
        .SogPages {
            font-size: 0px;
            text-align: right;
            padding: 8px;
            border: 1px solid #ccc;
            border-top: 0px;
        }

            .SogPages a {
                width: 31px;
                height: 26px;
                font-size: 14px;
                border: 1px solid #ccc;
                border-left: 0px;
                text-align: center;
                padding: 5px 0px 0px 0px;
                display: inline-block;
                transition: background-color ease-in-out 0.5s,box-shadow ease-in-out 0.5s;
            }

                .SogPages a:first-child {
                    border: 1px solid #ccc;
                }

            .SogPages a {
                cursor: pointer;
                color: #0f83b5;
                background: #ffffff;
            }

                .SogPages a:hover {
                    background-color: #eee;
                }

                .SogPages a.active {
                    display: inline-block;
                    background: #3BB9EF;
                    color: #ffffff;
                }
    </style>

    <!--弹出层-->
    <style>
        .SogModal {
            position: absolute;
            top: 0px;
            left: 50%;
            margin-left: -300px;
            margin-top: -700px;
            z-index: 1050;
            width: 600px;
            display: none;
        }

            .SogModal .modal_title {
                height: 40px;
                line-height: 40px;
                color: rgb(255, 255, 255);
                background: rgb(59, 185, 239);
                text-align: center;
                border-radius: 6px 6px 0px 0px;
                position: relative;
                font-weight: bold;
                cursor: move;
            }

        .modal_item {
            padding: 7px 0px;
        }

        .modal_item_title {
            text-align: right;
            font-size: 12px;
            font-weight: bold;
            cursor: pointer;
            width: 100px;
            display: inline-block;
            margin-right: 30px;
        }

        .modal_item_content {
            width: 400px;
            display: inline-block;
        }

        .modal_content {
            background-color: #fff;
            max-height: 600px;
            overflow-y: auto;
            min-height: 300px;
        }

        .modal_function {
            height: 50px;
            text-align: center;
            border-top: 1px solid #b7e5f9;
            padding-top: 10px;
            background-color: #fff;
            border-radius: 0px 0px 6px 6px;
        }

        .btn_full {
            display: inline-block;
            width: 80px;
            height: 30px;
            text-align: center;
            background-color: rgb(59, 185, 239);
            border: 1px solid rgb(59, 185, 239);
            color: #fff;
            line-height: 30px;
            border-radius: 20px;
            cursor: pointer;
        }

        .btn_empty {
            display: inline-block;
            width: 80px;
            height: 30px;
            text-align: center;
            background-color: #fff;
            border: 1px solid rgb(59, 185, 239);
            color: rgb(59, 185, 239);
            line-height: 30px;
            border-radius: 20px;
            cursor: pointer;
        }

        .modal_title i {
            padding: 10px 10px 0px 0px;
            font-size: 22px;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!--头部区域-->
        <div class="SogHead">
            <span class="SogTitle">合作伙伴</span>

            <div class="SogNavigate">
                <a>我的渠道</a>
                <a>我的渠道</a>
                <a class="active">全部渠道</a>
            </div>

            <div class="SogRight">
                <span class="add_botton">+</span>
            </div>
        </div>

        <!--条件区域-->
        <div class="SogCondition">
            <div class="open" style="display: none;">
                <div class="condition_item">
                    <div class="condition_head">班级属性</div>

                    <a>全部</a>
                    <a>公益课</a>
                    <a class="active">小咨询</a>
                    <a>推广课</a>
                    <a>大咨询</a>
                </div>


                <div class="condition_item">
                    <div class="condition_head">班级属性</div>
                    <a>全部</a>
                    <a>公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a class="active">小咨询公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>推广课公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>大咨询公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>小咨询公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>推广课公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>大咨询公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>小咨询公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>推广课公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>大咨询公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>小咨询公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>推广课公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                    <a>大咨询公益课是的发送到发送到发送到发达是大法师打发斯蒂芬</a>
                </div>

                <div class="condition_item">
                    <div class="condition_head">班级属性</div>

                    <a>公益课</a>
                    <a>私房课</a>
                    <a>小咨询</a>
                    <a class="active">推广课</a>
                    <a>大咨询</a>
                    <a>小咨询</a>
                    <a>推广课</a>
                    <a>大咨询</a>
                    <a>小咨询</a>
                    <a>推广课</a>
                    <a>大咨询</a>
                    <a>小咨询</a>
                    <a>推广课</a>
                    <a>大咨询</a>
                    <a>小咨询</a>
                    <a>推广课</a>
                    <a>大咨询</a>
                    <a>小咨询</a>
                    <a>推广课</a>
                    <a>大咨询</a>

                </div>


                <div class="condition_item" style="padding-left: 100px">
                    <div class="SogTextSearch">
                        <span>筛选</span>
                        <input type="text" placeholder="渠道/省份/城市" />
                        <span><i class="fa fa-search" aria-hidden="true"></i>搜索</span>
                    </div>
                </div>



            </div>
            <div class="close">
                <div class="condition_head">筛选条件</div>

                <div class="condition_desc">
                    <span>渠道分类:</span>
                    <span>全部</span>
                </div>

                <div class="condition_desc">
                    <span>渠道分类:</span>
                    <span>全部</span>
                </div>

                <div class="condition_desc">
                    <span>渠道分类:</span>
                    <span>全部</span>
                </div>

                <div class="condition_desc">
                    <span>渠道分类:</span>
                    <span>全部</span>
                </div>

                <div class="condition_desc">
                    <span>渠道分类:</span>
                    <span>全部</span>
                </div>
            </div>
            <div class="handler">
                <i class="small_btn fa fa-chevron-down"></i>
                <!--<span class="small_btn span_open"></span>-->
            </div>
        </div>

        <!--功能区域-->
        <div class="SogFunction">
            <span>已选</span>
            <span class="number">6</span>
            <span>合作伙伴</span>

            <button class="btn green_btn">删除</button>
            <button class="btn green_btn">公海</button>

            <button class="btn green_btn SogRight">公海1</button>
            <button class="btn green_btn SogRight">公海2</button>
            <button class="btn green_btn SogRight">公海3</button>
        </div>

        <!--表格区域-->
        <div class="SogContent">
            <table class="SogTable">
                <tr>
                    <th class="SogTdCheck">
                        <input class="check_all" type="checkbox">
                    </th>
                    <th>类别名称</th>
                    <th class="w-80 text-center">图片封面</th>
                    <th class="w-80">状态 </th>
                    <th class="w-80">操作</th>
                    <th class="w-80">操作</th>
                    <th class="w-80">操作</th>
                    <th class="w-80">操作</th>
                </tr>
                <tr>
                    <td class="chec-width ">
                        <input data-id="0" class="check_one" type="checkbox">
                    </td>
                    <td>{{value.MT_NAME}}</td>
                    <td class="text-center">
                        <img src="{{value.MT_COVER_IMAGE}}" alt=""></td>
                    <td class="status text-green">{{value.MT_ACTIVE}}</td>
                    <td class="status text-red">{{value.MT_ACTIVE}}</td>
                    <td class="w-80">操作</td>
                    <td class="w-80">操作</td>
                    <td class="operation">
                        <button class="btn green_btn sog_eidt">编辑</button>
                    </td>
                </tr>

                <tr>
                    <td class="chec-width ">
                        <input data-id="0" class="check_one" type="checkbox">
                    </td>
                    <td>{{value.MT_NAME}}</td>
                    <td class="text-center">
                        <img src="{{value.MT_COVER_IMAGE}}" alt=""></td>
                    <td class="status text-green">{{value.MT_ACTIVE}}</td>
                    <td class="status text-red">{{value.MT_ACTIVE}}</td>
                    <td class="w-80">操作</td>
                    <td class="w-80">操作</td>
                    <td class="operation">
                        <button class="btn green_btn">编辑</button>
                    </td>
                </tr>

                <tr>
                    <td class="chec-width ">
                        <input data-id="0" class="check_one" type="checkbox">
                    </td>
                    <td>{{value.MT_NAME}}</td>
                    <td class="text-center">
                        <img src="{{value.MT_COVER_IMAGE}}" alt=""></td>
                    <td class="status text-green">{{value.MT_ACTIVE}}</td>
                    <td class="status text-red">{{value.MT_ACTIVE}}</td>
                    <td class="w-80">操作</td>
                    <td class="w-80">操作</td>
                    <td class="operation">
                        <button type="button" class="btn btn-info update">编辑</button>
                    </td>
                </tr>
            </table>
            <div class="SogPages">

                <a>1</a>
                <a class="active">2</a>
                <a>3</a>
                <a>4</a>
                <a>5</a>
                <a>...</a>
            </div>
        </div>

        <div class="SogModal" id="div_add">
            <div class="modal_title">
                标题1
            <i class="fa fa-times-circle SogRight CoverClose" aria-hidden="true"></i>
            </div>
            <div class="modal_content">
                <div class="modal_item">
                    <span class="modal_item_title">公司名字</span>
                    <div class="modal_item_content">
                        <input class="SogControl SOGWarming" />
                    </div>
                </div>

                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>


                <div class="modal_item">
                    <span class="modal_item_title">渠道商名字可以</span>
                    <div class="modal_item_content">
                        <input class="SogControl" placeholder="渠道商名字可以" />
                    </div>
                </div>

                <div class="modal_item">
                    <span class="modal_item_title">阿手动阀是大法师打发斯蒂芬</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>


                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>

                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <select class="SogControl">
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                        </select>
                    </div>
                </div>

                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>

                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>
                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>
                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>
                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>
                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>
                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>
                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>
                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>

            </div>


            <div class="modal_function">
                <span class="btn_full">提交</span>
                <span class="btn_empty CoverClose">取消</span>
            </div>
        </div>

        <div class="SogModal" id="div_edit">
            <div class="modal_title">
                编辑
            <i class="fa fa-times-circle SogRight CoverClose" aria-hidden="true"></i>
            </div>
            <div class="modal_content">
                <div class="modal_item">
                    <span class="modal_item_title">公司名字</span>
                    <div class="modal_item_content">
                        <input class="SogControl SOGWarming" />
                    </div>
                </div>

                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>


                <div class="modal_item">
                    <span class="modal_item_title">渠道商名字可以</span>
                    <div class="modal_item_content">
                        <input class="SogControl" placeholder="渠道商名字可以" />
                    </div>
                </div>

                <div class="modal_item">
                    <span class="modal_item_title">阿手动阀是大法师打发斯蒂芬</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>


                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <input class="SogControl" />
                    </div>
                </div>

                <div class="modal_item">
                    <span class="modal_item_title">姓名</span>
                    <div class="modal_item_content">
                        <select class="SogControl">
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                            <option value="a">a</option>
                        </select>
                    </div>
                </div>
            </div>


            <div class="modal_function">
                <span class="btn_full">提交</span>
                <span class="btn_empty CoverClose">取消</span>
            </div>
        </div>

        <!--脚本交互-->
        <script type="text/javascript">
            $(".handler .small_btn").click(function () {
                $(".SogCondition .open").slideToggle();
                $(".SogCondition .close").slideToggle();
                if ($(this).hasClass("fa-chevron-down")) {
                    $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
                } else {
                    $(this).addClass("fa-chevron-down").removeClass("fa-chevron-up");
                }
            })

            $(".add_botton").click(function () {
                ShowUI("div_edit");
            })

            //显示UI
            function ShowUI(control_id) {
                //1显示遮罩层
                Cover(true);

                //2动画显示目标
                $("#" + control_id).css("display", "inline-block");
                $("#" + control_id).css("margin-left", "-300px;");
                $("#" + control_id).animate({ marginTop: "30px" }, 150);

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
                    e.css("opacity", "0.5");
                    e.css("transition", "opacity 0.15s linear");
                    console.log(e);
                    $("body").append(e);
                } else {
                    $("#" + id).remove();
                }
            }

            $(".SogNavigate a").click(function () {
                $(this).addClass("active").siblings().removeClass("active");
            })

            $(".SogPages a").click(function () {
                $(this).addClass("active").siblings().removeClass("active");
            })

            $(".condition_item a").click(function () {
                $(this).addClass("active").siblings().removeClass("active");
            })



        </script>

        <script type="text/javascript">

            $(".modal_content").on("mouseover", ".SOGWarming", function (e) {
                console.log(e);
            })

        </script>
    </form>
</body>
</html>
