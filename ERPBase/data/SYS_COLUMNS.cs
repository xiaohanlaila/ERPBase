using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class SYS_COLUMNS
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int SC_ID { get; set; }

        /// <summary>
        /// 功能ID
        /// </summary>
        public int SC_SO_ID { get; set; }

        /// <summary>
        /// 字段名字
        /// </summary>
        public string SC_COLUMN_NAME { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string SC_COLUMN_DESC { get; set; }

        /// <summary>
        /// 字段数据类型
        /// </summary>
        public string SC_COLUMN_DATATYPE { get; set; }

        /// <summary>
        /// 是否允许新增
        /// </summary>
        public bool SC_IS_ADD { get; set; }

        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool SC_IS_EDIT { get; set; }

        /// <summary>
        /// 是否允许筛选
        /// </summary>
        public bool SC_IS_SEARCH { get; set; }

        /// <summary>
        /// 是否允许筛选
        /// </summary>
        public bool SC_IS_TABLE { get; set; }

        /// <summary>
        /// 是否允许导入
        /// </summary>
        public bool SC_IS_IMPORT { get; set; }

        /// <summary>
        /// 是否允许导出
        /// </summary>
        public bool SC_IS_EXPORT { get; set; }


        /// <summary>
        /// 控件类型
        /// </summary>
        public string SC_CONTROL_TYPE { get; set; }

        /// <summary>
        /// 控件数据源
        /// </summary>
        public string SC_CONTROL_DATA { get; set; }

        /// <summary>
        /// 新增时数据源(为空就直接使用SC_CONTROL_DATA)
        /// </summary>
        public string SC_CONTROL_DATA_ADD { get; set; }

        /// <summary>
        /// 编辑时数据源(为空就直接使用SC_CONTROL_DATA)
        /// </summary>
        public string SC_CONTROL_DATA_EDIT { get; set; }

        /// <summary>
        /// 查询时数据源(为空就直接使用SC_CONTROL_DATA)
        /// </summary>
        public string SC_CONTROL_DATA_WHERE { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public double SC_SEQ { get; set; }

        /// <summary>
        /// 数据正则验证
        /// </summary>
        public string SC_RULE { get; set; }

        /// <summary>
        /// 数据正则验证错误时提示
        /// </summary>
        public string SC_RULE_DESC { get; set; }

        /// <summary>
        /// 在列表显示时的宽度
        /// </summary>
        public string SC_TABLE_CLASS { get; set; }

    }
}