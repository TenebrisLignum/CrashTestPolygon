using Data;
using Domain;
using Application;
using Presentation;
using Hubs;
using WebAPI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Users;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Hubs.Chats;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

var configuration = builder.Configuration;

var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services
    .AddAuthorizationBuilder();

builder.Services
    .AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>();

builder.Services
    .AddData()
    .AddApplication()
    .AddPresentation()
    .AddHubs();

builder.Services.AddSignalR();

builder.Services
    .AddCors(o => o.AddPolicy(Consts.CORSName, builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseCors(Consts.CORSName);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ChatRoomHub>("/chat-room-hub");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
