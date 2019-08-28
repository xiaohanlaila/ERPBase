using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class AP_FUNCTION_GROUP
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int FG_ID { get; set; }

        /// <summary>
        /// 分组描述
        /// </summary>
        public string FG_DESC { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public double FG_SEQ { get; set; }


        /// <summary>
        /// 跳转路径（一般为空）
        /// </summary>
        public string FG_URL { get; set; }


        /// <summary>
        /// 样式
        /// </summary>
        public string FG_CSS_CLASS { get; set; }


        /// <summary>
        /// 是否生效
        /// </summary>
        public bool FG_ACTIVE { get; set; }

    }
}