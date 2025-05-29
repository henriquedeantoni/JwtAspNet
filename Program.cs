using userJwtApp.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;

using ILogger = Serilog.ILogger;
using FluentValidation;
using userJwtApp.Validators.UserValidators;
using userJwtApp.Validators.ProductValidators;

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

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddScoped<IValidator<UserSignRequestModel>, UserSignRequestValidator>();
builder.Services.AddScoped<IValidator<UserLoginRequestModel>, UserLoginRequestValidator>();
builder.Services.AddScoped<IValidator<ProductRegisterRequestModel>, ProductRegisterRequestValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateRequestModel>, ProductUpdateRequestValidator>();
