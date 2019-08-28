using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBase
{
    /// <summary>
    /// 配置表
    /// </summary>
    public class H5Objects
    {
        /// <summary>
        /// 数据表名
        /// </summary>
        public string HO_TABLE_NAME { get; set; }

        /// <summary>
        /// 业务描述
        /// </summary>
        public string HO_NAME { get; set; }

        /// <summary>
        /// ID字段
        /// </summary>
        public string HO_ID_FIELD { get; set; }

        /// <summary>
        /// 申请用户字段
        /// </summary>
        public string HO_USER_FIELD { get; set; }

        /// <summary>
        /// 创建日期字段
        /// </summary>
        public string HO_DATE_FIELD { get; set; }

        /// <summary>
        /// 状态字段
        /// </summary>
        public string HO_STAUTS_FIELD { get; set; }

        /// <summary>
        /// 核心字段
        /// </summary>
        public string HO_MASTER_FIELD { get; set; }

        /// <summary>
        /// 业务类型[1=机票，2=采购]
        /// </summary>
        public int HO_BUSINESS_TYPE { get; set; }
    }
}