using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISouling.WebSite.Www
{
    /// <summary>
    /// VerifyCode 的摘要说明。
    /// </summary>
    public static class CaptchaMaker
    {
        public const int DefaultCodeNumber = 4;

        #region 私有变量定义
        private static double PI2 = Math.PI * 2;
        private static int _fontSize = 16;
        private static int _lineCount = 0;
        private static int _chaosNum = 50;

        //产生随机数字和字母
        //public static char[] Constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static char[] Constant = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y' };

        //产生随机颜色
        public static Color[] Colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple, Color.SkyBlue };

        //产生随机字体
        public static string[] Fonts = { "Franklin Gothic Demi", "Gill Sans MT", "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "Georgia", "Times New Roman", "Gulim" };
        #endregion

        #region
        /// <summary>
        /// 完全采用默认值产生验证码图片对象
        /// </summary>
        /// <param name="captcha">out参数，返回当前验证码图片的内容字串</param>
        /// <returns>包含图片信息的Image对象</returns>
        public static Image MakeCaptcha(out string captcha)
        {
            return MakeCaptcha(out captcha, DefaultCodeNumber);
        }

        /// <summary>
        /// 指定验证码数字长度产生验证码图片对象
        /// </summary>
        /// <param name="captcha">out参数，返回当前验证码图片的内容字串</param>
        /// <param name="codeNumber">指定产生验证码的数字位数，最大不得超过10位</param>
        /// <returns>包含图片信息的Image对象</returns>
        public static Image MakeCaptcha(out string captcha, int codeNumber)
        {
            return MakeCaptcha(out captcha, codeNumber, _fontSize, _chaosNum, _lineCount);
        }

        /// <summary>
        /// 指定验证码数字长度、字体大小、燥点的多少、干扰线的条数产生验证码图片对象
        /// </summary>
        /// <param name="captcha">out参数，返回当前验证码图片的内容字串</param>
        /// <param name="codeNumber">指定产生验证码的数字位数，最大不得超过10位</param>
        ///<param name="fontSize">指定产生图片中的字体的大小</param>
        /// <param name="chaoNum">指定产生图片中的燥点的多少</param>
        /// <param name="lineNum">指定产生图片中的干扰线的多少</param>
        /// <returns>包含图片信息的Image对象</returns>
        public static Image MakeCaptcha(out string captcha, int codeNumber, int fontSize, int chaoNum, int lineNum)
        {
            var objBitmap = new Bitmap(codeNumber * fontSize + 4, fontSize * 2);
            using (var graphics = Graphics.FromImage(objBitmap))
            {
                graphics.Clear(Color.White);//白色填充背景

                var rand = new Random();

                #region 给背景添加随机生成的燥点
                if (chaoNum > 0)
                    using (var pen = new Pen(Color.LightGray, 0))
                    {
                        for (var index = 0; index < chaoNum; index++)
                        {
                            //objBitmap.SetPixel(rand.Next(objBitmap.Width), rand.Next(objBitmap.Height), Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
                            pen.Color = Color.FromArgb(128, rand.Next(256), rand.Next(256), rand.Next(256));
                            graphics.DrawRectangle(pen, rand.Next(objBitmap.Width), rand.Next(objBitmap.Height), 1, 1);
                        }
                    }
                #endregion

                #region 给背景添加干扰线,干扰线条数由参数决定
                if (lineNum > 0)
                    using (var pen = new Pen(Color.White, 2))
                    {
                        for (var index = 0; index < lineNum; index++)
                        {
                            pen.Color = Colors[rand.Next(Colors.Length)];

                            var x = rand.Next(objBitmap.Width / 2);
                            var y = rand.Next(objBitmap.Height);
                            var xl = rand.Next(objBitmap.Width);
                            var yl = rand.Next(objBitmap.Height);

                            graphics.DrawLine(pen, x, y, x + xl, yl);
                        }
                    }
                #endregion

                #region 随机字体和颜色的验证码字符
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                var newRandom = new StringBuilder(codeNumber);

                for (var index = 0; index < codeNumber; index++)
                {
                    var ch = Constant[rand.Next(Constant.Length - 1)];
                    newRandom.Append(ch);

                    using (var randomFont = new Font(Fonts[rand.Next(Fonts.Length)], fontSize, FontStyle.Bold))
                    using (var randomBlush = new SolidBrush(Colors[rand.Next(Colors.Length)]))
                    {
                        graphics.DrawString(ch.ToString(), randomFont, randomBlush, index * fontSize, rand.Next(1, fontSize / 2));
                    }
                }

                captcha = newRandom.ToString();
                #endregion

                #region 扭曲
                switch (rand.Next(1, 9))
                {
                    case 1:
                        objBitmap = SinImageX(objBitmap, true, 3, 5);
                        break;
                    case 2:
                        objBitmap = SinImageX(objBitmap, true, 3, 4);
                        break;
                    case 3:
                        objBitmap = SinImageX(objBitmap, true, 4, 2);
                        break;
                    case 4:
                        objBitmap = SinImageX(objBitmap, true, 3, 1);
                        break;
                    case 5:
                        objBitmap = SinImageY(objBitmap, true, 3, 5);
                        break;
                    case 6:
                        objBitmap = SinImageY(objBitmap, true, 3, 4);
                        break;
                    case 7:
                        objBitmap = SinImageY(objBitmap, true, 4, 2);
                        break;
                    case 8:
                        objBitmap = SinImageY(objBitmap, true, 3, 1);
                        break;
                }
                #endregion
            }

            return objBitmap;
        }
        #endregion

        #region new 2009-9-14
        /**/
        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        private static Bitmap SinImageX(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            //将位图背景填充为白色
            using (var graph = Graphics.FromImage(destBmp))
            {
                graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
                graph.Dispose();

                var dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;

                for (var i = 0; i < destBmp.Width; i++)
                {
                    for (var j = 0; j < destBmp.Height; j++)
                    {
                        var dx = bXDir ? PI2 * j / dBaseAxisLen : PI2 * i / dBaseAxisLen;
                        dx += dPhase;

                        var dy = Math.Sin(dx);

                        // 取得当前点的颜色
                        var nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                        var nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                        var color = srcBmp.GetPixel(i, j);
                        if (nOldX >= 0 && nOldX < destBmp.Width
                            && nOldY >= 0 && nOldY < destBmp.Height)
                        {
                            destBmp.SetPixel(nOldX, nOldY, color);
                        }
                    }
                }
            }

            return destBmp;
        }

        private static Bitmap SinImageY(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            using (var graph = Graphics.FromImage(destBmp))
            {
                graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
                graph.Dispose();

                double dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;

                for (var i = 0; i < destBmp.Width; i++)
                {
                    for (var j = 0; j < destBmp.Height; j++)
                    {
                        var dy = bXDir ? PI2 * j / dBaseAxisLen : PI2 * i / dBaseAxisLen;
                        dy += dPhase;

                        var dx = Math.Sin(dy);

                        // 取得当前点的颜色
                        var nOldX = bXDir ? i + (int)(dx * dMultValue) : i;
                        var nOldY = bXDir ? j : j + (int)(dx * dMultValue);

                        var color = srcBmp.GetPixel(i, j);
                        if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                        {
                            destBmp.SetPixel(nOldX, nOldY, color);
                        }
                    }
                }
            }

            return destBmp;
        }
        #endregion
    }

    public class CaptchaResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "image/jpeg";

            string captcha;
            var img = CaptchaMaker.MakeCaptcha(out captcha);

            context.HttpContext.Session.SetString(Name, captcha.ToLower());

            img.Save(context.HttpContext.Response.Body, ImageFormat.Jpeg);

            return Task.CompletedTask;
        }

        public static string Name { get; } = "__Captcha__";
    }
}
