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
        public async Task<IActionResult> Update(string id, [FromBody] CarsFiltered updatedCar)
        {
            var collection = _mongoContext.GetCollection<CarsFiltered>("Filter");
            try
            {
                var filter = Builders<CarsFiltered>.Filter.Eq(x => x._id, id);

                var existingCar = await collection.Find(filter).FirstOrDefaultAsync();

                if (existingCar == null)
                {
                    return NotFound($"داده‌ای با این شناسه یافت نشد");
                }

                // جایگزین کردن فیلدها با اطلاعات جدید ارسال شده
                if (!string.IsNullOrEmpty(updatedCar.brandname))
                {
                    existingCar.brandname = updatedCar.brandname;
                }

                if (!string.IsNullOrEmpty(updatedCar.modelname))
                {
                    existingCar.modelname = updatedCar.modelname;
                }
                if (updatedCar.year != 0)
                {
                    existingCar.year = updatedCar.year;
                }
                if (updatedCar.price != 0)
                {
                    existingCar.price = updatedCar.price;
                }
                if (updatedCar.usagekilometere != 0)
                {
                    existingCar.usagekilometere = updatedCar.usagekilometere;
                }
                if (!string.IsNullOrEmpty(updatedCar.bodystatus))
                {
                    existingCar.bodystatus = updatedCar.bodystatus;
                }
                if (!string.IsNullOrEmpty(updatedCar.fueltype))
                {
                    existingCar.fueltype = updatedCar.fueltype;
                }
                if (!string.IsNullOrEmpty(updatedCar.gearboxtype))
                {
                    existingCar.gearboxtype = updatedCar.gearboxtype;
                }

                // سایر فیلدها را نیز به همین صورت چک کنید و جایگزین کنید

                var result = await collection.ReplaceOneAsync(filter, existingCar);

                if (result.ModifiedCount > 0)
                {
                    return Ok($"داده با موفقیت به‌روزرسانی شد");
                }
                else
                {
                    return NotFound($"داده‌ای با این شناسه یافت نشد یا هیچ تغییری اعمال نشد");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"خطا در به‌روزرسانی داده: {ex.Message}");
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, [FromBody] CarsFiltered updatedCar)
        //{
        //    var collection = _mongoContext.GetCollection<CarsFiltered>("Filter");
        //    try
        //    {
        //        var filter = Builders<CarsFiltered>.Filter.Eq(x => x._id, id);

        //        var update = Builders<CarsFiltered>.Update
        //            .Set(x => x.brandname, updatedCar.brandname)
        //            .Set(x => x.modelname, updatedCar.modelname)
        //            .Set(x => x.year, updatedCar.year)
        //            .Set(x => x.usagekilometere, updatedCar.usagekilometere)
        //            .Set(x => x.gearboxtype, updatedCar.gearboxtype)
        //            .Set(x => x.bodystatus, updatedCar.bodystatus)
        //            .Set(x => x.price, updatedCar.price)
        //            .Set(x => x.fueltype, updatedCar.fueltype);

        //        var result = await collection.UpdateOneAsync(filter, update);


        //        if (result.ModifiedCount > 0)
        //        {
        //            return Ok($"داده با موفقیت به‌روزرسانی شد");
        //        }
        //        else
        //        {
        //            return NotFound($"داده‌ای با این شناسه  یافت نشد");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"خطا در به‌روزرسانی داده: {ex.Message}");
        //    }
        //}


    }
}