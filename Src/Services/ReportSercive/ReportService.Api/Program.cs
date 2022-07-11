using System.Text;
using FluentValidation.AspNetCore;
using Med.Shared.Entities;
using Med.Shared.Helpers;
using Med.Shared.Profile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReportService.Api.Businness.Services.Implementations;
using ReportService.Api.Businness.Services.Interfaces;
using ReportService.Api.Core.Abstracts;
using ReportService.Api.Data.Concrete;
using ReportService.Api.Data.DAL;
using Serilog;
using Serilog.Events;


#region Logger

Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.File(
        Path.Combine("Logs", "Log.txt"),
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        retainedFileCountLimit: 30,
        rollOnFileSizeLimit: true,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(2))
    .WriteTo.Console()
    //.WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

#endregion

var builder = WebApplication.CreateBuilder(args);



//
//builder.WebHost.ConfigureKestrel(opts =>
//{
//    opts.Listen(IPAddress.Loopback, port: 5002);
//    opts.ListenAnyIP(5003);
//    opts.ListenLocalhost(5004, opts => opts.UseHttps());
//    opts.ListenLocalhost(5005, opts => opts.UseHttps());
//});     


// Add services to the container.
builder.Services.AddAutoMapper(typeof(GlobalMapper));
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
)
    .AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<Program>()).ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = CustomProblemDetails.MakeValidationResponse;
    });

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

}));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseSerilog();



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceUnitOfWork, ServiceUnitOfWork>();





builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("ReportService.Api.Data"));

});

//identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

#region Jwt



builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });


#endregion

//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

 app.Run();
