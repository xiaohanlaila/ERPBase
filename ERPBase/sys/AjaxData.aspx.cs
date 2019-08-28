using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPBase;
using System.Data;
using System.Text;
using System.Data.SqlClient;



public partial class AjaxData : AjaxSmartBasePage
{

    public int SO_ID
    {
        get
        {
            return Convert.ToInt32(Request["SO_ID"]);
        }
    }
    public string fx()
    {
        return "fx";
    }

    public SYS_OBJECTS OBJECT
    {
        get
        {
            return GSYS.GetObjects(SO_ID);
        }
    }

    /// <summary>
    /// 所有字段
    /// </summary>
    public List<SYS_COLUMNS> COLUMNS
    {
        get
        {
            return GSYS.GetColumns(SO_ID);
        }
    }

    /// <summary>
    /// 所有字段
    /// </summary>
    public List<SYS_COLUMNS> COLUMNS_TABLE
    {
        get
        {
            return COLUMNS.Where(O => O.SC_IS_TABLE == true).ToList();
        }
    }

    /// <summary>
    /// 条件字段
    /// </summary>
    public List<SYS_COLUMNS> COLUMNS_SEARCH
    {
        get
        {
            return COLUMNS.Where(o => o.SC_IS_SEARCH = true).ToList();
        }
    }

    public List<SYS_COLUMNS> COLUMNS_ADD
    {
        get
        {
            return COLUMNS.Where(o => o.SC_IS_ADD == true).ToList();
        }
    }
    public List<SYS_COLUMNS> COLUMNS_EDIT
    {
        get
        {
            return COLUMNS.Where(o => o.SC_IS_EDIT == true).ToList();
        }
    }

