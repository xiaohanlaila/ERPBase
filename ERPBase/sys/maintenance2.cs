//表单10万条数据，查询在1秒以内，安排性能测试 done
//安排js与css放到后台,特别是文件夹与文件部分 done

//头部筛选条件没有对齐(done)
//表格宽度没有定义(done)
//当没有数据时，应该显示暂无数据
//导航与选择过度效果难看(done)
//导航没有筛选实现(done)


//控件多样 按钮，输入框(done)，多行文本框(done)，时间，数字，可搜索下拉，可搜索下拉多选，下拉(done),文件(done)，多文件、白分比

//文件大小，扩展名,文件个数
//文件 文件实现思路1file，2type=hidden change上传，成功返回路径放到hidden里面，保存时提交hidden
//图片上传 文件返回会显示文件
//大文件
//文件夹（多文件,实现无限递归)保存文件夹ID
//文件服务器file  id=1&action=upload view(视频,图片,office,pdf,txt,声音) down【任何文件操作都是经过该代码】
//文件保存 年/月/guid.扩展名  表结构；id,文件夹ID，路径，文件名字，文件大小，缩略图，大图，原图，文件扩展名字，远程guid，创建人，创建时间，是否有效，业务类型

//白分比 可以拖动改变0到100，也可以输入值，对应控件改变



//实现新增,用户填写错误提示(done)
//实现修改,用户填写错误提示(done)
//实现删除(done)

//实现导出,用户填写错误提示
//导入

//开发者模式弹层，
//用户调整模式弹出
//当控件没有验证成功时,提示错误描述，问前端tips的思路
//表格选择需要改变颜色
//点击td也可以实现选中


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

namespace ERPBase
{
    public class maintenance2 : System.Web.UI.Page
    {
        public int SO_ID { get; set; }

        public SogGeneric G_SogHead;

        public SogNavigate G_Navigate = new SogNavigate();

        public SogGeneric G_SogCondition;

        public SogGeneric G_SogFunction;

        public SogGeneric G_SogContent;


        protected virtual void SogCssJsInit()
        {
            SOGCssControl css = new SOGCssControl();
            css.Add("~/css/maintenance.css");//页面布局核心
            css.Add("~/css/jquery.toast.css"); ;//提示窗口
            css.Add("~/css/SogFileUpload.css"); ;//文件上传
            css.Add("~/css/SogFolderUpload.css"); ;//文件夹上传
            Page.Header.Controls.Add(css);

            SOGJsControl js = new SOGJsControl();
            js.Add("~/js/jquery-1.11.0.min.js");
            js.Add("~/js/common.js");//基础类库
            js.Add("~/js/SogModal.js");//模态窗口
            js.Add("~/js/jquery.toast.js");//提示窗口
            js.Add("~/js/SogFileUpload.js");//文件上传
            js.Add("~/js/SogFolderUpload.js");//文件夹上传
            js.Add("~/js/maintenance.js");//增删改查
            Page.Form.Controls.Add(js);
        }

