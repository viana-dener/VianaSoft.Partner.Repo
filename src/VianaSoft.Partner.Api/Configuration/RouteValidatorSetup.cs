using FluentValidation;
using VianaSoft.Partner.Api.Validations;
using VianaSoft.Partner.App.Models.Request;

namespace VianaSoft.Partner.Api.Configuration
{
    public static class RouteValidatorSetup
    {
        public static IServiceCollection AddRouteValidator(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IValidator<PartnerRequestViewModel>, PartnerRouteValidator>();
            services.AddTransient<IValidator<ContactRequestViewModel>, ContactRouteValidator>();
            services.AddTransient<IValidator<PhoneRequestViewModel>, PhoneRouteValidator>();

            return services;
        }
    }
}