    public string GetTable()
    {
        Content_Type = "text/html";

        string CurrentPage = Request["CurrentPage"];
        if (string.IsNullOrEmpty(CurrentPage))
        {
            CurrentPage = "0";
        }

        //当前页码
        int SAO_CURRENT_PAGE = Convert.ToInt32(CurrentPage);

        //每页条数
        int SAO_PAGE_SIZE = 20;

        //1通过code 查询配置表
        //构造sql语句
        //查询记录条数
        //按照分页查询语句查出打datatable
        //配置SogTable 生成html返回
        //配置Sogpages 生成html返回
        StringBuilder strsql = new StringBuilder();
        StringBuilder str_count = new StringBuilder();
        StringBuilder str_column = new StringBuilder();
        StringBuilder str_where = new StringBuilder();
        StringBuilder str_order = new StringBuilder();

        List<SqlParameter> list_para = new List<SqlParameter>();


        StringBuilder SogTextSQL = new StringBuilder();
        foreach (SYS_COLUMNS c in COLUMNS_SEARCH)
        {
            if (!string.IsNullOrEmpty(c.SC_CONTROL_DATA))
            {
                string value = Request[c.SC_COLUMN_NAME];
                if (!string.IsNullOrEmpty(value))
                {
                    str_where.Append(" and " + c.SC_COLUMN_NAME + "=@" + c.SC_COLUMN_NAME + " ");
                    list_para.Add(new SqlParameter("@" + c.SC_COLUMN_NAME, Request[c.SC_COLUMN_NAME]));
                }
            }
            else
            {
                string value = Request["SogText"];
                if (!string.IsNullOrEmpty(value))
                {
                    if (SogTextSQL.Length == 0)
                    {
                        SogTextSQL.Append(" " + c.SC_COLUMN_NAME + " like @" + c.SC_COLUMN_NAME + " ");
                    }
                    else
                    {
                        SogTextSQL.Append("or " + c.SC_COLUMN_NAME + " like @" + c.SC_COLUMN_NAME + " ");
                    }
                    list_para.Add(new SqlParameter("@" + c.SC_COLUMN_NAME, "%" + Request["SogText"] + "%"));
                }
            }
        }

        //and (a like @a or b like @b)
        if (SogTextSQL.Length != 0)
        {
            str_where.Append(" and (" + SogTextSQL + ")");
        }

        string session_other_sql = Session["OtherSQL" + SO_ID.ToString()].ToString();
        if (!string.IsNullOrEmpty(session_other_sql))
        {
            List<SqlParameter> session_other_para = (List<SqlParameter>)Session["list_para" + SO_ID.ToString()];
            str_where.Append(session_other_sql);
            list_para.AddRange(session_other_para);
        }


        Dictionary<string, string> ht_order_search = (Dictionary<string, string>)Session["OtherSearch" + SO_ID.ToString()];
        foreach (KeyValuePair<string, string> item in ht_order_search)
        {
            string value = Request[item.Key];
            if (!string.IsNullOrEmpty(value))
            {
                str_where.Append(" " + item.Value + " ");
                list_para.Add(new SqlParameter("@" + item.Key, value));
            }
        }



        string session_other_order = Session["OtherOrder" + SO_ID.ToString()].ToString();
        if (string.IsNullOrEmpty(session_other_order))
        {
            str_order.Append("order by " + OBJECT.SO_TABLE_KEY + " desc");
        }
        else
        {
            str_order.Append(session_other_order);
        }


        str_count.Append("select " + "count(1)" + "  from " + OBJECT.SO_TABLE_NAME_SEARCH + " where 1=1 " + str_where);

        SqlConnection cn_count = comm_fun.get_cn();
        string count = "0";
        try
        {
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

        str_column.Append(OBJECT.SO_TABLE_KEY);
        foreach (SYS_COLUMNS c in COLUMNS_TABLE)
        {
            //str_column.Append("," + item.SC_COLUMN_NAME);

            if (c.SC_CONTROL_TYPE == "SogDate")
            {
                str_column.Append("," + "dbo.fn_convert_date(" + c.SC_COLUMN_NAME + ") as " + c.SC_COLUMN_NAME);
            }
            else if (c.SC_CONTROL_TYPE == "SogDateTime")
            {
                str_column.Append("," + "dbo.fn_convert_datetime(" + c.SC_COLUMN_NAME + ") as " + c.SC_COLUMN_NAME);
            }
            else
            {
                str_column.Append("," + c.SC_COLUMN_NAME);
            }
        }

        strsql.Append("select top " + SAO_PAGE_SIZE + "  " + str_column.ToString() + "  from " + OBJECT.SO_TABLE_NAME_SEARCH + " where 1=1");
        strsql.Append(" and " + OBJECT.SO_TABLE_KEY + "  not in (select top " + (SAO_CURRENT_PAGE * SAO_PAGE_SIZE).ToString() + " " + OBJECT.SO_TABLE_KEY + "   " + "  from " + OBJECT.SO_TABLE_NAME_SEARCH + " where 1=1 " + str_where.ToString() + "  " + str_order.ToString() + ")" + str_where.ToString() + "  " + str_order.ToString());


        SqlConnection cn = comm_fun.get_cn();
        DataTable dt = new DataTable();
        try
        {
            dt = comm_fun.GetDatatable(strsql.ToString(), cn, list_para);
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
        }
        finally
        {
            comm_fun.CloseConnection(cn);
        }


        for (Int32 i = 0; i < dt.Rows.Count; i++)
        {
            List<SYS_COLUMNS> list = COLUMNS.Where(o => string.IsNullOrEmpty(o.SC_CONTROL_DATA) == false).ToList();

            foreach (SYS_COLUMNS item in list)
            {
                DataRow dr = dt.Rows[i];
                dr[item.SC_COLUMN_NAME.ToColumnName()] = GetDataDescription(GetControlData(item.SC_CONTROL_DATA), dr[item.SC_COLUMN_NAME.ToColumnName()].ToString());
            }
        }

        //文件解析
        List<SYS_COLUMNS> list_column_file = COLUMNS_TABLE.Where(o => o.SC_CONTROL_TYPE == "SogFileUpload").ToList();
        foreach (SYS_COLUMNS c in list_column_file)
        {
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr[c.SC_COLUMN_NAME.ToColumnName()] = GetFileDescription(dr[c.SC_COLUMN_NAME.ToColumnName()].ToString());
            }
        }



        //文件夹解析
        List<SYS_COLUMNS> list_column_folder = COLUMNS_TABLE.Where(o => o.SC_CONTROL_TYPE == "SogFolderUpload").ToList();
        foreach (SYS_COLUMNS c in list_column_folder)
        {
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                dr[c.SC_COLUMN_NAME.ToColumnName()] = GetFolderDescription(dr[c.SC_COLUMN_NAME.ToColumnName()].ToString());
            }
        }