        protected override void OnInit(EventArgs e)
        {
            EnableViewState = false;

            SogDataInit();

            if (OBJECT.SO_IS_TITLE)
            {
                SogHeadInit();
            }

            if (OBJECT.SO_IS_CONDICTION && COLUMNS_SEARCH.Count != 0)
            {
                SogConditionInit();
            }

            SogFunctionInit();

            SogContentInit();

            SogModalInit();

            SogCssJsInit();

            List<SqlParameter> list_para = new List<SqlParameter>();
            string strsql = GetOtherSQL(list_para);
            Session["OtherSQL" + SO_ID.ToString()] = strsql;
            Session["list_para" + SO_ID.ToString()] = list_para;

            string str_order = GetOtherOrder();
            Session["OtherOrder" + SO_ID.ToString()] = str_order;

            Dictionary<string, string> ht = GetOtherSearch();
            Session["OtherSearch" + SO_ID.ToString()] = ht;

        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        protected virtual void SogDataInit()
        {
            SogDiv d = new SogDiv();
            d.CssClass = "hide";
            Form.Controls.Add(d);

            SogTextBox c_so_id = new SogTextBox();
            c_so_id.target = "SO_ID";
            c_so_id.ID = "SO_ID";
            c_so_id.value = SO_ID.ToString();
            d.Controls.Add(c_so_id);
        }

        /// <summary>
        ///头部区域初始化
        /// </summary>
        protected virtual void SogHeadInit()
        {
            SogDiv SogHead = new SogDiv();
            SogHead.CssClass = "SogHead";
            G_SogHead = SogHead;
            Form.Controls.Add(SogHead);

            SogSpan SogTitle = new SogSpan();
            SogTitle.CssClass = "SogTitle";
            SogTitle.InnerText = OBJECT.SO_TITLE;
            G_SogHead.Controls.Add(SogTitle);

            SogNavigate nv = NavigateInit();
            G_Navigate = nv;
            if (nv.DataSource.Count > 0)
            {
                G_SogHead.Controls.Add(nv);
            }

            SogDiv SogRight = new SogDiv();
            SogRight.CssClass = "SogRight";
            G_SogHead.Controls.Add(SogRight);

            if (OBJECT.SO_IS_ADD && COLUMNS_ADD.Count > 0)
            {
                SogSpan add_botton = new SogSpan();
                add_botton.CssClass = "add_botton";
                add_botton.InnerText = "+";
                SogRight.Controls.Add(add_botton);
            }
        }


        /// <summary>
        /// 初始化导航控件
        /// </summary>
        /// <returns></returns>
        protected virtual SogNavigate NavigateInit()
        {
            return new SogNavigate();
        }

        /// <summary>
        /// 筛选区域初始化
        /// </summary>
        public virtual void SogConditionInit()
        {
            SogDiv SogCondition = new SogDiv();
            SogCondition.CssClass = "SogCondition";
            G_SogCondition = SogCondition;
            Form.Controls.Add(SogCondition);

            SogDiv open = new SogDiv();
            open.CssClass = "open";
            open.Attributes.Add("style", "display: none;");
            SogCondition.Controls.Add(open);

            foreach (SYS_COLUMNS c in COLUMNS_SEARCH)
            {
                if (!string.IsNullOrEmpty(c.SC_CONTROL_DATA))
                {
                    condition_item obj_ci = new condition_item();
                    obj_ci.ID = "search_" + c.SC_COLUMN_NAME;
                    obj_ci.HeadText = c.SC_COLUMN_DESC;
                    obj_ci.target = c.SC_COLUMN_NAME;
                    obj_ci.DataSource = GetControlData(c.SC_CONTROL_DATA);
                    obj_ci.IsShowEmpty = true;
                    open.Controls.Add(obj_ci);
                }
            }

            List<SYS_COLUMNS> list = COLUMNS_SEARCH.Where(o => string.IsNullOrEmpty(o.SC_CONTROL_DATA) == true).ToList();
            if (list.Count > 0)
            {
                SogDiv condition_item = new SogDiv();
                condition_item.CssClass = "condition_item";
                condition_item.Attributes.Add("style", "padding-left:100px");
                open.Controls.Add(condition_item);

                SogTextSearch obj_SogTextSearch = new SogTextSearch();

                StringBuilder placeholder = new StringBuilder();
                StringBuilder target = new StringBuilder();
                foreach (SYS_COLUMNS c in list)
                {
                    placeholder.Append("/" + c.SC_COLUMN_DESC);
                    target.Append("," + c.SC_COLUMN_NAME);
                }
                obj_SogTextSearch.placeholder = placeholder.ToString().Substring(1);
                obj_SogTextSearch.target = "SogText";
                obj_SogTextSearch.CssClass = "SogText";
                obj_SogTextSearch.ID = "btn_search";
                condition_item.Controls.Add(obj_SogTextSearch);
            }


            SogDiv close = new SogDiv();
            close.CssClass = "close";
            SogCondition.Controls.Add(close);


            SogDiv condition_head = new SogDiv();
            condition_head.CssClass = "condition_head";
            condition_head.InnerText = "筛选条件";
            close.Controls.Add(condition_head);

            foreach (SYS_COLUMNS c in COLUMNS_SEARCH)
            {
                if (!string.IsNullOrEmpty(c.SC_CONTROL_DATA))
                {
                    condition_desc d = new condition_desc();
                    d.HeadText = c.SC_COLUMN_DESC;
                    d.ParentID = "search_" + c.SC_COLUMN_NAME;
                    close.Controls.Add(d);
                }
            }

            SogDiv handler = new SogDiv();
            handler.CssClass = "handler";
            SogCondition.Controls.Add(handler);

            SogIcon obj_i = new SogIcon();
            obj_i.CssClass = "small_btn fa fa-chevron-down";

            handler.Controls.Add(obj_i);
        }

        /// <summary>
        /// 功能区域初始化
        /// </summary>
        public virtual void SogFunctionInit()
        {
            SogDiv SogFunction = new SogDiv();
            SogFunction.CssClass = "SogFunction";
            G_SogFunction = SogFunction;
            Form.Controls.Add(SogFunction);

            SogSpan s1 = new SogSpan();
            s1.InnerText = "已选";
            G_SogFunction.Controls.Add(s1);

            SogSpan s2 = new SogSpan();
            s2.InnerText = "0";
            s2.CssClass = "number";
            G_SogFunction.Controls.Add(s2);

            SogSpan s3 = new SogSpan();
            s3.InnerText = OBJECT.SO_ITEM_DESC;
            G_SogFunction.Controls.Add(s3);

            SogDeleteButton btn_delete = new SogDeleteButton();
            btn_delete.ID = "btn_delete";
            G_SogFunction.Controls.Add(btn_delete);
        }

        /// <summary>
        /// 内容区域初始化
        /// </summary>
        public virtual void SogContentInit()
        {
            SogDiv SogContent = new SogDiv();
            SogContent.CssClass = "SogContent";
            G_SogContent = SogContent;
            Form.Controls.Add(SogContent);
        }

        /// <summary>
        /// 模态窗口初始化
        /// </summary>
        public virtual void SogModalInit()
        {
            if (OBJECT.SO_IS_ADD && COLUMNS_ADD.Count > 0)
            {
                SogModalAddInit();
            }

            if (OBJECT.SO_IS_EDIT && COLUMNS_EDIT.Count > 0)
            {
                SogModalEditInit();
            }
        }

        private void SogModalInit(List<SYS_COLUMNS> list_column, string type)
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("add", "新增");
            ht.Add("edit", "编辑");


            SogDiv SogModal = new SogDiv();
            SogModal.CssClass = "SogModal";
            SogModal.ID = "div_" + type;
            Form.Controls.Add(SogModal);


            SogDiv modal_title = new SogDiv();
            modal_title.CssClass = "modal_title";
            modal_title.InnerText = ht[type] + OBJECT.SO_ITEM_DESC;
            SogModal.Controls.Add(modal_title);

            SogIcon ic = new SogIcon();
            ic.CssClass = "fa fa-times-circle SogRight CoverClose";
            ic.Attributes.Add("aria-hidden", "true");
            modal_title.Controls.Add(ic);


            SogDiv modal_content = new SogDiv();
            modal_content.CssClass = "modal_content";
            SogModal.Controls.Add(modal_content);

            foreach (SYS_COLUMNS c in list_column)
            {
                SogDiv modal_item = new SogDiv();
                modal_item.CssClass = "modal_item";
                modal_content.Controls.Add(modal_item);

                SogSpan modal_item_title = new SogSpan();
                modal_item_title.InnerText = c.SC_COLUMN_DESC;
                modal_item_title.CssClass = "modal_item_title";
                modal_item.Controls.Add(modal_item_title);

                SogDiv modal_item_content = new SogDiv();
                modal_item_content.CssClass = "modal_item_content";
                modal_item.Controls.Add(modal_item_content);

                SogWebControl txt = GetControl(c, type);
                modal_item_content.Controls.Add(txt);
            }

            SogDiv modal_function = new SogDiv();
            modal_function.CssClass = "modal_function";
            SogModal.Controls.Add(modal_function);

            SogSpan btn_empty = new SogSpan();
            btn_empty.CssClass = "btn_empty CoverClose";
            btn_empty.InnerText = "取消";
            modal_function.Controls.Add(btn_empty);

            SogSpan btn_full = new SogSpan();
            btn_full.CssClass = "btn_full";
            btn_full.InnerText = "提交";
            btn_full.ID = "btn_" + type + "_save";
            modal_function.Controls.Add(btn_full);
        }

