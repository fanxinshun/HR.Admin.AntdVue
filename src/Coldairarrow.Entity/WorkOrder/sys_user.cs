using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.WorkOrder
{
    /// <summary>
    /// sys_user
    /// </summary>
    [Table("sys_user")]
    public class sys_user
    {

        /// <summary>
        /// id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 id { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public Int32 system_id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public String user_name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public String user_pwd { get; set; }

        /// <summary>
        /// 真实名字
        /// </summary>
        public String full_name { get; set; }

        /// <summary>
        /// 状态(0无效,1有效)
        /// </summary>
        public Int32 user_status { get; set; }

        /// <summary>
        /// 项目地编号
        /// </summary>
        public Int32? project_id { get; set; }

        /// <summary>
        /// 项目地名称
        /// </summary>
        public String project_name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_time { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime updated_time { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public Int32 is_deleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? deleted_time { get; set; }

    }
}