using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SixLaborsCaptcha.Core;
using SwitchAPI.DB;
using SwitchAPI.Models.Captcha;

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



        [Route("GuessCaptcha")]
        [HttpGet]
        public ActionResult<object> GuessCaptcha()
        {
            int number = 0;
            CancellationTokenSource globalCancellationTokenSource = new CancellationTokenSource();

            CancellationToken globalCancellationToken = globalCancellationTokenSource.Token;

            Thread thread1 = new Thread(() => CountUp(ref number, globalCancellationToken, globalCancellationTokenSource));
            Thread thread2 = new Thread(() => CountDown(ref number, globalCancellationToken, globalCancellationTokenSource));
            Thread thread3 = new Thread(() => CountUpAve(ref number, globalCancellationToken, globalCancellationTokenSource));
            Thread thread4 = new Thread(() => CountDownAve(ref number, globalCancellationToken, globalCancellationTokenSource));
            Thread thread5 = new Thread(() => CountUpAver(ref number, globalCancellationToken, globalCancellationTokenSource));
            Thread thread6 = new Thread(() => CountDownAver(ref number, globalCancellationToken, globalCancellationTokenSource));
            Thread thread7 = new Thread(() => UpAve(ref number, globalCancellationToken, globalCancellationTokenSource));
            Thread thread8 = new Thread(() => DownAve(ref number, globalCancellationToken, globalCancellationTokenSource));


            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();
            thread6.Start();
            thread7.Start();
            thread8.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
            thread5.Join();
            thread6.Join();
            thread7.Join();
            thread8.Join();



            globalCancellationTokenSource.Cancel(); // متوقف کردن تمام تردها

            return new { captchaKey = number };
        }
        public static void CountUp(ref int number, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            CountUpInRange(ref number, 100000, 999999 / 2, "Count Up", globalCancellationToken, globalCancellationTokenSource);
        }
        public static void CountUpAve(ref int number, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            CountUpInRange(ref number, 999999 / 2, 999999, "Count Up Ave", globalCancellationToken, globalCancellationTokenSource);
        }
        public static void CountDown(ref int number, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            CountDownInRange(ref number, 999999, 999999 / 2, "Count Down", globalCancellationToken, globalCancellationTokenSource);
        }
        public static void CountDownAve(ref int number, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            CountDownInRange(ref number, 999999 / 2, 100000, "Count Down Ave", globalCancellationToken, globalCancellationTokenSource);
        }

        public static void CountDownAver(ref int number, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            CountDownInRange(ref number, 499999 / 2, 100000, "Count Down Ave+", globalCancellationToken, globalCancellationTokenSource);
        }
        public static void CountUpAver(ref int number, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            CountUpInRange(ref number, 499999 / 2, 999999 / 2, "Count Up Ave+", globalCancellationToken, globalCancellationTokenSource);
        }
        public static void UpAve(ref int number, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            CountUpInRange(ref number, 749998, 999999, "Count Up Ave++", globalCancellationToken, globalCancellationTokenSource);
        }
        public static void DownAve(ref int number, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            CountDownInRange(ref number, 749998, 999999 / 2, "Count Down Ave++", globalCancellationToken, globalCancellationTokenSource);
        }


        public static void CountUpInRange(ref int number, int start, int end, string debugMessage, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            for (int num = start; num <= end; num++)
            {
                System.Diagnostics.Debug.WriteLine($" {debugMessage} {num}");
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
                {
                    System.Diagnostics.Debug.WriteLine($"{debugMessage} Ended due to matching CaptchaToken!");
                    globalCancellationTokenSource.Cancel();
                    return;
                }

                Task.Delay(10).Wait();

                if (globalCancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        public static void CountDownInRange(ref int number, int start, int end, string debugMessage, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource)
        {
            for (int num = start; num >= end; num--)
            {
                System.Diagnostics.Debug.WriteLine($" {debugMessage} {num}");
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
                {
                    System.Diagnostics.Debug.WriteLine($"{debugMessage} Ended due to matching CaptchaToken!");
                    globalCancellationTokenSource.Cancel();
                    return;
                }

                Task.Delay(10).Wait();

                if (globalCancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}























//**********************************************************************************

//public ActionResult<object> GuessCaptcha()
//{
//    int number = 0;
//    bool answerFound = false;
//    object lockObject = new object();
//    CancellationTokenSource cts = new CancellationTokenSource();
//    //CancellationTokenSource cts1 = new CancellationTokenSource();
//    //CancellationTokenSource cts2 = new CancellationTokenSource();
//    //CancellationTokenSource cts3 = new CancellationTokenSource();
//    //CancellationTokenSource cts4 = new CancellationTokenSource();
//    Thread thread1 = new Thread(() => CountUp(ref number, cts, ref answerFound, lockObject));
//    Thread thread2 = new Thread(() => CountDown(ref number, cts, ref answerFound, lockObject));
//    Thread thread3 = new Thread(() => CountUpAve(ref number, cts, ref answerFound, lockObject));
//    Thread thread4 = new Thread(() => CountDownAve(ref number, cts, ref answerFound, lockObject));
//    Thread thread5 = new Thread(() => CountUpAver(ref number, cts, ref answerFound, lockObject));
//    Thread thread6 = new Thread(() => CountDownAver(ref number, cts, ref answerFound, lockObject));
//    Thread thread7 = new Thread(() => UpAve(ref number, cts, ref answerFound, lockObject));
//    Thread thread8 = new Thread(() => DownAve(ref number, cts, ref answerFound, lockObject));
//    thread1.Start();
//    thread2.Start();
//    thread3.Start();
//    thread4.Start();
//    thread5.Start();
//    thread6.Start();
//    thread7.Start();
//    thread8.Start();

//    thread1.Join();
//    thread2.Join();
//    thread3.Join();
//    thread4.Join();
//    thread5.Join();
//    thread6.Join();
//    thread7.Join();
//    thread8.Join();
//    return new { captchaKey = number };
//}

//public static void CountUp(ref int number, CancellationTokenSource cancellationTokenSource, ref bool answerFound, object lockObject)
//{
//    CountUpInRange(ref number, cancellationTokenSource, 100000, 999999 / 2, "Count Up", ref answerFound, lockObject);
//}
//public static void CountDown(ref int number, CancellationTokenSource cancellationTokenSource, ref bool answerFound, object lockObject)
//{
//    CountDownInRange(ref number, cancellationTokenSource, 999999, 999999 / 2, "Count Down", ref answerFound, lockObject);
//}
//public static void CountDownAve(ref int number, CancellationTokenSource cancellationTokenSource, ref bool answerFound, object lockObject)
//{
//    CountDownInRange(ref number, cancellationTokenSource, 999999 / 2, 100000, "Count Down Ave", ref answerFound, lockObject);
//}

//public static void CountDownAver(ref int number, CancellationTokenSource cancellationTokenSource, ref bool answerFound, object lockObject)
//{
//    CountDownInRange(ref number, cancellationTokenSource, 499999 / 2, 100000, "Count Down Ave+", ref answerFound, lockObject);
//}
//public static void CountUpAver(ref int number, CancellationTokenSource cancellationTokenSource, ref bool answerFound, object lockObject)
//{
//    CountUpInRange(ref number, cancellationTokenSource, 499999 / 2, 999999 / 2, "Count Up Ave+", ref answerFound, lockObject);
//}
//public static void UpAve(ref int number, CancellationTokenSource cancellationTokenSource, ref bool answerFound, object lockObject)
//{
//    CountUpInRange(ref number, cancellationTokenSource, 749998, 999999, "Count Up Ave++", ref answerFound, lockObject);
//}
//public static void DownAve(ref int number, CancellationTokenSource cancellationTokenSource, ref bool answerFound, object lockObject)
//{
//    CountDownInRange(ref number, cancellationTokenSource, 749998, 999999 / 2, "Count Down Ave++", ref answerFound, lockObject);
//}
//*******************************************************************************
//***************************************************************************************************
//public static void CountUpInRange(ref int number, CancellationTokenSource cancellationTokenSource, int start, int end, string debugMessage, ref bool answerFound, object lockObject)
//{
//    for (int num = start; num <= end; num++)
//    {
//        System.Diagnostics.Debug.WriteLine($" {debugMessage} {num}");
//        if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
//        {
//            lock (lockObject)
//            {
//                if (!answerFound)
//                {
//                    System.Diagnostics.Debug.WriteLine($"{debugMessage} Ended due to matching CaptchaToken!");
//                    cancellationTokenSource.Cancel();
//                    answerFound = true;
//                    return;
//                }
//            }
//        }

//        Task.Delay(10).Wait();
//    }
//}
//************************************************************************************************
//        public static void CountDownInRange(ref int number, CancellationTokenSource cancellationTokenSource, int start, int end, string debugMessage, ref bool answerFound, object lockObject)
//        {
//            for (int num = start; num >= end; num--)
//            {
//                System.Diagnostics.Debug.WriteLine($" {debugMessage} {num}");
//                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
//                {
//                    lock (lockObject)
//                    {
//                        if (!answerFound)
//                        {
//                            System.Diagnostics.Debug.WriteLine($"{debugMessage} Ended due to matching CaptchaToken!");
//                            cancellationTokenSource.Cancel();
//                            answerFound = true;
//                            return;
//                        }
//                    }
//                }

//                Task.Delay(10).Wait();
//            }
//        }
//    }

//}