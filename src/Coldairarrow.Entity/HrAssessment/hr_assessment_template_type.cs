using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.HrAssessment
{
    /// <summary>
    /// hr_assessment_template_type
    /// </summary>
    [Table("hr_assessment_template_type")]
    public class hr_assessment_template_type
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 模板类型编号
        /// </summary>
        public Int32? type_code { get; set; }

        /// <summary>
        /// 模板类型名称
        /// </summary>
        public String type_name { get; set; }

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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

    }
}