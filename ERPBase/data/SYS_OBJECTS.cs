using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class SYS_OBJECTS
    {
        /// <summary>
        /// 功能ID
        /// </summary>
        public int SO_ID { get; set; }

        /// <summary>
        /// 数据表名字
        /// </summary>
        public string SO_TABLE_NAME { get; set; }

        /// <summary>
        /// 数据表主键字段
        /// </summary>
        public string SO_TABLE_KEY { get; set; }


        /// <summary>
        /// 查询时数据表名字
        /// </summary>
        public string SO_TABLE_NAME_SEARCH { get; set; }

        /// <summary>
        /// 界面头部文字描述
        /// </summary>
        public string SO_TITLE { get; set; }

        /// <summary>
        /// 每条数据的描述
        /// </summary>
        public string SO_ITEM_DESC { get; set; }

        /// <summary>
        /// 是否显示增加按钮
        /// </summary>
        public bool SO_IS_ADD { get; set; }

        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        public bool SO_IS_DELETE { get; set; }


        /// <summary>
        /// 是否显示编辑按钮
        /// </summary>
        public bool SO_IS_EDIT { get; set; }


        /// <summary>
        /// 是否显示导出按钮
        /// </summary>
        public bool SO_IS_EXPORT { get; set; }


        /// <summary>
        /// 是否显示导入按钮
        /// </summary>
        public bool SO_IS_IMPORT { get; set; }

        /// <summary>
        /// 是否显示开发按钮
        /// </summary>
        public bool SO_IS_DEVELOPMENT { get; set; }

        /// <summary>
        /// 是否显示选择框
        /// </summary>
        public bool SO_IS_GV_SELECT { get; set; }

        /// <summary>
        ///是否显示头部区域
        /// </summary>
        public bool SO_IS_TITLE { get; set; }

        /// <summary>
        /// 是否显示条件区域
        /// </summary>
        public bool SO_IS_CONDICTION { get; set; }

        /// <summary>
        /// 客户是否可以调整表格顺序与是否显示
        /// </summary>
        public bool SO_IS_CLIENT_DEVELOPMENT { get; set; }
       
    }
}