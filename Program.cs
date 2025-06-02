using System;
using System.Text;
using userJwtApp.Repositories.Contexts;
using userJwtApp.Controllers;
using userJwtApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DotNetEnv;
using userJwtApp.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.IdentityModel.Tokens;

using ILogger = Serilog.ILogger;
using FluentValidation;
using userJwtApp.Validators.UserValidators;
using userJwtApp.Validators.ProductValidators;
using userJwtApp.Services.Jwt;

Env.Load();

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog(logger);


/*
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
*/

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    const string connectionString = "server=localhost;port=3306;database=dataBaseName;user=myUser;password=wrongPass;";
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

WebApplication app = builder.Build();


using (IServiceScope scope = app.Services.CreateScope())
{
    DatabaseContext dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.EnsureCreated();
}

app.UseAuthentication();
app.UseAuthorization();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddScoped<IValidator<UserSignRequestModel>, UserSignRequestValidator>();
builder.Services.AddScoped<IValidator<UserLoginRequestModel>, UserLoginRequestValidator>();
builder.Services.AddScoped<IValidator<ProductRegisterRequestModel>, ProductRegisterRequestValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateRequestModel>, ProductUpdateRequestValidator>();

builder.Services.AddSingleton<JwtService>(_ =>
{
    byte[] byteKey = Encoding.UTF8.GetBytes(JwtConsts.JWT_SIMETRIC_KEY_SHA256);

    return new(byteKey);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    byte[] byteKey = Encoding.UTF8.GetBytes(JwtConsts.JWT_SIMETRIC_KEY_SHA256);

    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(byteKey),
        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256Signature }
    };
});

RouteGroupBuilder productGroup = app.MapGroup("Product")
    .RequireAuthorization(policyBuilder => policyBuilder.RequireClaim(JwtConsts.CLAIM_ID));

RouteGroupBuilder userGroup = app.MapGroup("user")
    .AllowAnonymous();

builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<ProductController>();

