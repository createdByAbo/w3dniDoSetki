using System.Text;
using dotenv.net;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using w3dniDoSetki;
using w3dniDoSetki.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using w3dniDoSetki.JWT;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthSettings();

// Add services to the container.
builder.Services.AddSingleton<W3dnidosetkiContext>();

builder.Services.AddScoped<ICarBrandsService, CarBrandsService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);


builder.Services.AddSingleton(authenticationSettings);

DotEnv.Load();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = true;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "https://ex.com",
        ValidAudience = "https://ex.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("SecKey")))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=User}/{action=Index}/{id?}");

app.Run();