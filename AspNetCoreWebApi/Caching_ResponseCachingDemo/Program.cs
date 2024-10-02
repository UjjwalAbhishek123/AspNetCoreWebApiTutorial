using Caching_ResponseCachingDemo.Repositories;
using Caching_ResponseCachingDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registering Response Caching
builder.Services.AddResponseCaching();

//Registering ProductService and IProductService
builder.Services.AddSingleton<IProductService, ProductService>();

//Registering ProductRepository and IProductRepository
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//enable response caching middleware
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
