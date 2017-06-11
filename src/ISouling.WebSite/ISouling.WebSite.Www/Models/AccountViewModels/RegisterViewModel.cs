using System.ComponentModel.DataAnnotations;

namespace ISouling.WebSite.Www.Models.AccountViewModels
{
    public enum RegisterType
    {
        MemberEmail = 0,
        MemberCellphone = 1
    }

    #region RegisterViewModel

    public abstract class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0}最少{2}最多{1}个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "{1}和{0}不匹配。")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "{0}最多{1}个字符。")]
        [Display(Name = "验证码")]
        public string VerificationCode { get; set; }
    }

    public abstract class EmailRegisterViewModel : RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
    }

    public abstract class PhoneRegisterViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "手机号")]
        public string PhoneNumber { get; set; }
    }
    #endregion

    #region MemberRegisterViewModel

    public class MemberEmailRegisterViewModel : EmailRegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0}最少{2}最多{1}个字符。", MinimumLength = 4)]
        [Display(Name = "昵称")]
        public string Nickname { get; set; }

        [Display(Name = "邀请码")]
        public string InvitationCode { get; set; }
    }

    public class MemberPhoneRegisterViewModel : PhoneRegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0}最少{2}最多{1}个字符。", MinimumLength = 4)]
        [Display(Name = "昵称")]
        public string Nickname { get; set; }

        [Display(Name = "邀请码")]
        public string InvitationCode { get; set; }
    }
    #endregion
}
