﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.MiniPrograms
{
    /// <summary>
    /// mini_project_user
    /// </summary>
    [Table("mini_project_user")]
    public class mini_project_user
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public String Project_Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public String User_Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Single? Sort { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

    }
}