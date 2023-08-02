var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute("default", "{controller=Hero}/{action=Index}/{id?}");

app.Run();
