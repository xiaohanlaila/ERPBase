using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class GData
    {
        /// <summary>
        /// 获取业务表配置
        /// </summary>
        public static H5Objects GetObject(int FunctionCode)
        {
            H5Objects o = new H5Objects();
            if (FunctionCode == 1)
            {
                o.HO_NAME = "机票";
                o.HO_TABLE_NAME = "SP_TICKET";
                o.HO_ID_FIELD = "TK_ID";
                o.HO_USER_FIELD = "TK_USER_ID";
                o.HO_DATE_FIELD = "TK_CREATE_DATE";
                o.HO_STAUTS_FIELD = "TK_STATUS";
                o.HO_MASTER_FIELD = "TK_NUMBER";
                o.HO_BUSINESS_TYPE = 1;
            }

            if (FunctionCode == 2)
            {
                o.HO_NAME = "采购";
                o.HO_TABLE_NAME = "SP_BUY";
                o.HO_ID_FIELD = "BY_ID";
                o.HO_USER_FIELD = "BY_USER_ID";
                o.HO_DATE_FIELD = "BY_CREATE_DATE";
                o.HO_STAUTS_FIELD = "BY_STATUS";
                o.HO_MASTER_FIELD = "BY_TYPE";
                o.HO_BUSINESS_TYPE = 2;
            }

            return o;
        }

        /// <summary>
        /// 获取业务字段配置
        /// </summary>
        public static List<H5Columns> GetColumn(int FunctionCode)
        {
            List<H5Columns> list = new List<H5Columns>();
            if (FunctionCode == 1)
            {
                list.Add(new H5Columns { HC_NAME = "TK_NUMBER", HC_DESC = "航班号码", HC_CONTROL_TYPE = "H5TextBox", HC_RULE = @"/\S/", HC_URL_DESC = "航班号码不能为空" });
                list.Add(new H5Columns { HC_NAME = "TK_TIME", HC_DESC = "航班时间", HC_CONTROL_TYPE = "H5DateTime" });
                list.Add(new H5Columns { HC_NAME = "TK_ID_NUMBER", HC_DESC = "身份证号", HC_CONTROL_TYPE = "H5TextBox", HC_RULE = @"/(^[a-z0-9A-Z]{16}$)|(^[a-z0-9A-Z]{18}$)/", HC_URL_DESC = "身份证号不正确" });
                list.Add(new H5Columns { HC_NAME = "TK_NAME", HC_DESC = "姓名", HC_CONTROL_TYPE = "H5TextBox", HC_RULE = @"/\S/", HC_URL_DESC = "姓名不能为空" });
                list.Add(new H5Columns { HC_NAME = "TK_PHONE", HC_DESC = "手机号码", HC_CONTROL_TYPE = "H5NumberBox" });
                list.Add(new H5Columns { HC_NAME = "TK_REASON", HC_DESC = "事由", HC_CONTROL_TYPE = "H5TextArea" });
            }

            if (FunctionCode == 2)
            {
                list.Add(new H5Columns { HC_NAME = "BY_NAME", HC_DESC = "物资名称", HC_CONTROL_TYPE = "H5TextBox", HC_RULE = @"/\S/", HC_URL_DESC = "物资名称不能为空" });
                list.Add(new H5Columns { HC_NAME = "BY_TYPE", HC_DESC = "物质型号", HC_CONTROL_TYPE = "H5TextBox", HC_RULE = @"/\S/", HC_URL_DESC = "物质型号不能为空" });
                list.Add(new H5Columns { HC_NAME = "BY_NUMBER", HC_DESC = "数量", HC_CONTROL_TYPE = "H5NumberBox", HC_RULE = @"/\S/", HC_URL_DESC = "数量不能为空" });
                list.Add(new H5Columns { HC_NAME = "BY_PRICE", HC_DESC = "预估单价", HC_CONTROL_TYPE = "H5NumberBox" });
                list.Add(new H5Columns { HC_NAME = "BY_ACTION_DATE", HC_DESC = "到货时间", HC_CONTROL_TYPE = "H5Date" });
                list.Add(new H5Columns { HC_NAME = "BY_REASON", HC_DESC = "事由", HC_CONTROL_TYPE = "H5TextArea" });
            }

            return list;
        }

        /// <summary>
        /// 获取用户手机号码
        /// </summary>
        public static string get_user_phone(string user_id)
        {
            Dictionary<string, string> ht = get_user_phone_ht();
            string phone = string.Empty;
            if (ht.Keys.Contains(user_id))
            {
                phone = ht[user_id];
            }
            return phone;
        }

        /// <summary>
        /// 获取用户名字
        /// </summary>
        public static string get_user_name(string user_id)
        {
            Dictionary<string, string> ht = get_user_name_ht();
            string name = string.Empty;
            if (ht.Keys.Contains(user_id))
            {
                name = ht[user_id];
            }
            return name;
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        public static string get_user_image(string user_id)
        {
            Dictionary<string, string> ht = get_user_image_ht();
            if (ht.Keys.Contains(user_id))
            {
                return ht[user_id];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取状态名字[0=待审核，1已审核，2未通过，3=待终审，4已撤回]
        /// </summary>
        public static string get_status_name(string status)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("0", "待审核");
            ht.Add("1", "已审核");
            ht.Add("2", "未通过");
            ht.Add("3", "待终审");
            ht.Add("4", "已撤回");

            if (ht.Keys.Contains(status))
            {
                return ht[status];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取明细状态名字[0=未审核 1=审批通过 2=驳回]
        /// </summary>
        public static string get_status_detail_name(string status)
        {

            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("0", "未审核");
            ht.Add("1", "审批通过");
            ht.Add("2", "驳回");

            if (ht.Keys.Contains(status))
            {
                return ht[status];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取状态样式(颜色 0=黄色，1=绿色，2=红色，3=紫色，4=灰色)
        /// </summary>
        public static string get_status_class(string status)
        {
            return "c" + status;
        }

        /// <summary>
        /// 是否为业务管理员
        /// </summary>
        public static bool check_administrator(int business_type, string user_id)
        {
            Dictionary<int, List<string>> ht = administrator;
            if (ht.Keys.Contains(business_type))
            {

                List<string> list = ht[business_type];
                return list.Contains(user_id);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取业务管理员
        /// </summary>
        public static int get_administrator(int business_type)
        {
            Dictionary<int, List<string>> ht = administrator;
            if (ht.Keys.Contains(business_type))
            {

                List<string> list = ht[business_type];
                if (list.Count > 0)
                {
                    return Convert.ToInt32(list[0]);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        #region 私有方法
        private static Dictionary<string, string> ht_name = new Dictionary<string, string>();
        private static Dictionary<string, string> get_user_name_ht()
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
        private static Dictionary<string, string> get_user_image_ht()
        {
            if (ht_image.Count > 0)
            {
                return ht_image;
            }

            string strsql = "select UR_USER_ID,UR_HEAD_IMAGE from AP_USER ";
            DataTable dt = GetDataTable(strsql, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ht_image.Add(dr["UR_USER_ID"].ToString(), dr["UR_HEAD_IMAGE"].ToString());
                }
            }
            return ht_image;
        }

        private static Dictionary<string, string> ht_phone = new Dictionary<string, string>();
        private static Dictionary<string, string> get_user_phone_ht()
        {
            if (ht_phone.Count > 0)
            {
                return ht_phone;
            }

            string strsql = "select UR_USER_ID,UR_PHONE from AP_USER ";
            DataTable dt = GetDataTable(strsql, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ht_phone.Add(dr["UR_USER_ID"].ToString(), dr["UR_PHONE"].ToString());
                }
            }
            return ht_phone;
        }

        private static DataTable GetDataTable(string strsql, List<SqlParameter> list)
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

        private static Dictionary<int, List<string>> administrator
        {
            get
            {
                Dictionary<int, List<string>> ht = new Dictionary<int, List<string>>();
                List<string> list_ticket = new List<string>();
                list_ticket.Add("66");
                ht.Add(1, list_ticket);

                List<string> list_buy = new List<string>();
                list_buy.Add("66");
                ht.Add(2, list_buy);
                return ht;
            }
        }

        #endregion
    }

}