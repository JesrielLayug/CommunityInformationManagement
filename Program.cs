using BarangayInformationManagement.Data;
using BarangayInformationManagement.Repositories;
using BarangayInformationManagement.Repositories.Interface;
using BarangayInformationManagement.Services;
using BarangayInformationManagement.Services.Interface;
using BarangayInformationManagement.Utilities;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MudBlazor.Services;

namespace BarangayInformationManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<DatabaseSettings>(
            builder.Configuration.GetSection(nameof(DatabaseSettings)));

            builder.Services.AddSingleton<IDatabaseSettings>(
                sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(s =>
            new MongoClient(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")));

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            
            builder.Services.AddScoped<IBarangayOfficialRepository, BarangayOfficialRepository>();
            builder.Services.AddScoped<IBarangayOfficialService , BarangayOfficialService>();

            builder.Services.AddMudServices();

            builder.Services.AddOptions();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddHttpClient();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
        }
    }
}
