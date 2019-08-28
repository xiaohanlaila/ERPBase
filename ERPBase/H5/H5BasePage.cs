using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ERPBase
{
    /// <summary>
    /// H5页面基类
    /// </summary>
    public class H5BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            EnableViewState = false;
        }
    }

    /// <summary>
    /// 新增页面
    /// </summary>
    public class H5WorkFollowAdd : H5BasePage
    {

        public int FunctionCode { get; set; }

        public H5Control Gcontainer;

        protected override void OnInit(EventArgs e)
        {
            H5Div container = new H5Div();

            container.CssClass = "container";
            container.Attributes.Add("value", FunctionCode.ToString());
            Gcontainer = container;
            Form.Controls.Add(container);

            RenderTitle();

            RenderForm();

            RenderPerson();

            RenderFunction();
        }


        public void RenderTitle()
        {
            H5Objects o = H5Object;

            H5Head h = new H5Head();
            h.HeadText = o.HO_NAME;
            h.IsShowMore = true;
            Gcontainer.Controls.Add(h);
        }

        public void RenderForm()
        {
            H5Div form = new H5Div();
            form.CssClass = "form";
            Gcontainer.Controls.Add(form);

            List<H5Columns> list = H5Column;
            foreach (H5Columns o in list)
            {
                if (o.HC_CONTROL_TYPE == "H5TextArea")
                {
                    H5Div item3 = new H5Div();
                    item3.CssClass = "item3";
                    form.Controls.Add(item3);

                    H5Control c = GetControl(o);
                    item3.Controls.Add(c);
                }
                else
                {
                    H5Div item = new H5Div();
                    item.CssClass = "item";
                    form.Controls.Add(item);

                    H5Span item_head = new H5Span();
                    item_head.CssClass = "item_head";
                    item_head.InnerHtml = o.HC_DESC;
                    item.Controls.Add(item_head);

                    H5Control c = GetControl(o);
                    item.Controls.Add(c);
                }
            }
        }

        public void RenderPerson()
        {
            H5Person p = new H5Person();
            Gcontainer.Controls.Add(p);
        }

        public void RenderFunction()
        {
            H5Button b = new H5Button();
            b.InnerText = "提交";
            Gcontainer.Controls.Add(b);
        }

        public H5Control GetControl(H5Columns item)
        {
            H5Control c = new H5Control();
            if (item.HC_CONTROL_TYPE == "H5TextBox")
            {
                c = new H5TextBox();
                c.CssClass = "phone_control";
                c.target = item.HC_NAME;
                c.placeholder = item.HC_DESC;
            }

            if (item.HC_CONTROL_TYPE == "H5DateTime")
            {
                c = new H5DateTime();
                c.CssClass = "phone_control";
                c.target = item.HC_NAME;
                c.placeholder = item.HC_DESC;
            }

            if (item.HC_CONTROL_TYPE == "H5Date")
            {
                c = new H5Date();
                c.CssClass = "phone_control";
                c.target = item.HC_NAME;
                c.placeholder = item.HC_DESC;
            }

            if (item.HC_CONTROL_TYPE == "H5NumberBox")
            {
                c = new H5NumberBox();
                c.CssClass = "phone_control";
                c.target = item.HC_NAME;
                c.placeholder = item.HC_DESC;
            }

            if (item.HC_CONTROL_TYPE == "H5TextArea")
            {
                c = new H5TextArea();
                c.CssClass = "full_control";
                c.target = item.HC_NAME;
                c.placeholder = item.HC_DESC;
            }

            if (!string.IsNullOrEmpty(item.HC_RULE))
            {
                c.rule = item.HC_RULE;
                c.rule_desc = item.HC_URL_DESC;
            }
            c.CssClass += " IData ";

            return c;
        }

        public H5Objects H5Object
        {
            get
            {
                return GData.GetObject(FunctionCode);
            }
        }

        public List<H5Columns> H5Column
        {
            get
            {
                return GData.GetColumn(FunctionCode);
            }
        }
    }

    /// <summary>
    /// 列表页面
    /// </summary>
    public class H5WorkFollowList : H5BasePage
    {
        public int FunctionCode { get; set; }

        public H5Control Gcontainer;

        protected override void OnInit(EventArgs e)
        {
            H5Div container = new H5Div();

            container.CssClass = "container";
            container.Attributes.Add("value", FunctionCode.ToString());
            Gcontainer = container;
            Form.Controls.Add(container);

            RenderTitle();

            RenderContent();

        }

        public void RenderTitle()
        {
            H5Objects o = H5Object;

            H5Head h = new H5Head();
            h.HeadText = o.HO_NAME + "列表";
            h.IsShowMore = false;
            Gcontainer.Controls.Add(h);
        }



        public void RenderContent()
        {
            H5Div content = new H5Div();
            content.CssClass = "content";
            Gcontainer.Controls.Add(content);



            H5Div content_scroll = new H5Div();
            content_scroll.CssClass = "content_scroll";
            content.Controls.Add(content_scroll);


        }
        public H5Objects H5Object
        {
            get
            {
                return GData.GetObject(FunctionCode);
            }
        }
    }

    /// <summary>
    /// 明细页面
    /// </summary>
    public class H5WorkFollowDetail : H5BasePage
    {
        public int FunctionCode { get; set; }

        public int Key { get; set; }

        public bool CancleAble { get; set; }


        public bool ApproveAble { get; set; }


        public H5Control Gcontainer;

        public H5Control Gwork_follow_content;

        public H5Control Gitem3;

        protected override void OnInit(EventArgs e)
        {
            Key = Convert.ToInt32(Request["id"]);
            H5Div container = new H5Div();

            container.CssClass = "container";
            container.Attributes.Add("value", FunctionCode.ToString());
            Gcontainer = container;
            Form.Controls.Add(container);
            RenderTitle();

            H5Div work_follow_content = new H5Div();
            work_follow_content.CssClass = "work_follow_content";
            Gwork_follow_content = work_follow_content;
            Gcontainer.Controls.Add(work_follow_content);

            RenderRequestPerson();

            RenderReasonBox();

            RenderWorkFollow();

            RenderFunction();

            ComfirmReasonBox();

        }

        public void RenderTitle()
        {
            H5Objects o = H5Object;

            H5Head h = new H5Head();
            h.HeadText = o.HO_NAME + "详情";
            h.IsShowMore = false;
            Gcontainer.Controls.Add(h);
        }

        public void RenderRequestPerson()
        {
            DataTable dt = get_form_data();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                request_person obj = new request_person();
                obj.HeadImage = GData.get_user_image(dr["user_id"].ToString());
                obj.Name = GData.get_user_name(dr["user_id"].ToString());
                obj.StatusClass = GData.get_status_class(dr["status"].ToString());
                obj.StatusText = GData.get_status_name(dr["status"].ToString());
                Gwork_follow_content.Controls.Add(obj);


                H5Div detail_message = new H5Div();
                detail_message.CssClass = "detail_message";
                Gwork_follow_content.Controls.Add(detail_message);

                List<H5Columns> list_column = H5Column;
                foreach (H5Columns item in list_column)
                {
                    detail_message_item c = new detail_message_item();
                    c.Key = item.HC_DESC;
                    if (item.HC_CONTROL_TYPE == "H5Date")
                    {
                        if (!string.IsNullOrEmpty(dr[item.HC_NAME].ToString()))
                        {
                            c.Value = Convert.ToDateTime(dr[item.HC_NAME].ToString()).ToString("yyyy-MM-dd");
                        }
                    }

                    if (item.HC_CONTROL_TYPE == "H5DateTime")
                    {
                        if (!string.IsNullOrEmpty(dr[item.HC_NAME].ToString()))
                        {
                            c.Value = Convert.ToDateTime(dr[item.HC_NAME].ToString()).ToString("yyyy-MM-dd HH:mm");
                        }
                    }

                    if (string.IsNullOrEmpty(c.Value))
                    {
                        c.Value = dr[item.HC_NAME].ToString();
                    }

                    detail_message.Controls.Add(c);
                }
            }

        }

        public void RenderWorkFollow()
        {

            H5Div work_follow = new H5Div();
            work_follow.CssClass = "work_follow";
            Gwork_follow_content.Controls.Add(work_follow);

            DataTable dt = get_work_follow_data();

            foreach (DataRow dr in dt.Rows)
            {
                work_follow_item c = new work_follow_item();
                c.name = dr["WF_TO_USER_NAME"].ToString();
                c.HeadImage = dr["WF_TO_USER_IMAGE"].ToString();
                c.time = dr["WF_APPROVE_DATE"].ToString();
                c.Reason = dr["WF_REASON"].ToString();
                c.StatusText = dr["WF_STATUS_TEXT"].ToString();
                c.StatusClass = dr["WF_STATUS_CLASS"].ToString();
                c.Status = dr["WF_STATUS"].ToString();
                work_follow.Controls.Add(c);
            }

            H5Div work_follow_empty = new H5Div();
            work_follow_empty.CssClass = "work_follow_empty";
            work_follow.Controls.Add(work_follow_empty);
        }

        public void RenderReasonBox()
        {
            H5Div item3 = new H5Div();
            item3.CssClass = "item3";
            Gitem3 = item3;
            Gwork_follow_content.Controls.Add(item3);

            H5TextArea c = new H5TextArea();
            c.CssClass = "full_control Idata";
            c.target = "WF_REASON";
            c.placeholder = "审批意见(100字以内)";
            item3.Controls.Add(c);

        }

        public void ComfirmReasonBox()
        {
            if (!ApproveAble)
            {
                Gwork_follow_content.Controls.Remove(Gitem3);
            }
        }

        public void RenderFunction()
        {
            if (CancleAble)
            {
                H5Button b = new H5Button();
                b.InnerText = "撤回";
                b.CssClass = "cancle";
                Gcontainer.Controls.Add(b);
            }
            else
            {
                if (ApproveAble)
                {
                    H5ApproveButton b = new H5ApproveButton();
                    Gcontainer.Controls.Add(b);
                }
            }
        }

        public H5Objects H5Object
        {
            get
            {
                return GData.GetObject(FunctionCode);
            }
        }

        public List<H5Columns> H5Column
        {
            get
            {
                return GData.GetColumn(FunctionCode);
            }
        }

        private DataTable get_form_data()
        {
            StringBuilder strsql = new StringBuilder();
            StringBuilder str_column = new StringBuilder();
            StringBuilder str_where = new StringBuilder();
            StringBuilder str_order = new StringBuilder();
            List<SqlParameter> list_para = new List<SqlParameter>();
            H5Objects o = H5Object;
            str_column.Append(o.HO_ID_FIELD + " as id");
            str_column.Append("," + o.HO_USER_FIELD + " as user_id");
            str_column.Append("," + o.HO_STAUTS_FIELD + " as status");

            List<H5Columns> list_column = H5Column;
            foreach (H5Columns item_column in list_column)
            {
                str_column.Append("," + item_column.HC_NAME);
            }

            str_where.Append(" and " + o.HO_ID_FIELD + "=@" + o.HO_ID_FIELD);
            list_para.Add(new SqlParameter("@" + o.HO_ID_FIELD, Key));

            strsql.Append("select " + str_column + " from " + o.HO_TABLE_NAME + " where 1=1 " + str_where);

            SqlConnection cn = comm_fun.get_cn();
            try
            {
                DataTable dt = comm_fun.GetDatatable(strsql.ToString(), cn, list_para);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string status = dr["status"].ToString();

                    if (status == "0" || status == "3")
                    {
                        ApproveAble = true;
                    }

                    if (status == "0")
                    {
                        CancleAble = true;
                    }


                }
                return dt;

            }
            catch (Exception ex)
            {
                comm_fun.WriteLog(ex.ToString());
            }
            finally
            {
                comm_fun.CloseConnection(cn);
            }
            return new DataTable();
        }

        private DataTable get_work_follow_data()
        {
            StringBuilder strsql = new StringBuilder();
            List<SqlParameter> list_para = new List<SqlParameter>();
            H5Objects o = H5Object;

            strsql.Append("select WF_FROM_USER,WF_TO_USER,dbo.fn_convert_datetime(WF_APPROVE_DATE) as WF_APPROVE_DATE,WF_REASON,WF_STATUS from SP_WORK_FOLLOW where WF_BUSINESS_TYPE=@WF_BUSINESS_TYPE and WF_BUSINESS_KEY=@WF_BUSINESS_KEY ORDER BY WF_CREATE_DATE DESC ");
            list_para.Add(new SqlParameter("@WF_BUSINESS_TYPE", o.HO_BUSINESS_TYPE));
            list_para.Add(new SqlParameter("@WF_BUSINESS_KEY", Key));
            SqlConnection cn = comm_fun.get_cn();
            try
            {
                DataTable dt = comm_fun.GetDatatable(strsql.ToString(), cn, list_para);
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("WF_TO_USER_NAME");
                    dt.Columns.Add("WF_TO_USER_IMAGE");
                    dt.Columns.Add("WF_STATUS_CLASS");
                    dt.Columns.Add("WF_STATUS_TEXT");

                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["WF_TO_USER_NAME"] = GData.get_user_name(dr["WF_TO_USER"].ToString());
                        dr["WF_TO_USER_IMAGE"] = GData.get_user_image(dr["WF_TO_USER"].ToString());
                        dr["WF_STATUS_CLASS"] = GData.get_status_class(dr["WF_STATUS"].ToString());
                        dr["WF_STATUS_TEXT"] = GData.get_status_detail_name(dr["WF_STATUS"].ToString());
                    }
                }

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    string WF_FROM_USER = dr["WF_FROM_USER"].ToString();
                    string WF_STATUS = dr["WF_STATUS"].ToString();
                    if (WF_FROM_USER == Cookies.UserCode && WF_STATUS == "0" && CancleAble)
                    {
                        CancleAble = true;
                    }
                    else
                    {
                        CancleAble = false;
                    }
                }
                else
                {
                    CancleAble = false;
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string WF_TO_USER = dr["WF_TO_USER"].ToString();
                    string WF_STATUS = dr["WF_STATUS"].ToString();
                    if (WF_TO_USER == Cookies.UserCode && WF_STATUS == "0" && ApproveAble)
                    {
                        ApproveAble = true;
                    }
                    else
                    {
                        ApproveAble = false;
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                comm_fun.WriteLog(ex.ToString());
            }
            finally
            {
                comm_fun.CloseConnection(cn);
            }

            return new DataTable();
        }
    }

    /// <summary>
    /// 审批页面
    /// </summary>
    public class H5Approve : H5BasePage
    {
        //支持多个FunctionCode
        public List<int> FunctionCodeList { get; set; }

        public H5Control Gcontainer;

        protected override void OnInit(EventArgs e)
        {
            H5Div container = new H5Div();

            StringBuilder str_function_code = new StringBuilder();

            FunctionCodeList.ForEach(o => str_function_code.Append("_"+o.ToString()));
            container.CssClass = "container";
            container.Attributes.Add("value", str_function_code.ToString().Substring(1));
            Gcontainer = container;
            Form.Controls.Add(container);

            RenderTitle();

            RenderTag();

            RenderContent();

        }

        public void RenderTitle()
        {
            H5Head h = new H5Head();
            h.HeadText = "审批列表";
            h.IsShowMore = false;
            Gcontainer.Controls.Add(h);
        }

        public void RenderTag()
        {
            H5Div navgite = new H5Div();
            navgite.CssClass = "navgite";
            Gcontainer.Controls.Add(navgite);

            H5Span other = new H5Span();
            other.CssClass = "other item active";
            other.InnerText = "我的审批";
            navgite.Controls.Add(other);

            H5Span me = new H5Span();
            me.CssClass = "me item";
            me.InnerText = "我的发起";
            navgite.Controls.Add(me);

            H5Div line = new H5Div();
            line.CssClass = "line";
            navgite.Controls.Add(line);
        }



        public void RenderContent()
        {
            H5Div content = new H5Div();
            content.CssClass = "content";
            Gcontainer.Controls.Add(content);

            H5Div content_scroll_other = new H5Div();
            content_scroll_other.CssClass = "content_scroll other";
            content_scroll_other.Attributes.Add("type", "other");
            content.Controls.Add(content_scroll_other);

            H5Div content_scroll_me = new H5Div();
            content_scroll_me.CssClass = "content_scroll me";
            content_scroll_me.Attributes.Add("type", "me");
            content.Controls.Add(content_scroll_me);
        }
    }

    /// <summary>
    /// 外部登录页面
    /// </summary>
    public class H5LoginApp : H5BasePage
    {

        protected override void OnInit(EventArgs e)
        {
            if (string.IsNullOrEmpty(Cookies.UserCode))
            {
                string id = Request["id"];
                string user_id = Cookies.Login(id);
                if (string.IsNullOrEmpty(user_id))
                {
                    Response.Redirect("~/H5/work_follow/Error.aspx");
                }
            }
            base.OnInit(e);
        }

    }

}