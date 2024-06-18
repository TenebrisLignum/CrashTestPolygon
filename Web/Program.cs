using Data;
using Domain;
using Application;
using Presentation;
using WebAPI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services
    .AddAuthorizationBuilder();

builder.Services
    .AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services
    .AddData()
    .AddApplication()
    .AddPresentation();

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
app.UseAuthorization();
app.MapControllers();
app.MapIdentityApi<AppUser>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
