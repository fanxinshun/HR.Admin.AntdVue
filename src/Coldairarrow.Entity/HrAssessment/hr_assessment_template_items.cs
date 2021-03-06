﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.HrAssessment
{
    /// <summary>
    /// hr_assessment_template_items
    /// </summary>
    [Table("hr_assessment_template_items")]
    public class hr_assessment_template_items
    {

        /// <summary>
        /// id
        /// </summary>
        [Key, Column(Order = 1)]
        public String id { get; set; }

        /// <summary>
        /// 测评模板id
        /// </summary>
        public String assessment_template_id { get; set; }

        /// <summary>
        /// 考核项目
        /// </summary>
        public String item_name { get; set; }

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