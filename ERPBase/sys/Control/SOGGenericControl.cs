using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;

namespace ERPBase
{
    public class SogGeneric : HtmlGenericControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Attributes.Add("class", CssClass);

        }
        protected override void Render(HtmlTextWriter writer)
        {

            base.Render(writer);
            //writer.Write(writer.NewLine);
        }

        public string CssClass { get; set; }
    }

    public class SogDiv : SogGeneric
    {
        public SogDiv()
        {
            this.TagName = "div";
        }
    }

    public class SogSpan : SogGeneric
    {
        public SogSpan()
        {
            this.TagName = "span";
        }
    }

    public class SogButton : SogSpan
    {

    }

    public class SogDeleteButton : SogButton
    {
        public SogDeleteButton()
        {
            this.CssClass = "btn green_btn";
            this.InnerText = "删除";
        }
    }

    public class SogIcon : SogGeneric
    {
        public SogIcon()
        {
            this.TagName = "i";
        }
    }





    public class SogNavigate : DataControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            foreach (KeyValuePair<string, string> item in DataSource)
            {
                Dictionary<string, string> ht0 = new Dictionary<string, string>();
                if (item.Key == Value)
                {
                    ht0.Add("class", "active");
                }
                else
                {
                    ht0.Add("class", "");
                }
                ht0.Add("value", item.Key);
                str_html.Append(sog_render("a", item.Value, ht0));
            }

            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("class", "SogNavigate");
            ht.Add("value", Value);
            if (!string.IsNullOrEmpty(target))
            {
                ht.Add("target", target);
            }
            writer.Write(sog_render("div", str_html.ToString(), ht));
        }
    }

    public class condition_item : DataControl
    {
        public string HeadText = string.Empty;

        public bool IsShowEmpty { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder str_html = new StringBuilder();
            Dictionary<string, string> ht1 = new Dictionary<string, string>();
            ht1.Add("class", "condition_head");
            str_html.Append(sog_render("div", HeadText, ht1));

            if (IsShowEmpty)
            {
                Dictionary<string, string> ht0 = new Dictionary<string, string>();
                ht0.Add("value", string.Empty);
                if (string.IsNullOrEmpty(Value))
                {
                    ht0.Add("class", "active");
                }
                else
                {
                    ht0.Add("class", "");
                }
                str_html.Append(sog_render("a", "全部", ht0));
            }

            foreach (KeyValuePair<string, string> item in DataSource)
            {
                Dictionary<string, string> ht0 = new Dictionary<string, string>();
                if (item.Key == Value)
                {
                    ht0.Add("class", "active");
                }
                else
                {
                    ht0.Add("class", "");
                }
                ht0.Add("value", item.Key);
                str_html.Append(sog_render("a", item.Value, ht0));
            }

            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("class", "condition_item");
            if (!string.IsNullOrEmpty(ID))
            {
                ht.Add("id", ID);
            }
            ht.Add("value", Value);
            if (!string.IsNullOrEmpty(target))
            {
                ht.Add("target", target);
            }
            writer.Write(sog_render("div", str_html.ToString(), ht));
        }

    }



    public class SOGCssControl : SogControl
    {
        private List<string> arr_url_v = new List<string>();

        public SOGCssControl()
        {

        }

        public SOGCssControl(string str_js_url_v)
        {
            arr_url_v.Add(str_js_url_v);
        }


        public SOGCssControl(List<string> arr_js_url_v)
        {
            arr_url_v.AddRange(arr_js_url_v);
        }

        public void Add(string str_js_url_v)
        {
            arr_url_v.Add(str_js_url_v);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string str_css_new = create_new_css();
            StringBuilder str_html = new StringBuilder();
            Dictionary<string, string> attr = new Dictionary<string, string>();
            attr.Add("rel", "stylesheet");
            attr.Add("href", str_css_new);
            str_html.Append(sog_single_render("link", attr));
            writer.Write(str_html);
        }

        private string create_new_css()
        {

            if (arr_url_v.Count > 0)
            {
                string str_file_name = "";
                //arr_url_v.Sort();
                foreach (string item_v in arr_url_v)
                {
                    string item_a = HttpContext.Current.Server.MapPath(item_v);
                    if (System.IO.File.Exists(item_a))
                    {
                        str_file_name += GetMD5HashFromFile(item_a);
                    }
                }
                if (!string.IsNullOrEmpty(str_file_name))
                {
                    str_file_name = GetMD5WithString(str_file_name);
                }
                string str_css_new_url_c = "/css/" + str_file_name + ".css";
                string str_css_new = HttpContext.Current.Server.MapPath("~" + str_css_new_url_c);
                if (!System.IO.File.Exists(str_css_new))
                {
                    StringBuilder str_content = new StringBuilder();
                    foreach (string item_v in arr_url_v)
                    {
                        string item_a = HttpContext.Current.Server.MapPath(item_v);
                        if (System.IO.File.Exists(item_a))
                        {
                            //生成新的css
                            str_content.Append(System.Environment.NewLine + "/*" + item_v + " begin*/" + System.Environment.NewLine);
                            str_content.Append(System.IO.File.ReadAllText(item_a));
                            str_content.Append(System.Environment.NewLine + "/*" + item_v + " end*/" + System.Environment.NewLine);
                        }
                    }
                    System.IO.File.WriteAllText(str_css_new, str_content.ToString());
                }
                return str_css_new_url_c;
            }
            else
            {
                return string.Empty;
            }
        }


        private static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string GetMD5WithString(string sDataIn)
        {
            string str = "";
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(sDataIn);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(data);
            for (int i = 0; i < bytes.Length; i++)
            {
                str += bytes[i].ToString("x2");
            }
            return str;
        }

    }

    public class SOGJsControl : SogControl
    {
        private List<string> arr_url_v = new List<string>();

        public SOGJsControl()
        {

        }

        public SOGJsControl(string str_js_url_v)
        {
            arr_url_v.Add(str_js_url_v);
        }


        public SOGJsControl(List<string> arr_js_url_v)
        {
            arr_url_v.AddRange(arr_js_url_v);
        }

        public void Add(string str_js_url_v)
        {
            arr_url_v.Add(str_js_url_v);
        }




        protected override void Render(HtmlTextWriter writer)
        {
            string str_css_new = create_new_js();
            StringBuilder str_html = new StringBuilder();
            Dictionary<string, string> attr = new Dictionary<string, string>();
            attr.Add("type", "text/javascript");
            attr.Add("src", str_css_new);
            str_html.Append(sog_render("script", string.Empty, attr));
            writer.Write(str_html);
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    StringBuilder str_html = new StringBuilder();
        //    foreach (string item in arr_url_v)
        //    {
        //        Dictionary<string, string> attr = new Dictionary<string, string>();
        //        attr.Add("type", "text/javascript");
        //        attr.Add("src", item.Replace("~","")+"?v="+DateTime.Now.ToString("fff"));
        //        str_html.Append(sog_render("script", string.Empty, attr));
        //    }
        //    writer.Write(str_html);
        //}


        private string create_new_js()
        {

            if (arr_url_v.Count > 0)
            {
                string str_file_name = "";
                //arr_url_v.Sort();
                foreach (string item_v in arr_url_v)
                {
                    string item_a = HttpContext.Current.Server.MapPath(item_v);
                    if (System.IO.File.Exists(item_a))
                    {
                        str_file_name += GetMD5HashFromFile(item_a);
                    }
                }
                if (!string.IsNullOrEmpty(str_file_name))
                {
                    str_file_name = GetMD5WithString(str_file_name);
                }
                string str_css_new_url_c = "/js/" + str_file_name + ".js";
                string str_css_new = HttpContext.Current.Server.MapPath("~" + str_css_new_url_c);
                if (!System.IO.File.Exists(str_css_new))
                {
                    StringBuilder str_content = new StringBuilder();
                    foreach (string item_v in arr_url_v)
                    {
                        string item_a = HttpContext.Current.Server.MapPath(item_v);
                        if (System.IO.File.Exists(item_a))
                        {
                            //生成新的css
                            str_content.Append(System.Environment.NewLine + "/*" + item_v + " begin*/" + System.Environment.NewLine);
                            str_content.Append(System.IO.File.ReadAllText(item_a));
                            str_content.Append(System.Environment.NewLine + "/*" + item_v + " end*/" + System.Environment.NewLine);
                        }
                    }
                    System.IO.File.WriteAllText(str_css_new, str_content.ToString());
                }
                return str_css_new_url_c;
            }
            else
            {
                return string.Empty;
            }
        }



        private static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string GetMD5WithString(string sDataIn)
        {
            string str = "";
            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(sDataIn);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(data);
            for (int i = 0; i < bytes.Length; i++)
            {
                str += bytes[i].ToString("x2");
            }
            return str;
        }

    }




}