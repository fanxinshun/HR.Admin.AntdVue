using Coldairarrow.Business.HrAssessment;
using Coldairarrow.Entity.HrAssessment;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.HrAssessment
{
    [Route("/HrAssessment/[controller]/[action]")]
    public class hr_assessment_template_typeController : BaseApiController
    {
        #region DI

        public hr_assessment_template_typeController(Ihr_assessment_template_typeBusiness hr_assessment_template_typeBus)
        {
            _hr_assessment_template_typeBus = hr_assessment_template_typeBus;
        }

        Ihr_assessment_template_typeBusiness _hr_assessment_template_typeBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<hr_assessment_template_type>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _hr_assessment_template_typeBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<hr_assessment_template_type> GetTheData(IdInputDTO input)
        {
            return await _hr_assessment_template_typeBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(hr_assessment_template_type data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _hr_assessment_template_typeBus.AddDataAsync(data);
            }
            else
            {
                await _hr_assessment_template_typeBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _hr_assessment_template_typeBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}