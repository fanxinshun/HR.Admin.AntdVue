﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.HrAssessment
{
    /// <summary>
    /// hr_assessment_score
    /// </summary>
    [Table("hr_assessment_score")]
    public class hr_assessment_score
    {

        /// <summary>
        /// id
        /// </summary>
        [Key, Column(Order = 1)]
        public String id { get; set; }

        /// <summary>
        /// 测评id
        /// </summary>
        public String assessment_evaluation_id { get; set; }

        /// <summary>
        /// 考核项目id
        /// </summary>
        public String assessment_items_id { get; set; }

        /// <summary>
        /// 测评人id
        /// </summary>
        public String assessment_user_id { get; set; }

        /// <summary>
        /// 测评人姓名
        /// </summary>
        public String assessment_user_name { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        public Int32? score { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String create_user { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? created_time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public String updated_user { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? updated_time { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Single? Sort { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }
    }
}