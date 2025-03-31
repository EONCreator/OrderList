using MongoDB.Driver;
using Newtonsoft.Json;
using OrderList.Application.Interfaces;
using OrderList.Application.Orders;
using OrderList.Infrastructure.Database;
using OrderList.Infrastructure.Database.Filters;
using OrderList.Services.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

builder.Services.AddScoped<AppDbContextFactory>();
builder.Services.AddScoped<AppDbContext>(provider =>
{
    var factory = provider.GetRequiredService<AppDbContextFactory>();
    return factory.Create();
});

builder.Services.AddScoped<FilterMap>();
builder.Services.AddScoped<FilterBuilder>();

builder.Services.AddScoped<SortBuilder>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "http://localhost:2301")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();