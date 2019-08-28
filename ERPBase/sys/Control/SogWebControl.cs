using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERPBase
{
    public class SogControl : WebControl
    {
        public static string BeginTag = "<";
        public static string EndTag = ">";
        public static string Le = @"/";
        public static string NewLine = System.Environment.NewLine;
        public static string Space(int i)
        {
            return " ";
        }

        public string target { get; set; }

        protected static string sog_render(string tag, string sub_html, Dictionary<string, string> attr)
        {
            StringBuilder str_html = new StringBuilder();

            str_html.Append(BeginTag + tag);

            if (attr != null)
            {
                foreach (KeyValuePair<string, string> item in attr)
                {
                    str_html.Append(Space(1) + item.Key + "='" + item.Value + "'");
                }
            }
            str_html.Append(EndTag + NewLine);


            if (!string.IsNullOrEmpty(sub_html))
            {
                str_html.Append(sub_html + NewLine);
            }
            str_html.Append(BeginTag + Le + tag + EndTag + NewLine);

            return str_html.ToString();
        }

        protected static string sog_render(string tag, string sub_html)
        {
            return sog_render(tag, sub_html, null);
        }

        protected static string sog_single_render(string tag, Dictionary<string, string> attr)
        {
            StringBuilder str_html = new StringBuilder();

            str_html.Append(BeginTag + tag);

            if (attr != null)
            {
                foreach (KeyValuePair<string, string> item in attr)
                {
                    str_html.Append(Space(1) + item.Key + "='" + item.Value + "'");
                }
            }
            str_html.Append(Le + EndTag + NewLine);

            return str_html.ToString();
        }


    }

    public class DataControl : SogControl
    {
        public Dictionary<string, string> DataSource = new Dictionary<string, string>();
        public string Value = string.Empty;
    }

    public class SogTextSearch : SogControl
    {
        public string HeadText { get; set; }

        public string ButtonText { get; set; }

        public string placeholder { get; set; }



        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(HeadText))
            {
                HeadText = "筛选";
            }

            if (string.IsNullOrEmpty(ButtonText))
            {
                ButtonText = "搜索";
            }

            StringBuilder str_html = new StringBuilder();
            str_html.Append(sog_render("span", HeadText));

            str_html.Append("<input type ='text' ");
            if (!string.IsNullOrEmpty(placeholder))
            {
                str_html.Append(" placeholder='" + placeholder + "' ");
            }

            if (!string.IsNullOrEmpty(CssClass))
            {
                str_html.Append(" class='" + CssClass + "' ");
            }

            if (!string.IsNullOrEmpty(target))
            {
                str_html.Append(" target='" + target + "' ");
            }


            str_html.Append(" />");

            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("class", "fa fa-search");
            ht.Add("aria-hidden", "true");

            Dictionary<string, string> ht0 = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(ID))
            {
                ht0.Add("id", ID);
            }
            str_html.Append(sog_render("span", sog_render("i", "", ht) + ButtonText, ht0));


            Dictionary<string, string> ht1 = new Dictionary<string, string>();
            ht1.Add("class", "SogTextSearch");

            writer.Write(sog_render("div", str_html.ToString(), ht1));
        }
    }

    public class condition_desc : SogControl
    {
        public string ParentID { get; set; }
        public string HeadText = string.Empty;

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append(sog_render("span", HeadText + ":"));
            Dictionary<string, string> ht0 = new Dictionary<string, string>();
            ht0.Add("class", ParentID);
            str_html.Append(sog_render("span", "全部", ht0));

            Dictionary<string, string> ht1 = new Dictionary<string, string>();
            ht1.Add("class", "condition_desc");
            writer.Write(sog_render("div", str_html.ToString(), ht1));
        }
    }

    public class SogWebControl : WebControl
    {
        public static string BeginTag = "<";
        public static string EndTag = ">";
        public static string Le = @"/";
        public static string NewLine = System.Environment.NewLine;
        public static string Space(int i)
        {
            return " ";
        }

        public string target { get; set; }

        public string rule { get; set; }


        public string rule_desc { get; set; }


        public string placeholder { get; set; }

        public Dictionary<string, string> ht_attr = new Dictionary<string, string>();
        public virtual string get_attr()
        {
            StringBuilder str_html = new StringBuilder();
            ht_attr.Add("id", ID);
            ht_attr.Add("class", CssClass);
            ht_attr.Add("target", target);
            ht_attr.Add("rule", rule);
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

    public class SogTextBox : SogWebControl
    {

        public string value { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            ht_attr.Add("type", "text");
            ht_attr.Add("value", value);
            string attr = get_attr();

            StringBuilder str_html = new StringBuilder();
            str_html.Append(BeginTag + "input");
            str_html.Append(attr);
            str_html.Append(Le + EndTag);
            writer.Write(str_html.ToString());
        }

    }

    public class SOGDropDownList : SogWebControl
    {

        public string value { get; set; }

        public Dictionary<string, string> DataSource = new Dictionary<string, string>();

        protected override void Render(HtmlTextWriter writer)
        {
            string str_attr = get_attr();
            StringBuilder str_html = new StringBuilder();
            StringBuilder str_son = new StringBuilder();

            foreach (KeyValuePair<string, string> item in DataSource)
            {
                str_son.Append("<option value='" + item.Key + "'>" + item.Value + "</option>");
            }

            str_html.Append(BeginTag + "select" + str_attr + EndTag);
            str_html.Append(str_son);
            str_html.Append(BeginTag + Le + "select" + EndTag);
            writer.Write(str_html.ToString());

        }

    }

    public class SogTextArea : SogWebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            ht_attr.Add("style", "height:70px");
            string str_attr = get_attr();

            str_html.Append(BeginTag + "textarea rows ='3' " + str_attr + EndTag);
            str_html.Append(BeginTag + Le + "textarea" + EndTag);
            writer.Write(str_html.ToString());
        }

    }


    public class SogFileUpload : SogWebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            CssClass += " SogFileUpload";
            string attr = get_attr();
            writer.Write("<div " + attr + ">");
            writer.Write("<input class='hide file_input' type='file'>");
            writer.Write("<i class='fa fa-plus-circle file_icon' aria-hidden='true'></i>");
            writer.Write("<a class='file_url'></a>");
            writer.Write("<i class='fa fa-times file_clear hide' aria-hidden='true'></i>");
            writer.Write("</div>");
        }
    }

    public class SogFolderUpload : SogWebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            CssClass += " SogFolderUpload";
            string attr = get_attr();
            writer.Write("<div " + attr + ">");
            writer.Write("<input class='hide folder_input' type='file'multiple='multiple' webkitdirectory='webkitdirectory' >");
            writer.Write("<i class='fa fa-folder-open folder_icon' aria-hidden='true'></i>");
            writer.Write("<a class='folder_url'></a>");
            writer.Write("<i class='fa fa-times folder_clear hide' aria-hidden='true'></i>");
            writer.Write("</div>");
        }
    }

    public class SogDate : SogWebControl
    {
        public string value { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            ht_attr.Add("type", "text");
            ht_attr.Add("value", value);
            string attr = get_attr();

            StringBuilder str_html = new StringBuilder();
            str_html.Append(BeginTag + "input");
            str_html.Append(attr);
            str_html.Append(Le + EndTag);
            writer.Write(str_html.ToString());

            writer.Write("<i class='fa fa-calendar SogDateIcon' aria-hidden='true'></i>");
        }
    }

    public class SogDateTime : SogWebControl
    {
        public string value { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            ht_attr.Add("type", "text");
            ht_attr.Add("value", value);
            string attr = get_attr();

            StringBuilder str_html = new StringBuilder();
            str_html.Append(BeginTag + "input");
            str_html.Append(attr);
            str_html.Append(Le + EndTag);
            writer.Write(str_html.ToString());
            writer.Write("<i class='fa fa-calendar SogDateIcon' aria-hidden='true'></i>");

        }
    }




}