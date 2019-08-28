using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace ERPBase
{
    public class AjaxSmartBasePage : AjaxBasePage
    {

        public string action = "";

        public string Content_Type = "";

        protected override void OnLoad(EventArgs e)
        {
            if (string.IsNullOrEmpty(action))
            {
                action = Request["action"];
            }


            if (MethodList.Keys.Contains(action) && false)
            {
                MethodInfo mi = MethodList[action];
                string str_result = mi.Invoke(this, null).ToString();
                if (string.IsNullOrEmpty(Content_Type))
                {
                    Response.ContentType = "application/json";
                }
                else
                {
                    Response.ContentType = Content_Type;
                }
                Response.Write(str_result);
            }
            else
            {
                if (!string.IsNullOrEmpty(action))
                {
                    MethodInfo mi = this.GetType().GetMethod(action);
                    if (mi != null)
                    {
                        string str_result = mi.Invoke(this, null).ToString();
                        if (!MethodList.Keys.Contains(action))
                        {
                            MethodList.Add(action, mi);
                        }
                        Response.Write(str_result);
                    }
                    else
                    {
                        Response.Write(new AjaxExceptionResult("不存在方法" + action).ToString());
                    }
                }
                else
                {
                    Response.Write(new AjaxExceptionResult("参数action不能为空").ToString());
                }

            }
        }

        private Dictionary<string, MethodInfo> MethodList
        {
            get
            {
                string str_key = AppRelativeVirtualPath + "_ListMethod_" + DateTime.Now.ToString("yyyy_MM_dd");
                if (Cache[str_key] == null)
                {
                    Cache[str_key] = new Dictionary<string, MethodInfo>();
                }
                return (Dictionary<string, MethodInfo>)Cache[str_key];
            }
            set
            {
                string str_key = AppRelativeVirtualPath + "_ListMethod_" + DateTime.Now.ToString("yyyy_MM_dd");
                Cache[str_key] = value;
            }
        }

    }


    public class AjaxSmartBasePage1 : AjaxBasePage
    {

        public string action = "";

        protected override void OnLoad(EventArgs e)
        {
            if (string.IsNullOrEmpty(action))
            {
                action = Request["action"];
            }


            if (MethodList.Keys.Contains(action) && false)
            {
                MethodInfo mi = MethodList[action];
                mi.Invoke(this, null);
            }
            else
            {
                if (!string.IsNullOrEmpty(action))
                {
                    MethodInfo mi = this.GetType().GetMethod(action);
                    if (mi != null)
                    {
                        mi.Invoke(this, null);
                        if (!MethodList.Keys.Contains(action))
                        {
                            MethodList.Add(action, mi);
                        }
                    }
                    else
                    {
                        Response.Write(new AjaxExceptionResult("不存在方法" + action).ToString());
                    }
                }
                else
                {
                    Response.Write(new AjaxExceptionResult("参数action不能为空").ToString());
                }

            }
        }

        private Dictionary<string, MethodInfo> MethodList
        {
            get
            {
                string str_key = AppRelativeVirtualPath + "_ListMethod_" + DateTime.Now.ToString("yyyy_MM_dd");
                if (Cache[str_key] == null)
                {
                    Cache[str_key] = new Dictionary<string, MethodInfo>();
                }
                return (Dictionary<string, MethodInfo>)Cache[str_key];
            }
            set
            {
                string str_key = AppRelativeVirtualPath + "_ListMethod_" + DateTime.Now.ToString("yyyy_MM_dd");
                Cache[str_key] = value;
            }
        }

    }
}

