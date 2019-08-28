using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERPBase;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class notice : System.Web.UI.Page
{
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

    ERPBaseContext db = new ERPBaseContext();

    /// <summary>
    /// 是否正常处理业务
    /// </summary>
    private static bool IsBusy { get; set; }

    /// <summary>
    /// 执行通知业务
    /// </summary>
    /// <returns></returns>
    public string done()
    {
        if (IsBusy == false)
        {
            IsBusy = true;
            System.Threading.Thread t = new System.Threading.Thread(my_action);
            t.IsBackground = true;
            t.Start();
            return new AjaxSuccessResult("业务处理中").ToString();
        }
        return new AjaxSuccessResult("业务繁忙").ToString();
    }

    /// <summary>
    /// 0单线程通知
    /// </summary>
    private void my_action()
    {
        List<int> ListFunctionCode = new List<int>();
        ListFunctionCode.Add(1);
        ListFunctionCode.Add(2);
        foreach (int item_function in ListFunctionCode)
        {
            work_follow_master(item_function);
        }
        send_sms();
        IsBusy = false;
    }

 
    /// <summary>
    /// 1审批表单主表处理
    /// </summary>
    /// <param name="item_function">业务类型</param>
    private void work_follow_master(int item_function)
    {

        //【待审核通知】查询出所有状态为0或者3（待审核,待终审）的数据
        H5Objects o = GData.GetObject(item_function);
        StringBuilder strsql = new StringBuilder();
        strsql.Append("select ");
        strsql.Append(o.HO_ID_FIELD + " as id");
        strsql.Append("," + o.HO_USER_FIELD + " as user_id");
        strsql.Append("," + o.HO_MASTER_FIELD + " as master");
        strsql.Append(" from " + o.HO_TABLE_NAME);
        strsql.Append(" where " + o.HO_STAUTS_FIELD + " in (0,3)");

        DataTable dt = GetDataTable(strsql.ToString(), null);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr["id"]);//数据ID
                int user_id = Convert.ToInt32(dr["user_id"]);//申请用户
                string master = dr["master"].ToString();//核心字段的数据
                work_follow_detail_waitting(item_function, id, user_id, master);
            }
        }


        //【已审核通知】 查询出所有状态为1或者2（审核通过,驳回）的数据
        StringBuilder strsql1 = new StringBuilder();
        strsql1.Append("select ");
        strsql1.Append(o.HO_ID_FIELD + " as id");
        strsql1.Append("," + o.HO_USER_FIELD + " as user_id");
        strsql1.Append("," + o.HO_MASTER_FIELD + " as master");
        strsql1.Append("," + o.HO_STAUTS_FIELD + " as status");
        strsql1.Append(" from " + o.HO_TABLE_NAME);
        strsql1.Append(" where " + o.HO_STAUTS_FIELD + " in (1,2)");
        DataTable dt1 = GetDataTable(strsql1.ToString(), null);

        if (dt1.Rows.Count > 0)
        {
            foreach (DataRow dr1 in dt1.Rows)
            {
                int id = Convert.ToInt32(dr1["id"]);//数据ID
                int user_id = Convert.ToInt32(dr1["user_id"]);//申请用户
                string master = dr1["master"].ToString();
                int status = Convert.ToInt32(dr1["status"]);
                work_follow_detail_done(item_function, id, user_id, master, status);
            }
        }

    }

    /// <summary>
    /// 1.1有审批结果的通知
    /// </summary>
    public void work_follow_detail_done(int item_function, int id, int user, string master, int status)
    {
        //获取最后一条审批记录，WF_TIMES<100，通知用户然后WF_TIMES加上100
        H5Objects o = GData.GetObject(item_function);
        IQueryable<SP_WORK_FOLLOW> list = null;

        list = db.SP_WORK_FOLLOW.Where(item => item.WF_BUSINESS_TYPE == o.HO_BUSINESS_TYPE);
        list = list.Where(item => item.WF_BUSINESS_KEY == id);
        list = list.Where(item => item.WF_STATUS == 1);
        list = list.Where(item => item.WF_TIMES < 100);
        if (list.ToList().Count > 0)
        {
            work_follow_end_notice_user(user, master, o.HO_NAME, status);
            foreach (SP_WORK_FOLLOW one_item in list.ToList())
            {
                if (one_item != null)
                {
                    one_item.WF_TIMES += 100;
                }
            }
            db.SaveChanges();
        }
    }

    /// <summary>
    /// 1.1.1审批完成通知申请人
    /// </summary>
    /// <param name="user">申请人</param>
    /// <param name="master">核心信息</param>
    /// <param name="business_text">业务描述（机票，采购）</param>
    /// <param name="status">状态【0=待审核，1已审核，2未通过，3=待终审，4已撤回】</param>
    public void work_follow_end_notice_user(int user, string master, string business_text, int status)
    {
        //【审批完成】你申请的(采购，机票)(DH232444)审核结果为审批通过。
        List<string> list = new List<string>();
        list.Add(business_text + "(" + master + ")");
        list.Add(GData.get_status_name(status.ToString()));

        string temp = "审批完成:你申请的{1}审核结果为{2},请登陆中力家查看";
        string str_full_content = temp;
        for (int i = 0; i < list.Count; i++)
        {
            str_full_content = str_full_content.Replace("{" + (i + 1).ToString() + "}", list[i]);
        }

        SP_SMS obj = new SP_SMS();
        obj.SM_TEMP_ID = "395663";
        obj.SM_SHORT_CONTENT = string.Join(",", list.ToArray()); ;
        obj.SM_FULL_CONTENT = str_full_content;
        obj.SM_PHONE = GData.get_user_phone(user.ToString());
        obj.SM_CREATE_DATE = DateTime.Now;
        obj.SM_IS_SEND = false;
        db.SP_SMS.Add(obj);
        db.SaveChanges();

    }

    /// <summary>
    /// 1.2无审批结果的通知
    /// </summary>
    public void work_follow_detail_waitting(int item_function, int id, int user, string master)
    {
        
        //0为没有任何提醒，1=10分钟后提示一次，2=4小时提示一次，n=每天9点提醒一次，n>5停止提醒，n=6通知申请客户
        H5Objects o = GData.GetObject(item_function);
        IQueryable<SP_WORK_FOLLOW> list = null;

        list = db.SP_WORK_FOLLOW.Where(item => item.WF_BUSINESS_TYPE == o.HO_BUSINESS_TYPE);
        list = list.Where(item => item.WF_BUSINESS_KEY == id);
        list = list.Where(item => item.WF_STATUS == 0);

        //10分钟提醒
        DateTime dt1 = DateTime.Now.AddMinutes(-10);
        IQueryable<SP_WORK_FOLLOW> list1 = list.Where(item => item.WF_CREATE_DATE < dt1 && item.WF_TIMES == 0);
        foreach (SP_WORK_FOLLOW item in list1.ToList())
        {
            work_follow_star_notice_user(user, master, o.HO_NAME, item.WF_TO_USER);
            item.WF_TIMES = 1;
        }
        db.SaveChanges();

        //4小时提醒
        DateTime dt2 = DateTime.Now.AddHours(-4);
        IQueryable<SP_WORK_FOLLOW> list2 = list.Where(item => item.WF_CREATE_DATE < dt2 && item.WF_TIMES == 1);
        foreach (SP_WORK_FOLLOW item in list2.ToList())
        {
            work_follow_star_notice_user(user, master, o.HO_NAME, item.WF_TO_USER);
            item.WF_TIMES = 2;
        }
        db.SaveChanges();

        //n = 每天9点提醒一次，n > 5停止提醒
        for (int i = 0; i < 3; i++)
        {
            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 18)
            {
                DateTime dt_begin = Convert.ToDateTime(DateTime.Now.AddDays(-(i + 1)).ToString("yyyy-MM-dd"));
                DateTime dt_end = Convert.ToDateTime(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));
                IQueryable<SP_WORK_FOLLOW> listn = list.Where(item => item.WF_CREATE_DATE > dt_begin && item.WF_CREATE_DATE < dt_end && item.WF_TIMES == (2 + i));

                foreach (SP_WORK_FOLLOW item in listn.ToList())
                {
                    work_follow_star_notice_user(user, master, o.HO_NAME, item.WF_TO_USER);
                    item.WF_TIMES = item.WF_TIMES + 1;
                }
                db.SaveChanges();
            }
        }

        //n = 6通知申请客户
        DateTime dt6 = Convert.ToDateTime(DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"));
        IQueryable<SP_WORK_FOLLOW> list6 = list.Where(item => item.WF_TIMES < 6);
        list6 = list6.Where(item => item.WF_CREATE_DATE < dt6);

        foreach (SP_WORK_FOLLOW item in list6.ToList())
        {
            work_follow_stop_notice_user(user, master, o.HO_NAME, item.WF_TO_USER);
            item.WF_TIMES = 6;
        }
        db.SaveChanges();
    }

    /// <summary>
    /// 1.2.1审批流程到审批者，通知审批者
    /// </summary>
    /// <param name="user">申请人</param>
    /// <param name="master">核心信息</param>
    /// <param name="master">业务描述（机票，采购）</param>
    /// <param name="to_user">审批人</param>
    public void work_follow_star_notice_user(int user, string master, string business_text, int to_user)
    {
        //【审批通知】你收到[赵泽辉]的待审核的(采购，机票)(Ap2790)信息，快登陆中力家审批吧
        List<string> list = new List<string>();
        list.Add(GData.get_user_name(user.ToString()));
        list.Add(business_text + "(" + master + ")");

        string temp = "审批通知:你收到{1}的待审核的{2}信息，快登陆中力家审批吧";
        string str_full_content = temp;
        for (int i = 0; i < list.Count; i++)
        {
            str_full_content = str_full_content.Replace("{" + (i + 1).ToString() + "}", list[i]);
        }

        SP_SMS obj = new SP_SMS();
        obj.SM_TEMP_ID = "392035";
        obj.SM_SHORT_CONTENT = string.Join(",", list.ToArray()); ;
        obj.SM_FULL_CONTENT = str_full_content;
        obj.SM_PHONE = GData.get_user_phone(to_user.ToString());
        obj.SM_CREATE_DATE = DateTime.Now;
        obj.SM_IS_SEND = false;
        db.SP_SMS.Add(obj);
        db.SaveChanges();
    }

    /// <summary>
    /// 1.2.2审批暂停通知申请人
    /// </summary>
    /// <param name="user">申请人</param>
    /// <param name="master">核心信息</param>
    /// <param name="master">业务描述（机票，采购）</param>
    /// <param name="to_user">审批人</param>
    public void work_follow_stop_notice_user(int user, string master, string business_text, int to_user)
    {
        //【审批停顿】你申请的(采购，机票)信息(DH232444)还没完成审批。请你线下与[赵泽辉]沟通审批进度
        List<string> list = new List<string>();
        list.Add(business_text + "(" + master + ")");
        list.Add(GData.get_user_name(to_user.ToString()));

        string temp = "审批停顿:你申请的{1}还没完成审批。请你线下与{2}沟通审批进度";
        string str_full_content = temp;
        for (int i = 0; i < list.Count; i++)
        {
            str_full_content = str_full_content.Replace("{" + (i + 1).ToString() + "}", list[i]);
        }

        SP_SMS obj = new SP_SMS();
        obj.SM_TEMP_ID = "395658";
        obj.SM_SHORT_CONTENT = string.Join(",", list.ToArray()); ;
        obj.SM_FULL_CONTENT = str_full_content;
        obj.SM_PHONE = GData.get_user_phone(user.ToString());
        obj.SM_CREATE_DATE = DateTime.Now;
        obj.SM_IS_SEND = false;
        db.SP_SMS.Add(obj);
        db.SaveChanges();
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

    private void send_sms()
    {
        List<SP_SMS> list_sms = db.SP_SMS.Where(o => o.SM_IS_SEND == false).ToList();
        foreach (SP_SMS item in list_sms)
        {
            List<string> list_short = item.SM_SHORT_CONTENT.Split(",".ToCharArray()).ToList();
            bool b = SMS.Send(item.SM_PHONE, Convert.ToInt32(item.SM_TEMP_ID), list_short);
            //bool b = SMS.Send("13430380816", Convert.ToInt32(item.SM_TEMP_ID), list_short);
            if (b)
            {
                item.SM_IS_SEND = true;
                item.SM_SEND_DATE = DateTime.Now;
            }
            System.Threading.Thread.Sleep(100);//防止短信过快发送，导致接口频率失败
        }
        db.SaveChanges();

    }
}
