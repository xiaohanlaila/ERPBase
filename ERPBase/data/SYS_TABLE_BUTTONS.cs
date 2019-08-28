using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class SYS_TABLE_BUTTONS
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int SB_ID { get; set; }

        /// <summary>
        /// 功能ID
        /// </summary>
        public int SB_SO_ID { get; set; }

        /// <summary>
        /// 头部标题
        /// </summary>
        public string SB_HEAD_TEXT { get; set; }

        /// <summary>
        /// 头部样式
        /// </summary>
        public string SB_HEAD_CSSCLASS { get; set; }

        /// <summary>
        /// 内部文字
        /// </summary>
        public string SB_INNER_TEXT { get; set; }

        /// <summary>
        /// 内部样式
        /// </summary>
        public string SB_INNER_CSSCLASS { get; set; }
    }
}