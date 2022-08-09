using AutoMapper;
using ECommerce;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>().ReverseMap();
                cfg.CreateMap<Category, CategoryViewModel>().ReverseMap();
                cfg.CreateMap<Admin, AdminViewModel>().ReverseMap();
                cfg.CreateMap<User, UserViewModel>().ReverseMap();
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
builder.Services.AddScoped<BaseRepo<Category>, CategoryRepository>();
builder.Services.AddScoped<BaseRepo<Admin>, AdminRepository>();
builder.Services.AddScoped<BaseRepo<User>, UserRepository>();

builder.Services.AddScoped<BaseUnitOfWork<Product>, ProductUnitOfWork>();
builder.Services.AddScoped<BaseUnitOfWork<Category>, CategoryUnitOfWork>();
builder.Services.AddScoped<BaseUnitOfWork<Admin>, AdminUnitOfWork>();
builder.Services.AddScoped<BaseUnitOfWork<User>, UserUnitOfWork>();

builder.Services.AddScoped<AbstractValidator<Product>, ProductValidator>();
builder.Services.AddScoped<AbstractValidator<Category>, CategoryValidator>();
builder.Services.AddScoped<AbstractValidator<Admin>, AdminValidator>();
builder.Services.AddScoped<AbstractValidator<User>, UserValidator>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition(
            "oauth2",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Name = "Authorization"
            }
        );
        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                    },
                    Array.Empty<string>()
                }
            }
        );
    }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else app.UseHsts();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
