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
            services.AddScoped<IContactApplication, ContactApplication>();
            services.AddScoped<IPhoneApplication, PhoneApplication>();

            // Domain
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IPhoneService, PhoneService>();

            // Repository
            services.AddScoped<DataContext>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();

            // Validate
            services.AddScoped<IFactoryValidators<Domain.Entities.Partner>, FactoryValidators<Domain.Entities.Partner>>();
            services.AddScoped<IMyValidatorBase<Domain.Entities.Partner>, PartnerValidator>();
            services.AddScoped<IMyValidator<Domain.Entities.Partner>, MyValidator<Domain.Entities.Partner>>();

            services.AddScoped<IFactoryValidators<Domain.Entities.Contact>, FactoryValidators<Domain.Entities.Contact>>();
            services.AddScoped<IMyValidatorBase<Domain.Entities.Contact>, ContactValidator>();
            services.AddScoped<IMyValidator<Domain.Entities.Contact>, MyValidator<Domain.Entities.Contact>>();

            services.AddScoped<IFactoryValidators<Domain.Entities.Phone>, FactoryValidators<Domain.Entities.Phone>>();
            services.AddScoped<IMyValidatorBase<Domain.Entities.Phone>, PhoneValidator>();
            services.AddScoped<IMyValidator<Domain.Entities.Phone>, MyValidator<Domain.Entities.Phone>>();

            return services;
        }
    }
}
