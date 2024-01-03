using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SixLaborsCaptcha.Core;
using SwitchAPI.DB;
using SwitchAPI.Models.Captcha;
using System.Threading;

namespace SwitchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

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
            var captcha = new CaptchaModels(captchaToken, randomNumber)
            {
                ExpiryTime = DateTime.Now.AddMinutes(2) 
            };

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
        public async Task<ActionResult<object>> GuessCaptcha()
        {
            var numberContainer = new GuessCaptcha();
            numberContainer.Number = 0;

            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            var tasks = new List<Task>();
            tasks.Add(Task.Run(() => RunCountUpAsync(numberContainer, cancellationToken, 100000, 499999/ 2, cancellationTokenSource)));
            tasks.Add(Task.Run(() => RunCountDownAsync(numberContainer, cancellationToken, 499999/2,100000, cancellationTokenSource)));
            tasks.Add(Task.Run(() => RunCountUpAsync(numberContainer, cancellationToken, 499999/2, 499999, cancellationTokenSource)));
            tasks.Add(Task.Run(() => RunCountDownAsync(numberContainer, cancellationToken, 499999, 499999/2, cancellationTokenSource)));
            tasks.Add(Task.Run(() => RunCountUpAsync(numberContainer, cancellationToken, 499999,749998, cancellationTokenSource)));
            tasks.Add(Task.Run(() => RunCountDownAsync(numberContainer, cancellationToken, 749998, 499999, cancellationTokenSource))); 
            tasks.Add(Task.Run(() => RunCountUpAsync(numberContainer, cancellationToken, 749998, 999999, cancellationTokenSource)));
            tasks.Add(Task.Run(() => RunCountDownAsync(numberContainer, cancellationToken, 999999, 749998, cancellationTokenSource)));

            await Task.WhenAny(Task.WhenAll(tasks), Task.Delay(Timeout.Infinite, cancellationToken));

            cancellationTokenSource.Cancel(); // Stop all tasks

            return new { captchaKey = numberContainer.Number };
        }

        private async Task RunCountUpAsync(GuessCaptcha numberContainer, CancellationToken cancellationToken, int start, int end, CancellationTokenSource cancellationTokenSource)
        {
            if (numberContainer.Number != 0)
            {
                return;
            }

            for (int num = start; num <= end; num++)
            {
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
                {
                    lock (numberContainer)
                    {
                        if (numberContainer.Number == 0)
                        {
                            numberContainer.Number = num;
                            cancellationTokenSource.Cancel();
                        }
                    }
                    return;
                }

                await Task.Delay(10, cancellationToken);
            }
        }

        private async Task RunCountDownAsync(GuessCaptcha numberContainer, CancellationToken cancellationToken, int start, int end, CancellationTokenSource cancellationTokenSource)
        {
            if (numberContainer.Number != 0)
            {
                return;
            }

            for (int num = start; num >= end; num--)
            {
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaAnswer, out int token) && token == num))
                {
                    lock (numberContainer)
                    {
                        if (numberContainer.Number == 0)
                        {
                            numberContainer.Number = num;
                            cancellationTokenSource.Cancel();
                        }
                    }
                    return;
                }

                await Task.Delay(10, cancellationToken);
            }
        }

    }
}



