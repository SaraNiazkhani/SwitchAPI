using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MongoDB.Bson;
using MongoDB.Driver;
using SixLaborsCaptcha.Core;
using SwitchAPI.DB;
using SwitchAPI.Models;
using SwitchAPI.Models.Captcha;

namespace SwitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploaderController : ControllerBase
    {
        private IMongoCollection<BsonDocument> _collection;
        private FileExtensionContentTypeProvider _contentTypeProvider;
        private MongoContext _mongoContext;
        private CaptchaGenerator _captchaGenerator;
        public FileUploaderController(MongoContext mongoContext, CaptchaGenerator captchaGenerator)
        {
            _mongoContext = mongoContext;
            _contentTypeProvider = new FileExtensionContentTypeProvider();
            _captchaGenerator = captchaGenerator;
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

        [Route("UploadFile")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] string CaptchaToken, [FromQuery] string CapcthaAnswer)
        {
            var collection = _mongoContext.GetCollection<FilesModel>("files");

            try
            {

                if (file != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();

                        var fileModel = new FilesModel
                        {
                            FileName = file.FileName,
                            FileData = fileBytes
                        };


                        collection.InsertOneAsync(fileModel);

                        return Ok($"File uploaded successfully." +
                            $"FileName:{file.FileName}");
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
