using AutoMapper;
using Coldairarrow.Business.Cache;
using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Entity;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.MiniPrograms;
using Coldairarrow.Entity.WorkOrder;
using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using Coldairarrow.Util.DataAccess;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class HomeBusiness : BaseBusiness<Base_User>, IHomeBusiness, ITransientDependency
    {
        readonly IOperator _operator;
        readonly IMapper _mapper;
        readonly Base_UserBusiness _Base_UserBusiness;
        IBase_UserCache _userCache { get; }
        public HomeBusiness(IDbAccessor db, IOperator @operator,
            IMapper mapper,
            Imini_project_userBusiness mini_project_userBusiness,
            IBase_UserCache userCache,
            IWorkOrderDbAccessor workOrderDbAccessor,
            Base_UserBusiness base_UserBusiness)
            : base(db)
        {
            _operator = @operator;
            _mapper = mapper;
            _userCache = userCache;
            _Base_UserBusiness = base_UserBusiness;
        }

        public async Task<string> SubmitLoginAsync(LoginInputDTO input)
        {
            switch (input.LoginSource)
            {
                case 0://登陆测评系统
                    //切换项目重新登录的时候免输入验证码，自动添加验证码
                    //input.verification_code = input.verification_code == "verification_code" ? VerifyCode() : input.verification_code;
                    if (input.verification_code.Trim().IsNullOrEmpty())
                        throw new BusException("请输入验证码！");
                    if (!input.verification_code.VerifyCode())
                        throw new BusException("验证码不正确或已失效，请重新输入！");

                    var theUser = await GetIQueryable().FirstOrDefaultAsync(x => x.UserName == input.userName && x.Password == input.password.ToMD5String());
                    if (theUser.IsNullOrEmpty())
                        throw new BusException("账号或密码不正确！");

                    var theUserDto = await _Base_UserBusiness.GetTheDataAsync(theUser?.Id);
                    if (theUserDto.UserName.ToUpper() != GlobalData.ADMINID.ToUpper() && (theUserDto.RoleIdList.Count == 0 || theUserDto.ProjectIdList.Count == 0))
                    {
                        throw new BusException("该账号没有项目地或者角色信息，请联系管理员添加权限");
                    }

                    JWTPayload jWTPayload = new JWTPayload
                    {
                        UserId = theUser.Id,
                        ProjectId = input.projectId ?? theUserDto.Last_Interview_Project
                    };
                    return jWTPayload.GetToken();
                case 1://登陆工单系统
                    var url = ConfigHelper.GetValue("WorkOrder:Service") + ConfigHelper.GetValue("WorkOrder:Login");
                    var dicts = new Dictionary<string, object>()
                    {
                        { "user_name",input.userName},
                        { "user_pwd",input.password},
                        { "verification_code",input.verification_code},
                    };
                    return HttpHelper.PostData(url, dicts, null, ContentType.Json);
                default:
                    return "请指定要登陆的系统";
            }
        }

        public async Task ChangePwdAsync(ChangePwdInputDTO input)
        {
            var theUser = _operator.Property;
            if (theUser.Password != input.oldPwd?.ToMD5String())
                throw new BusException("原密码错误!");

            theUser.Password = input.newPwd.ToMD5String();
            await UpdateAsync(_mapper.Map<Base_User>(theUser));
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public byte[] VerifyCodeByte()
        {
            return ImgVerifyCodeHelper.BuildVerifyCodeByte(4);
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public string VerifyCode()
        {
            return ImgVerifyCodeHelper.BuildVerifyCode(4);
        }
    }
}
