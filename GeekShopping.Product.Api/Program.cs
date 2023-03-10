using GeekShopping.Product.Api.Config;
using GeekShopping.Product.Api.Grpc;
using GeekShopping.Product.Api.Models.Context;
using GeekShopping.Product.Api.Repository;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var isDockerEnvironment = builder.Environment.EnvironmentName == "Docker"; 

if (isDockerEnvironment)
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(builder.Configuration.GetValue<int>("DEFAULT_PORT"));
        options.ListenAnyIP(builder.Configuration.GetValue<int>("GRPC_PORT"), listenOptions =>
        {
            listenOptions.Protocols = HttpProtocols.Http2;
        });
    });
}

// Add services to the container.
builder.Services.AddDbContext<MySqlContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var mapper = MappingConfig.RegisterMappings().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();

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
app.MapGrpcService<ProductGrpcService>();

app.Run();
