using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;




namespace ERPBase
{
    public class SP_WORK_FOLLOW
    {
        ///<summary>
        ///主键
        ///</summary>
        [Key]
        public int WF_ID { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public int WF_FROM_USER { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public int WF_TO_USER { get; set; }

        /// <summary>
        /// 业务类型[1=机票，2=采购]
        /// </summary>
        public int WF_BUSINESS_TYPE { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public int WF_BUSINESS_KEY { get; set; }

        /// <summary>
        /// 审核状态[0=未审核 1=审批通过 2=驳回]
        /// </summary>
        public int WF_STATUS { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime WF_CREATE_DATE { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? WF_APPROVE_DATE { get; set; }

        /// <summary>
        /// 审批建议
        /// </summary>
        public string WF_REASON { get; set; }

        /// <summary>
        /// 通知次数 1=10分钟提醒,2=4小时提醒,3到5是每天9点提醒,6通知申请人线下沟通
        /// </summary>
        public int WF_TIMES { get; set; }
        

    }
}