using SkillProfiWeb.Data;
using SkillProfiWeb.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IRequestData, RequestDataApi>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute("default", "{controller=Hero}/{action=Index}/{id?}");

app.Run();
