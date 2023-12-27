using Microsoft.AspNetCore.Mvc;
using SwitchAPI.Models.Captcha;

namespace SwitchAPI.Models
{
    public class Comment
    {  //    [Route("GuessCaptcha")]
        //    [HttpGet]
        //    public async Task<ActionResult<object>> GuessCaptchaAsync()
        //    {
        //        var numberContainer = new GuessCaptcha();
        //        numberContainer.Number = 0;

        //        var globalCancellationTokenSource = new CancellationTokenSource();
        //        var globalCancellationToken = globalCancellationTokenSource.Token;

        //        List<Task> tasks = new List<Task>();

        //        tasks.Add(CountUpInRangeAsync(numberContainer, globalCancellationToken, globalCancellationTokenSource, 100000, 999999 / 2, "Count Up"));
        //        tasks.Add(CountDownInRangeAsync(numberContainer, globalCancellationToken, globalCancellationTokenSource, 999999, 999999 / 2, "Count Down"));
        //        // Add other tasks as needed...

        //        await Task.WhenAny(tasks); // Wait for any of the tasks to complete

        //        globalCancellationTokenSource.Cancel(); // Cancel all tasks

        //        return new { captchaKey = numberContainer.Number };
        //    }

        //    public async Task CountUpInRangeAsync(GuessCaptcha numberContainer, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
        //    {
        //        if (numberContainer.Number != 0)
        //        {
        //            return;
        //        }

        //        for (int num = start; num <= end; num++)
        //        {
        //            System.Diagnostics.Debug.WriteLine($" {debugMessage} {num}");
        //            if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
        //            {
        //                System.Diagnostics.Debug.WriteLine($"{debugMessage} Ended due to matching CaptchaToken!");
        //                lock (globalCancellationTokenSource)
        //                {
        //                    if (numberContainer.Number == 0)
        //                    {
        //                        numberContainer.Number = num;
        //                        globalCancellationTokenSource.Cancel();
        //                    }
        //                }
        //                return;
        //            }

        //            await Task.Delay(10);

        //            if (globalCancellationToken.IsCancellationRequested)
        //            {
        //                return;
        //            }
        //        }
        //    }

        //    public async Task CountDownInRangeAsync(GuessCaptcha numberContainer, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
        //    {
        //        if (numberContainer.Number != 0)
        //        {
        //            return;
        //        }

        //        for (int num = start; num >= end; num--)
        //        {
        //            System.Diagnostics.Debug.WriteLine($" {debugMessage} {num}");
        //            if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
        //            {
        //                System.Diagnostics.Debug.WriteLine($"{debugMessage} Ended due to matching CaptchaToken!");
        //                lock (globalCancellationTokenSource)
        //                {
        //                    if (numberContainer.Number == 0)
        //                    {
        //                        numberContainer.Number = num;
        //                        globalCancellationTokenSource.Cancel();
        //                    }
        //                }
        //                return;
        //            }

        //            await Task.Delay(10);

        //            if (globalCancellationToken.IsCancellationRequested)
        //            {
        //                return;
        //            }
        //        }
        //    }
        //}











//        [Route("GuessCaptcha")]
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






        //        [Route("GuessCaptcha")]
        //        [HttpGet]
        //        public async Task<ActionResult<object>> GuessCaptcha()
        //        {
        //            var numberContainer = new GuessCaptcha();
        //            numberContainer.Number = 0;

        //            List<Task> tasks = new List<Task>();

        //            tasks.Add(CountUpAsync(numberContainer));
        //            tasks.Add(CountDownAsync(numberContainer));

        //            // انتظار اجرای تمام تسک‌ها
        //            await Task.WhenAny(Task.WhenAll(tasks), Task.Delay(Timeout.Infinite));

        //            return new { captchaKey = numberContainer.Number };
        //        }

        //        public async Task CountUpAsync(GuessCaptcha numberContainer)
        //        {
        //            if (numberContainer.Number != 0)
        //            {
        //                return;
        //            }

