using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SkillProfi.DAL.Models;
using SkillProfiWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

var pathToFile = @".\connection.json";
var dataConnection = GetConnection(pathToFile);
var stringCon = $"Data source={dataConnection["server_address"]};" +
    $"Database={dataConnection["database"]};" +
    $"User Id={dataConnection["user_db"]};" +
    $"password={dataConnection["password_db"]};" +
    $"TrustServerCertificate=True;";

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(stringCon
    ?? throw new InvalidOperationException("Connection string 'DataContext' not found."))
        );


builder.Services.AddAuthorization();
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

bool isConnectedToSQL;

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    isConnectedToSQL = SeedData.Initialize(services, pathToFile);
}

if (!isConnectedToSQL)
{
    Console.ReadLine();
    return;
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


JObject GetConnection(string path)
{
    if (!File.Exists(path))
    {
        var data = new JObject();
        data["server_address"] = "localhost";
        data["database"] = "skillprofi";
        data["user_db"] = "admin";
        data["password_db"] = "admin";
        data["user_web"] = "admin";
        data["password_web"] = "admin";
        File.WriteAllText(path, data.ToString());
    }

    return JObject.Parse(File.ReadAllText(path));
};