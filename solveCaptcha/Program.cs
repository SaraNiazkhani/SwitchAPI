// See https://aka.ms/new-console-template for more information
using SwitchAPI.Models.Captcha;
using System;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace ConcurrentTasksExample
{
    class Program
    {
        private static CaptchaGenerator _captchaGenerator;
        static async Task Main(string[] args)
        {
            Thread thread1 = new Thread(CountUp);
            Thread thread2 = new Thread(CountDown);
            Thread thread3 = new Thread(CountUpAve);
            Thread thread4 = new Thread(CountDownAve);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

        }
        public static void CountDown()
        {
            for (int number = 999999; number >= 999999 / 2; number--)
            {
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaToken, out int token) && token == number))
                {
                    Console.WriteLine($" Count Down {number}");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Count Down Ended!");
              
            }


        }
        public static void CountUp()
        {
            for (int number = 100000; number <= 999999 / 2; number++)
            {
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaToken, out int token) && token == number))
                {
                    Console.WriteLine($" Count UP {number}");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Count Up Ended!");

            }

        }
        public static void CountDownAve()
        {
            for (int number = 999999 / 2; number >= 100000; number--)
            {
                
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaToken, out int token) && token == number))
                {
                    Console.WriteLine($" Count Down Ave {number}");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Count Down Ave Ended!");
            }


        }
        public static void CountUpAve()
        {
            for (int number = 999999 / 2; number <= 999999; number++)
            {
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaToken, out int token) && token == number))
                {
                    Console.WriteLine($" Count Up Ave {number}");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Count Up Ave Ended!");
              
            }
        }
      
     
    }
}

