using System.Text;
using FluentValidation.AspNetCore;
using IdentityService.Api.DAL;
using Med.Shared.Entities;
using Med.Shared.Helpers;
using Med.Shared.Profile;
using Med.Shared.Validators.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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


// Add services to the container.


builder.Services.AddAutoMapper(typeof(GlobalMapper));
builder.Services.AddControllers()
    .AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<UserLoginDtoValidator>())
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = CustomProblemDetails.MakeValidationResponse;
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseSerilog();

//db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
//identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false; //@ * gibi karakterler olmalı

    options.Lockout.MaxFailedAccessAttempts = 15; //5 girişten sonra kilitlenioyr. 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5); //5 dk sonra açılır
    options.Lockout.AllowedForNewUsers = true; //üsttekilerle alakalı

    //options.User.AllowedUserNameCharacters = ""; //olmasını istediğiniz kesin karaterrleri yaz

    options.User.RequireUniqueEmail = true; //unique emaail adresleri olsun her kullanıcının 

    options.SignIn.RequireConfirmedEmail = false; //Kayıt olduktan email ile token gönderecek 
    options.SignIn.RequireConfirmedPhoneNumber = false; //telefon doğrulaması
});


#region Jwt

builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
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
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero
        };
        // options.SaveToken = true;
        // options.Events = new JwtBearerEvents();
        // options.Events.OnMessageReceived = context =>
        // {
        //     if (context.Request.Cookies.ContainsKey("X-Access-Token"))
        //     {
        //         context.Token = context.Request.Cookies["X-Access-Token"];
        //     }
        //
        //     return Task.CompletedTask;
        // };
    });
    // .AddCookie(options =>
    // {
    //     options.Cookie.SameSite = SameSiteMode.Strict;
    //     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //     options.Cookie.IsEssential = true;
    // });

#endregion


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