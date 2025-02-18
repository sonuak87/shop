using API.MiddleWare;
using Core.Interface;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddCors();
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var conString = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("Connection string is empty");
    var configOption = ConfigurationOptions.Parse(conString, true);
    return ConnectionMultiplexer.Connect(configOption);
});
builder.Services.AddSingleton<ICartService, CartService>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleWare>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.MapControllers();
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedDataAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
app.Run();
