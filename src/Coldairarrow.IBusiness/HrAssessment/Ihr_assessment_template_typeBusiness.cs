using Coldairarrow.Entity.HrAssessment;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.HrAssessment
{
    public interface Ihr_assessment_template_typeBusiness
    {
        Task<PageResult<hr_assessment_template_type>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<hr_assessment_template_type> GetTheDataAsync(string id);
        Task AddDataAsync(hr_assessment_template_type data);
        Task UpdateDataAsync(hr_assessment_template_type data);
        Task DeleteDataAsync(List<string> ids);
    }
}