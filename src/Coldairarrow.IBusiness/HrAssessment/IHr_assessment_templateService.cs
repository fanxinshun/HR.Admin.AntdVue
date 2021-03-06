﻿using Coldairarrow.Entity.HrAssessment;
using Coldairarrow.IBusiness.HrAssessment.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.HrAssessment
{
    /// <summary>
    /// HR考评模板接口
    /// </summary>
    public interface IHr_assessment_templateService
    {
        /// <summary>
        /// 模板下拉框
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input);

        /// <summary>
        /// 查询一个模板
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<HrTemplateInfoDTO> GetHrTemplateInfo(IdInputDTO input);

        /// <summary>
        /// 查询模板列表
        /// </summary>
        /// <param name="pageInput"></param>
        /// <returns></returns>
        Task<PageResult<hr_assessment_template>> GetHrTemplateList(PageInput pageInput);

        /// <summary>
        /// 创建模板
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<AjaxResult> InsertHrTemplateInfo(HrTemplateInfoDTO vm);

        /// <summary>
        /// 修改模板
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<AjaxResult> UpdateHrTemplateInfo(HrTemplateInfoDTO vm);

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<AjaxResult> DeleteHrTemplateInfo(List<string> ids);


        /// <summary>
        /// 创建测评信息
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<AjaxResult> InsertHrHrEvaluationInfo(HrEvaluationDTO vm);

        /// <summary>
        /// 测评得分保存
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<AjaxResult> InsertHrAssessmentScore(List<hr_assessment_score> vm);

        /// <summary>
        /// 查询测评列表
        /// </summary>
        /// <param name="pageInput"></param>
        /// <returns></returns>
        Task<PageResult<HrEvaluationDTO>> GetHrEvaluationList(PageInput<HrEvaluationInput> pageInput);

        /// <summary>
        /// 查看测评结果
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<HrEvaluationDTO> GetHrEvaluationInfo(IdInputDTO input);

        /// <summary>
        /// 导出测评结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<byte[]> ExportHrEvaluationInfo(string id);

        /// <summary>
        /// 删除测评
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<AjaxResult> DeleteHrEvaluationInfo(List<string> ids);
    }

    /// <summary>
    /// 查询参数
    /// </summary>
    public class HrEvaluationInput
    {
        public string template_name { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
    }
}