        //日期控件解析
        //List<SYS_COLUMNS> list_column_date = COLUMNS_TABLE.Where(o => o.SC_CONTROL_TYPE == "SogDate").ToList();
        //foreach (SYS_COLUMNS c in list_column_date)
        //{
        //    dt.Columns.Add(c.SC_COLUMN_NAME.ToColumnName() + "_desc");

        //    for (Int32 i = 0; i < dt.Rows.Count; i++)
        //    {
        //        DataRow dr = dt.Rows[i];
        //        dr[c.SC_COLUMN_NAME.ToColumnName() + "_desc"] = GetDate(dr[c.SC_COLUMN_NAME.ToColumnName()].ToString());
        //    }
        //}







        SogTable tb = new SogTable();
        tb.Buttons = GSYS.GetButton(SO_ID);
        tb.DataSource = dt;
        tb.KeyField = OBJECT.SO_TABLE_KEY;
        tb.HasCheck = true;


        foreach (SYS_COLUMNS item in COLUMNS_TABLE)
        {
            //if (item.SC_CONTROL_TYPE == "SogDate")
            //{
            //    tb.Add(item.SC_COLUMN_NAME.ToColumnName() + "_desc", item.SC_COLUMN_DESC, "", item.SC_TABLE_CLASS);
            //}
            //else
            //{
            //    tb.Add(item.SC_COLUMN_NAME.ToColumnName(), item.SC_COLUMN_DESC, "", item.SC_TABLE_CLASS);
            //}

            tb.Add(item.SC_COLUMN_NAME.ToColumnName(), item.SC_COLUMN_DESC, "", item.SC_TABLE_CLASS);


        }

