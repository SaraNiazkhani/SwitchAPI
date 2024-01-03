using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SwitchAPI.DB;
using SwitchAPI.Models;
using System;

namespace SwitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private MongoContext _mongoContext;
        public BrandsController(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }
        [HttpGet]
        public async Task<IEnumerable<Cars>> GetBrands()
        {
            var collection = _mongoContext.GetCollection<Cars>("Brands");
            var data = await collection.Find(_ => true).ToListAsync();
            return data;
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var collection = _mongoContext.GetCollection<Cars>("Brands"); 
            try
            {
               
                var deleteFilter = Builders<Cars>.Filter.Eq(x => x._id, id);
                var result = await collection.DeleteOneAsync(deleteFilter); 
                if (result.DeletedCount > 0)
                {
                    return Ok($"خودرو بااین  شناسه با موفقیت حذف شد");
                }
                else
                {
                    return NotFound($"خودرو با موفقیت حذف شد");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"خطا در حذف خودرو: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Cars updatedCar)
        {
            var collection = _mongoContext.GetCollection<Cars>("Brands");
            try
            {
                var filter = Builders<Cars>.Filter.Eq(x => x._id, id);

                var update = Builders<Cars>.Update
                    .Set(x => x.title, updatedCar.title);

                var result = await collection.UpdateOneAsync(filter, update);


                if (result.ModifiedCount > 0)
                {
                    return Ok($"داده با موفقیت به‌روزرسانی شد");
                }
                else
                {
                    return NotFound($"داده‌ای با این شناسه  یافت نشد");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"خطا در به‌روزرسانی داده: {ex.Message}");
            }
        }


    }
}