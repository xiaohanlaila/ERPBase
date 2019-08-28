using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERPBase.H5.sms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> l = new List<string>();
                l.Add("赵泽辉");
                l.Add("机票");
                bool b = SMS.Send("13430380816", 392035, l);
                Response.Write(b);
            }
        }
    }
}