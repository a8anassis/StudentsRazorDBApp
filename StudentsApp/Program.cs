using Serilog;
using StudentsApp.Configuration;
using StudentsApp.Core;
using StudentsApp.DAO;
using StudentsApp.Services;

namespace StudentsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<DBHelper>();
            builder.Services.AddScoped<IStudentDAO, StudentDAOImpl>();
            builder.Services.AddScoped<IStudentService, StudentServiceImpl>();

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperConfig>());

            var app = builder.Build();

            app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("UserId", httpContext.User?.Identity?.Name);
                    diagnosticContext.Set("RemoteIP", httpContext.Connection.RemoteIpAddress);
                };
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
