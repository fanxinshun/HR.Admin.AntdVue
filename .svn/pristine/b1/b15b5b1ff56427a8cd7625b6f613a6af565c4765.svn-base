using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IHomeBusiness
    {
        Task<string> SubmitLoginAsync(LoginInputDTO input);
        Task ChangePwdAsync(ChangePwdInputDTO input);
        byte[] VerifyCodeByte();
    }

    public class LoginInputDTO
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }


        public string projectId { get; set; }

        [Required]
        public string verification_code { get; set; }

        /// <summary>
        /// 登陆系统0：人事测评，1：工单系统
        /// </summary>
        public int LoginSource { get; set; } = 0;
    }

    public class ChangePwdInputDTO
    {
        [Required]
        public string oldPwd { get; set; }

        [Required]
        public string newPwd { get; set; }
    }


    public class ChangeProjectInputDTO
    {
        [Required]
        public string token { get; set; }

        [Required]
        public string projectId { get; set; }
    }
}
