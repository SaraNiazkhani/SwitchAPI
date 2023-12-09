//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SixLaborsCaptcha.Core;
//using SwitchAPI.DB;
//using SwitchAPI.Models.Captcha;

//namespace SwitchAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]

//    public class SolveCaptchaController : ControllerBase
//    {

//        private CaptchaGenerator _captchaGenerator;
//        public CaptchaService _captchaService { get; set; }
//        public SolveCaptchaController(CaptchaGenerator captchaGenerator)
//        {
//            _captchaGenerator = captchaGenerator;
//        }

//        [Route("GenerateCaptcha")]
//        [HttpGet]
//        public ActionResult GenerateCaptcha()
//        {
//            _captchaService.Captchas.Add(DateTime.Now.Ticks % 10000000);
//            return Ok();
//        }
//        [Route("SolveCaptcha")]
//        [HttpGet]
//        public async Task<IActionResult> SolveCaptcha()
//        {


//            // اجرای همزمان دو فانکشن
           
//            // انتظار برای اتمام هر دو تسک
//            await Task.WhenAll(task1, task2);
//            return Ok("هر دو فانکشن با موفقیت اجرا شدند.");
//        }
//        public static void CountDown()
//        {
//            var captcha = _captchaGenerator.Captchas.FirstOrDefault(c => c.CaptchaToken == CaptchaToken);
//            for (int i = 10; i >= 0; i--)
//            {
//                return ($"Timer Count Down {i}");
//                Thread.Sleep(1000);
//            }
//            Console.WriteLine("Count Down Ended!");
//        }

//        private async Task FunctionOne()
//        {
//            var captcha = _captchaGenerator.Captchas.FirstOrDefault(c => c.CaptchaToken == CaptchaToken);
//            try
//            {
//                for (int number = 999999; number >= 999999 / 2; number--)
//                {
//                    if (CapcthaAnswer == captcha.CaptchaAnswer)
//                    {


//                        Console.WriteLine($"عدد کاهش یافته: {number}");
//                        return Ok("عدد کاهش یافته با موفقیت درست است.");
//                    }
//                    else
//                    {
//                        return BadRequest("کد CAPTCHA اشتباه است.");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"خطا در اجرای کار: {ex.Message}");
//            }
//        }
//        private async Task FunctionTwo()
//        {
//            var captcha = _captchaGenerator.Captchas.FirstOrDefault(c => c.CaptchaToken == CaptchaToken);
//            try
//            {
//                for (int number = 100000; number <= 999999 / 2; number++)
//                {
//                    if (CapcthaAnswer == captcha.CaptchaAnswer)
//                    {


//                        Console.WriteLine($"عدد افزایش یافته: {number}");
//                        return Ok("عدد افزایش یافته با موفقیت درست است.");
//                    }
//                    else
//                    {
//                        return BadRequest("کد CAPTCHA اشتباه است.");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"خطا در اجرای کار: {ex.Message}");
//            }
//        }

//        [Route("GuessCaptcha")]
//        [HttpPost]
//        public ActionResult GuessCaptcha()
//        {
//            Task task1 = FunctionOne();
//            Task task2 = FunctionTwo();
//            Task.WhenAll(task1, task2);
            
//            return Ok("هر دو فانکشن با موفقیت اجرا شدند.");
//        }




//        //TODO: inject captcha service












































//        //private CaptchaGenerator _captchaGenerator;
//        //public SolveCaptchaController(CaptchaGenerator captchaGenerator)
//        //{
//        //    _captchaGenerator = captchaGenerator;

//        //}

//        //[Route("SolveCaptcha")]
//        //[HttpGet]
//        //public async Task<IActionResult> SolveCaptcha()
//        //{


//        //    // اجرای همزمان دو فانکشن
//        //    Task task1 = FunctionOne();
//        //    Task task2 = FunctionTwo();

//        //    // انتظار برای اتمام هر دو تسک
//        //    await Task.WhenAll(task1, task2);
//        //    return Ok("هر دو فانکشن با موفقیت اجرا شدند.");
//        //}
//        //private async Task FunctionOne()
//        //{
//        //    var captcha = _captchaGenerator.Captchas.FirstOrDefault(c => c.CaptchaToken == CaptchaToken);
//        //    try
//        //    {
//        //        for(int number = 999999; number >=999999 / 2; number--)
//        //        {
//        //            if (CapcthaAnswer == captcha.CaptchaAnswer)
//        //            {


//        //                Console.WriteLine($"عدد کاهش یافته: {number}");
//        //                return Ok("عدد کاهش یافته با موفقیت درست است.");
//        //            }
//        //            else
//        //            {
//        //                return BadRequest("کد CAPTCHA اشتباه است.");
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return StatusCode(500, $"خطا در اجرای کار: {ex.Message}");
//        //    }
//        //}
//        //private async Task FunctionTwo()
//        //{
//        //    var captcha = _captchaGenerator.Captchas.FirstOrDefault(c => c.CaptchaToken == CaptchaToken);
//        //    try
//        //    {
//        //        for (int number = 100000; number<= 999999 / 2; number++)
//        //        {
//        //            if (CapcthaAnswer == captcha.CaptchaAnswer)
//        //            {


//        //                Console.WriteLine($"عدد افزایش یافته: {number}");
//        //                return Ok("عدد افزایش یافته با موفقیت درست است.");
//        //            }
//        //            else
//        //            {
//        //                return BadRequest("کد CAPTCHA اشتباه است.");
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return StatusCode(500, $"خطا در اجرای کار: {ex.Message}");
//        //    }
//        //}

//    }
//}
