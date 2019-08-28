<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo3.aspx.cs" Inherits="ERPBase.demo.demo3" %>

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
      </form>
    <script>
        $(".handler .small_btn,.close").click(function () {
            $(".SogCondition .open").slideToggle();
            $(".SogCondition .close").slideToggle();
            if ($(this).hasClass("fa-chevron-down")) {
                $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
            } else {
                $(this).addClass("fa-chevron-down").removeClass("fa-chevron-up");
            }
        })

        $(".SogNavigate a").click(function () {
            $(this).addClass("active").siblings().removeClass("active");
            $(this).parent().attr("value", $(this).attr("value"));
        });

        $(".condition_item a").click(function () {
            $(this).addClass("active").siblings().removeClass("active");
            $(this).parent().attr("value", $(this).attr("value"));
            var id = $(this).parent().attr("id");
            //console.log(id);
            var text = $(this).text();
            //console.log(text);
            $("." + id).text(text);
        });

        $('.SogContent').on('click', '.SogPages a', function () {
            $(this).addClass("active").siblings().removeClass("active");
            $(this).parent().attr("value", $(this).attr("value"));
        });

        $('.SogContent').on('click', '.check_all', function () {
            $(".check_one").prop("checked", this.checked);
        });

        $(".add_botton").click(function () {
            ShowUI("div_add");
        });


        $('.SogContent').on('click', '.Iedit', function () {
            ShowUI("div_add");
        });


        $.ajax({
            type: "POST",
            url: "/sys/AjaxData.aspx?action=GetTable",
            dataType: "json",
            success: function (result) {
                $(".SogContent").html(result.message);
            }
        });

    </script>
</body>
</html>
