using SkillProfiWeb.Data;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IData<Request>, RequestDataApi>();
builder.Services.AddTransient<IData<Service>, ServiceDataApi>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute("default", "{controller=Hero}/{action=Index}/{id?}");

app.Run();