        SogPages p = new SogPages();
        p.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(count) / Convert.ToDouble(SAO_PAGE_SIZE)));
        p.CurrentPage = SAO_CURRENT_PAGE;

        return tb.ToHtml() + p.ToHtml();
    }

    /// <summary>
    /// 导出
    /// </summary>
    /// <returns></returns>
    public string Export()
    {
        return string.Empty;

    }

    /// <summary>
    /// 导入
    /// </summary>
    /// <returns></returns>
    public string Import()
    {
        return string.Empty;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <returns></returns>
    public string Add()
    {
        StringBuilder strsql = new StringBuilder();
        StringBuilder str_column = new StringBuilder();
        StringBuilder str_para = new StringBuilder();
        List<SqlParameter> list_para = new List<SqlParameter>();

        if (COLUMNS_ADD.Count > 0)
        {
            foreach (SYS_COLUMNS c in COLUMNS_ADD)
            {
                str_column.Append("," + c.SC_COLUMN_NAME);
                str_para.Append(",@" + c.SC_COLUMN_NAME);
                list_para.Add(new SqlParameter("@" + c.SC_COLUMN_NAME, GetControlValue(c)));
            }
            strsql.Append("insert into " + OBJECT.SO_TABLE_NAME + "(" + str_column.ToString().Substring(1) + ")");
            strsql.Append("values(" + str_para.ToString().Substring(1) + ")");

            SqlConnection cn = comm_fun.get_cn();
            try
            {
                bool b = comm_fun.ExecuteNonQuery(strsql.ToString(), cn, list_para);
                if (b)
                {
                    return new AjaxSuccessResult("保存成功").ToString();
                }
                else
                {
                    return new AjaxErrorResult("保存失败").ToString();
                }

            }
            catch (Exception ex)
            {
                comm_fun.WriteLog(ex.ToString());
                return new AjaxExceptionResult("保存异常").ToString();
            }
            finally
            {
                comm_fun.CloseConnection(cn);
            }
        }
        else
        {
            return new AjaxErrorResult("保存失败").ToString();
        }
        //System.Threading.Thread.Sleep(5000);
        //return new AjaxErrorResult("保存失败").ToString();
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <returns></returns>
    public string Edit()
    {
        StringBuilder strsql = new StringBuilder();
        StringBuilder str_column = new StringBuilder();
        StringBuilder str_where = new StringBuilder();
        List<SqlParameter> list_para = new List<SqlParameter>();
        if (COLUMNS_EDIT.Count > 0)
        {
            foreach (SYS_COLUMNS c in COLUMNS_EDIT)
            {
                str_column.Append("," + c.SC_COLUMN_NAME + "=@" + c.SC_COLUMN_NAME);
                list_para.Add(new SqlParameter("@" + c.SC_COLUMN_NAME, GetControlValue(c)));
            }

            str_where.Append(" where " + OBJECT.SO_TABLE_KEY + "=@" + OBJECT.SO_TABLE_KEY);
            list_para.Add(new SqlParameter("@" + OBJECT.SO_TABLE_KEY, Request["data_id"]));

            strsql.Append("update " + OBJECT.SO_TABLE_NAME + " set " + str_column.ToString().Substring(1) + " " + str_where.ToString());


            SqlConnection cn = comm_fun.get_cn();
            try
            {
                bool b = comm_fun.ExecuteNonQuery(strsql.ToString(), cn, list_para);
                if (b)
                {
                    return new AjaxSuccessResult("保存成功").ToString();
                }
                else
                {
                    return new AjaxErrorResult("保存失败").ToString();
                }

            }
            catch (Exception ex)
            {
                comm_fun.WriteLog(ex.ToString());
                return new AjaxExceptionResult("保存异常").ToString();
            }
            finally
            {
                comm_fun.CloseConnection(cn);
            }
        }
        else
        {
            return new AjaxErrorResult("保存失败").ToString();
        }

        //System.Threading.Thread.Sleep(500);
        //return new AjaxErrorResult("保存失败").ToString();
        //return new AjaxSuccessResult("保存成功").ToString();
    }

    /// <summary>
    /// 获取一条记录
    /// </summary>
    /// <returns></returns>
    public string GetOne()
    {
        if (COLUMNS_ADD.Count > 0)
        {
            StringBuilder strsql = new StringBuilder();
            StringBuilder str_column = new StringBuilder();
            StringBuilder str_where = new StringBuilder();
            List<SqlParameter> list_para = new List<SqlParameter>();

            foreach (SYS_COLUMNS c in COLUMNS_EDIT)
            {
                if (c.SC_CONTROL_TYPE == "SogDate")
                {
                    str_column.Append("," + "dbo.fn_convert_date(" + c.SC_COLUMN_NAME + ") as " + c.SC_COLUMN_NAME);
                }
                else if (c.SC_CONTROL_TYPE == "SogDateTime")
                {
                    str_column.Append("," + "dbo.fn_convert_datetime(" + c.SC_COLUMN_NAME + ") as " + c.SC_COLUMN_NAME);
                }
                else
                {
                    str_column.Append("," + c.SC_COLUMN_NAME);
                }
            }

            string data_id = Request["data_id"];
            str_where.Append(" and " + OBJECT.SO_TABLE_KEY + "=@" + OBJECT.SO_TABLE_KEY + "  ");
            list_para.Add(new SqlParameter("@" + OBJECT.SO_TABLE_KEY, data_id));

            strsql.Append("select " + str_column.ToString().Substring(1) + " from " + OBJECT.SO_TABLE_NAME + " where 1=1 " + str_where);

            SqlConnection cn = comm_fun.get_cn();
            DataTable dt = new DataTable();
            try
            {
                dt = comm_fun.GetDatatable(strsql.ToString(), cn, list_para);
                return new AjaxDataResult(dt).ToString();
            }
            catch (Exception ex)
            {
                comm_fun.WriteLog(ex.ToString());
                return new AjaxExceptionResult("查询异常").ToString();
            }
            finally
            {
                comm_fun.CloseConnection(cn);
            }
        }
        else
        {
            return new AjaxErrorResult("查询失败").ToString();
        }
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <returns></returns>
    public string Delete()
    {

        string data_id = Request["data_id"];
        SqlConnection cn = comm_fun.get_cn();
        try
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("DELETE FROM  " + OBJECT.SO_TABLE_NAME + " WHERE " + OBJECT.SO_TABLE_KEY + " IN (" + data_id + ")");
            bool b_result = comm_fun.ExecuteNonQuery(strsql.ToString(), cn, null);
            if (b_result)
            {
                return new AjaxSuccessResult("删除成功").ToString();
            }
            else
            {
                return new AjaxErrorResult("删除失败").ToString();
            }
        }
        catch (Exception ex)
        {
            comm_fun.WriteLog(ex.ToString());
            return new AjaxErrorResult("删除异常").ToString();
        }
        finally
        {
            comm_fun.CloseConnection(cn);
        }

        //System.Threading.Thread.Sleep(5000);
        //return new AjaxErrorResult("保存失败").ToString();
    }

    /// <summary>
    /// 列表
    /// </summary>
    /// <returns></returns>
    public string View()
    {
        SogAajaxTable t = new SogAajaxTable();
        t.CssClass = "SogTable";

        SogAajaxTr tr = new SogAajaxTr();
        t.Controls.Add(tr);

        return new AjaxSuccessResult(t.ToHtml()).ToString();
    }

    public class SogTable
    {
        protected class TableColumn
        {
            public string name { get; set; }
            public string desc { get; set; }
            public string format { get; set; }
            public string css { get; set; }
        }

        protected List<TableColumn> Columns = new List<TableColumn>();

        public List<SYS_TABLE_BUTTONS> Buttons = new List<SYS_TABLE_BUTTONS>();

        public bool HasCheck { get; set; }
        public DataTable DataSource { get; set; }

        public string KeyField { get; set; }

        public void Add(string column_name, string column_desc, string column_format, string column_css)
        {
            TableColumn t = new TableColumn();
            t.name = column_name;
            t.desc = column_desc;
            t.format = column_format;
            t.css = column_css;
            Columns.Add(t);
        }

        public string ToHtml()
        {
            SogAajaxTable tb = new SogAajaxTable();
            tb.CssClass = "SogTable";
            SogAajaxTr tr_head = new SogAajaxTr();

            if (HasCheck)
            {
                SogAajaxTh th = new SogAajaxTh();
                th.CssClass = "SogTdCheck";

                SogAjaxCheck chk = new SogAjaxCheck();
                chk.CssClass = "check_all";
                th.Controls.Add(chk);

                tr_head.Controls.Add(th);
            }
            foreach (TableColumn c in Columns)
            {
                SogAajaxTh th = new SogAajaxTh();
                th.target = c.name;
                th.CssClass = c.css;
                th.InnerText = c.desc;
                tr_head.Controls.Add(th);
            }

            foreach (SYS_TABLE_BUTTONS btn in Buttons)
            {
                SogAajaxTh th = new SogAajaxTh();
                th.CssClass = btn.SB_HEAD_CSSCLASS;
                th.InnerText = btn.SB_HEAD_TEXT;
                tr_head.Controls.Add(th);
            }

            tb.Controls.Add(tr_head);

            foreach (DataRow dr in DataSource.Rows)
            {
                SogAajaxTr tr_content = new SogAajaxTr();

                if (HasCheck)
                {
                    SogAajaxTh th = new SogAajaxTh();
                    th.CssClass = "SogTdCheck";

                    SogAjaxCheck chk = new SogAjaxCheck();
                    chk.CssClass = "check_one";
                    chk.Attributes.Add("data_id", dr[KeyField].ToString());
                    th.Controls.Add(chk);

                    tr_content.Controls.Add(th);
                }

                foreach (TableColumn c in Columns)
                {
                    SogAajaxTd td = new SogAajaxTd();
                    td.InnerText = dr[c.name].ToString();
                    tr_content.Controls.Add(td);
                }

                foreach (SYS_TABLE_BUTTONS btn in Buttons)
                {
                    SogAajaxTd td = new SogAajaxTd();
                    tr_content.Controls.Add(td);

                    SogAajaxSpan s = new SogAajaxSpan();
                    s.CssClass = btn.SB_INNER_CSSCLASS;
                    s.InnerText = btn.SB_INNER_TEXT;
                    s.Attributes.Add("data_id", dr[KeyField].ToString());
                    td.Controls.Add(s);
                }

                tb.Controls.Add(tr_content);
            }
            return tb.ToHtml();
        }
    }


    public class SogPages
    {
        public int CurrentPage { get; set; }

        public int TotalPage { get; set; }

        public int StarPage
        {
            get
            {
                return (CurrentPage / PageLength) * PageLength;
            }
        }


        public int PageLength = 10;

        public string ToHtml()
        {
            StringBuilder str_html = new StringBuilder();
            str_html.Append("<div class='SogPages' target='CurrentPage' value='" + CurrentPage.ToString() + "' >");


            if (StarPage > 0)
            {
                str_html.Append("<a value='" + (StarPage - 1) + "'>...</a>");
            }

            for (int i = StarPage; i < TotalPage; i++)
            {
                if (i >= StarPage + PageLength)
                {
                    break;
                }

                if (CurrentPage == i)
                {
                    str_html.Append("<a class='active' value='" + i.ToString() + "'>" + (i + 1).ToString() + "</a>");
                }
                else
                {
                    str_html.Append("<a value='" + i.ToString() + "'>" + (i + 1).ToString() + "</a>");
                }

            }

            if (StarPage + PageLength < TotalPage)
            {
                str_html.Append("<a value='" + (PageLength + StarPage) + "'>...</a>");
            }

            str_html.Append("</div>");

            return str_html.ToString();

        }
    }


    /// <summary>
    /// 查询简单数据
    /// </summary>
    /// <param name="str_control_data">数据源SQL</param>
    /// <returns>DataTable</returns>
    protected virtual DataTable GetControlData(string str_control_data)
    {
        if (IsHightPerformance)
        {
            if (ht_cache_data.ContainsKey(str_control_data))
            {
                return ht_cache_data[str_control_data];
            }
        }
        DataTable dt = comm_fun.GetDatatable(str_control_data, comm_fun.get_cn(), null);

        if (IsHightPerformance)
        {
            ht_cache_data[str_control_data] = dt;
        }
        return dt;
    }
    private static bool IsHightPerformance = true;
    private static Dictionary<string, DataTable> ht_cache_data = new Dictionary<string, DataTable>();


    /// <summary>
    /// 获取表中的描述
    /// </summary>
    /// <param name="dt">表包含两列 列0为key 列1为value</param>
    /// <param name="str_key">key[例如001,,002]</param>
    /// <returns>返回values[例如苹果,雪梨]</returns>
    public virtual string GetDataDescription(DataTable dt, string str_key)
    {
        StringBuilder str_value = new StringBuilder();
        if (str_key.IndexOf(",") < 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].ToString() == str_key)
                {
                    str_value.Append(dr[1].ToString());
                    break;
                }
            }
        }
        else
        {
            string[] arr_key = str_key.Split(",".ToArray());
            foreach (DataRow dr in dt.Rows)
            {
                foreach (string item_key in arr_key)
                {
                    str_value.Append(dr[1].ToString() + ",");
                }
            }
            if (str_value.Length > 0)
            {
                str_value.Length = str_value.Length - 1;
            }


        }

        return str_value.ToString();
    }

    ERPBaseContext db = new ERPBaseContext();

    //效率非常慢，需要优化
    public string GetFileDescription(string str_key)
    {
        if (string.IsNullOrEmpty(str_key))
        {
            return string.Empty;
        }

        int FL_ID = Convert.ToInt32(str_key);
        var f = db.SYS_FILE.Where(o => o.FL_ID == FL_ID).FirstOrDefault();
        if (f != null)
        {
            return "<a value='" + str_key + "' class='down_file' >" + f.FL_NAME + "</span>";
        }
        else
        {
            return string.Empty;
        }
    }

    //效率非常慢，需要优化
    public string GetFolderDescription(string str_key)
    {
        if (string.IsNullOrEmpty(str_key))
        {
            return string.Empty;
        }

        int FD_ID = Convert.ToInt32(str_key);
        var fd = db.SYS_FOLDER.Where(o => o.FD_ID == FD_ID).FirstOrDefault();
        if (fd != null)
        {
            return "<a value='" + str_key + "' class='view_folder' >文件" + fd.FD_FILE_COUNT + "个</span>";
        }
        else
        {
            return string.Empty;
        }
    }


    private object GetControlValue(SYS_COLUMNS c)
    {
        string value = Request[c.SC_COLUMN_NAME];
        if (string.IsNullOrEmpty(value))
        {
            return DBNull.Value;
        }
        else
        {
            return value;
        }
    }

    //public string GetDate(string d)
    //{
    //    //return "2019-09-09";
    //    if (!string.IsNullOrEmpty(d))
    //    {
    //        return Convert.ToDateTime(d).ToString("yyyy-MM-dd");
    //    }
    //    return string.Empty;
    //}

}
