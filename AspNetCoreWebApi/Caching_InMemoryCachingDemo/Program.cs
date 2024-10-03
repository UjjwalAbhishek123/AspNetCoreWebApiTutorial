using Caching_InMemoryCachingDemo.Repositories;
using Caching_InMemoryCachingDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register In-Memory Cache
builder.Services.AddMemoryCache();

//registering IProductRepository, ProductRepository to DI container
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

//registering IProductService, ProductService to DI container
builder.Services.AddSingleton<IProductService, ProductService>();

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
