using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Notifications;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Resources;
using VianaSoft.BuildingBlocks.Core.User.Interfaces;
using VianaSoft.BuildingBlocks.Core.User;
using VianaSoft.BuildingBlocks.Core.Validations.Interfaces;
using VianaSoft.BuildingBlocks.Core.Validations;
using VianaSoft.Partner.App.AutoMapper;
using VianaSoft.Partner.App.Interfaces;
using VianaSoft.Partner.App.Services;
using VianaSoft.Partner.Data.Context;
using VianaSoft.Partner.Data.Repositories;
using VianaSoft.Partner.Domain.Interfaces;
using VianaSoft.Partner.Domain.Services;
using VianaSoft.Partner.Domain.Validations;

namespace VianaSoft.Partner.Ioc
{
    public static class BootStrapper
    {
        public static IServiceCollection AddBootStrapper(this IServiceCollection services)
        {
            // Services
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<ILanguageMessage, LanguageMessage>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Applications
            services.AddScoped<IPartnerApplication, PartnerApplication>();

            // Domain
            services.AddScoped<IPartnerService, PartnerService>();

            // Repository
            services.AddScoped<PartnerContext>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();

            // Validate
            services.AddScoped<IFactoryValidators<Domain.Entities.Partner>, FactoryValidators<Domain.Entities.Partner>>();
            services.AddScoped<IMyValidatorBase<Domain.Entities.Partner>, PartnerValidator>();
            services.AddScoped<IMyValidator<Domain.Entities.Partner>, MyValidator<Domain.Entities.Partner>>();

            return services;
        }
    }
}
