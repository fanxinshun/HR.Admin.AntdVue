﻿using Coldairarrow.Entity.Enum;
using Coldairarrow.Entity.HrAssessment;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.IBusiness.HrAssessment.DTO
{
    /// <summary>
    /// 测评信息
    /// </summary>
    [Map(typeof(hr_assessment_evaluation))]
    public class HrEvaluationDTO : hr_assessment_evaluation
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public EvaluationStatus? status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public String statusText { get => status.IsNullOrEmpty() ? string.Empty : ((EvaluationStatus)status).ToString(); }

        /// <summary>
        /// 测评人列表
        /// </summary>
        public List<hr_assessment_user> H_users { get; set; }

        /// <summary>
        /// 考核模板项--测评人--评分
        /// </summary>
        public HrTemplateInfoDTO TemplateInfo { get; set; }

    }

    /// <summary>
    /// 测评人类型
    /// </summary>
    public enum AssessmentUserType
    {
        用人部门负责人 = 1,
        同职级测评人 = 2,
        下级测评人 = 3,
        HR部门负责人 = 4
    }
}
