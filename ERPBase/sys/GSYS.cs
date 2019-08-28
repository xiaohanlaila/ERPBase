using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ERPBase
{
    public class GSYS
    {

        public static List<SYS_TABLE_BUTTONS> controls
        {
            get
            {

                List<SYS_TABLE_BUTTONS> s = new List<SYS_TABLE_BUTTONS>();
                SYS_TABLE_BUTTONS t = new SYS_TABLE_BUTTONS();
                t.SB_HEAD_TEXT = "a";
                t.SB_HEAD_CSSCLASS = "SogTdCheck";
                t.SB_INNER_TEXT = "编辑";
                t.SB_INNER_CSSCLASS = "btn green_btn Iedit";
                s.Add(t);

                SYS_TABLE_BUTTONS t1 = new SYS_TABLE_BUTTONS();
                t1.SB_HEAD_TEXT = "a1";
                t1.SB_HEAD_CSSCLASS = "SogTdCheck";
                t1.SB_INNER_TEXT = "编辑1";
                t1.SB_INNER_CSSCLASS = "btn green_btn";
                s.Add(t1);
                return s;

            }
        }

        public static string ToHead(int function_code)
        {
            StringBuilder str_html = new StringBuilder();
            foreach (SYS_TABLE_BUTTONS item in controls)
            {
                str_html.Append("<th class='" + item.SB_HEAD_CSSCLASS + "'>" + item.SB_HEAD_TEXT + "</th>");
            };
            return str_html.ToString();
        }

        public static string ToBody(int function_code, string id)
        {
            StringBuilder str_html = new StringBuilder();
            foreach (SYS_TABLE_BUTTONS item in controls)
            {
                str_html.Append("<td>" + "<span data-id='" + id + "' class='" + item.SB_INNER_CSSCLASS + "'>" + item.SB_INNER_TEXT + "</span>" + "</td>");
            };
            return str_html.ToString();
        }


        public static SYS_OBJECTS GetObjects(int SO_ID)
        {
            SYS_OBJECTS O = new SYS_OBJECTS();
            O.SO_TABLE_KEY = "UR_USER_ID";
            O.SO_TABLE_NAME_SEARCH = "AP_USER";
            O.SO_TABLE_NAME = "AP_USER";
            O.SO_IS_ADD = true;
            O.SO_IS_EDIT = true;
            O.SO_IS_CONDICTION = true;
            O.SO_IS_TITLE = true;
            O.SO_TITLE = "用户维护";
            O.SO_ITEM_DESC = "用户";
            return O;
        }

        public static List<SYS_COLUMNS> GetColumns(int SO_ID)
        {
            List<SYS_COLUMNS> list_column = new List<SYS_COLUMNS>();

            SYS_COLUMNS s0 = new SYS_COLUMNS();
            s0.SC_COLUMN_NAME = "UR_TYPE";
            s0.SC_COLUMN_DESC = "职称";
            s0.SC_CONTROL_DATA = "SELECT YS_CODE,YS_DESC FROM dbo.AP_STATUS WITH(NOLOCK) WHERE YS_TYPE='USER_TYPE'";
            s0.SC_IS_SEARCH = true;
            s0.SC_IS_TABLE = true;

            list_column.Add(s0);

            SYS_COLUMNS s = new SYS_COLUMNS();
            s.SC_COLUMN_NAME = "UR_GROUP";
            s.SC_COLUMN_DESC = "权限组";
            s.SC_CONTROL_TYPE = "SOGDropDownList";
            s.SC_CONTROL_DATA = "select RTRIM(LTRIM(UG_CODE)) AS UG_CODE,UG_DESC from AP_USER_GROUP";
            s.SC_IS_ADD = true;
            s.SC_IS_EDIT = true;
            //s.SC_RULE = @"/\S/";
            s.SC_RULE_DESC = "权限组不允许为空";
            s.SC_IS_SEARCH = true;
            s.SC_IS_TABLE = true;
            list_column.Add(s);

            SYS_COLUMNS s1 = new SYS_COLUMNS();
            s1.SC_COLUMN_NAME = "UR_NAME";
            s1.SC_COLUMN_DESC = "名字";
            s1.SC_CONTROL_TYPE = "SogTextArea";
            s1.SC_RULE = @"/\S/";
            s1.SC_RULE_DESC = "公司名字不允许为空";
            s1.SC_IS_SEARCH = true;
            s1.SC_IS_ADD = true;
            s1.SC_IS_EDIT = true;
            s1.SC_IS_TABLE = true;
            s1.SC_TABLE_CLASS = "width-sm";
            list_column.Add(s1);

            SYS_COLUMNS s2 = new SYS_COLUMNS();
            s2.SC_COLUMN_NAME = "UR_PHONE";
            s2.SC_COLUMN_DESC = "手机号码";
            s2.SC_CONTROL_TYPE = "SogTextBox";
            s2.SC_IS_ADD = true;
            s2.SC_IS_SEARCH = true;
            s2.SC_IS_EDIT = true;
            s2.SC_IS_TABLE = true;
            s2.SC_TABLE_CLASS = "width-md";
            list_column.Add(s2);

            SYS_COLUMNS s3 = new SYS_COLUMNS();
            s3.SC_COLUMN_NAME = "UR_REMARK";
            s3.SC_COLUMN_DESC = "档案";
            s3.SC_CONTROL_TYPE = "SogFileUpload";
            //s3.SC_RULE = @"/\S/";
            s3.SC_RULE_DESC = "文件不能为空";
            s3.SC_IS_ADD = true;
            s3.SC_IS_SEARCH = true;
            s3.SC_IS_EDIT = true;
            s3.SC_IS_TABLE = true;
            s3.SC_TABLE_CLASS = "width-md";
            list_column.Add(s3);

            SYS_COLUMNS s4 = new SYS_COLUMNS();
            s4.SC_COLUMN_NAME = "UR_REMARK1";
            s4.SC_COLUMN_DESC = "档案1";
            s4.SC_CONTROL_TYPE = "SogFolderUpload";
            //s4.SC_RULE = @"/\S/";
            //s4.SC_RULE_DESC = "文件不能为空";
            s4.SC_IS_ADD = true;
            s4.SC_IS_SEARCH = true;
            s4.SC_IS_EDIT = true;
            s4.SC_IS_TABLE = true;
            s4.SC_TABLE_CLASS = "width-md";
            list_column.Add(s4);


            //

            SYS_COLUMNS s5 = new SYS_COLUMNS();
            s5.SC_COLUMN_NAME = "UR_LAST_UPDATE_PASSWORD";
            s5.SC_COLUMN_DESC = "密码时间";
            s5.SC_CONTROL_TYPE = "SogDate";
            //s4.SC_RULE = @"/\S/";
            //s4.SC_RULE_DESC = "文件不能为空";
            s5.SC_IS_ADD = true;
            s5.SC_IS_SEARCH = true;
            s5.SC_IS_EDIT = true;
            s5.SC_IS_TABLE = true;
            s5.SC_TABLE_CLASS = "width-date";

            list_column.Add(s5);




            SYS_COLUMNS s6 = new SYS_COLUMNS();
            s6.SC_COLUMN_NAME = "UR_LAST_UPDATE_DATE";
            s6.SC_COLUMN_DESC = "更新时间";
            s6.SC_CONTROL_TYPE = "SogDateTime";
            //s4.SC_RULE = @"/\S/";
            //s4.SC_RULE_DESC = "文件不能为空";
            s6.SC_IS_ADD = true;
            s6.SC_IS_SEARCH = true;
            s6.SC_IS_EDIT = true;
            s6.SC_IS_TABLE = true;
            s6.SC_TABLE_CLASS = "width-datetime";

            list_column.Add(s6);





            return list_column;
        }

        public static List<SYS_TABLE_BUTTONS> GetButton(int SO_ID)
        {
            List<SYS_TABLE_BUTTONS> list_btn = new List<SYS_TABLE_BUTTONS>();
            list_btn.Add(new SYS_TABLE_BUTTONS() { SB_HEAD_TEXT = "编辑1", SB_HEAD_CSSCLASS = "td3", SB_INNER_TEXT = "edit1", SB_INNER_CSSCLASS = "btn green_btn Iedit" });
            //list_btn.Add(new SYS_TABLE_BUTTONS() { SB_HEAD_TEXT = "编辑2", SB_HEAD_CSSCLASS = "td3", SB_INNER_TEXT = "edit2", SB_INNER_CSSCLASS = "btn green_btn" });
            return list_btn;
        }

        public static void Clear()
        {

        }
    }



}