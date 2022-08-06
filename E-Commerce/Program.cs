using AutoMapper;
using ECommerce;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>().ReverseMap();
                cfg.CreateMap<Category,CategoryViewModel>().ReverseMap();
            });
            var mapper = new Mapper(mapperConfiguration);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddSingleton<IMapper>(mapper);
builder.Services.AddScoped<BaseRepo<Product>, ProductRepository>();
builder.Services.AddScoped<BaseUnitOfWork<Product>, ProductUnitOfWork>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
