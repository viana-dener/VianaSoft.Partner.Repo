using FluentValidation;
using VianaSoft.BuildingBlocks.Core.Extensions;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.Partner.App.Models.Request;

namespace VianaSoft.Partner.Api.Validations
{
    public class PartnerRouteValidator : AbstractValidator<PartnerRequestViewModel>
    {
        #region Properties

        private readonly ILanguageMessage _message;

        #endregion

        #region Builders

        public PartnerRouteValidator(IHttpContextAccessor context, ILanguageMessage message)
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

            RuleFor(model => model.Document)
                .NotEmpty()
                .WithMessage(_message.Required("Document"))
                .MaximumLength(14)
                .WithMessage(_message.MaxValueCharacters("14"));

            RuleFor(model => model.Name)
                .NotEmpty()
                .WithMessage(_message.Required("Name"))
                .MaximumLength(255)
                .WithMessage(_message.MaxValueCharacters("255"));

            RuleFor(model => model.Description)
                .NotEmpty()
                .WithMessage(_message.Required("Description"))
                .MaximumLength(500)
                .WithMessage(_message.MaxValueCharacters("500"));
        }

        private bool ValidateGuid(string id)
        {
            return id.IsGuid().Result;
        }

        #endregion
    }
}
