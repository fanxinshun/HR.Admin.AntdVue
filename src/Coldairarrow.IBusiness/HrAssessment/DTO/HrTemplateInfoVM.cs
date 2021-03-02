using Coldairarrow.Entity.HrAssessment;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.IBusiness.HrAssessment.DTO
{
    /// <summary>
    /// 测评模板
    /// </summary>
    [Map(typeof(hr_assessment_template))]
    public class HrTemplateInfoDTO : hr_assessment_template
    {
        /// <summary>
        /// 考核项目列表
        /// </summary>
        public List<templatItemsDTO> templatItems { get; set; }

    }

    /// <summary>
    /// 模板项
    /// </summary>
    [Map(typeof(hr_assessment_template_items))]
    public class templatItemsDTO : hr_assessment_template_items
    {
        /// <summary>
        /// 模板分数范围列表
        /// </summary>
        public List<hr_assessment_fractional_items> fractiona_items { get; set; }

        /// <summary>
        /// 模板项项得分
        /// </summary>
        public string score { get; set; }

        /// <summary>
        /// 模板项测评人及评分
        /// </summary>
        public List<hr_assessment_score> H_Score { get; set; }
    }
}
