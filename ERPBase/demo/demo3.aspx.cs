using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERPBase.demo
{
    public partial class demo3 : maintenance
    {
        protected override void OnPreInit(EventArgs e)
        {
            FunctionCode = 123;
            base.OnPreInit(e);
        }
    }
}