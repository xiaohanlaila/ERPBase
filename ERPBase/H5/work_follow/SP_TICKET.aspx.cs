using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERPBase.H5.work_follow
{
    public partial class SP_TICKET : H5LoginApp
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/H5/work_follow/add.aspx?function_code=1");
        }
    }
}