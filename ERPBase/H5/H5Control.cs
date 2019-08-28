using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ERPBase
{

    /// <summary>
    /// 手机控件基类
    /// </summary>
    public class H5Control : HtmlGenericControl
    {

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Attributes.Add("class", CssClass);

        }

        public static string Space(int i)
        {
            return " ";
        }

        public string CssClass { get; set; }

        public string rule { get; set; }

        public string target { get; set; }

        public string rule_desc { get; set; }

        public string placeholder { get; set; }


        public Dictionary<string, string> ht_attr = new Dictionary<string, string>();
        public virtual string get_attr()
        {
            StringBuilder str_html = new StringBuilder();
            ht_attr.Add("id", ID);
            ht_attr.Add("class", CssClass);
            ht_attr.Add("rule", rule);
            ht_attr.Add("target", target);
            ht_attr.Add("rule_desc", rule_desc);
            ht_attr.Add("placeholder", placeholder);

            foreach (KeyValuePair<string, string> item in ht_attr)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    str_html.Append(Space(1) + item.Key + "=" + "'" + item.Value + "'");
                }
            }
            return str_html.ToString();

        }

    }

    /// <summary>
    /// 显示头部
    /// </summary>
    public class H5Head : H5Control
    {
        public string HeadText { get; set; }

        public bool IsShowMore { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<div class='head'>");
            str_html.Append("<div class='Iback'></div>");
            str_html.Append("<span class='icon_back'></span>");
            str_html.Append(" <span class='title'>" + HeadText + "</span>");
            if (IsShowMore)
            {
                str_html.Append("<i class='fa fa-list icon_more' aria-hidden='true'>");
                str_html.Append("<span class='icon_msg_num hide1'>0</span>");
                str_html.Append("</i>");
                str_html.Append("<div class='Imore'></div>");
            }

            str_html.Append("</div>");

            writer.Write(str_html.ToString());
            base.Render(writer);
        }

    }

    /// <summary>
    /// 选择审批人
    /// </summary>
    public class H5Person : H5Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<div class='person'>");
            str_html.Append("<div class='person_title'>");
            str_html.Append("<span class='person_title_text'>审批人</span>");
            str_html.Append("<span class='person_title_desc'>(点击头像可以删除)</span>");
            str_html.Append("</div>");
            str_html.Append("<div class='person_select'>");
            str_html.Append("<img src = '/H5/image/add.jpg' class='img_person' />");
            str_html.Append("</div>");
            str_html.Append("</div>");
            writer.Write(str_html);
        }
    }

    /// <summary>
    /// 按钮
    /// </summary>
    public class H5Button : H5Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<div class='function'>");
            str_html.Append("<span class='btn WaterWave " + CssClass + "'>" + InnerText + "</span>");
            str_html.Append("</div>");
            writer.Write(str_html);
        }
    }

    /// <summary>
    /// 审批按钮
    /// </summary>
    public class H5ApproveButton : H5Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<div class='bottom_function'>");
            str_html.Append("<span class='b b2 btn_back' >驳回</span>");
            str_html.Append("<span class='b b1 btn_approve' >同意</span>");
            str_html.Append("<span class='b b0 btn_end_approve' >终审</span>");
            str_html.Append("</div>");
            writer.Write(str_html);
        }
    }

    /// <summary>
    /// DIV
    /// </summary>
    public class H5Div : H5Control
    {
        public H5Div()
        {
            this.TagName = "div";
        }
    }

    /// <summary>
    /// 显示申请人
    /// </summary>
    public class request_person : H5Control
    {
        public string HeadImage { get; set; }
        public string Name { get; set; }

        public string StatusText { get; set; }

        public string StatusClass { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<div class='request_person'>");
            str_html.Append("<img src = '" + HeadImage + "' class='head_image' />");
            str_html.Append("<div class='message'>");
            str_html.Append("<div class='name'>" + Name + "</div>");
            str_html.Append("<div class='stauts " + StatusClass + "'>" + StatusText + "</div>");
            str_html.Append("</div>");
            str_html.Append("</div>");
            writer.Write(str_html);
        }
    }


    /// <summary>
    /// 表单明细显示
    /// </summary>
    public class detail_message_item : H5Control
    {
        public string Key { get; set; }

        public string Value { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<div class='item'>");
            str_html.Append("<span class='key'>" + Key + "</span>");
            str_html.Append("<span class='value'>" + Value + "</span>");
            str_html.Append("</div>");
            writer.Write(str_html);
        }

    }

    /// <summary>
    /// 审批流程控件
    /// </summary>
    public class work_follow_item : H5Control
    {

        public string HeadImage { get; set; }

        public string name { get; set; }

        public string time { get; set; }

        public string Reason { get; set; }

        public string StatusText { get; set; }

        public string StatusClass { get; set; }

        public string Status { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Reason))
            {
                Reason = "无";
            }
           
            string icon_class = string.Empty;
            if (Status == "0")
            {
                icon_class = "fa-clock-o";
            }

            if (Status == "1")
            {
                icon_class = "fa-check-circle";
            }

            if (Status == "2")
            {
                icon_class ="fa-undo";
            }


            StringBuilder str_html = new StringBuilder();
            str_html.Append("<div class='item'>");
            str_html.Append("<div class='content'>");
            str_html.Append("<div class='card'>");
            str_html.Append("<i class='fa "+ icon_class + " icon "+ StatusClass + "' aria-hidden='true'></i>");
            str_html.Append("<img src = '" + HeadImage + "' class='head_image' />");
            str_html.Append("<div class='text'>");
            str_html.Append("<span class='name'>" + name + "</span>");
            str_html.Append("<span class='time'>" + time + "</span>");
            str_html.Append("<div class='message'>审批意见:" + Reason + "</div>");
            str_html.Append("<div class='status "+ StatusClass + "'>"+ StatusText + "</div>");

            str_html.Append("</div>");
            str_html.Append("</div>");
            str_html.Append("</div>");
            str_html.Append("</div>");
            writer.Write(str_html);
        }

    }


    /// <summary>
    /// 文字控件
    /// </summary>
    public class H5Span : H5Control
    {
        public H5Span()
        {
            this.TagName = "span";
        }
    }

    /// <summary>
    /// 数字输入框
    /// </summary>
    public class H5TextBox : H5Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<input type ='text' " + get_attr() + ">");
            //str_html.Append("</div>");
            writer.Write(str_html.ToString());
            //base.Render(writer);
        }
    }

    /// <summary>
    /// 文本输入框
    /// </summary>
    public class H5NumberBox : H5Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<input type ='number' " + get_attr() + ">");
            writer.Write(str_html.ToString());
        }
    }

    /// <summary>
    /// 时间输入框
    /// </summary>
    public class H5DateTime : H5Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            CssClass += " datetime";
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<input type ='text'readonly='readonly' unselectable='on' onfocus='this.blur()' " + get_attr() + " />");
            //str_html.Append("</div>");
            writer.Write(str_html.ToString());
            //base.Render(writer);
        }
    }

    /// <summary>
    /// 日期输入框
    /// </summary>
    public class H5Date : H5Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            CssClass += " date";
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<input type ='text'readonly='readonly' unselectable='on' onfocus='this.blur()' " + get_attr() + " />");
            //str_html.Append("</div>");
            writer.Write(str_html.ToString());
            //base.Render(writer);
        }
    }

    /// <summary>
    /// 多行文本输入框
    /// </summary>
    public class H5TextArea : H5Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<textarea rows ='5' " + get_attr() + " ></textarea>");
            writer.Write(str_html.ToString());
        }
    }

}