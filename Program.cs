using System.Text;
using dotenv.net;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.Cookies;
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

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<ICarModelsService, CarModelsService>();
builder.Services.AddScoped<ICarBrandsService, CarBrandsService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);


builder.Services.AddSingleton(authenticationSettings);

DotEnv.Load();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = "https://ex.com",
            ValidAudience = "https://ex.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("SecKey"))),
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                {
                    context.Token = context.Request.Cookies["X-Access-Token"];
                }
                return Task.CompletedTask;
            }
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

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 401)
    {
        context.Response.Redirect("/Login?reason=401");
    }
});

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