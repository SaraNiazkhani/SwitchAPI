using SixLaborsCaptcha.Mvc.Core;
using SwitchAPI.DB;
using SwitchAPI.Models.Captcha;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new MongoContext("mongodb://127.0.0.1:27017", "Switch"));
builder.Services.AddSingleton<CaptchaGenerator>();
builder.Services.AddSixLabCaptcha(x =>
{
    x.NoiseRate = 12;
    x.DrawLines = 3;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
