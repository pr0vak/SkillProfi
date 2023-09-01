using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SkillProfi.DAL.Models;
using SkillProfi.Domain.Auth;
using SkillProfi.Web.Data;
using SkillProfi.Web.Interfaces;
using SkillProfi.Web.Middlewares;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var isConnected = DataApi.Init();

// Настройка параметров аутентификации
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IData<Request>, RequestDataApi>();
builder.Services.AddTransient<IData<Service>, ServiceDataApi>();
builder.Services.AddTransient<IData<Project>, ProjectDataApi>();
builder.Services.AddTransient<IData<Blog>, BlogDataApi>();

var app = builder.Build();

app.UseMiddleware<JwtMiddleware>();

app.UseStaticFiles();

app.UseRouting();

app.UseStatusCodePages(async context =>
{
    var request = context.HttpContext.Request;
    var response = context.HttpContext.Response;

    // Если пользователь не авторизован, то перекидываем на страницу авторизации
    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        response.Redirect($"/Account?returnUrl={request.Path}");
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Hero}/{action=Index}/{id?}");

if (!isConnected)
{
    return;
}

app.Run();
