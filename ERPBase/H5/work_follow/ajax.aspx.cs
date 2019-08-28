using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPBase;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class ajax : System.Web.UI.Page
{
    ERPBaseContext db = new ERPBaseContext();

    #region ajax基础方法

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

    #endregion

    public string f()
    {
        return "f";
    }

    public string get_department()
    {
        string strsql = "SELECT DP_ID,DP_DEPARTMENT_NAME,substring(RTRIM(LTRIM( DP_DEPARTMENT_NAME)),1,1) as DP_NAME   FROM AP_DEPARTMENT WHERE DSTATUS=1 ORDER BY DP_ID";
        DataTable dt = GetDataTable(strsql, null);
        return new AjaxDataResult(dt).ToString();
    }

    public string get_user()
    {
        string strsql = "select UR_USER_ID,UR_NAME,UR_HEAD_IMAGE,UR_DEPARTMENT_ID,(select DP_DEPARTMENT_NAME from AP_DEPARTMENT where DP_ID=UR_DEPARTMENT_ID) as DP_DEPARTMENT_NAME,isnull(UR_NAME,'')+isnull(UR_PHONE,'')  as content from AP_USER where UR_ACTIVE=1 order by UR_USER_ID";
        DataTable dt = GetDataTable(strsql, null);
        return new AjaxDataResult(dt).ToString();
    }

    public object GetControlValue(H5Columns item_column)
    {
        if (item_column.HC_CONTROL_TYPE == "H5Date" || item_column.HC_CONTROL_TYPE == "H5DateTime")
        {
            if (string.IsNullOrEmpty(Request[item_column.HC_NAME]))
            {
                return DBNull.Value;
            }
        }

        if (item_column.HC_CONTROL_TYPE == "H5NumberBox")
        {
            if (string.IsNullOrEmpty(Request[item_column.HC_NAME]))
            {
                return DBNull.Value;
            }
        }
        return Request[item_column.HC_NAME];
    }

    public string add_work_form()
    {
        string function_code = Request["function_code"];
        if (string.IsNullOrEmpty(function_code))
        {
            return new AjaxErrorResult("function_code不能为空").ToString();
        }

        H5Objects o = GData.GetObject(Convert.ToInt32(function_code));
        List<H5Columns> list_column = GData.GetColumn(Convert.ToInt32(function_code));
        StringBuilder strsql = new StringBuilder();
        StringBuilder strsql_field = new StringBuilder();
        StringBuilder strsql_para = new StringBuilder();
        List<SqlParameter> list_para = new List<SqlParameter>();
        foreach (H5Columns item_column in list_column)
        {
            strsql_field.Append("," + item_column.HC_NAME);
            strsql_para.Append(",@" + item_column.HC_NAME);
            list_para.Add(new SqlParameter("@" + item_column.HC_NAME, GetControlValue(item_column)));

        }

        strsql_field.Append("," + o.HO_USER_FIELD);
        strsql_para.Append(",@" + o.HO_USER_FIELD);
        list_para.Add(new SqlParameter("@" + o.HO_USER_FIELD, Cookies.UserCode));

        strsql_field.Append("," + o.HO_DATE_FIELD);
        strsql_para.Append(",@" + o.HO_DATE_FIELD);
        list_para.Add(new SqlParameter("@" + o.HO_DATE_FIELD, DateTime.Now));

        strsql_field.Append("," + o.HO_STAUTS_FIELD);
        strsql_para.Append(",@" + o.HO_STAUTS_FIELD);
        list_para.Add(new SqlParameter("@" + o.HO_STAUTS_FIELD, "0"));

        strsql.Append("insert into " + o.HO_TABLE_NAME);
        strsql.Append("(" + strsql_field.ToString().Substring(1) + ")");
        strsql.Append("values");
        strsql.Append("(" + strsql_para.ToString().Substring(1) + ")");

        strsql.Append("  " + "select @ID=@@identity");
        SqlParameter para_id = new SqlParameter("@ID", SqlDbType.Int, 8);
        para_id.Direction = ParameterDirection.Output;
        list_para.Add(para_id);

        SqlConnection cn = comm_fun.get_cn();
        try
        {
            bool b_reslut = comm_fun.ExecuteNonQuery(strsql.ToString(), cn, list_para);
            if (b_reslut)
            {
                int id = Convert.ToInt32(para_id.Value);
                add_work_follow(Convert.ToInt32(Cookies.UserCode), Convert.ToInt32(Request["to_user"]), Convert.ToInt32(function_code), id, string.Empty);
                return new AjaxSuccessResult("保存成功" + id.ToString()).ToString();
            }
            else
            {
                return new AjaxErrorResult("执行失败").ToString();
            }
        }
        catch
        {
            return new AjaxExceptionResult("执行异常").ToString();
        }
        finally

        {
            comm_fun.CloseConnection(cn);
        }
    }

    public string get_work_form_waitting()
    {
        string function_code = Request["function_code"];
        if (string.IsNullOrEmpty(function_code))
        {
            return new AjaxErrorResult("function_code不能为空").ToString();
        }
        FunctionCode = Convert.ToInt32(function_code);

        SqlConnection cn_count = comm_fun.get_cn();
        string count = "0";
        try
        {
            StringBuilder str_count = new StringBuilder();
            StringBuilder str_where = new StringBuilder();
            H5Objects o = GData.GetObject(Convert.ToInt32(function_code));
            List<SqlParameter> list_para = new List<SqlParameter>();

            str_where.Append("  and " + o.HO_STAUTS_FIELD + "  in (0,3) ");//待审核或者待终审
            str_where.Append(" and " + o.HO_USER_FIELD + "=@" + o.HO_USER_FIELD);
            list_para.Add(new SqlParameter("@" + o.HO_USER_FIELD, Cookies.UserCode));

            str_count.Append("select " + "count(1)" + "  from " + o.HO_TABLE_NAME + " where 1=1 " + str_where);
            count = comm_fun.ExecuteScalar(str_count.ToString(), cn_count, list_para);

        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
            count = "0";
        }
        finally
        {
            comm_fun.CloseConnection(cn_count);
        }

        if (Convert.ToInt32(count) > 99)
        {
            count = "99";
        }
        return new AjaxSuccessResult(count).ToString();
    }

    private bool add_work_follow(int WF_FROM_USER, int WF_TO_USER, int WF_BUSINESS_TYPE, int WF_BUSINESS_KEY, string WF_REASON)
    {
        //"SP_WORK_FOLLOW"
        StringBuilder strsql = new StringBuilder();
        List<SqlParameter> list_para = new List<SqlParameter>();
        strsql.Append("insert into SP_WORK_FOLLOW");
        strsql.Append("(WF_FROM_USER, WF_TO_USER, WF_BUSINESS_TYPE, WF_BUSINESS_KEY, WF_STATUS, WF_CREATE_DATE, WF_APPROVE_DATE, WF_REASON, WF_TIMES)");
        //strsql.Append("()");
        strsql.Append("values");
        //strsql.Append("()");
        strsql.Append("(@WF_FROM_USER, @WF_TO_USER, @WF_BUSINESS_TYPE, @WF_BUSINESS_KEY, @WF_STATUS, @WF_CREATE_DATE, @WF_APPROVE_DATE, @WF_REASON, @WF_TIMES)");


        list_para.Add(new SqlParameter("@WF_FROM_USER", WF_FROM_USER));
        list_para.Add(new SqlParameter("@WF_TO_USER", WF_TO_USER));
        list_para.Add(new SqlParameter("@WF_BUSINESS_TYPE", WF_BUSINESS_TYPE));
        list_para.Add(new SqlParameter("@WF_BUSINESS_KEY", WF_BUSINESS_KEY));
        list_para.Add(new SqlParameter("@WF_STATUS", "0"));
        list_para.Add(new SqlParameter("@WF_CREATE_DATE", DateTime.Now));
        list_para.Add(new SqlParameter("@WF_APPROVE_DATE", DBNull.Value));
        list_para.Add(new SqlParameter("@WF_REASON", WF_REASON));
        list_para.Add(new SqlParameter("@WF_TIMES", "0"));

        SqlConnection cn = comm_fun.get_cn();
        try
        {
            return comm_fun.ExecuteNonQuery(strsql.ToString(), cn, list_para);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            return false;
        }
        finally
        {
            comm_fun.CloseConnection(cn);
        }
    }

    public string get_work_form_list()
    {
        string function_code = Request["function_code"];
        if (string.IsNullOrEmpty(function_code))
        {
            return new AjaxErrorResult("function_code不能为空").ToString();
        }
        FunctionCode = Convert.ToInt32(function_code);

        string page = Request["page"];
        if (string.IsNullOrEmpty(page))
        {
            return new AjaxErrorResult("page不能为空").ToString();
        }

        H5Objects o = GData.GetObject(Convert.ToInt32(function_code));
        List<H5Columns> list_column = GData.GetColumn(Convert.ToInt32(function_code));
        List<SqlParameter> list_para = new List<SqlParameter>();
        StringBuilder strsql = new StringBuilder();
        StringBuilder str_column = new StringBuilder();

        StringBuilder str_where = new StringBuilder();
        StringBuilder str_order = new StringBuilder();
        StringBuilder str_count = new StringBuilder();

        str_where.Append(" and " + o.HO_USER_FIELD + "=@" + o.HO_USER_FIELD);
        str_order.Append(" order by " + o.HO_DATE_FIELD + " desc");
        list_para.Add(new SqlParameter("@" + o.HO_USER_FIELD, Cookies.UserCode));

        SqlConnection cn_count = comm_fun.get_cn();
        string count = "0";
        try
        {
            str_count.Append("select " + "count(1)" + "  from " + o.HO_TABLE_NAME + " where 1=1 " + str_where);
            count = comm_fun.ExecuteScalar(str_count.ToString(), cn_count, list_para);
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
            count = "0";
        }
        finally
        {
            comm_fun.CloseConnection(cn_count);
        }

        int SAO_PAGE_SIZE = 20;
        int SAO_CURRENT_PAGE = Convert.ToInt32(page);


        str_column.Append(o.HO_ID_FIELD + " as id");
        str_column.Append("," + o.HO_USER_FIELD + " as user_id");
        str_column.Append(",dbo.fn_convert_datetime(" + o.HO_DATE_FIELD + ") as date");
        str_column.Append("," + o.HO_STAUTS_FIELD + " as status");
        str_column.Append("," + o.HO_MASTER_FIELD + " as master");

        strsql.Append("select top " + SAO_PAGE_SIZE + "  " + str_column.ToString() + "  from " + o.HO_TABLE_NAME + " where 1=1");
        strsql.Append(" and " + o.HO_ID_FIELD + "  not in (select top " + (SAO_CURRENT_PAGE * SAO_PAGE_SIZE).ToString() + " " + o.HO_ID_FIELD + "   " + "  from " + o.HO_TABLE_NAME + " where 1=1 " + str_where.ToString() + "  " + str_order.ToString() + ")" + str_where.ToString() + "  " + str_order.ToString());



        DataTable dt = new DataTable();
        SqlConnection cn = comm_fun.get_cn();
        try
        {
            dt = comm_fun.GetDatatable(strsql.ToString(), cn, list_para);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("user_name");
                dt.Columns.Add("user_image");
                dt.Columns.Add("status_name");
                dt.Columns.Add("status_class");

                foreach (DataRow dr in dt.Rows)
                {
                    dr["user_name"] = get_user_name(dr["user_id"].ToString());
                    dr["user_image"] = GData.get_user_image(dr["user_id"].ToString());

                    dr["status_name"] = GData.get_status_name(dr["status"].ToString());
                    dr["status_class"] = GData.get_status_class(dr["status"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
        }
        if (dt.Rows.Count > 0)
        {
            return new AjaxDataResult(dt).ToString();
        }
        else
        {
            return new AjaxErrorResult("").ToString();
        }


    }

    public int FunctionCode { get; set; }
    public H5Objects H5Object
    {
        get
        {
            return GData.GetObject(FunctionCode);
        }
    }

    private static Dictionary<string, string> ht_name = new Dictionary<string, string>();
    private Dictionary<string, string> get_user_name_ht()
    {
        if (ht_name.Count > 0)
        {
            return ht_name;
        }

        string strsql = "select UR_USER_ID,UR_NAME from AP_USER ";
        DataTable dt = GetDataTable(strsql, null);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ht_name.Add(dr["UR_USER_ID"].ToString(), dr["UR_NAME"].ToString());
            }
        }
        return ht_name;
    }


    private static Dictionary<string, string> ht_image = new Dictionary<string, string>();


    private string get_user_name(string user_id)
    {
        Dictionary<string, string> ht = get_user_name_ht();
        string name = string.Empty;
        if (ht.Keys.Contains(user_id))
        {
            name = ht[user_id];
        }
        return name + "的" + H5Object.HO_NAME;
    }

    private DataTable GetDataTable(string strsql, List<SqlParameter> list)
    {
        SqlConnection cn = comm_fun.get_cn();
        try
        {
            return comm_fun.GetDatatable(strsql, cn, list);
        }
        catch
        {
            return new DataTable();
        }
        finally
        {
            comm_fun.CloseConnection(cn);
        }
    }

    public string handle_work_follow()
    {
        string function_code = Request["function_code"];
        if (string.IsNullOrEmpty(function_code))
        {
            return new AjaxErrorResult("function_code不能为空").ToString();
        }
        FunctionCode = Convert.ToInt32(function_code);

        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            return new AjaxErrorResult("id不能为空").ToString();
        }
        int Id = Convert.ToInt32(id);

        string WF_STATUS = Request["WF_STATUS"];
        if (string.IsNullOrEmpty(WF_STATUS))
        {
            return new AjaxErrorResult("WF_STATUS不能为空").ToString();
        }

        string WF_REASON = Request["WF_REASON"];
        if (string.IsNullOrEmpty(WF_REASON))
        {
            WF_REASON = string.Empty;
        }

        string WF_TO_USER = Request["WF_TO_USER"];
        H5Objects obj = H5Object;

        try
        {
            if (WF_STATUS == "1")
            {
                int to_user = 0;
                //审批通过
                if (string.IsNullOrEmpty(WF_TO_USER))
                {
                    to_user = GData.get_administrator(obj.HO_BUSINESS_TYPE);
                }
                else
                {
                    to_user = Convert.ToInt32(WF_TO_USER);
                }
                //普通审核（更新本数据，插入待审核数据）

                int my_id = Convert.ToInt32(Cookies.UserCode);

                //更新本数据
                SP_WORK_FOLLOW one_work = db.SP_WORK_FOLLOW.Where(o => o.WF_BUSINESS_TYPE == obj.HO_BUSINESS_TYPE && o.WF_BUSINESS_KEY == Id && o.WF_TO_USER == my_id && (o.WF_STATUS == 0 || o.WF_STATUS == 3)).FirstOrDefault();
                one_work.WF_STATUS = Convert.ToInt32(WF_STATUS);
                one_work.WF_REASON = WF_REASON;
                one_work.WF_APPROVE_DATE = DateTime.Now;

                //判断自己是否为终审人，
                if (GData.check_administrator(obj.HO_BUSINESS_TYPE, Cookies.UserCode))
                {
                    //更新状态为终审
                    update_form_status(Id, 1, FunctionCode);
                    db.SaveChanges();
                    AjaxSuccessResult re = new AjaxSuccessResult("保存成功");
                    re.message = GData.get_status_name("1");
                    re.primary_key = "1";
                    return re.ToString();
                }
                else
                {
                    //插入待审数据
                    SP_WORK_FOLLOW new_work = new SP_WORK_FOLLOW();
                    new_work.WF_BUSINESS_KEY = one_work.WF_BUSINESS_KEY;
                    new_work.WF_BUSINESS_TYPE = one_work.WF_BUSINESS_TYPE;
                    new_work.WF_CREATE_DATE = DateTime.Now;
                    new_work.WF_FROM_USER = my_id;
                    new_work.WF_STATUS = 0;
                    new_work.WF_TO_USER = to_user;
                    db.SP_WORK_FOLLOW.Add(new_work);


                    //判断下一个审批人是否为终审人，是就改变状态为待终审
                    if (GData.check_administrator(obj.HO_BUSINESS_TYPE, to_user.ToString()))
                    {
                        update_form_status(Id, 3, FunctionCode);
                        db.SaveChanges();

                        AjaxSuccessResult re = new AjaxSuccessResult("");
                        re.message = GData.get_status_name("3");
                        re.primary_key = "3";
                        return re.ToString();
                    }
                    else
                    {
                        db.SaveChanges();

                        AjaxSuccessResult re = new AjaxSuccessResult("");
                        re.message = GData.get_status_name("0");
                        re.primary_key = "0";
                        return re.ToString();
                    }
                }

            }

            if (WF_STATUS == "2")
            {
                //驳回（更新本数据，更新主数据）

                //更新本数据
                int my_id = Convert.ToInt32(Cookies.UserCode);
                SP_WORK_FOLLOW one_work = db.SP_WORK_FOLLOW.Where(o => o.WF_BUSINESS_TYPE == obj.HO_BUSINESS_TYPE && o.WF_BUSINESS_KEY == Id && o.WF_TO_USER == my_id && o.WF_STATUS == 0).FirstOrDefault();
                one_work.WF_STATUS = Convert.ToInt32(WF_STATUS);
                one_work.WF_REASON = WF_REASON;
                one_work.WF_APPROVE_DATE = DateTime.Now;

                //更新主数据[驳回]
                update_form_status(Id, 2, FunctionCode);

                db.SaveChanges();

                AjaxSuccessResult re = new AjaxSuccessResult("保存成功");
                re.message = GData.get_status_name("2");
                re.primary_key = "2";
                return re.ToString();
            }

            if (WF_STATUS == "4")
            {
                //更新主数据[撤回]
                update_form_status(Id, 4, FunctionCode);
                AjaxSuccessResult re = new AjaxSuccessResult("保存成功");
                re.message = GData.get_status_name("4");
                re.primary_key = "4";
                return re.ToString();

            }

            return new AjaxSuccessResult("保存成功").ToString();
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
            return new AjaxErrorResult("保存失败").ToString();
        }






    }


    public bool update_form_status(int id, int status, int function_code)
    {
        H5Objects o = GData.GetObject(function_code);
        StringBuilder strsql = new StringBuilder();
        List<SqlParameter> list_para = new List<SqlParameter>();
        strsql.Append("update " + o.HO_TABLE_NAME + " set " + o.HO_STAUTS_FIELD + "=@" + o.HO_STAUTS_FIELD + " where " + o.HO_ID_FIELD + "=@" + o.HO_ID_FIELD);
        list_para.Add(new SqlParameter("@" + o.HO_STAUTS_FIELD, status));
        list_para.Add(new SqlParameter("@" + o.HO_ID_FIELD, id));
        SqlConnection cn = comm_fun.get_cn();
        try
        {
            return comm_fun.ExecuteNonQuery(strsql.ToString(), cn, list_para);
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
            return false;
        }
        finally
        {
            comm_fun.CloseConnection(cn);
        }
    }


    public string get_approve_work_form_list()
    {

        string function_code_list = Request["function_code_list"];
        if (string.IsNullOrEmpty(function_code_list))
        {
            return new AjaxErrorResult("function_code_list不能为空").ToString();
        }

        string type = Request["type"];
        string page = Request["page"];

        StringBuilder str_table = new StringBuilder();
        StringBuilder str_table_column = new StringBuilder();
        List<string> list = function_code_list.Split("_".ToCharArray()).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            string function_code = list[i];

            H5Objects o = GData.GetObject(Convert.ToInt32(function_code));
            List<H5Columns> list_column = GData.GetColumn(Convert.ToInt32(function_code));

            if (i > 0)
            {
                str_table_column.Append("  union all ");
            }

            str_table_column.Append(" select ");
            str_table_column.Append(o.HO_ID_FIELD + " as id");
            str_table_column.Append("," + o.HO_USER_FIELD + " as user_id");
            str_table_column.Append(",dbo.fn_convert_datetime(" + o.HO_DATE_FIELD + ") as date");
            str_table_column.Append("," + o.HO_STAUTS_FIELD + " as status");
            str_table_column.Append("," + o.HO_MASTER_FIELD + " as master");
            str_table_column.Append("," + o.HO_DATE_FIELD + " as create_date ");
            str_table_column.Append("," + o.HO_BUSINESS_TYPE + " as business");
            str_table_column.Append("," + "'" + o.HO_NAME + "'" + " as business_text");
            str_table_column.Append("  from " + o.HO_TABLE_NAME + "");
        }

        str_table.Append("(" + str_table_column.ToString() + ") as sp_view");



        //H5Objects o = GData.GetObject(Convert.ToInt32(function_code));
        //List<H5Columns> list_column = GData.GetColumn(Convert.ToInt32(function_code));
        List<SqlParameter> list_para = new List<SqlParameter>();
        List<SqlParameter> list_para_count = new List<SqlParameter>();
        StringBuilder strsql = new StringBuilder();
        StringBuilder str_column = new StringBuilder();

        StringBuilder str_where = new StringBuilder();
        StringBuilder str_order = new StringBuilder();
        StringBuilder str_count = new StringBuilder();

        //我的发起
        if (type == "me")
        {
            str_where.Append(" and user_id=@user_id");
            list_para.Add(new SqlParameter("@user_id", Cookies.UserCode));
            list_para_count.Add(new SqlParameter("@user_id", Cookies.UserCode));
        }

        //我的审批
        if (type == "other")
        {
            str_table.Append(" inner join SP_WORK_FOLLOW on WF_BUSINESS_TYPE=business and WF_BUSINESS_KEY=id ");
            str_where.Append(" and WF_TO_USER=@WF_TO_USER");
            list_para.Add(new SqlParameter("@WF_TO_USER", Cookies.UserCode));
            list_para_count.Add(new SqlParameter("@WF_TO_USER", Cookies.UserCode));
        }

        str_order.Append(" order by create_date desc");


        SqlConnection cn_count = comm_fun.get_cn();
        string count = "0";
        try
        {
            str_count.Append("select " + "count(1)" + "  from " + str_table.ToString() + " where 1=1 " + str_where);
            count = comm_fun.ExecuteScalar(str_count.ToString(), cn_count, list_para_count);
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
            count = "0";
        }
        finally
        {
            comm_fun.CloseConnection(cn_count);
        }

        int SAO_PAGE_SIZE = 20;
        int SAO_CURRENT_PAGE = Convert.ToInt32(page);


        str_column.Append("id");
        str_column.Append(",user_id");
        str_column.Append(",date");
        str_column.Append(",status");
        str_column.Append(",master");
        str_column.Append(",business");
        str_column.Append(",business_text");
        if (type == "other")
        {
            str_column.Append(",WF_STATUS");
        }
        

        strsql.Append("select top " + SAO_PAGE_SIZE + "  " + str_column.ToString() + "  from " + str_table.ToString() + " where 1=1");
        strsql.Append(" and id not in (select top " + (SAO_CURRENT_PAGE * SAO_PAGE_SIZE).ToString() + " id  " + "  from " + str_table.ToString() + " where 1=1 " + str_where.ToString() + "  " + str_order.ToString() + ")" + str_where.ToString() + "  " + str_order.ToString());



        DataTable dt = new DataTable();
        SqlConnection cn = comm_fun.get_cn();
        try
        {
            dt = comm_fun.GetDatatable(strsql.ToString(), cn, list_para);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("user_name");
                dt.Columns.Add("user_image");
                dt.Columns.Add("status_name");
                dt.Columns.Add("status_class");

                foreach (DataRow dr in dt.Rows)
                {
                    dr["user_name"] = get_user_name(dr["user_id"].ToString());
                    dr["user_image"] = GData.get_user_image(dr["user_id"].ToString());

                    dr["status_name"] = GData.get_status_name(dr["status"].ToString());
                    dr["status_class"] = GData.get_status_class(dr["status"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
        }
        if (dt.Rows.Count > 0)
        {
            return new AjaxDataResult(dt).ToString();
        }
        else
        {
            return new AjaxErrorResult("").ToString();
        }
    }

}



