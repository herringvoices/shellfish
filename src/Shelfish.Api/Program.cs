using System.Text;
using System.Text.Json.Serialization;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shelfish.Api.Data;
using Shelfish.Api.Models;
using Shelfish.Api.Services;
using Shelfish.Api.Services.Interfaces;

Env.Load(); // load .env

var builder = WebApplication.CreateBuilder(args);

/* ── PostgreSQL connection ───────────────── */
var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME") ?? "postgres";
var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";
var conn = $"Host=localhost;Database=shelfishdb;Username={dbUser};Password={dbPass}";

builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(conn));

/* ── Identity ────────────────────────────── */
builder
    .Services.AddIdentityCore<User>(o => o.User.RequireUniqueEmail = true)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager();

/* ── JWT in HttpOnly cookie ─────────────── */
var jwtSecret =
    Environment.GetEnvironmentVariable("JWT_SIGNING_KEY")
    ?? throw new InvalidOperationException("JWT_SIGNING_KEY missing");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

builder
    .Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero,
        };
        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                var cookie = ctx.Request.Cookies["jwt"];
                if (!string.IsNullOrEmpty(cookie))
                    ctx.Token = cookie;
                return Task.CompletedTask;
            },
        };
    });

builder.Services.ConfigureApplicationCookie(c =>
{
    c.Cookie.SameSite = SameSiteMode.None;
    c.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    c.Events.OnRedirectToLogin = ctx =>
    {
        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

/* ── DI placeholder ─────────────────────── */
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();

// ── AutoMapper + Google Books DI ───────────────────────────────
builder.Services.AddAutoMapper(typeof(Shelfish.Api.Mapping.MappingProfile));

builder.Services.AddHttpClient<IGoogleBooksService, GoogleBooksService>();

// ── Controllers & JSON options ─────────────────────────────────
builder
    .Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.MaxDepth = 32;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Shelfish API", Version = "v1" }
    );

    var jwtCookie = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "jwt",
        In = Microsoft.OpenApi.Models.ParameterLocation.Cookie,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "JWT stored in HttpOnly cookie named **jwt**",
        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            Id = "cookieAuth",
        },
    };

    c.AddSecurityDefinition("cookieAuth", jwtCookie);

    c.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            { jwtCookie, Array.Empty<string>() },
        }
    );
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await Shelfish.Api.Data.SeedData.InitialiseAsync(app.Services);
app.Run();
