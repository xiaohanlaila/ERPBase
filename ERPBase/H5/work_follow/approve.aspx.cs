using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPBase;


public partial class approve : H5Approve
{
    protected override void OnPreInit(EventArgs e)
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(2);
        FunctionCodeList = list;
    }
}
