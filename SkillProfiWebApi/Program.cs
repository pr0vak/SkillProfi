using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SkillProfi.DAL.Models;
using SkillProfiWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

var dataConnection = JObject.Parse(File.ReadAllText(@".\connection.json"));
var stringCon = $"Data source={dataConnection["server_address"]};" +
    $"Database={dataConnection["database"]};" +
    $"User Id={dataConnection["user"]};" +
    $"password={dataConnection["password"]};" +
    $"TrustServerCertificate=True;";

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(stringCon
    ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));

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

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
