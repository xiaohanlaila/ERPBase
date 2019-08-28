using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ERPBase
{
    public interface IHtmlAble
    {
        string ToHtml();
    }

    public class SogAjaxControl : IHtmlAble
    {

        public List<IHtmlAble> Controls = new List<IHtmlAble>();
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();

        protected Dictionary<string, string> attr = new Dictionary<string, string>();

        public static string BeginTag = "<";
        public static string EndTag = ">";
        public static string Le = @"/";
        public static string NewLine = System.Environment.NewLine;
        public string TagName { get; set; }

        private string _cssclase = string.Empty;
        public string CssClass
        {
            get
            {
                return _cssclase;
            }

            set
            {
                _cssclase = value;
                attr["class"] = value;
            }
        }


        private string _target = string.Empty;
        public string target
        {
            get
            {
                return _target;
            }

            set
            {
                _target = value;
                attr["target"] = value;
            }
        }


        public static string Space(int i)
        {
            return " ";
        }

        public virtual string ToHtml()
        {
            return string.Empty;
        }

    }

    public class SogSingleTagControl : SogAjaxControl
    {
        public override string ToHtml()
        {
            StringBuilder str_html = new StringBuilder();
            StringBuilder str_html_attr = new StringBuilder();

            //输出内部属性
            foreach (KeyValuePair<string, string> item in attr)
            {
                str_html_attr.Append(Space(1) + item.Key + "=" + "'" + item.Value + "'" + Space(1));
            }

            //输出外部属性
            foreach (KeyValuePair<string, string> item in Attributes)
            {
                str_html_attr.Append(Space(1) + item.Key + "=" + "'" + item.Value + "'" + Space(1));
            }

            str_html.Append(BeginTag + TagName + str_html_attr.ToString() + Le + EndTag);
            return str_html.ToString();
        }
    }


    public class SogDoubleTagControl : SogAjaxControl
    {
        public string InnerText { get; set; }


        public override string ToHtml()
        {
            StringBuilder str_html = new StringBuilder();
            StringBuilder str_html_attr = new StringBuilder();
            StringBuilder str_child_html = new StringBuilder();
            //先输出子的内容
            foreach (IHtmlAble item in Controls)
            {
                str_child_html.Append(item.ToHtml());
            }

            //输出内部属性
            foreach (KeyValuePair<string, string> item in attr)
            {
                str_html_attr.Append(Space(1) + item.Key + "=" + "'" + item.Value + "'" + Space(1));
            }

            //输出外部属性
            foreach (KeyValuePair<string, string> item in Attributes)
            {
                str_html_attr.Append(Space(1) + item.Key + "=" + "'" + item.Value + "'" + Space(1));
            }

            //在输出自己
            str_html.Append(BeginTag + TagName + str_html_attr + EndTag);
            str_html.Append(str_child_html+ InnerText);
            str_html.Append(BeginTag + Le + TagName + EndTag);
            return str_html.ToString();
        }

    }

    public class SogAajaxTd : SogDoubleTagControl
    {
        public SogAajaxTd()
        {
            base.TagName = "td";
        }
    }

    public class SogAajaxTr : SogDoubleTagControl
    {
        public SogAajaxTr()
        {
            base.TagName = "tr";
        }
    }

    public class SogAajaxTable : SogDoubleTagControl
    {
        public SogAajaxTable()
        {
            base.TagName = "table";
        }
    }

    public class SogAajaxTh : SogDoubleTagControl
    {
        public SogAajaxTh()
        {
            base.TagName = "th";
        }
    }

    public class SogAajaxSpan : SogDoubleTagControl
    {
        public SogAajaxSpan()
        {
            base.TagName = "span";
        }
    }


    public class SogAjaxCheck : SogSingleTagControl
    {
        public SogAjaxCheck()
        {
            base.TagName = "input";
            Attributes.Add("type", "checkbox");
        }

    }

}