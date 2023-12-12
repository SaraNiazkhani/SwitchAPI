using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MongoDB.Bson;
using MongoDB.Driver;
using SixLaborsCaptcha.Core;
using SwitchAPI.DB;
using SwitchAPI.Models;
using SwitchAPI.Models.Captcha;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace SwitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploaderController : ControllerBase
    {
        private IMongoCollection<BsonDocument> _collection;
        private FileExtensionContentTypeProvider _contentTypeProvider;
        private MongoContext _mongoContext;
        private static CaptchaGenerator _captchaGenerator;
        public FileUploaderController(MongoContext mongoContext, CaptchaGenerator captchaGenerator)
        {
            _mongoContext = mongoContext;
            _contentTypeProvider = new FileExtensionContentTypeProvider();
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
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Thread thread1 = new Thread(() => CountUp(ref number, cancellationTokenSource));
            Thread thread2 = new Thread(() => CountDown(ref number, cancellationTokenSource));
            Thread thread3 = new Thread(() => CountUpAve(ref number, cancellationTokenSource));
            Thread thread4 = new Thread(() => CountDownAve(ref number, cancellationTokenSource));
            //Thread thread1 = new Thread(CountUp);
            //Thread thread2 = new Thread(CountDown);
            //Thread thread3 = new Thread(CountUpAve);
            //Thread thread4 = new Thread(CountDownAve);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();

            return new { captchaKey=  number};
        }
        public static void CountDown(ref int number, CancellationTokenSource cancellationTokenSource)
        {
            for (int num = 999999; num >= 999999 / 2; num--)
            {
                System.Diagnostics.Debug.WriteLine($" Count Down {num}");
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaToken, out int token) && token == num))
                {
                    System.Diagnostics.Debug.WriteLine("Count Down Ended due to matching CaptchaToken!");
                    cancellationTokenSource.Cancel();
                    break;
                }

                Task.Delay(10).Wait();
            }


        }
        public static void CountUp(ref int number, CancellationTokenSource cancellationTokenSource)
        {
            for (int num = 100000; num <= 999999 / 2; num++)
            {
                System.Diagnostics.Debug.WriteLine($" Count Up {num}");
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaToken, out int token) && token == num))
                {
                    System.Diagnostics.Debug.WriteLine("Count Up Ended due to matching CaptchaToken!");
                    cancellationTokenSource.Cancel();
                    break;
                }

                Task.Delay(10).Wait();
            }

        }
        public static void CountDownAve(ref int number, CancellationTokenSource cancellationTokenSource)
        {
            for (int num = 999999 / 2; num >= 100000; num--)
            {
                System.Diagnostics.Debug.WriteLine($" Count Down Ave {num   }");
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaToken, out int token) && token == num))
                {
                    System.Diagnostics.Debug.WriteLine("Count Down Ave Ended due to matching CaptchaToken!");
                    cancellationTokenSource.Cancel();
                    break;
                }

                Task.Delay(10).Wait();
            }


        }
        public static void CountUpAve(ref int number, CancellationTokenSource cancellationTokenSource)
        {
            for (int num = 999999 / 2; num <= 999999; num++)
            {
                System.Diagnostics.Debug.WriteLine($" Count Up Ave {num}");
                if (_captchaGenerator.Captchas.Any(captcha => int.TryParse(captcha.CaptchaToken, out int token) && token == num))
                {
                    System.Diagnostics.Debug.WriteLine("Count Up Ave Ended due to matching CaptchaToken!");
                    cancellationTokenSource.Cancel();
                    break;
                }

                Task.Delay(10).Wait();
            }
        }
        [Route("UploadFile")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] string CaptchaToken, [FromQuery] string CapcthaAnswer)
        {
            var captcha = _captchaGenerator.Captchas.FirstOrDefault(c => c.CaptchaToken == CaptchaToken);
            if (captcha != null && CapcthaAnswer == captcha.CaptchaAnswer)
            {
                var collection = _mongoContext.GetCollection<FilesModel>("files");

                try
                {
                    if (file != null && file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            file.CopyTo(memoryStream);
                            byte[] fileBytes = memoryStream.ToArray();

                            var fileModel = new FilesModel
                            {
                                FileName = file.FileName,
                                FileData = fileBytes
                            };

                            collection.InsertOne(fileModel);

                            return Ok($"File uploaded successfully. FileName: {file.FileName}");
                        }
                    }
                    else
                    {
                        return BadRequest("File is empty or null.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            }
            else
            {
                // مطابقت کپچا با تصویر نادرست است
                return BadRequest("Invalid Captcha. Please try again.");
            }
        }

        [Route("DownloadFile")]
        [HttpGet]
        public async Task<IActionResult> DownloadFile(string fileId)
        {
            var collection = _mongoContext.GetCollection<FilesModel>("files");

            try
            {
                var objectId = ObjectId.Parse(fileId);
                var filter = Builders<FilesModel>.Filter.Eq("_id", objectId);
                var fileModel = await collection.Find(filter).FirstOrDefaultAsync();

                if (fileModel == null)
                {
                    return NotFound("File not found.");
                }
                var fileBytes = fileModel.FileData;

                string mimeType;

                new FileExtensionContentTypeProvider().TryGetContentType(fileModel.FileName, out mimeType);
                return File(fileBytes, mimeType ?? "application/octet-stream", fileModel.FileName);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
