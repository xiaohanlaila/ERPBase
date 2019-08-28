<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="ERPBase.H5.work_follow.add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=0" />
    <title></title>
    <link href="/H5/css/font-awesome4.7/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/H5/css/work_follow.css?id=16" rel="stylesheet" />
    <link href="/H5/css/pick.css" rel="stylesheet" />

    <script src="/H5/js/jquery-1.11.0.min.js"></script>
    <script src="/H5/js/datepicker2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <iframe src="/H5/select_user.aspx" class="iframe_user"></iframe>
    </form>
    <script src="/H5/js/work_follow.js?id=15"></script>
    <script>
        $(".Iback").click(function () {
            BackToApp();
        })

        $('.IData').blur(function () {
            document.body.scrollTop = 0;
        });

        $(".Imore").click(function () {
            list_show();
        })

        //日期控件初始化
        $(".datetime").click(function () {
            var _this = this;
            new DatePicker({
                "type": 5,//0年, 1年月, 2月日, 3年月日
                "title": '请选择日期',//标题(可选)
                "maxYear": "",//最大年份（可选）
                "minYear": "",//最小年份（可选）
                "separator": "-",//分割符(可选)
                "defaultValue": _this.value,//默认值（可选）
                "callBack": function (val) {
                    _this.value = val;
                }
            });
        });

        //日期控件初始化
        $(".date").click(function () {
            var _this = this;
            new DatePicker({
                "type": 3,//0年, 1年月, 2月日, 3年月日
                "title": '请选择日期',//标题(可选)
                "maxYear": "",//最大年份（可选）
                "minYear": "",//最小年份（可选）
                "separator": "-",//分割符(可选)
                "defaultValue": _this.value,//默认值（可选）
                "callBack": function (val) {
                    _this.value = val;
                }
            });
        });

        function get_work_form_waitting() {
            var json_post = {};
            json_post["function_code"] = $(".container").attr("value");
            var url = "/H5/work_follow/ajax.aspx?action=get_work_form_waitting"
            var json = Ajax(url, json_post, function (data) {
                if (data.status == "1") {
                    if (data.message == "0") {
                        $(".icon_msg_num").addClass("hide1");
                    } else {
                        $(".icon_msg_num").removeClass("hide1").text(data.message);
                    }
                }
            });
        }
        get_work_form_waitting();

        //新增时点击保存按钮
        $(".btn").click(function (e) {
            //锁定控件
            var this_control = this;
            LockControl(this_control);

            //检查数据
            var json_post = {};
            var IsAllTrue = true;
            $(".IData").each(function () {
                if (!CheckControl(this)) {
                    OpenControl(this_control);
                    IsAllTrue = false;
                    return false;
                }
                json_post[$(this).attr("target")] = GetControlValue(this);
            })
            json_post["function_code"] = $(".container").attr("value");

            if (!IsAllTrue) {
                return false;
            }

            //审批人检查
            var to_user = $(".person_select").attr("value");
            if (IsNullOrEmpty(to_user)) {
                IsAllTrue = false;
                MessageBox("请选择审批人");
                OpenControl(this_control);
            }
            json_post["to_user"] = to_user;


            if (!IsAllTrue) {
                return false;
            }

            //组织数据提交
            var url = "/H5/work_follow/ajax.aspx?action=add_work_form"
            var json = Ajax(url, json_post);

            OpenControl(this);//接口返回后，解控控件
            if (json.status == 1) {
                //清除控件
                $(".IData").each(function () {
                    ClearControlValue(this);
                });

                //显示动画效果
                var endP = {
                    x: $('.icon_msg_num').offset().left,
                    y: $('.icon_msg_num').offset().top,
                }
                var starP = {
                    x: e.pageX,
                    y: e.pageY
                };
                animateTwoPoint(starP, endP, function () {
                    var icon_msg_num = parseInt($(".icon_msg_num").text()) + 1;
                    $(".icon_msg_num").removeClass("hide1").text(icon_msg_num);
                })
            } else {
                MessageBox(json.message);
            }

        });
    </script>
</body>
</html>
