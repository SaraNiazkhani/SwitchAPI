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



        //****************این به شکل تسکه ولی ارور میده میگه پارامتر زیاد داید 
      

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



        //****************این درسته فقط زمان اجراش زیاده خیلی گفت راهش اینه که به صورت task thread 
       // [Route("GuessCaptcha")]
        //        [HttpGet]
        //        public ActionResult<object> GuessCaptcha()
        //        {
        //            var numberContainer = new GuessCaptcha();
        //            numberContainer.Number = 0;
        //            int number = 0;
        //            CancellationTokenSource globalCancellationTokenSource = new CancellationTokenSource();

        //            CancellationToken globalCancellationToken = globalCancellationTokenSource.Token;

        //            List<Thread> threads = new List<Thread>();

        //            Thread thread1 = CreateAndStartThreadUp(numberContainer, globalCancellationToken, globalCancellationTokenSource, 100000, 999999 / 2, "Count Up");
        //            Thread thread2 = CreateAndStartThreadDown(numberContainer, globalCancellationToken, globalCancellationTokenSource, 999999, 999999 / 2, "Count Down");
        //            Thread thread3 = CreateAndStartThreadUp(numberContainer, globalCancellationToken, globalCancellationTokenSource, 999999 / 2, 999999, "Count Up Ave");
        //            Thread thread4 = CreateAndStartThreadDown(numberContainer, globalCancellationToken, globalCancellationTokenSource, 999999 / 2, 100000, "Count Down Ave");
        //            Thread thread5 = CreateAndStartThreadDown(numberContainer, globalCancellationToken, globalCancellationTokenSource, 499999 / 2, 100000, "Count Down Ave+");
        //            Thread thread6 = CreateAndStartThreadUp(numberContainer, globalCancellationToken, globalCancellationTokenSource, 499999 / 2, 999999 / 2, "Count Up Ave+");
        //            Thread thread7 = CreateAndStartThreadUp(numberContainer, globalCancellationToken, globalCancellationTokenSource, 749998, 999999, "Count Up Ave++");
        //            Thread thread8 = CreateAndStartThreadDown(numberContainer, globalCancellationToken, globalCancellationTokenSource, 749998, 999999 / 2, "Count Down Ave++");

        //            threads.Add(thread1);
        //            threads.Add(thread2);
        //            threads.Add(thread3);
        //            threads.Add(thread4);
        //            threads.Add(thread5);
        //            threads.Add(thread6);
        //            threads.Add(thread7);
        //            threads.Add(thread8);

        //            foreach (var thread in threads)
        //            {
        //                thread.Join();
        //            }

        //            globalCancellationTokenSource.Cancel(); // متوقف کردن تمام تردها

        //            return new { captchaKey = numberContainer.Number };
        //        }

        //        public static Thread CreateAndStartThreadUp(GuessCaptcha numberContainer, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
        //        {
        //            Thread thread = new Thread(() => CountUpInRange(numberContainer, globalCancellationToken, globalCancellationTokenSource, start, end, debugMessage));
        //            thread.Start();
        //            return thread;
        //        }
        //        public static Thread CreateAndStartThreadDown(GuessCaptcha numberContainer, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
        //        {
        //            Thread thread = new Thread(() => CountDownInRange(numberContainer, globalCancellationToken, globalCancellationTokenSource, start, end, debugMessage));
        //            thread.Start();
        //            return thread;
        //        }



        //        public static void CountUpInRange(GuessCaptcha numberContainer, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
        //        {
        //            if (numberContainer.Number != 0)
        //            {
        //                return;
        //            }

        //            for (int num = start; num <= end; num++)
        //            {
        //                System.Diagnostics.Debug.WriteLine($" {debugMessage} {num}");
        //                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
        //                {
        //                    System.Diagnostics.Debug.WriteLine($"{debugMessage} Ended due to matching CaptchaToken!");
        //                    lock (globalCancellationTokenSource)
        //                    {
        //                        if (numberContainer.Number == 0)
        //                        {
        //                            numberContainer.Number = num;
        //                            globalCancellationTokenSource.Cancel();
        //                        }
        //                    }
        //                    return;
        //                }

        //                Task.Delay(10).Wait();

        //                if (globalCancellationToken.IsCancellationRequested)
        //                {
        //                    return;
        //                }
        //            }
        //        }
        //        public static void CountDownInRange(GuessCaptcha numberContainer, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
        //        {
        //            if (numberContainer.Number != 0)
        //            {
        //                return;
        //            }

        //            for (int num = start; num >= end; num--)
        //            {
        //                System.Diagnostics.Debug.WriteLine($" {debugMessage} {num}");
        //                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
        //                {
        //                    System.Diagnostics.Debug.WriteLine($"{debugMessage} Ended due to matching CaptchaToken!");
        //                    lock (globalCancellationTokenSource)
        //                    {
        //                        if (numberContainer.Number == 0)
        //                        {
        //                            numberContainer.Number = num;
        //                            globalCancellationTokenSource.Cancel();
        //                        }
        //                    }
        //                    return;
        //                }

        //                Task.Delay(10).Wait();

        //                if (globalCancellationToken.IsCancellationRequested)
        //                {
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //}

       
    }
}