        /// <summary>
        /// 新增模态窗口
        /// </summary>
        public virtual void SogModalAddInit()
        {
            SogModalInit(COLUMNS_ADD, "add");
        }

        /// <summary>
        /// 修改模态窗口
        /// </summary>
        public virtual void SogModalEditInit()
        {
            SogModalInit(COLUMNS_EDIT, "edit");
        }

        /// <summary>
        ///  导入模态窗口
        /// </summary>
        public virtual void SogModalImportInit()
        {

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

        public Dictionary<string, string> GetControlData(string strsql)
        {

            SqlConnection cn = comm_fun.get_cn();
            try
            {
                DataTable dt = comm_fun.GetDatatable(strsql, cn);
                Dictionary<string, string> ht = new Dictionary<string, string>();
                foreach (DataRow dr in dt.Rows)
                {
                    ht.Add(dr[0].ToString(), dr[1].ToString());
                }
                return ht;
            }
            catch (Exception ex)
            {
                comm_fun.WriteLog(ex.ToString());
                return new Dictionary<string, string>();
            }
            finally
            {
                comm_fun.CloseConnection(cn);
            }
        }

        public Dictionary<string, string> GetControlData1(string strsql)
        {

            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("", "");
            foreach (KeyValuePair<string, string> item in GetControlData(strsql))
            {
                ht.Add(item.Key, item.Value);
            }

            return ht;

        }

        public SogWebControl GetControl(SYS_COLUMNS c, string type)
        {
            SogWebControl cto = new SogWebControl();
            if (c.SC_CONTROL_TYPE == "SogTextBox")
            {
                cto = new SogTextBox();
            }

            if (c.SC_CONTROL_TYPE == "SOGDropDownList")
            {
                SOGDropDownList s = new SOGDropDownList();
                s.DataSource = GetControlData1(c.SC_CONTROL_DATA);
                cto = s;
            }

            if (c.SC_CONTROL_TYPE == "SogTextArea")
            {
                SogTextArea s = new SogTextArea();
                cto = s;
            }

            if (c.SC_CONTROL_TYPE == "SogFileUpload")
            {
                SogFileUpload s = new SogFileUpload();
                cto = s;
            }

            if (c.SC_CONTROL_TYPE == "SogFolderUpload")
            {
                SogFolderUpload s = new SogFolderUpload();
                cto = s;
            }

            if (c.SC_CONTROL_TYPE == "SogDate")
            {
                SogDate s = new SogDate();
                s.CssClass += "SogDate";
                cto = s;

            }

            if (c.SC_CONTROL_TYPE == "SogDateTime")
            {
                SogDateTime s = new SogDateTime();
                s.CssClass += "SogDateTime";
                cto = s;
            }


            if (string.IsNullOrEmpty(cto.CssClass))
            {
                cto.CssClass = "SogControl";
            }
            else
            {
                cto.CssClass += " SogControl ";
            }
           
            cto.ID = type + "_" + c.SC_COLUMN_NAME;
            cto.placeholder = c.SC_COLUMN_DESC;
            cto.target = c.SC_COLUMN_NAME;
            cto.rule = c.SC_RULE;
            cto.rule_desc = c.SC_RULE_DESC;
            return cto;
        }

        public virtual string GetOtherSQL(List<SqlParameter> list_para)
        {
            //功能，人
            return string.Empty;
        }

        public virtual string GetOtherOrder()
        {
            return string.Empty;

        }

        public virtual Dictionary<string, string> GetOtherSearch()
        {
            return new Dictionary<string, string>();
        }

    }
}