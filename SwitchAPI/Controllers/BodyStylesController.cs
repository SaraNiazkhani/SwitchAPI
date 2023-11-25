using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SwitchAPI.DB;
using SwitchAPI.Models;

namespace SwitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyStylesController : ControllerBase
    {
        private MongoContext _mongoContext;
        public BodyStylesController(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }
        [HttpGet]
        public async Task<IEnumerable<Cars>> GetBrands()
        {
            var collection = _mongoContext.GetCollection<Cars>("BodyStyle");
            var data = await collection.Find(_ => true).ToListAsync();
            return data;
        }
    }
}
