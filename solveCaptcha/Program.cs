// See https://aka.ms/new-console-template for more information
using System;
using System.Threading.Tasks;

namespace ConcurrentTasksExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int answer = 123456; // مقدار ورودی

            // شروع دو تسک به صورت همزمان
            Task<int> decreaseTask = DecreaseNumberAsync(999999, answer);
            Task<int> increaseTask = IncreaseNumberAsync(0, answer);

            // انتظار برای اتمام هر دو تسک
            int decreasedResult = await decreaseTask;
            int increasedResult = await increaseTask;

            Console.WriteLine($"تسک کاهش: {decreasedResult}");
            Console.WriteLine($"تسک افزایش: {increasedResult}");
        }

        static async Task<int> DecreaseNumberAsync(int number, int answer)
        {
            int result = 0;
            while (number > 0)
            {
                int digit = number % 10;
                number /= 10;
                result = result * 10 + (digit - 1);
                await Task.Delay(100); // شبیه‌سازی عملیات طولانی مدت
            }

            if (result == answer)
                Console.WriteLine("تسک کاهش: مقدار مطابق است.");
            else
                Console.WriteLine("تسک کاهش: مقدار مطابق نیست.");

            return result;
        }

        static async Task<int> IncreaseNumberAsync(int number, int answer)
        {
            int result = 0;
            while (number < 999999)
            {
                int digit = number % 10;
                number /= 10;
                result = result * 10 + (digit + 1);
                await Task.Delay(100); // شبیه‌سازی عملیات طولانی مدت
            }

            if (result == answer)
                Console.WriteLine("تسک افزایش: مقدار مطابق است.");
            else
                Console.WriteLine("تسک افزایش: مقدار مطابق نیست.");

            return result;
        }
    }
}

