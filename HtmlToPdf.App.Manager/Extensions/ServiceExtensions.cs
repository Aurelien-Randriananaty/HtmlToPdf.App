using DinkToPdf;
using DinkToPdf.Contracts;
using HtmlToPdf.App.Service;
using HtmlToPdf.App.Service.Contracts;
using System.Net;

namespace HtmlToPdf.App.Manager.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {
        });

    //public static void ConfigureLoggerService(this IServiceCollection services) =>
    //    services.AddSingleton<ILoggerManager, LoggerManager>();

    //public static void ConfigureRepositoryManager(this IServiceCollection services) =>
    //    services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureConvertor(this IServiceCollection services) => 
        services.AddSingleton<IConverter, SynchronizedConverter>(sp => 
        new SynchronizedConverter(new PdfTools()));

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IPdfService, PdfService>();

    //public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
    //    services.AddDbContext<RepositoryContext>(opts =>
    //        opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
}
