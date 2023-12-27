using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SixLaborsCaptcha.Core;
using SwitchAPI.DB;
using SwitchAPI.Models.Captcha;
using System.Threading;

namespace SwitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private static CaptchaGenerator _captchaGenerator;


        public CaptchaController(CaptchaGenerator captchaGenerator)
        {

            _captchaGenerator = captchaGenerator;

        }

        [Route("GetCaptcha")]
        [HttpGet]
        public CaptchaResult GetCaptchaImage([FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha)
        {
            var randomNumber = new Random().Next(100000, 999999).ToString();
            var imageTxt = sixLaborsCaptcha.Generate(randomNumber);
            var captchaToken = Guid.NewGuid().ToString();
            var captcha = new CaptchaModels(captchaToken, randomNumber);
            var captcharesult = new CaptchaResult()
            {
                CaptchaImage = imageTxt,
                CaptchaToken = captchaToken
            };
            _captchaGenerator.Captchas.Add(captcha);
            return captcharesult;
        }




        //**************این درسته ولی زمان اجراش زیاده 

        [Route("GuessCaptcha")]
        [HttpGet]
        public async Task<ActionResult<object>> GuessCaptcha()
        {
            var numberContainer = new GuessCaptcha();
            numberContainer.Number = 0;


            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            var tasks = new List<Task>();



            tasks.Add(Task.Run(() => CountUpInRangeAsync(numberContainer, cancellationTokenSource, cancellationToken, 100000, 999999 / 2, "Count Up")));
            tasks.Add(Task.Run(() => CountDownInRangeAsync(numberContainer, cancellationTokenSource, cancellationToken, 999999, 999999 / 2, "Count Down")));
            tasks.Add(Task.Run(() => CountUpInRangeAsync(numberContainer, cancellationTokenSource, cancellationToken, 999999 / 2, 999999, "Count Up Ave")));
            tasks.Add(Task.Run(() => CountDownInRangeAsync(numberContainer, cancellationTokenSource, cancellationToken, 2, 100000, "Count Down Ave")));
            tasks.Add(Task.Run(() => CountUpInRangeAsync(numberContainer, cancellationTokenSource, cancellationToken, 499999 / 2, 100000, "Count Down Ave+")));
            tasks.Add(Task.Run(() => CountDownInRangeAsync(numberContainer, cancellationTokenSource, cancellationToken, 499999 / 2, 999999 / 2, "Count Up Ave+")));
            tasks.Add(Task.Run(() => CountUpInRangeAsync(numberContainer, cancellationTokenSource, cancellationToken, 749998, 999999, "Count Up Ave++")));
            tasks.Add(Task.Run(() => CountDownInRangeAsync(numberContainer, cancellationTokenSource, cancellationToken, 749998, 999999 / 2, "Count Down Ave++")));

            await Task.WhenAny(Task.WhenAll(tasks), Task.Delay(TimeSpan.FromSeconds(30))); // Set a timeout for completion

            cancellationTokenSource.Cancel(); // Cancel remaining tasks

            return new { captchaKey = numberContainer.Number };
        }


        public async Task CountUpInRangeAsync(GuessCaptcha numberContainer, CancellationTokenSource cancellationTokenSource, CancellationToken cancellationToken, int start, int end, string debugMessage)
        {
            if (numberContainer.Number != 0)
            {
                cancellationTokenSource.Cancel();
                return;
            }

            for (int num = start; num <= end; num++)
            {
                // ... 
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
                {
                    
                    lock (cancellationTokenSource)
                    {
                        if (numberContainer.Number == 0)
                        {
                            numberContainer.Number = num;
                            cancellationTokenSource.Cancel();
                        }
                    }
                    return;
                }
                // ...
            }
        }

        public async Task CountDownInRangeAsync(GuessCaptcha numberContainer, CancellationTokenSource cancellationTokenSource, CancellationToken cancellationToken, int start, int end, string debugMessage)
        {
            if (numberContainer.Number != 0)
            {
                cancellationTokenSource.Cancel();
                return;
            }

            for (int num = start; num >= end; num--)
            {
                
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
                {
                    
                    lock (cancellationTokenSource)
                    {
                        if (numberContainer.Number == 0)
                        {
                            numberContainer.Number = num;
                            cancellationTokenSource.Cancel();
                        }
                    }
                    return;
                }
               
            }
        }

        //****************این به شکل تسک ولی ارور میده میگه پارامتر زیاد داید 
        //[Route("CheckNumbers")]
        //[HttpGet]
        //public async Task<ActionResult<object>> CheckNumbers()
        //{
        //    var numberContainer = new GuessCaptcha();
        //    numberContainer.Number = 0;

        //    List<Task> tasks = new List<Task>();

        //    tasks.Add(DecreaseTo77Async(numberContainer));
        //    tasks.Add(IncreaseTo77Async(numberContainer));

        //    // انتظار اجرای تمام تسک‌ها
        //    await Task.WhenAny(Task.WhenAll(tasks), Task.Delay(Timeout.Infinite));

        //    return new { Result = numberContainer.Number == 77, Value = numberContainer.Number };
        //}

        //public async Task DecreaseTo77Async(GuessCaptcha numberContainer)
        //{
        //    for (int num = 50; num >= 0; num--)
        //    {
        //        await Task.Delay(10);

        //        lock (numberContainer)
        //        {
        //            if (numberContainer.Number == 77)
        //            {
        //                return;
        //            }
        //            numberContainer.Number = num;
        //        }
        //    }
        //}

        //public async Task IncreaseTo77Async(GuessCaptcha numberContainer)
        //{
        //    for (int num = 50; num <= 77; num++)
        //    {
        //        await Task.Delay(10);

        //        lock (numberContainer)
        //        {
        //            if (numberContainer.Number == 77)
        //            {
        //                return;
        //            }
        //            numberContainer.Number = num;
        //        }
        //    }
        //}

    }
}
