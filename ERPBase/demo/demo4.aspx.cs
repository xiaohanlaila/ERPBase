using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPBase;
using System.Data.SqlClient;


public partial class demo4 : maintenance1
{
    protected override void OnPreInit(EventArgs e)
    {
        SO_ID = 1;
        base.OnPreInit(e);
    }

    protected override SogNavigate NavigateInit()
    {
        Dictionary<string, string> ht = new Dictionary<string, string>();
        ht.Add("1", "在职");
        ht.Add("0", "离职");
        SogNavigate nv = new SogNavigate();
        nv.DataSource = ht;
        nv.Value = "1";
        nv.target = "UR_ACTIVE";
        return nv;
    }

    public override string GetOtherOrder()
    {
        return "order by ur_user_id desc ";
    }

    public override Dictionary<string, string> GetOtherSearch()
    {
        Dictionary<string, string> ht = new Dictionary<string, string>();
        ht.Add("UR_ACTIVE", " and UR_ACTIVE=@UR_ACTIVE ");
        return ht;
    }

}
