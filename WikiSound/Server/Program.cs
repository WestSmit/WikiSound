using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WikiSound.Server.Database;
using WikiSound.Server.Repositories;
using WikiSound.Server;
using WikiSound.Server.Services;
using Nest;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WikiSound.Server.Configurations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// DB services
var dbConnectionString = config.GetConnectionString("WikiSoundDb");

builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<Seed>();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(dbConnectionString));

// ElasticSearch services
var elasticConfig = config.GetRequiredSection("ElasticSearch")
    .Get<ElasticSearchConfiguration>();

builder.Services.AddScoped<IElasticSearchService, ElasticSearchService>();

builder.Services.AddSingleton<IElasticClient, ElasticClient>(x => 
    new ElasticClient(
        new ConnectionSettings(
            new Uri(elasticConfig.Uri))
                .CertificateFingerprint(elasticConfig.CertificateFingerprint)
                .BasicAuthentication(elasticConfig.User, elasticConfig.Password)));

// Auth
var jwtTokenConfiguration = config.GetRequiredSection("JwtSettings").Get<JwtTokenConfiguration>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => opt.TokenValidationParameters = jwtTokenConfiguration.CreateValidationParameters());


builder.Services.AddAuthorization();


builder.Services.AddSingleton<ITokenService>(x => ActivatorUtilities.CreateInstance<TokenService>(x, jwtTokenConfiguration));

builder.Services
    .AddIdentityCore<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddSignInManager<SignInManager<IdentityUser>>();

// Other services
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSeedData();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

