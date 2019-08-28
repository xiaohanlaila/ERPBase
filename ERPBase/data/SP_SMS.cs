using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPBase
{
    /// <summary>
    /// 短信表
    /// </summary>
    public class SP_SMS
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int SM_ID { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string SM_TEMP_ID { get; set; }


        /// <summary>
        /// 手机号码
        /// </summary>
        public string SM_PHONE { get; set; }

        /// <summary>
        /// 填空内容
        /// </summary>
        public string SM_SHORT_CONTENT { get; set; }

        /// <summary>
        /// 完整内容
        /// </summary>
        public string SM_FULL_CONTENT { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime SM_CREATE_DATE { get; set; }

        /// <summary>
        /// 是否发送
        /// </summary>
        public bool SM_IS_SEND { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SM_SEND_DATE { get; set; }

      
    }
}