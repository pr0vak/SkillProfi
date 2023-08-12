using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Data;
using SkillProfiWeb.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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

builder.Services.AddTransient<IData<Request>, RequestDataApi>();
builder.Services.AddTransient<IData<Service>, ServiceDataApi>();
builder.Services.AddTransient<IData<Project>, ProjectDataApi>();
builder.Services.AddTransient<IData<Blog>, BlogDataApi>();

var app = builder.Build();

app.Use((ctx, next) =>
{
    var headers = ctx.Request.Headers;

    headers.Add("Authorization", $"Bearer {DataApi.Token}");

    return next();
});

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Hero}/{action=Index}/{id?}");

app.Run();
