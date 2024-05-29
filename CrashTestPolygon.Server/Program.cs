using Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var presentationAssembly = Assembly.Load("Presentation");
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddCors(o => o.AddPolicy("CrashTestPolygonPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    }));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(@"Server=DESKTOP-9F5QCG9\SQLEXPRESS; Database=CrashTestPolygon; Persist Security Info=false; MultipleActiveResultSets=True; Trusted_Connection=True; TrustServerCertificate=True;",
        b => b.MigrationsAssembly("Data"));
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseCors("CrashTestPolygonPolicy");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
