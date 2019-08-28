using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ERPBase.demo
{
    public class maintenance : System.Web.UI.Page
    {
        public int FunctionCode { get; set; }

        public SogGeneric G_SogHead;

        public SogGeneric G_SogCondition;

        public SogGeneric G_SogFunction;

        public SogGeneric G_SogContent;

        protected override void OnInit(EventArgs e)
        {

            EnableViewState = false;

            SogHeadInit();

            SogConditionInit();

            SogFunctionInit();

            SogContentInit();

            SogModalInit();

            //base.OnInit(e);
        }

        protected virtual void SogHeadInit()
        {
            SogDiv SogHead = new SogDiv();
            SogHead.CssClass = "SogHead";
            G_SogHead = SogHead;
            Form.Controls.Add(SogHead);

            SogSpan SogTitle = new SogSpan();
            SogTitle.CssClass = "SogTitle";
            SogTitle.InnerText = "合作伙伴";
            G_SogHead.Controls.Add(SogTitle);


            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("1", "我的渠道");
            ht.Add("2", "全部渠道");
            ht.Add("3", "公海渠道");
            SogNavigate obj_SogNavigate = new SogNavigate();
            obj_SogNavigate.DataSource = ht;
            obj_SogNavigate.Value = "2";
            G_SogHead.Controls.Add(obj_SogNavigate);

            SogDiv SogRight = new SogDiv();
            SogRight.CssClass = "SogRight";
            G_SogHead.Controls.Add(SogRight);

            SogSpan add_botton = new SogSpan();
            add_botton.CssClass = "add_botton";
            add_botton.InnerText = "+";
            SogRight.Controls.Add(add_botton);
        }

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

            for (int i = 0; i < 5; i++)
            {
                condition_item c = new condition_item();
                c.ID = "c" + i.ToString();
                c.HeadText = "股权激励" + i.ToString();
                c.Value = i.ToString();

                Dictionary<string, string> ht = new Dictionary<string, string>();
                for (int j = 0; j < 10; j++)
                {
                    ht.Add(j.ToString(), "股权" + i.ToString() + j.ToString());
                }

                c.DataSource = ht;
                open.Controls.Add(c);
            }
            SogDiv condition_item = new SogDiv();
            condition_item.CssClass = "condition_item";
            condition_item.Attributes.Add("style", "padding-left:100px");
            open.Controls.Add(condition_item);

            SogTextSearch obj_SogTextSearch = new SogTextSearch();
            obj_SogTextSearch.placeholder = "渠道/省份/城市";
            condition_item.Controls.Add(obj_SogTextSearch);

            SogDiv close = new SogDiv();
            close.CssClass = "close";
            SogCondition.Controls.Add(close);


            SogDiv condition_head = new SogDiv();
            condition_head.CssClass = "condition_head";
            condition_head.InnerText = "筛选条件";
            close.Controls.Add(condition_head);

            for (int i = 0; i < 5; i++)
            {
                condition_desc d = new condition_desc();
                d.HeadText = "股权激励" + i.ToString();
                d.ParentID = "c" + i.ToString();
                close.Controls.Add(d);
            }

            SogDiv handler = new SogDiv();
            handler.CssClass = "handler";
            SogCondition.Controls.Add(handler);

            SogIcon obj_i = new SogIcon();
            obj_i.CssClass = "small_btn fa fa-chevron-down";
            handler.Controls.Add(obj_i);
        }

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
            s3.InnerText = "合作伙伴";
            G_SogFunction.Controls.Add(s3);

            SogSpan b1 = new SogSpan();
            b1.CssClass = "btn green_btn";
            b1.InnerText = "删除";
            G_SogFunction.Controls.Add(b1);

            SogSpan b2 = new SogSpan();
            b2.CssClass = "btn green_btn";
            b2.InnerText = "公海";
            G_SogFunction.Controls.Add(b2);

            SogSpan b3 = new SogSpan();
            b3.CssClass = "btn green_btn SogRight";
            b3.InnerText = "公海1";
            G_SogFunction.Controls.Add(b3);

            SogSpan b4 = new SogSpan();
            b4.CssClass = "btn green_btn SogRight";
            b4.InnerText = "公海2";
            G_SogFunction.Controls.Add(b4);

            SogSpan b5 = new SogSpan();
            b5.CssClass = "btn green_btn SogRight";
            b5.InnerText = "公海3";
            G_SogFunction.Controls.Add(b5);
        }


        public virtual void SogContentInit()
        {
            SogDiv SogContent = new SogDiv();
            SogContent.CssClass = "SogContent";
            G_SogContent = SogContent;
            Form.Controls.Add(SogContent);
        }

        public virtual void SogModalInit()
        {

            SogDiv SogModal = new SogDiv();
            SogModal.CssClass = "SogModal";
            SogModal.ID = "div_add";
            Form.Controls.Add(SogModal);


            SogDiv modal_title = new SogDiv();
            modal_title.CssClass = "modal_title";
            modal_title.InnerText = "编辑";
            SogModal.Controls.Add(modal_title);

            SogIcon ic = new SogIcon();
            ic.CssClass = "fa fa-times-circle SogRight CoverClose";
            ic.Attributes.Add("aria-hidden", "true");
            modal_title.Controls.Add(ic);


            SogDiv modal_content = new SogDiv();
            modal_content.CssClass = "modal_content";
            SogModal.Controls.Add(modal_content);

            for (int i = 0; i < 100; i++)
            {
                SogDiv modal_item = new SogDiv();
                modal_item.CssClass = "modal_item";
                modal_content.Controls.Add(modal_item);

                SogSpan modal_item_title = new SogSpan();
                modal_item_title.InnerText = "公司名字" + i.ToString();
                modal_item_title.CssClass = "modal_item_title";
                modal_item.Controls.Add(modal_item_title);

                SogDiv modal_item_content = new SogDiv();
                modal_item_content.CssClass = "modal_item_content";
                modal_item.Controls.Add(modal_item_content);

                System.Web.UI.WebControls.TextBox txt = new System.Web.UI.WebControls.TextBox();
                txt.CssClass = "SogControl";
                txt.Attributes.Add("placeholder", "公司名字" + i.ToString());
                modal_item_content.Controls.Add(txt);
            }


            SogDiv modal_function = new SogDiv();
            modal_function.CssClass = "modal_function";
            SogModal.Controls.Add(modal_function);

            SogSpan btn_full = new SogSpan();
            btn_full.CssClass = "btn_full";
            btn_full.InnerText = "提交";
            modal_function.Controls.Add(btn_full);

            SogSpan btn_empty = new SogSpan();
            btn_empty.CssClass = "btn_empty CoverClose";
            btn_empty.InnerText = "取消";
            modal_function.Controls.Add(btn_empty);
        }
    }
}