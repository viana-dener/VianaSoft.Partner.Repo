using FluentValidation;
using VianaSoft.BuildingBlocks.Core.Extensions;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.Partner.App.Models.Request;

namespace VianaSoft.Partner.Api.Validations
{
    public class PhoneRouteValidator : AbstractValidator<PhoneRequestViewModel>
    {
        #region Properties

        private readonly ILanguageMessage _message;

        #endregion

        #region Builders

        public PhoneRouteValidator(IHttpContextAccessor context, ILanguageMessage message)
        {
            _message = message;
            ValidateRoute(context);
        }

        #endregion

        #region Private Methods

        private void ValidateRoute(IHttpContextAccessor context)
        {
            if (context.HttpContext?.Request.Method == HttpMethod.Get.Method ||
                context.HttpContext?.Request.Method == HttpMethod.Put.Method ||
                context.HttpContext?.Request.Method == HttpMethod.Patch.Method ||
                context.HttpContext?.Request.Method == HttpMethod.Delete.Method)
            {
                var routeData = context.HttpContext.GetRouteData();
                var id = routeData.Values["id"]?.ToString();

                RuleFor(x => id)
                    .NotEmpty()
                    .WithMessage(_message.Required("Id"))
                    .MaximumLength(36)
                    .WithMessage(_message.MaxValueCharacters("36"))
                    .Must(ValidateGuid)
                    .WithMessage(_message.NotGuid());
            }

            RuleFor(model => model.ContactId)
                .NotEmpty()
                .WithMessage(_message.Required("ContactId"))
                .MaximumLength(36)
                .WithMessage(_message.MaxValueCharacters("36"))
                .Must(ValidateGuid)
                .WithMessage(_message.NotGuid());

            RuleFor(model => model.DDICode)
                .NotEmpty()
                .WithMessage(_message.Required("DDICode"))
                .MaximumLength(5)
                .WithMessage(_message.MaxValueCharacters("5"));

            RuleFor(model => model.DDDCode)
                .NotEmpty()
                .WithMessage(_message.Required("DDDCode"))
                .MaximumLength(5)
                .WithMessage(_message.MaxValueCharacters("5"));

            RuleFor(model => model.Number)
                .NotEmpty()
                .WithMessage(_message.Required("Number"))
                .MaximumLength(20)
                .WithMessage(_message.MaxValueCharacters("20"));

            RuleFor(model => model.IsCellPhone)
                .NotEmpty()
                .WithMessage(_message.Required("IsCellPhone"));

            RuleFor(model => model.IsWhatsapp)
                .NotEmpty()
                .WithMessage(_message.Required("IsWhatsapp"));

            RuleFor(model => model.IsTelegram)
                .NotEmpty()
                .WithMessage(_message.Required("IsTelegram"));
        }

        private bool ValidateGuid(string id)
        {
            return id.IsGuid().Result;
        }

        #endregion
    }
}
