using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Data;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Middlewares;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var isConnected = DataApi.Init();

// ��������� ���������� ��������������
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


app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<JwtMiddleware>();
app.UseStatusCodePages(async context =>
{
    var request = context.HttpContext.Request;
    var response = context.HttpContext.Response;

    // ���� ������������ �� �����������, �� ������������ �� �������� �����������
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