        //            for (int num = 100000; num <= 999999 / 2; num++)
        //            {
        //                if (await CheckCaptchaAsync(num, numberContainer))
        //                {
        //                    return;
        //                }

        //                await Task.Delay(10);
        //            }
        //        }

        //        public async Task CountDownAsync(GuessCaptcha numberContainer)
        //        {
        //            if (numberContainer.Number != 0)
        //            {
        //                return;
        //            }

        //            for (int num = 999999; num >= 999999 / 2; num--)
        //            {
        //                if (await CheckCaptchaAsync(num, numberContainer))
        //                {
        //                    return;
        //                }

        //                await Task.Delay(10);
        //            }
        //        }

        //        public async Task<bool> CheckCaptchaAsync(int num, GuessCaptcha numberContainer)
        //        {
        //            if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
        //            {
        //                lock (numberContainer)
        //                {
        //                    if (numberContainer.Number == 0)
        //                    {
        //                        numberContainer.Number = num;
        //                    }
        //                }
        //                return true;
        //            }

        //            await Task.Delay(10);

        //            return false;
        //        }
        //    }
        //}


        //        public async Task <object> GuessCaptchaAsync()
        //        {
        //            var globalCancellationTokenSource = new CancellationTokenSource();

        //            // ایجاد یک نمونه جدید از کلاس GuessCaptcha
        //            var numberContainer = new GuessCaptcha();

        //            // ایجاد یک نمونه جدید از CancellationTokenSource
        //            var globalCancellationToken = globalCancellationTokenSource.Token;

        //            await CountUpInRangeAsync(numberContainer, globalCancellationTokenSource, 100000, 999999 / 2, "Count Up");
        //            await CountDownInRangeAsync(numberContainer, globalCancellationTokenSource, 999999, 999999 / 2, "Count Down");

        //            globalCancellationTokenSource.Cancel(); // Cancel all tasks

        //            return new { captchaKey = numberContainer.Number };
        //        }

        //        public async Task CountUpInRangeAsync(GuessCaptcha numberContainer, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
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

        //                await Task.Delay(10);

        //                if (globalCancellationTokenSource.Token.IsCancellationRequested)
        //                {
        //                    return;
        //                }
        //            }
        //        }

        //        public async Task CountDownInRangeAsync(GuessCaptcha numberContainer, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
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

        //                await Task.Delay(10);

        //                if (globalCancellationTokenSource.Token.IsCancellationRequested)
        //                {
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //}

        //    [Route("GuessCaptcha")]
        //        [HttpGet]
        //        public async Task<ActionResult<object>> GuessCaptchaAsync()
        //        {
        //            var numberContainer = new GuessCaptcha();
        //            numberContainer.Number = 0;

        //            var globalCancellationTokenSource = new CancellationTokenSource();
        //            var globalCancellationToken = globalCancellationTokenSource.Token;

        //            List<Task> tasks = new List<Task>();

        //            tasks.Add(CountUpInRangeAsync(numberContainer, globalCancellationToken, globalCancellationTokenSource, 100000, 999999 / 2, "Count Up"));
        //            tasks.Add(CountDownInRangeAsync(numberContainer, globalCancellationToken, globalCancellationTokenSource, 999999, 999999 / 2, "Count Down"));
        //            // Add other tasks as needed...

        //            await Task.WhenAny(tasks); // Wait for any of the tasks to complete

        //            globalCancellationTokenSource.Cancel(); // Cancel all tasks

        //            return new { captchaKey = numberContainer.Number };
        //        }

        //        public async Task CountUpInRangeAsync(GuessCaptcha numberContainer, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
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

        //                await Task.Delay(10);

        //                if (globalCancellationToken.IsCancellationRequested)
        //                {
        //                    return;
        //                }
        //            }
        //        }

        //        public async Task CountDownInRangeAsync(GuessCaptcha numberContainer, CancellationToken globalCancellationToken, CancellationTokenSource globalCancellationTokenSource, int start, int end, string debugMessage)
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

        //                await Task.Delay(10);

        //                if (globalCancellationToken.IsCancellationRequested)
        //                {
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //}
        //********************************************************
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
