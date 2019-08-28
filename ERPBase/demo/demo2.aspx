<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo2.aspx.cs" Inherits="ERPBase.demo.demo2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/css/font-awesome4.7/css/font-awesome.min.css" />
    <link href="/css/maintenance.css" rel="stylesheet" />
    <link href="/css/jquery.toast.css" rel="stylesheet" />
    <script src="/js/jquery-1.11.0.min.js"></script>
    <script src="/js/SogModal.js"></script>
    <script src="/js/jquery.toast.js"></script>
    <script src="/js/maintenance.js"></script>
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

            <span class="btn green_btn">删除</span>
            <span class="btn green_btn">公海</span>

            <span class="btn green_btn SogRight">公海1</span>
            <span class="btn green_btn SogRight">公海2</span>
            <span class="btn green_btn SogRight">公海3</span>
        </div>

        <!--表格区域-->
        <div class="SogContent">
            <table class="SogTable">
                <tr>
                    <th class="SogTdCheck">
                        <input class="check_all" type="checkbox" />
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
                    <td>
                        <input data-id="0" class="check_one" type="checkbox" />
                    </td>
                    <td>{{value.MT_NAME}}</td>
                    <td class="text-center">
                        <img src="{{value.MT_COVER_IMAGE}}" alt=""></td>
                    <td class="status text-green">{{value.MT_ACTIVE}}</td>
                    <td class="status text-red">{{value.MT_ACTIVE}}</td>
                    <td class="w-80">操作</td>
                    <td class="w-80">操作</td>
                    <td class="operation">
                        <span class="btn green_btn sog_eidt">编辑</span>
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
                        <span class="btn green_btn">编辑</span>
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
                        <span class="btn btn-info update">编辑</span>
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
                标题
            <i class="fa fa-times-circle SogRight CoverClose" aria-hidden="true"></i>
            </div>
            <div class="modal_content">
                <div class="modal_item">
                    <span class="modal_item_title">公司名字</span>
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
            });

            $(".SogNavigate a").click(function () {
                $(this).addClass("active").siblings().removeClass("active");
            })

            $(".SogPages a").click(function () {
                $(this).addClass("active").siblings().removeClass("active");
                //ErrorBox("保存失败");
            })

            $(".condition_item a").click(function () {
                $(this).addClass("active").siblings().removeClass("active");
            });

            //$(".green_btn").click(function () {
            //    ShowLoading();
            //    window.setTimeout(HideLoading, 3000);
            //    return false;
            //});


        </script>
    </form>
</body>
</html>
