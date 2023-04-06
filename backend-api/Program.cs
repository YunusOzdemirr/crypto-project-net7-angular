using crypto_api.Hubs;
using crypto_api.Interfaces;
using crypto_api.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBinanceService, BinanceService>();

builder.Services.AddSignalR(options => { options.EnableDetailedErrors = true; });
builder.Services.AddSingleton<BinanceHub>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.Services.GetService<BinanceHub>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());


app.UseHsts();
app.UseRouting();
app.UseWebSockets();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(
//         builder.Environment.ContentRootPath + "/wwwroot" + "/Uploads"),
//     //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads")),
//     RequestPath = new PathString("/Uploads"),
// });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints => { endpoints.MapHub<BinanceHub>("/binance"); });
app.Run();