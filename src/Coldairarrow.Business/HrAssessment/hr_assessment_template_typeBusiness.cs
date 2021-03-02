using AutoMapper;
using Coldairarrow.Entity.HrAssessment;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.HrAssessment
{
    public class hr_assessment_template_typeBusiness : BaseBusiness<hr_assessment_template_type>, Ihr_assessment_template_typeBusiness, ITransientDependency
    {
        private readonly IMapper _mapper;
        private readonly Operator _operator;
        public hr_assessment_template_typeBusiness(IDbAccessor db, IMapper mapper, Operator op) : base(db)
        {
            _mapper = mapper;
            _operator = op;
        }

        #region 外部接口

        public async Task<PageResult<hr_assessment_template_type>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<hr_assessment_template_type>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<hr_assessment_template_type, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<hr_assessment_template_type> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(hr_assessment_template_type data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(hr_assessment_template_type data)
        {
            data.updated_user = _operator?.UserId;
            data.updated_time = DateTime.Now;
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await UpdateAsync(x => ids.Contains(x.Id), x =>
            {
                x.Deleted = true;
            });
        }

        #endregion

        #region 私有成员

        #endregion
    }
}