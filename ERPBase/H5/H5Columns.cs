using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBase
{
    /// <summary>
    /// 配置字段
    /// </summary>
    public class H5Columns
    {
        /// <summary>
        /// 字段名字
        /// </summary>
        public string HC_NAME { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string HC_DESC { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public string HC_CONTROL_TYPE { get; set; }

        /// <summary>
        /// 正则规则
        /// </summary>
        public string HC_RULE { get; set; }

        /// <summary>
        /// 违反正则提示信息
        /// </summary>
        public string HC_URL_DESC { get; set; }

    }
}