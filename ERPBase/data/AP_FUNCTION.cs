using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class AP_FUNCTION
    {

        /// <summary>
        /// 主键
        /// </summary>
        public int FN_ID { get; set; }

        /// <summary>
        /// 功能描述
        /// </summary>
        public string FN_DESC { get; set; }

        /// <summary>
        /// 功能路径
        /// </summary>
        public string FN_URL { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public int FN_FG_ID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public double FN_SEQ { get; set; }


        /// <summary>
        /// 样式
        /// </summary>
        public string FN_CSS_CLASS { get; set; }


        /// <summary>
        /// 是否生效
        /// </summary>
        public bool FN_ACTIVE { get; set; }

    }
}