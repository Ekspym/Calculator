using Calculator;
using Calculator.Data;
using Calculator.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Hosting;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {

        var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        try
        {


            var builder = WebApplication.CreateBuilder(args);

            Log.Information("creating Builder");

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                // Optionally, include comments from XML documentation (if you have it)
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddLogging()
                .AddSerilog();

            Log.Information("creating Swagger");

            builder.Services.AddScoped<IErrorLogger, ErrorLogger>();
            builder.Services.AddScoped<ICalculator, Calculator.Services.DefaultCalculator>();
            builder.Services.AddScoped<ICalculator, Calculator.Services.WholeNumberCalculator>();

            builder.Services.AddTransient<ICalculatorSwitcher, CalculatorSwitcher>();



            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            Log.Information("creating DBContext");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            });
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            Log.Information("Running app");



            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Terminated");
            throw ex;
        }

    }


}
