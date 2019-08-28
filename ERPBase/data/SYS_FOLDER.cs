using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class SYS_FOLDER
    {

        ///<summary>
        ///主键
        ///</summary>
        [Key]
        public int FD_ID { get; set; }

        /// <summary>
        /// 父文件夹ID(暂时无用)
        /// </summary>
        public int FD_PARENTID { get; set; }

        /// <summary>
        /// 文件夹名字(暂时无用)
        /// </summary>
        public string FD_NAME { get; set; }

        /// <summary>
        /// 文件数量
        /// </summary>
        public int? FD_FILE_COUNT { get; set; }

        /// <summary>
        /// 文件容量
        /// </summary>
        public System.Double? FD_FILE_SIZE { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string FD_BUSINESS_TYPE { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FD_CREATE_DATE { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        public int FD_CREATE_USER { get; set; }

    }
}