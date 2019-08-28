using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ERPBase
{
    public class AjaxBasePage : System.Web.UI.Page
    {

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
}

