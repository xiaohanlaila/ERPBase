<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/bootstrap.css" rel="stylesheet" />
    <link href="/css/font-awesome4.7/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/css/animate.css" rel="stylesheet" />
    <link href="/css/easyui.css" rel="stylesheet" />
    <link href="/css/IndexNew.css" rel="stylesheet" />
    <script src="/js/jquery-1.11.0.min.js"></script>
    <script src="/js/bootstrap.js"></script>
    <script src="/js/jquery.slimscroll.js"></script>
    <script src="/js/jquery.easyui.min.js"></script>
</head>
<body>

    <div class="nav_header">
        <div class="nav_header_logo" id="nav_header_logo">
            <span>中力知识科技中力知识科技中力知识科技</span>
        </div>

        <div class="nav_header_shrink" turn="off">
            <span class="turn_off"></span>
        </div>

        <div class="aside_head_list" id="logout">
            <small class="icon-close"></small>
            <p>
                退出
            </p>
        </div>

    </div>
    <div class="nav_content">
        <div class="nav_aside">
            <div class="nav_aside_body">
                <div class="nav_aside_head">
                    <div class="aside_head_img">
                        <img class="nav_aside_img" />
                    </div>
                    <div class="aside_head_name">
                        张小凡
                    </div>
                    <div class="aside_head_place">
                        股权激励讲师
                    </div>
                </div>
                <div class="collapse_content">
                    <ul id="accordion">
                        <!--<li>
                            <div class="link head_icon1">
                                渠道商管理
                                <i class="fa fa-angle-down"></i>
                            </div>
                            <ul class="submenu">
                                <li><span class="head_list_icon1">渠道商类型设置</span></li>
                                <li><span class="head_list_icon2">渠道商设置</span></li>
                            </ul>
                        </li>
                        <li>
                            <div class="link head_icon2">
                                活动
                                <i class="fa fa-angle-down"></i>
                            </div>
                            <ul class="submenu">
                                <li><span class="head_list_icon3">大讲堂</span></li>
                                <li><span class="head_list_icon4">申请公益</span></li>
                            </ul>
                        </li>
                        <li>
                            <div class="link head_icon3">
                                顾问管理
                                <i class="fa fa-angle-down"></i>
                            </div>
                            <ul class="submenu">
                                <li><span class="head_list_icon5">顾问行程</span></li>
                                <li><span class="head_list_icon6">课程类型维护</span></li>
                                <li><span class="head_list_icon1">分配会员顾问</span></li>
                                <li><span class="head_list_icon2">讲师确认</span></li>
                            </ul>
                        </li>
                        <li>
                            <div class="link head_icon4">
                                课程管理
                                <i class="fa fa-angle-down"></i>
                            </div>
                            <ul class="submenu">
                                <li><span class="head_list_icon3">渠道管理</span></li>
                                <li><span class="head_list_icon4">客户分配</span></li>
                            </ul>
                        </li>-->
                    </ul>
                </div>
            </div>
        </div>
        <div class="nav_section">
            <div class="nav_section_body">
                <div id="tab_content" class="easyui-tabs" data-options="tools:'#tab-tools'">
                    <div title="Home">
                        <iframe scrolling="auto" frameborder="0" src="https://www.baidu.com/" style="width: 100%; box-sizing: border-box; height: 100%;"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        //页面加载中上出现进度条效果
        function LoadingPage() {
            var e_id = "web_loading";
            var html = '<div id="' + e_id + '"></div>';
            var e = $(html);
            e.attr("style", "z-index:99999;height:3px;position:absolute;top:0px;left:0px;background:#3BB9EF;");
            $(".nav_section_body").append(e);
            e.width("0").show();
            e.animate({ width: "100%" }, 800, function () {
                e.fadeOut(500);
                setTimeout(function () {
                    e.remove();
                }, 500);

            });
        }

        //左侧菜单功能实现，手风琴效果
        function Accordion() {
            var Accordion = function (el, multiple) {
                this.el = el || {};
                this.multiple = multiple || false;

                // Variables privadas
                var links = this.el.find('.link');
                // Evento
                links.on('click', { el: this.el, multiple: this.multiple }, this.dropdown)
            }

            Accordion.prototype.dropdown = function (e) {
                var $el = e.data.el;
                $this = $(this),
                $next = $this.next();

                $next.slideToggle();
                $this.parent().toggleClass('open');

                if (!e.data.multiple) {
                    $el.find('.submenu').not($next).slideUp().parent().removeClass('open');
                };
            }

            var accordion = new Accordion($('#accordion'), false);
        }

        //打开页面函数
        function tabs(liKey, dataUrl) {
            if ($('#tab_content').tabs('exists', liKey)) {
                $('#tab_content').tabs('select', liKey);

            } else {
                var content = '<iframe scrolling="auto" frameborder="0"  src="' + dataUrl + '" style="width:100%;box-sizing:border-box;height:100%;"></iframe>';
                $('#tab_content').tabs('add', {
                    title: liKey,
                    content: content,
                    closable: true
                });
                LoadingPage();
            }
        }

        //生成左侧菜单
        function GetFunction() {
            var s = '{"status":1,"message":"加载成功","items":[{"FG_CODE":"channel_business_new1","FG_DESC":"渠道管理","FG_SEQ":1.0,"FG_URL":"","FG_CLASS_ID":"51","FG_TYPE":"user"},{"FG_CODE":"customer_management_new","FG_DESC":"客户管理","FG_SEQ":2.0,"FG_URL":"","FG_CLASS_ID":null,"FG_TYPE":"user"},{"FG_CODE":"IntegrationAdmin_new","FG_DESC":"集服管理","FG_SEQ":10.0,"FG_URL":"","FG_CLASS_ID":"47","FG_TYPE":"user"},{"FG_CODE":"class_management","FG_DESC":"开班管理","FG_SEQ":13.0,"FG_URL":"","FG_CLASS_ID":"41","FG_TYPE":"user"},{"FG_CODE":"MemberSS","FG_DESC":"会员专区","FG_SEQ":20.0,"FG_URL":"","FG_CLASS_ID":"10","FG_TYPE":"user"},{"FG_CODE":"product_info","FG_DESC":"产品信息","FG_SEQ":23.0,"FG_URL":"","FG_CLASS_ID":"53","FG_TYPE":"user"},{"FG_CODE":"ProductManagement","FG_DESC":"产品管理","FG_SEQ":24.0,"FG_URL":"","FG_CLASS_ID":"46","FG_TYPE":"user"},{"FG_CODE":"CONSULTING","FG_DESC":"咨询项目","FG_SEQ":25.0,"FG_URL":"","FG_CLASS_ID":"35","FG_TYPE":"user"},{"FG_CODE":"Consulting_management","FG_DESC":"咨询管理","FG_SEQ":27.0,"FG_URL":"","FG_CLASS_ID":"44","FG_TYPE":"user"},{"FG_CODE":"task_byjsc","FG_DESC":"任务管理","FG_SEQ":30.0,"FG_URL":"","FG_CLASS_ID":"52","FG_TYPE":"user"},{"FG_CODE":"personal_trip","FG_DESC":"行程管理","FG_SEQ":31.0,"FG_URL":"","FG_CLASS_ID":"30","FG_TYPE":"user"},{"FG_CODE":"Finance","FG_DESC":"财务管理","FG_SEQ":32.0,"FG_URL":"","FG_CLASS_ID":"21","FG_TYPE":"user"},{"FG_CODE":"FileSystem","FG_DESC":"文件系统","FG_SEQ":33.0,"FG_URL":"","FG_CLASS_ID":"20","FG_TYPE":"user"},{"FG_CODE":"form","FG_DESC":"表单管理","FG_SEQ":43.0,"FG_URL":"","FG_CLASS_ID":null,"FG_TYPE":"user"},{"FG_CODE":"WX","FG_DESC":"微信信息","FG_SEQ":46.0,"FG_URL":"","FG_CLASS_ID":null,"FG_TYPE":"user"},{"FG_CODE":"report_management","FG_DESC":"报表管理","FG_SEQ":47.0,"FG_URL":"","FG_CLASS_ID":null,"FG_TYPE":"user"},{"FG_CODE":"Book","FG_DESC":"书籍订单","FG_SEQ":48.0,"FG_URL":"","FG_CLASS_ID":null,"FG_TYPE":"user"},{"FG_CODE":"industry","FG_DESC":"中力产业","FG_SEQ":49.0,"FG_URL":"","FG_CLASS_ID":null,"FG_TYPE":"user"},{"FG_CODE":"temp","FG_DESC":"临时业务","FG_SEQ":100.0,"FG_URL":"","FG_CLASS_ID":null,"FG_TYPE":"user"}]}';
            var data = $.parseJSON(s);
            $(".my_items").remove();
            if (data.status == 1) {
                var html = "";
                for (var i = 0; i < data.items.length; i++) {
                    if (data.items[i].FG_URL != null && data.items[i].FG_URL != "") {
                        html += '<li class="my_items specil_items">'
                    } else {
                        html += '<li class="my_items">'
                    }
                    html += '<div data_url="' + data.items[i].FG_URL + '" class="link head_icon' + data.items[i].FG_CLASS_ID + '">'
                                + data.items[i].FG_DESC
                                + '<i class="fa fa-angle-down"></i>'
                            + '</div>'
                            + '<ul class="submenu" id="' + data.items[i].FG_CODE + '">'
                            + '</ul>'
                        + '</li>'
                }
                $("#accordion").html(html);
            } else {

            }


            var data = { "status": 1, "message": "加载成功", "items": [{ "fn_code": "1101_1", "fn_desc": "会员列表(管理员)", "fn_file_location": "/Admin/pages/material-classify.html", "fn_group": "MemberAdmin", "fn_class_id": null }, { "fn_code": "1101_2", "fn_desc": "会员企业分析", "fn_file_location": "Report/CompanyAnalysis/JFMemberAnalysis.aspx?type=0", "fn_group": "company_analysis", "fn_class_id": null }, { "fn_code": "1101_3", "fn_desc": "会员企业分析(管)", "fn_file_location": "Report/CompanyAnalysis/JFMemberAnalysis.aspx?type=1", "fn_group": "company_analysis_manage", "fn_class_id": null }, { "fn_code": "1101_4", "fn_desc": "输入企业分析", "fn_file_location": "Report/CompanyAnalysis/JFImportEnterprise.aspx?type=0", "fn_group": "company_analysis", "fn_class_id": null }, { "fn_code": "1101_5", "fn_desc": "输入企业分析(管)", "fn_file_location": "Report/CompanyAnalysis/JFImportEnterprise.aspx?type=1", "fn_group": "company_analysis_manage", "fn_class_id": null }, { "fn_code": "1101_6", "fn_desc": "成交企业分析", "fn_file_location": "Report/CompanyAnalysis/JFClosingEnterprise.aspx?type=0", "fn_group": "company_analysis", "fn_class_id": null }, { "fn_code": "1101_7", "fn_desc": "成交企业分析(管)", "fn_file_location": "Report/CompanyAnalysis/JFClosingEnterprise.aspx?type=1", "fn_group": "company_analysis_manage", "fn_class_id": null }, { "fn_code": "196", "fn_desc": "房型", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=HA_TYPE&FUNCTION_DESC=房型", "fn_group": "SystemSetup", "fn_class_id": null }, { "fn_code": "2044_1", "fn_desc": "会员专区沟通目的", "fn_file_location": "MemberSS/APTraineeCompany/Communicative_JF.aspx?status=2", "fn_group": "MemberSetup", "fn_class_id": null }, { "fn_code": "2044_2", "fn_desc": "渠道沟通目的维护", "fn_file_location": "channel_manage/Communicative_channel.aspx?status=3", "fn_group": "channel_setting", "fn_class_id": null }, { "fn_code": "2047", "fn_desc": "系统管理", "fn_file_location": "admin/AP_SYSTEM.aspx", "fn_group": "admin", "fn_class_id": "192" }, { "fn_code": "2052_2", "fn_desc": "会员参课分析", "fn_file_location": "Report/CompanyAnalysis/JFClassReport.aspx", "fn_group": "company_analysis", "fn_class_id": null }, { "fn_code": "2052_3", "fn_desc": "会员参课分析(管)", "fn_file_location": "Report/CompanyAnalysis/ManageJFClassReport.aspx", "fn_group": "company_analysis_manage", "fn_class_id": null }, { "fn_code": "2058", "fn_desc": "用户设置（开发者）", "fn_file_location": "maintenance/user_maintenance_admin.aspx", "fn_group": "admin", "fn_class_id": "203" }, { "fn_code": "2059", "fn_desc": "课程开班分析报表", "fn_file_location": "Report/CustomerAnalysis/AnalysisReport.aspx", "fn_group": "Report", "fn_class_id": "204" }, { "fn_code": "2077", "fn_desc": " 功能组类型", "fn_file_location": "admin/AP_FUNCTION_TYPE.aspx", "fn_group": "admin", "fn_class_id": null }, { "fn_code": "2086", "fn_desc": "产品管理", "fn_file_location": "ProductManagement/AP_PRODUCT_MANAGE.aspx", "fn_group": "ProductManagement", "fn_class_id": null }, { "fn_code": "2087", "fn_desc": "产品类别", "fn_file_location": "ProductManagement/AP_PRODUCT_CATEGORY.aspx", "fn_group": "ProductManagement", "fn_class_id": null }, { "fn_code": "2089", "fn_desc": "会员参课(管理员)", "fn_file_location": "Report/CustomerAnalysis/ClassReport.aspx?UR_TYPE=lecturer_admin", "fn_group": "MemberAdmin", "fn_class_id": null }, { "fn_code": "2100", "fn_desc": "问卷", "fn_file_location": "Questionnaire/AP_QUESTIONNAIRE.aspx", "fn_group": "SystemSetup", "fn_class_id": null }, { "fn_code": "2107", "fn_desc": "渠道列表(管)", "fn_file_location": "channel_manage/channel_manage_list.aspx", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2108", "fn_desc": "推广会(管)", "fn_file_location": "channel_manage/channel_promotion_meeting.aspx", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2109", "fn_desc": "企业管理(管)", "fn_file_location": "channel_manage/channel_company_manage.aspx?status=1", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2113", "fn_desc": "业务员分析(管)", "fn_file_location": "channel_manage/UserRankings.aspx", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2114", "fn_desc": "渠道组分析(管)", "fn_file_location": "channel_manage/DepartmentRankings.aspx", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2115", "fn_desc": "推广会分析(管)", "fn_file_location": "channel_manage/PromotionAnalysis.aspx", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2116", "fn_desc": "输送企业分析(管)", "fn_file_location": "channel_manage/TransportationEnterprise.aspx", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2117", "fn_desc": "全景分析(管)", "fn_file_location": "channel_manage/PanoramicAnalysis.aspx", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2118", "fn_desc": "渠道联系记录", "fn_file_location": "channel_manage/channel_record_list.aspx", "fn_group": "channel_setting", "fn_class_id": null }, { "fn_code": "2119", "fn_desc": "联系分析图", "fn_file_location": "channel_manage/ContactAnalysisChart.aspx", "fn_group": "channel_setting", "fn_class_id": null }, { "fn_code": "2112", "fn_desc": "渠道开发分析(管)", "fn_file_location": "channel_manage/channel_manage_analysis.aspx", "fn_group": "channel_manage", "fn_class_id": null }, { "fn_code": "2125", "fn_desc": "渠道黑名单", "fn_file_location": "channel_business_new/AP_BLACKLIST.aspx", "fn_group": "channel_setting", "fn_class_id": null }, { "fn_code": "2130", "fn_desc": "接收企业", "fn_file_location": "CSAdmin/Receive_Customers.aspx", "fn_group": "customer_management_new", "fn_class_id": null }, { "fn_code": "2131", "fn_desc": "学员企业", "fn_file_location": "CSAdmin/Student_Customers.aspx", "fn_group": "customer_management_new", "fn_class_id": null }, { "fn_code": "2132", "fn_desc": "成交企业", "fn_file_location": "CSAdmin/Deal_Customers.aspx", "fn_group": "customer_management_new", "fn_class_id": null }, { "fn_code": "2150", "fn_desc": "定制班分配", "fn_file_location": "commercial_college/CustomizedProductList.aspx", "fn_group": "commercial_college", "fn_class_id": null }, { "fn_code": "6003", "fn_desc": "成交报表", "fn_file_location": "Report/DealReportNew.html", "fn_group": "report_management", "fn_class_id": null }, { "fn_code": "602", "fn_desc": "客户地区统计表", "fn_file_location": "Report/trainee_company_region_report.aspx", "fn_group": "Report", "fn_class_id": "67" }, { "fn_code": "194", "fn_desc": "酒店设置", "fn_file_location": "Hotel/ling/Hotel_List.html", "fn_group": "class_management", "fn_class_id": null }, { "fn_code": "201", "fn_desc": "渠道商类型", "fn_file_location": "maintenance/channel_business_category_maintenance_new.aspx", "fn_group": "channel_setting", "fn_class_id": "55" }, { "fn_code": "2018_3", "fn_desc": "商机", "fn_file_location": "consult_web/BusinessList.aspx", "fn_group": "industry", "fn_class_id": null }, { "fn_code": "2090", "fn_desc": "公司文件", "fn_file_location": "FileSystem/CompanyFile_V2.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "2090_1", "fn_desc": "公司文件(内网)", "fn_file_location": "EF.aspx?str_url=http://192.168.6.80:8001/FileSystem/CompanyFile_V2.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "2087_4", "fn_desc": "培训产品", "fn_file_location": "ProductManagement/ProductManagementNew.html?PC_ID=4&PM_ID=-1&verson=1", "fn_group": "product_info", "fn_class_id": null }, { "fn_code": "2060", "fn_desc": "咨询分配", "fn_file_location": "MemberSS/ConsultingProject/AP_CONSULTING_PROJECT.aspx", "fn_group": "Consulting_management", "fn_class_id": null }, { "fn_code": "2060_1", "fn_desc": "项目列表(查看)", "fn_file_location": "MemberSS/ConsultingProject/PROJECT_LIST.aspx", "fn_group": "CONSULTING", "fn_class_id": null }, { "fn_code": "187", "fn_desc": "开课分析", "fn_file_location": "Report/CustomerAnalysis/CourseAnalysisNew.aspx", "fn_group": "student_report", "fn_class_id": "191" }, { "fn_code": "141", "fn_desc": "会员审核", "fn_file_location": "Finance_Manage/Finance_audit_member.aspx", "fn_group": "Finance", "fn_class_id": "35" }, { "fn_code": "1103", "fn_desc": "会员分配", "fn_file_location": "MemberSS/MemberTeacherManage.aspx", "fn_group": "MemberAdmin", "fn_class_id": "14" }, { "fn_code": "124", "fn_desc": "参课分析", "fn_file_location": "Report/InClassAnalysis_New.aspx", "fn_group": "Report", "fn_class_id": "23" }, { "fn_code": "130", "fn_desc": "行程主页", "fn_file_location": "activity/course_plan_teacher_calendar.aspx?ur_type=lecturer", "fn_group": "personal_trip", "fn_class_id": "29" }, { "fn_code": "10001", "fn_desc": "抽奖", "fn_file_location": "lotteryDraw/temp_company.aspx", "fn_group": "temp", "fn_class_id": null }, { "fn_code": "1004", "fn_desc": "系统按钮维护", "fn_file_location": "SYS_DBManagement/button_maintenance.aspx", "fn_group": "admin", "fn_class_id": "4" }, { "fn_code": "102", "fn_desc": "组织架构", "fn_file_location": "maintenance/department_maintenance_new.aspx", "fn_group": "Maintenance", "fn_class_id": "6" }, { "fn_code": "1101", "fn_desc": "会员列表", "fn_file_location": "MemberSS/MemberList.aspx", "fn_group": "MemberSS", "fn_class_id": "12" }, { "fn_code": "6007", "fn_desc": "出货单", "fn_file_location": "Book/AP_BOOK_ORDER.aspx", "fn_group": "Book", "fn_class_id": null }, { "fn_code": "3211", "fn_desc": "渠道客户", "fn_file_location": "/trainee_center/Trainee_User_Receiving_Channel_Customers.aspx", "fn_group": "IntegrationAdmin_new", "fn_class_id": null }, { "fn_code": "6001", "fn_desc": "中力知识科技", "fn_file_location": "WX/AP_WX_Template.aspx?appID=wx3d2e921c0b275005", "fn_group": "WX", "fn_class_id": null }, { "fn_code": "6202", "fn_desc": "渠道商", "fn_file_location": "/demo/demo1.aspx", "fn_group": "channel_business_new1", "fn_class_id": null }, { "fn_code": "6601", "fn_desc": "表单管理", "fn_file_location": "question/AP_FORM_TABLE.html", "fn_group": "form", "fn_class_id": null }, { "fn_code": "8001", "fn_desc": "任务视图", "fn_file_location": "Task/index.html?v=8.3", "fn_group": "task_byjsc", "fn_class_id": null }, { "fn_code": "9105", "fn_desc": "客户基本信息", "fn_file_location": "/Report/sample_report.aspx?FunctionCode=9105", "fn_group": "Report", "fn_class_id": "140" }, { "fn_code": "212", "fn_desc": "功能组管理", "fn_file_location": "admin/AP_FUNCTION_GROUP.aspx", "fn_group": "admin", "fn_class_id": "142" }, { "fn_code": "2105", "fn_desc": "酒店房间配置", "fn_file_location": "CSAdmin/InvitationToClass.aspx?type=hotel_using", "fn_group": "class_management", "fn_class_id": null }, { "fn_code": "2110", "fn_desc": "部门文件(内网)", "fn_file_location": "EF.aspx?str_url=http://192.168.6.80:8001/FileSystem/DepartmentFile_V2.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "2122", "fn_desc": "成交确认(管)", "fn_file_location": "Finance_Manage/ManagePageNew.aspx", "fn_group": "Finance", "fn_class_id": null }, { "fn_code": "301", "fn_desc": "用户设置", "fn_file_location": "maintenance/user_maintenance_new.aspx", "fn_group": "Maintenance", "fn_class_id": "63" }, { "fn_code": "3126", "fn_desc": "客户分配", "fn_file_location": "trainee_center/Trainee_User_Distribution.aspx", "fn_group": "IntegrationAdmin_new", "fn_class_id": "25" }, { "fn_code": "213", "fn_desc": "功能管理", "fn_file_location": "admin/AP_FUNCTION.aspx", "fn_group": "admin", "fn_class_id": "143" }, { "fn_code": "3210", "fn_desc": "课程开班", "fn_file_location": "maintenance/course_plan_section.aspx", "fn_group": "class_management", "fn_class_id": "61" }, { "fn_code": "2132_1", "fn_desc": "企业列表(查看)", "fn_file_location": "MemberSS/ConsultingProject/MEMBER_LIST.aspx", "fn_group": "CONSULTING", "fn_class_id": null }, { "fn_code": "8002", "fn_desc": "任务列表", "fn_file_location": "Task/list.html?version=11", "fn_group": "task_byjsc", "fn_class_id": null }, { "fn_code": "903", "fn_desc": "部门文件", "fn_file_location": "FileSystem/DepartmentFile_V2.aspx", "fn_group": "FileSystem", "fn_class_id": "137" }, { "fn_code": "6602", "fn_desc": "报名列表", "fn_file_location": "question/AP_ANSWER_QUESTIONNAIRE.html", "fn_group": "form", "fn_class_id": null }, { "fn_code": "6002", "fn_desc": "测试专用", "fn_file_location": "WX/AP_WX_Template_user.aspx", "fn_group": "WX", "fn_class_id": null }, { "fn_code": "6008", "fn_desc": "出货单[财]", "fn_file_location": "Book/AP_BOOK_ORDER_FINANCE.aspx", "fn_group": "Book", "fn_class_id": null }, { "fn_code": "6021", "fn_desc": "前端引流企业", "fn_file_location": "/demo/demo2.aspx", "fn_group": "channel_business_new1", "fn_class_id": null }, { "fn_code": "10002", "fn_desc": "报名表", "fn_file_location": "Enlist/AP_ENLIST.aspx?en_business_type=channel&en_business_id=-1", "fn_group": "temp", "fn_class_id": null }, { "fn_code": "130_10", "fn_desc": "行程足迹", "fn_file_location": "activity/redirect_teacher.aspx", "fn_group": "personal_trip", "fn_class_id": null }, { "fn_code": "191_2", "fn_desc": "沟通方式管理", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=Communication_Mode&FUNCTION_DESC=沟通方式", "fn_group": "MemberSetup", "fn_class_id": "154" }, { "fn_code": "2087_5", "fn_desc": "会员产品", "fn_file_location": "ProductManagement/ProductManagementNew.html?PC_ID=5&PM_ID=-1&verson=1", "fn_group": "product_info", "fn_class_id": null }, { "fn_code": "2052_1", "fn_desc": "会员分析(管理员)", "fn_file_location": "Report/CustomerAnalysis/TeacherAnalysis.aspx?UR_TYPE=lecturer_admin", "fn_group": "MemberAdmin", "fn_class_id": null }, { "fn_code": "2049", "fn_desc": "客户分析", "fn_file_location": "Report/CustomerAnalysis/CustomerAnalysis.aspx", "fn_group": "student_report", "fn_class_id": "195" }, { "fn_code": "204", "fn_desc": "咨询产品", "fn_file_location": "/MemberSS/ConsultingProject/ProductManagement.html?type=2×tamp=1494550781521&CT_ID=20&CT_DESC=%25E8%25B5%2584%25E6%259C%25AC%25E8%25A7%2584%25E5%2588%2592%25E4%25B8%258E%25E5%25B8%2582%25E5%2580%25BC%25E7%25AE%25A1%25E7%2590%2586&CT_NICK_DESC=%25E8%25B5%2584%25E6%259C%25AC%25E5%25B8%2582%25E5%2580%25BC", "fn_group": "CONSULTING", "fn_class_id": null }, { "fn_code": "2087_6", "fn_desc": "小咨询产品", "fn_file_location": "ProductManagement/ProductManagementNew.html?PC_ID=7&PM_ID=-1&verson=1", "fn_group": "product_info", "fn_class_id": null }, { "fn_code": "2092", "fn_desc": "项目文件", "fn_file_location": "FileSystem/AP_PRODUCT_FILE_MANAGE.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "191_3", "fn_desc": "日常服务类型", "fn_file_location": "MemberSS/DailyServiceTypeManage.aspx", "fn_group": "MemberSetup", "fn_class_id": "155" }, { "fn_code": "190", "fn_desc": "登录日志", "fn_file_location": "admin/AP_LOGIN_LOG.aspx", "fn_group": "admin", "fn_class_id": "141" }, { "fn_code": "168", "fn_desc": "用户组维护", "fn_file_location": "maintenance/user_group_maintenance.aspx", "fn_group": "Maintenance", "fn_class_id": "51" }, { "fn_code": "130_11", "fn_desc": "行程排行榜", "fn_file_location": "activity/redirect_trip.aspx", "fn_group": "personal_trip", "fn_class_id": null }, { "fn_code": "10003", "fn_desc": "报名设置", "fn_file_location": "Enlist/AP_ENLIST_SETTING.aspx", "fn_group": "temp", "fn_class_id": null }, { "fn_code": "6121", "fn_desc": "推广营销活动", "fn_file_location": "/demo/demo3.aspx", "fn_group": "channel_business_new1", "fn_class_id": null }, { "fn_code": "6020", "fn_desc": "会员诊断", "fn_file_location": "Report/AP_DIAGNOSIS_COMPANY.aspx", "fn_group": "MemberAdmin", "fn_class_id": null }, { "fn_code": "6009", "fn_desc": "出货单[行]", "fn_file_location": "Book/AP_BOOK_ORDER_ASSISTANT.aspx", "fn_group": "Book", "fn_class_id": null }, { "fn_code": "4000", "fn_desc": "支付方式", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=CRE_PAYMENT_TYPE&FUNCTION_DESC=支付方式", "fn_group": "SystemSetup", "fn_class_id": null }, { "fn_code": "6603", "fn_desc": "报名分析", "fn_file_location": "question/FORM_REPORT.html", "fn_group": "form", "fn_class_id": null }, { "fn_code": "931_1", "fn_desc": "微信模板消息", "fn_file_location": "WX/AP_WX_Template1.aspx?appID=wx3d2e921c0b275005_1", "fn_group": "WX", "fn_class_id": null }, { "fn_code": "3207", "fn_desc": "课程邀约", "fn_file_location": "CSAdmin/InvitationToClass.aspx", "fn_group": "class_management", "fn_class_id": "61" }, { "fn_code": "3208", "fn_desc": "酒店预订", "fn_file_location": "CSAdmin/InvitationToClass.aspx?type=hotel_new", "fn_group": "class_management", "fn_class_id": "61" }, { "fn_code": "2123", "fn_desc": "财务确认", "fn_file_location": "Finance_Manage/FinacePageNew.aspx", "fn_group": "Finance", "fn_class_id": null }, { "fn_code": "2111", "fn_desc": "入组企业", "fn_file_location": "maintenance/course_plan_section_teacher.aspx", "fn_group": "MemberSS", "fn_class_id": null }, { "fn_code": "2087_3", "fn_desc": "咨询产品", "fn_file_location": "ProductManagement/ProductManagementNew.html?PC_ID=3&PM_ID=-1&verson=1", "fn_group": "product_info", "fn_class_id": null }, { "fn_code": "907", "fn_desc": "个人文件", "fn_file_location": "FileSystem/PrivateFile_V2.aspx", "fn_group": "FileSystem", "fn_class_id": "139" }, { "fn_code": "6604", "fn_desc": "报名客户", "fn_file_location": "question/AP_FORM_SALES.aspx", "fn_group": "form", "fn_class_id": null }, { "fn_code": "6213", "fn_desc": "联系人", "fn_file_location": "/channel_business_new/channel_business_users_all.aspx", "fn_group": "channel_business_new1", "fn_class_id": null }, { "fn_code": "4001", "fn_desc": "客户类型", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=CRE_TYPE&FUNCTION_DESC=客户类型", "fn_group": "SystemSetup", "fn_class_id": null }, { "fn_code": "10004", "fn_desc": "官网报名表", "fn_file_location": "Enlist/AP_ENLIST.aspx?en_business_type=guanwan&en_business_id=-1", "fn_group": "temp", "fn_class_id": null }, { "fn_code": "130_12", "fn_desc": "行程总结", "fn_file_location": "activity/redirect_summary.aspx", "fn_group": "personal_trip", "fn_class_id": null }, { "fn_code": "161", "fn_desc": "上市情况维护", "fn_file_location": "status/stauts_Company_Listed_Planning.aspx", "fn_group": "Maintenance", "fn_class_id": "46" }, { "fn_code": "130_4", "fn_desc": "行程排行榜", "fn_file_location": "Report\\TeacherTrip\\company_trip_report_chart.html", "fn_group": "student_report", "fn_class_id": "183" }, { "fn_code": "197", "fn_desc": "职称", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=USER_TYPE&FUNCTION_DESC=职称", "fn_group": "Maintenance", "fn_class_id": null }, { "fn_code": "191_4", "fn_desc": "业务类型", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=Service_Type&FUNCTION_DESC=业务类型", "fn_group": "MemberSetup", "fn_class_id": "156" }, { "fn_code": "2050", "fn_desc": "会员质量分析", "fn_file_location": "Report/CustomerAnalysis/NumberAnalysis.aspx", "fn_group": "student_report", "fn_class_id": "196" }, { "fn_code": "2051", "fn_desc": "学员质量分析", "fn_file_location": "Report/CustomerAnalysis/StudentAnalysis.aspx", "fn_group": "student_report", "fn_class_id": "197" }, { "fn_code": "2048", "fn_desc": "渠道分析", "fn_file_location": "Report/CustomerAnalysis/ChannelAnalysis.aspx", "fn_group": "channel_business_new1", "fn_class_id": "194" }, { "fn_code": "2074", "fn_desc": "我的项目", "fn_file_location": "MemberSS/ConsultingProject/ProjectHome.aspx", "fn_group": "CONSULTING", "fn_class_id": null }, { "fn_code": "191_5", "fn_desc": "上市规划管理", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=TC_Company_Listed_Planning&FUNCTION_DESC=上市规划", "fn_group": "MemberSetup", "fn_class_id": "157" }, { "fn_code": "130_13", "fn_desc": "行程图表", "fn_file_location": "activity/redirect_chart.aspx", "fn_group": "personal_trip", "fn_class_id": null }, { "fn_code": "10005", "fn_desc": "官网报名表1", "fn_file_location": "Enlist/AP_ENLIST.aspx?en_business_type=guanwan1&en_business_id=-1", "fn_group": "temp", "fn_class_id": null }, { "fn_code": "1005", "fn_desc": "系统消息模板", "fn_file_location": "maintenance/message_demo_maintenance.aspx", "fn_group": "admin", "fn_class_id": "5" }, { "fn_code": "904", "fn_desc": "共享文件", "fn_file_location": "FileSystem/ShareFile_V3.aspx", "fn_group": "FileSystem", "fn_class_id": "138" }, { "fn_code": "904_1", "fn_desc": "共享文件(内网)", "fn_file_location": "EF.aspx?str_url=http://192.168.6.80:8001/FileSystem/ShareFile_V3.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "2087_2", "fn_desc": "基金产品", "fn_file_location": "ProductManagement/ProductManagementNew.html?PC_ID=2&PM_ID=-1&verson=1", "fn_group": "product_info", "fn_class_id": null }, { "fn_code": "3209", "fn_desc": "会务费", "fn_file_location": "CSAdmin/InvitationToClass.aspx?type=fees", "fn_group": "class_management", "fn_class_id": "61" }, { "fn_code": "2112_1", "fn_desc": "渠道开发分析", "fn_file_location": "channel_business_new/channel_manage_analysis_user.aspx", "fn_group": "Report", "fn_class_id": null }, { "fn_code": "2117_1", "fn_desc": "全景分析", "fn_file_location": "channel_business_new/PanoramicAnalysisUser.aspx", "fn_group": "channel_business_new1", "fn_class_id": null }, { "fn_code": "10006", "fn_desc": "推广课时间", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=CLASS_TIME&FUNCTION_DESC=推广课时间", "fn_group": "temp", "fn_class_id": null }, { "fn_code": "191_6", "fn_desc": "身份维护", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=Position_Type&FUNCTION_DESC=身份维护", "fn_group": "MemberSetup", "fn_class_id": "158" }, { "fn_code": "159", "fn_desc": "公司性质", "fn_file_location": "status/status_company_nature.aspx", "fn_group": "Maintenance", "fn_class_id": "44" }, { "fn_code": "2087_1", "fn_desc": "生态产品", "fn_file_location": "ProductManagement/ProductManagementNew.html?PC_ID=1&PM_ID=-1&verson=1", "fn_group": "product_info", "fn_class_id": null }, { "fn_code": "2099", "fn_desc": "收藏文件", "fn_file_location": "FileSystem/CollectFile.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "2045", "fn_desc": "诊断任务管理", "fn_file_location": "MemberSS/APTraineeCompany/AP_DIAGNOSIS_TASK_MANAGE.aspx", "fn_group": "MemberSetup", "fn_class_id": "188" }, { "fn_code": "2044", "fn_desc": "沟通目的维护", "fn_file_location": "MemberSS/APTraineeCompany/AP_COMMUNICATION_PURPOSE.aspx", "fn_group": "MemberSetup", "fn_class_id": "187" }, { "fn_code": "2052", "fn_desc": "会员分析", "fn_file_location": "Report/CustomerAnalysis/TeacherAnalysis.aspx?UR_TYPE=lecturer", "fn_group": "Report", "fn_class_id": null }, { "fn_code": "2053", "fn_desc": "成交原因", "fn_file_location": "maintenance/AP_STATUS.aspx?ys_type=TC_Deal_Reason&FUNCTION_DESC=成交原因", "fn_group": "MemberSetup", "fn_class_id": "198" }, { "fn_code": "2087_7", "fn_desc": "推广课程", "fn_file_location": "ProductManagement/ProductManagementNew.html?PC_ID=8&PM_ID=-1&verson=1", "fn_group": "product_info", "fn_class_id": null }, { "fn_code": "158", "fn_desc": "行业维护", "fn_file_location": "maintenance/status_maintenance.aspx", "fn_group": "Maintenance", "fn_class_id": "43" }, { "fn_code": "1005_1", "fn_desc": "系统参数", "fn_file_location": "admin/AP_SETTING.aspx", "fn_group": "admin", "fn_class_id": null }, { "fn_code": "3140", "fn_desc": "升级会员", "fn_file_location": "trainee_center/trainee_upgrade_member.aspx", "fn_group": "customer_management_new", "fn_class_id": "34" }, { "fn_code": "908", "fn_desc": "推广教程", "fn_file_location": "FileSystem/out_file_V3.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "6214", "fn_desc": "邀约情况", "fn_file_location": "/channel_business_new/AP_COURSE_PlAN_SECTION_business.aspx", "fn_group": "channel_business_new1", "fn_class_id": null }, { "fn_code": "3212", "fn_desc": "签到分组", "fn_file_location": "CSAdmin/InvitationToClass.aspx?type=group", "fn_group": "class_management", "fn_class_id": null }, { "fn_code": "3212_1", "fn_desc": "年会分组", "fn_file_location": "CSAdmin/InvitationToClass.aspx?type=group1", "fn_group": "class_management", "fn_class_id": null }, { "fn_code": "909", "fn_desc": "推广老师", "fn_file_location": "Out/AP_OUT_TEACHER.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "162", "fn_desc": "客户回收站", "fn_file_location": "trainee_center/trainee_company_recycle.aspx", "fn_group": "customer_management_new", "fn_class_id": "47" }, { "fn_code": "157", "fn_desc": "课程类型维护", "fn_file_location": "ConsultantManagement/Course_Type_Maintenance.aspx", "fn_group": "MemberSetup", "fn_class_id": "42" }, { "fn_code": "1911", "fn_desc": "培训相册", "fn_file_location": "FileSystem/class_image_V3.aspx", "fn_group": "FileSystem", "fn_class_id": null }, { "fn_code": "3213", "fn_desc": "商业活动", "fn_file_location": "CSAdmin/AP_ACTIVITY.aspx", "fn_group": "class_management", "fn_class_id": null }, { "fn_code": "6004", "fn_desc": "加班情况表", "fn_file_location": "Overtime List/AP_OVERTIME_LIST.aspx", "fn_group": "admin", "fn_class_id": null }, { "fn_code": "6400", "fn_desc": "渠道分组设置", "fn_file_location": "maintenance/AP_STATUS1.aspx?ys_type=CB_TYPE&FUNCTION_DESC=渠道分组设置", "fn_group": "channel_setting", "fn_class_id": null }, { "fn_code": "2125_1", "fn_desc": "企业黑名单", "fn_file_location": "channel_business_new/AP_BLACKLIST.aspx", "fn_group": "customer_management_new", "fn_class_id": null }, { "fn_code": "2133", "fn_desc": "批量处理集服", "fn_file_location": "CSAdmin/Control_Company1.aspx", "fn_group": "customer_management_new", "fn_class_id": null }, { "fn_code": "6401", "fn_desc": "渠道人员配置", "fn_file_location": "maintenance/AP_CHANNEL_OWNER_USER.aspx", "fn_group": "channel_setting", "fn_class_id": null }, { "fn_code": "60005", "fn_desc": "加班情况表1", "fn_file_location": "Test/ov-form/views/AP_OVERTIME_LIST.html", "fn_group": "admin", "fn_class_id": null }] };
            if (data.status == 1) {
                //console.log(data);
                for (var i = 0; i < data.items.length; i++) {
                    //console.log(data.items[i].fn_group);
                    //console.log($("#" + data.items[i].fn_group));
                    $("#" + data.items[i].fn_group).append('<li><span class="head_list_icon' + data.items[i].fn_class_id + '" data_url="' + data.items[i].fn_file_location + '" fn_code="' + data.items[i].fn_code + '" data_key="' + data.items[i].fn_desc + '">' + data.items[i].fn_desc + '</span></li>')
                }

            } else {

            }
            Accordion();
        }

        $(function () {

            //打开首页的效果
            LoadingPage();

            //生成菜单
            GetFunction();

            //点击伸缩按钮控制左侧菜单伸缩
            $(".nav_header_shrink").click(function () {
                var type = $(this).attr("turn");
                if (type == "off") {
                    $(this).attr("turn", "on");
                    $(this).find("span").removeClass("turn_off").addClass("turn_on");
                    $(".nav_aside").animate({ left: "-239px" }, 500);
                    $(".nav_content").animate({ paddingLeft: "0" }, 500);

                    $(".nav_header").animate({ height: "0px" }, 500);
                    $("body").animate({ paddingTop: "0px" }, 500);
                    //$(".nav_header_shrink").addClass("nav_header_shrink1");
                    //$("body").attr("style", "padding-top:0px");
                } else {
                    $(this).attr("turn", "off");
                    $(this).find("span").removeClass("turn_on").addClass("turn_off")
                    $(".nav_aside").animate({ left: "0" }, 500);
                    $(".nav_content").animate({ paddingLeft: "239px" }, 500);

                    $(".nav_header").animate({ height: "50px" }, 500);
                    $("body").animate({ paddingTop: "50px" }, 500);
                }

            });

            $(".tabs").dblclick(function () {
                $(".nav_header_shrink").click();
            }).attr("title", "双击退出");

            //点击退出按钮退出登录
            $("#logout").click(function () {
                console.log("退出登录");
            });

            //左侧导航滚动条美化
            $(".collapse_content").slimScroll({
                width: '100%', //可滚动区域宽度
                height: '100%', //可滚动区域高度
                size: '5px', //组件宽度
                color: '#ccc', //滚动条颜色
                position: 'right', //组件位置：left/right
                distance: '0px', //组件与侧边之间的距离
                start: 'top', //默认滚动位置：top/bottom
                opacity: .2, //滚动条透明度
                alwaysVisible: true, //是否 始终显示组件
                disableFadeOut: false, //是否 鼠标经过可滚动区域时显示组件，离开时隐藏组件
                railVisible: true, //是否 显示轨道
                railColor: '#333', //轨道颜色
                railOpacity: 0, //轨道透明度
                railDraggable: true, //是否 滚动条可拖动
                railClass: 'slimScrollRail', //轨道div类名
                barClass: 'slimScrollBar', //滚动条div类名
                wrapperClass: 'slimScrollDiv', //外包div类名
                allowPageScroll: true, //是否 使用滚轮到达顶端/底端时，滚动窗口
                wheelStep: 20, //滚轮滚动量
                touchScrollStep: 200, //滚动量当用户使用手势
                borderRadius: '7px', //滚动条圆角
                railBorderRadius: '7px' //轨道圆角
            });

            //点击父菜单可以跳转
            $(document).on('click', '.specil_items>.link', function () {
                var self = $(this);
                setTimeout(function () {
                    tabs(self.text(), self.attr("data_url"));
                }, 300)
            });

            //点击子菜单可以跳转
            $(document).on('click', '.submenu>li>span', function () {
                $temp = $(this).attr("data_url");
                if (typeof ($temp) == "undefined" || $temp == "") {

                } else {
                    var liKey = $(this).attr("data_key");
                    var dataUrl = $(this).attr("data_url");
                    tabs(liKey, dataUrl);
                    $(this).addClass("active");
                    $(this).parent().siblings("li").find("span").removeClass("active");
                    $(this).parent().parent().parent().siblings("li").find(".submenu span").removeClass("active");
                }
            });


            //左侧菜单与右侧显示项匹配
            $('#tab_content').tabs({
                onSelect: function (title, index) {
                    var my_title;
                    if (title == "Home") {
                        $(".submenu").slideUp();
                        $(".my_items").removeClass("open");
                        $(".submenu span").removeClass("active");
                    }
                    $(".submenu span").each(function () {
                        my_title = $(this).text();
                        if (my_title == title) {
                            $(this).parents(".my_items").addClass("open").siblings().removeClass("open");
                            $(this).parents(".submenu").slideDown();
                            $(this).parents(".my_items").siblings().find(".submenu").slideUp();
                            $(this).addClass("active");
                            $(this).parent().siblings("li").find("span").removeClass("active");
                            $(this).parent().parent().parent().siblings("li").find(".submenu span").removeClass("active");
                        }
                    });
                },
                onUpdate: function (title, index) {
                    tabsLength = $('#tab_content').tabs("tabs").length;
                    if (tabsLength >= 2) {
                        $(".tabs").removeClass("hide");
                        $("#tab_content").css("padding-top", "30px");
                    } else {
                        $(".tabs").addClass("hide");
                        $("#tab_content").css("padding-top", "0");
                    }
                },
                onClose: function () {
                    tabsLength = $('#tab_content').tabs("tabs").length;
                    if (tabsLength >= 2) {
                        $(".tabs").removeClass("hide");
                        $("#tab_content").css("padding-top", "30px");
                    } else {
                        $(".tabs").addClass("hide");
                        $("#tab_content").css("padding-top", "0");
                    }
                }
            });
        })

    </script>



    <div class="message_box" style="display: none;">
        <div class="message_title">
            <span>信息提醒</span>
            <span class="message_close">X</span>
        </div>

        <div class="message_content">
            asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfas
            dfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf
            asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasd
            fasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdf
        </div>
    </div>

    <style type="text/css">
        .message_box {
            display: inline-block;
            position: fixed;
            width: 350px;
            bottom: -600px;
            right: 0px;
            border-radius: 5px;
            z-index: 10;
            overflow: hidden;
        }

            .message_box .message_title {
                text-align: center;
                background-color: #3BB9EF;
                color: #fff;
                font-size: 16px;
                font-weight: 400;
                padding: 5px 0px;
            }

            .message_box .message_close {
                float: right;
                background: #fff;
                color: #3BB9EF;
                display: inline-block;
                width: 20px;
                height: 20px;
                border-radius: 50%;
                line-height: 20px;
                margin-right: 10px;
                font-size: 12px;
                padding: 2px;
                cursor: pointer;
            }

            .message_box .message_content {
                min-height: 200px;
                max-height: 500px;
                overflow-y: auto;
                background: #fff;
                font-size: 14px;
                padding: 10px;
                word-break: break-all;
                text-align: justify;
                line-height: 25px;
            }
    </style>


    <script type="text/javascript">
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
                e.css("opacity", "0");
                e.animate({ "opacity": "0.5" });
                $("body").append(e);
            } else {
                $("#" + id).remove();
            }
        }

        var a = {};
        a.title = "信息提醒";
        a.content = "信息提醒信息提醒信息提醒信息醒提醒";
        a.auto_close = false;

        function message_box(json) {
            if (json.title) {
                json.title = "提醒";
            }
            var message_box = $(' <div class="message_box"></div>')
            var message_title = $('<div class="message_title"></div>');
            var message_content = $('<div class="message_content"></div>');
            message_box.append(message_title);
            message_box.append(message_content);

            var text = $("<span>" + json.title + "</span>");
            var message_close = $('<span class="message_close">X</span>');
            message_title.append(text).append(message_close);
            message_content.html(json.content);

            $("body").append(message_box);

            if (!json.auto_close) {
                Cover(true);
            }

            var _that = this;
            message_close.click(function () {
                _that.Hide();
            });

            this.Show = function () {
                message_box.animate({ bottom: "0px" });
            }

            this.Hide = function () {
                message_box.animate({ bottom: "-600px" }, undefined, undefined, function () {
                    $(this).remove();
                });
                if (!json.auto_close) {
                    Cover(false);
                }
            }

        }
        var obj = new message_box(a);
        obj.Show();

        var obj1 = new message_box(a);
        obj1.Show();

    </script>
</body>


</html>
