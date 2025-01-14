using API.MiddleWare;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleWare>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));
app.MapControllers();

app.Run();
