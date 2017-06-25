using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ISouling.Component.Web
{
    public class EmailOrCellphoneAttribute : DataTypeAttribute
    {
        public EmailOrCellphoneAttribute() : base(DataType.Text)
        {
            ErrorMessage = "{0}不是有效的邮箱或手机号";
        }

        public override bool IsValid(object value)
        {
            return new EmailAddressAttribute().IsValid(value) || Regex.IsMatch(value as string, @"^1\d{10}$", RegexOptions.Compiled);
        }
    }

    public class CaptchaAttribute : DataTypeAttribute
    {
        public CaptchaAttribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value) =>
            string.Compare(value as string, HttpContext.Current.Session.GetString(CaptchaResult.Name), StringComparison.OrdinalIgnoreCase) == 0;
    }
}