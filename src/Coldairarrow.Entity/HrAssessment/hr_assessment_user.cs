using Coldairarrow.Entity.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.HrAssessment
{
    /// <summary>
    /// hr_assessment_user
    /// </summary>
    [Table("hr_assessment_user")]
    public class hr_assessment_user
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
        /// 测评人类别(1.用人部门负责人；2.同职级测评人；3.下级测评人；4.HR部门负责人)
        /// </summary>
        public Int32? user_type { get; set; }

        /// <summary>
        /// 测评人ID
        /// </summary>
        public String user_id { get; set; }

        /// <summary>
        /// 测评人姓名
        /// </summary>
        public String user_name { get; set; }

        /// <summary>
        /// 测评状态(1:待测评,2:已测评)
        /// </summary>
        public EvaluationStatus status { get; set; }

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