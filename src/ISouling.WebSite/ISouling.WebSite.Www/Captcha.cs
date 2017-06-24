﻿using ISouling.Component.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ISouling.WebSite.Www
{
    public class CaptchaResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "image/jpeg";

            var captcha = CaptchaMaker.MakeCaptcha();

            context.HttpContext.Session.SetString(Name, captcha.Captcha.ToLower());

            captcha.Save(context.HttpContext.Response.Body);

            return Task.CompletedTask;
        }

        public static string Name { get; } = "__Captcha__";
    }
}