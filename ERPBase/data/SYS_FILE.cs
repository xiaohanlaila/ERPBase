using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class SYS_FILE
    {

        ///<summary>
        ///主键
        ///</summary>
        [Key]
        public int FL_ID { get; set; }

        /// <summary>
        /// 文件夹ID
        /// </summary>
        public int FL_FD_ID { get; set; }


        /// <summary>
        /// 文件路径
        /// </summary>
        public string FL_URL { get; set; }

        /// <summary>
        /// 文件名字
        /// </summary>
        public string FL_NAME { get; set; }

       /// <summary>
       /// 大小
       /// </summary>
        public double FL_SIZE { get; set; }

        /// <summary>
        /// 文件小图标
        /// </summary>
        public string FL_ICON_S { get; set; }

        /// <summary>
        /// 文件中图标
        /// </summary>
        public string FL_ICON_M { get; set; }

        /// <summary>
        /// 文件大图标
        /// </summary>
        public string FL_ICON_L { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string FL_EXTENSION { get; set; }

        /// <summary>
        /// 云端ID
        /// </summary>
        public string FL_GUID { get; set; }

        /// <summary>
        /// 云端路径
        /// </summary>
        public string FL_GUID_URL { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FL_CREATE_DATE { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int FL_CREATE_USER { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string FL_BUSINESS_TYPE { get; set; }

        /// <summary>
        /// 是否生效
        /// </summary>
        public bool FL_ACTIVE { get; set; }

    }
}