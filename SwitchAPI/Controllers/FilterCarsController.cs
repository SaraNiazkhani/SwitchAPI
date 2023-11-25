using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SwitchAPI.DB;
using SwitchAPI.Models;

namespace SwitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterCarsController : ControllerBase
    {
        private IMongoCollection<CarsFiltered> _filterCollection;
        private MongoContext _mongoContext;
        public FilterCarsController(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }
        [HttpPost]

        public IEnumerable<CarsFiltered> PostFilter(FilterModel filter)
        {
            var collection = _mongoContext.GetCollection<CarsFiltered>("Filter");
            var filterBuilder = Builders<CarsFiltered>.Filter;
            if (filter.IsEmpty())
            {
                var data = collection.Find(c => true).ToList();
                return data;
            }
            else
            {
                var filterDefinition = filterBuilder.Ne(c => c._id, null);
                if (!string.IsNullOrEmpty(filter.brandname))
                    filterDefinition &= filterBuilder.Eq(c => c.brandname, filter.brandname);
                if (!string.IsNullOrEmpty(filter.modelname))
                    filterDefinition &= filterBuilder.Eq(c => c.modelname, filter.modelname);
                if (!string.IsNullOrEmpty(filter.gearboxtype))
                    filterDefinition &= filterBuilder.Eq(c => c.gearboxtype, filter.gearboxtype);
                if (!string.IsNullOrEmpty(filter.fueltype))
                    filterDefinition &= filterBuilder.Eq(c => c.fueltype, filter.fueltype);
                if (!string.IsNullOrEmpty(filter.bodystatus))
                    filterDefinition &= filterBuilder.Eq(c => c.bodystatus, filter.bodystatus);

                var result = collection.Find(filterDefinition).ToList();
                return result;
            }
        }
    }
}
