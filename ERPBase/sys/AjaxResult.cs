/*
'所有结果必须已json的格式返回
-2 参数不能为空
-1 代表无权限 用户未登录系统
'0 代表成功  成功 items 或者 message 必须有值
'1 代表失败  可以提示用户message里面的值
'2 代表异常  可以提示用户程序异常,message写在console里面
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace ERPBase
{
    public class AjaxResult
    {
        public string status;
        public string message;
        public string items;

        public string primary_key;
        public string total_record;

        public override string ToString()
        {
            StringBuilder str_result = new StringBuilder();
            str_result.Append("{");
            if (!string.IsNullOrEmpty(status))
            {
                str_result.Append(@"""status"":""" + status + @"""" + ",");
            }
            if (!string.IsNullOrEmpty(message))
            {
                str_result.Append(@"""message"":""" + message + @"""" + ",");
            }

            if (!string.IsNullOrEmpty(items))
            {
                //str_result.Append(@"""items"":""" + items + @"""" + ",");
                str_result.Append(@"""items"":" + items + ",");
            }

            if (!string.IsNullOrEmpty(primary_key))
            {
                str_result.Append(@"""primary_key"":""" + primary_key + @"""" + ",");
            }

            if (!string.IsNullOrEmpty(total_record))
            {
                str_result.Append(@"""total_record"":""" + total_record + @"""" + ",");
            }
            if (str_result.Length > 0)
            {
                str_result.Length = str_result.Length - 1;
            }
            str_result.Append("}");
            return str_result.ToString();
        }

        protected string Json(object obj)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string str_json = jsSerializer.Serialize(obj);
            return str_json;
        }



    }

    public class AjaxObjectResult : AjaxResult
    {
        public AjaxObjectResult(object obj)
        {
            if (obj == null)
            {
                status = "0";
                message = "未将对象引入实例";
            }
            else
            {
                status = "1";
                message = "加载成功";
                items = Json(obj);
            }

        }
    }

    public class AjaxDataResult : AjaxResult
    {
        public AjaxDataResult(DataTable dt)
        {
            if (dt == null)
            {
                status = "0";
                message = "未将对象引入实例";
            }
            else
            {
                if (dt.Rows.Count == 0)
                {
                    status = "1";
                    message = "数据不存在任何行";
                    items = "[]";
                }
                else
                {

                    status = "1";
                    message = "加载成功";
                    items = dt.ToJsonString();
                }
            }
        }

    }

    public class AjaxMessageResult : AjaxResult
    {
        public AjaxMessageResult(string str_mesage)
        {
            message = str_mesage;
        }
        public AjaxMessageResult(Exception ex)
        {
            message = ex.Message;
            WriteLog(ex.ToString());
        }

        public virtual void WriteLog(string Content)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(@"~\LogFile");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                System.IO.File.AppendAllText(path + @"\" + DateTime.Now.ToString("yyyyMMdd") + ".txt", DateTime.Now.ToString("yyyyMMdd_HHmmss") + "   " + Content + Environment.NewLine);
            }
            catch
            {

            }
        }

    }

    public class AjaxSuccessResult : AjaxMessageResult
    {
        public AjaxSuccessResult(string str_mesage) : base(str_mesage)
        {
            status = "1";
        }
    }

    public class AjaxErrorResult : AjaxMessageResult
    {
        public AjaxErrorResult(string str_mesage) : base(str_mesage)
        {
            status = "0";
        }
    }

    public class AjaxExceptionResult : AjaxMessageResult
    {
        public AjaxExceptionResult(string str_mesage) : base(str_mesage)
        {
            status = "2";
        }
        public AjaxExceptionResult(Exception ex) : base(ex)
        {
            status = "2";
        }
    }

    public class AjaxNoRightResult : AjaxMessageResult
    {
        public AjaxNoRightResult(string str_mesage) : base(str_mesage)
        {
            status = "-1";
        }
    }

    public class AjaxNoParameterResult : AjaxMessageResult
    {
        public AjaxNoParameterResult(string str_mesage) : base(str_mesage)
        {
            status = "-2";
        }
    }

}