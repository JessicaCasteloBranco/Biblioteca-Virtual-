using EnterpriseMinimal.Data;
using EnterpriseMinimal.Data.Models;
using EnterpriseMinimal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();// você pode misturar mvc ou qualquer outra pagina aqui que vai funcionar
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<LivrosService>();

builder.Services.AddMudServices();
var app = builder.Build();

// Configure the HTTP request pipeline. configure o pipeline de solicitação HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
