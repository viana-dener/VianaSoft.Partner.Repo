﻿using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json.Serialization;
using VianaSoft.BuildingBlocks.Core.Configuration;
using VianaSoft.Partner.Data.Context;
using VianaSoft.Partner.Ioc;

namespace VianaSoft.Partner.Api.Configuration
{
    public static class ApiSetup
    {
        public static void AddApiSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddFilterDynamic();
            services.AddFluentValidation(configuration);
            services.AddJwt(configuration);
            services.AddRouteValidator();
            services.AddBootStrapper();
            services.AddSwagger(configuration);
        }

        public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
        {
            var company = app.Configuration.GetSection("ApplicationSettings:Application").Value;
            var apiName = app.Configuration.GetSection("ApplicationSettings:ApiSettings:ApiName").Value;
            var environment = app.Configuration.GetSection("ApplicationSettings:Environment").Value;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{company} {apiName} - {environment} v1");
            });

            var localizationOptions = new RequestLocalizationOptions();
            var supportedCultures = new[]
            {
                new CultureInfo("pt-BR"),
                new CultureInfo("en-US"),
                new CultureInfo("es-ES"),
                new CultureInfo("fr-FR"),
            };

            localizationOptions.SupportedCultures = supportedCultures;
            localizationOptions.SupportedUICultures = supportedCultures;
            localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
            localizationOptions.SetDefaultCulture("pt-BR");

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                ApplyCurrentCultureToResponseHeaders = true,
                DefaultRequestCulture = new RequestCulture("pt-BR")
            });

            app.UseHttpsRedirection();
            app.UseCors("Total");
            app.UseAuthConfiguration();
            app.MapControllers();
        }
    }
}
