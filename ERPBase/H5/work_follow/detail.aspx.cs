using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERPBase.H5.work_follow
{
    public partial class detail : H5WorkFollowDetail
    {
        protected override void OnPreInit(EventArgs e)
        {
            string function_code = Request["function_code"];
            if (string.IsNullOrEmpty(function_code))
            {
                Response.Redirect("~/H5/work_follow/Error.aspx");
            }
            FunctionCode = Convert.ToInt32(function_code);
            base.OnPreInit(e);
        }
    }
}