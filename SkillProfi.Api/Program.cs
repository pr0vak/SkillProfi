using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SkillProfi.Api.Data;
using SkillProfi.Domain.Auth;
using SkillProfi.Domain.Interfaces;
using SkillProfi.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Настраиваем подлючение к БД
var pathToFile = @".\connection.json";
var dataConnection = SeedData.GetConnection(pathToFile);
var stringCon = $"Data source={dataConnection["server_address"]};" +
    $"Database={dataConnection["database"]};" +
    $"User Id={dataConnection["user_db"]};" +
    $"password={dataConnection["password_db"]};" +
    $"TrustServerCertificate=True;";


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(stringCon
    ?? throw new InvalidOperationException("Connection string 'DataContext' not found."))
        );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkillProfi.Api", Version = "v2" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "JWT Authorization header using the Bearer scheme.",

            Scheme = "bearer",
            BearerFormat = "JWT",

            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new List<string>()
            }
        });
    });

var app = builder.Build();

bool isConnectedToSQL;

// Инициализируем и проверяем доступ к БД
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    isConnectedToSQL = SeedData.Initialize(services, pathToFile);
}

// Если соединение с БД не было установлено, то завершаем работу приложения с описанием ошибки
if (!isConnectedToSQL)
{
    Console.ReadLine();
    return;
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
