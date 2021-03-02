using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.HrAssessment
{
    /// <summary>
    /// hr_assessment_template
    /// </summary>
    [Table("hr_assessment_template")]
    public class hr_assessment_template
    {

        /// <summary>
        /// id
        /// </summary>
        [Key, Column(Order = 1)]
        public String id { get; set; }

        /// <summary>
        /// 模板编号
        /// </summary>
        public Int32? template_code { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public String template_name { get; set; }

        /// <summary>
        /// 考核标准
        /// </summary>
        public Int32? assessment_criteria { get; set; }

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