using AttributeRouting.Web.Mvc;
using GAR_table.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<GarContext>(options => options.UseNpgsql(connection));
builder.Services.AddDbContext<GarContextRegion50>(options => options.UseNpgsql(connection));
builder.Services.AddDbContext<GarContextRegion66>(options => options.UseNpgsql(connection));
builder.Services.AddDbContext<GarContextRegion86>(options => options.UseNpgsql(connection));
builder.Services.AddDbContext<GarContextRegion87>(options => options.UseNpgsql(connection));

// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

//app.mapfallbacktofile("index.html");

app.Run();
