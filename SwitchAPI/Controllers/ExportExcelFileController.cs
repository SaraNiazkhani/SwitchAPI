using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SwitchAPI.DB;
using SwitchAPI.Models;

namespace SwitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportExcelFileController : ControllerBase
    {
        private MongoContext _mongoContext;
        public ExportExcelFileController(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }
      
        [HttpPost]
        public IActionResult ExportBrandsToExcel(FilterModel filter)
        {
            var collection = _mongoContext.GetCollection<CarsFiltered>("Filter");
            var data = collection.Find(c => true).ToList();

            var mappedData = data.Select(item => new FilterModel
            {
                brandname = item.brandname,
                modelname = item.modelname,
                year = item.year,
                usagekilometere = item.usagekilometere,
                gearboxtype = item.gearboxtype,
                bodystatus = item.bodystatus,
                price = item.price,
                fueltype = item.fueltype
            }).ToList();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Filter");

            // ستون‌ها را تعریف می‌کنیم
            worksheet.Cell(1, 1).Value = "brandname";
            worksheet.Cell(1, 2).Value = "modelname";
            worksheet.Cell(1, 3).Value = "year";
            worksheet.Cell(1, 4).Value = "usagekilometere";
            worksheet.Cell(1, 5).Value = "gearboxtype";
            worksheet.Cell(1, 6).Value = "bodystatus";
            worksheet.Cell(1, 7).Value = "price";
            worksheet.Cell(1, 8).Value = "fueltype";

            // اضافه کردن داده‌ها به ستون‌ها
            int row = 2;
            foreach (var item in mappedData)
            {
                worksheet.Cell(row, 1).Value = item.brandname;
                worksheet.Cell(row, 2).Value = item.modelname;
                worksheet.Cell(row, 3).Value = item.year;
                worksheet.Cell(row, 4).Value = item.usagekilometere;
                worksheet.Cell(row, 5).Value = item.gearboxtype;
                worksheet.Cell(row, 6).Value = item.bodystatus;
                worksheet.Cell(row, 7).Value = item.price;
                worksheet.Cell(row, 8).Value = item.fueltype;
                row++;
            }

            // ذخیره فایل Excel
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cars.xlsx");
            }
        }

    }
}
    
