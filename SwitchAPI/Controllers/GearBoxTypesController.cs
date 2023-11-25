using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SwitchAPI.DB;
using SwitchAPI.Models;

namespace SwitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GearBoxTypesController : ControllerBase
    {
        private MongoContext _mongoContext;
        public GearBoxTypesController(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }
        [HttpGet]
        public async Task<IEnumerable<Cars>> GetGearboxTypes()
        {
            var collection = _mongoContext.GetCollection<Cars>("GearBoxType");
            var data = await collection.Find(_ => true).ToListAsync();
            return data;
        }
    }
}
