using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.MiniPrograms
{
    public class mini_project_userBusiness : BaseBusiness<mini_project_user>, Imini_project_userBusiness, ITransientDependency
    {
        public mini_project_userBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        /// <summary>
        /// 查询项目中用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<UserProjectDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = from a in Db.GetIQueryable<mini_project_user>()
                    join b in Db.GetIQueryable<mini_project>() on a.Project_Id equals b.Id
                    join c in Db.GetIQueryable<Base_User>() on a.User_Id equals c.Id
                    where a.Deleted == false
                       && a.Deleted == false
                       && b.Deleted == false
                       && c.Deleted == false
                    select new UserProjectDTO
                    {
                        Id = a.Id,
                        Project_Id = a.Project_Id,
                        Project_Code = b.Project_Code,
                        Project_Name = b.Project_Name,
                        User_Id = a.User_Id,
                        User_Code = c.UserName,
                        User_Name = c.RealName
                    };
            var where = LinqHelper.True<UserProjectDTO>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<UserProjectDTO, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        /// <summary>
        /// 获取用户项目--列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<UserProjectDTO>> GetUserProjectListAsync(string id)
        {
            var q = from a in Db.GetIQueryable<mini_project_user>()
                    join b in Db.GetIQueryable<mini_project>() on a.Project_Id equals b.Id
                    where a.Deleted == false
                       && a.User_Id == id
                       && a.Deleted == false
                       && b.Deleted == false
                    select new UserProjectDTO
                    {
                        Project_Code = b.Project_Code,
                        Project_Name = b.Project_Name,
                        Id = a.Project_Id,
                        User_Id = a.User_Id
                    };

            return await q.ToListAsync();
        }

        public async Task<mini_project_user> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        /// <summary>
        /// 新增项目用户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataAsync(mini_project_user data)
        {
            var puser = await Db.GetIQueryable<mini_project_user>()
                .FirstOrDefaultAsync(x => x.User_Id == data.User_Id
                                            && x.Project_Id == data.Project_Id
                                            && !x.Deleted);
            if (!puser.IsNullOrEmpty())
                throw new BusException("添加的用户已经在当前项目中了，无需重复添加");
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(mini_project_user data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }
        #endregion

        #region 私有成员

        #endregion
    }
}