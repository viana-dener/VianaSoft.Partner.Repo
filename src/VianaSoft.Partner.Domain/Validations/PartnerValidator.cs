using FluentValidation;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Validations;
using VianaSoft.BuildingBlocks.Core.Validations.Enumerators;

namespace VianaSoft.Partner.Domain.Validations
{
    public class PartnerValidator : MyValidatorBase<Entities.Partner>
    {
        #region Properties

        private readonly ILanguageMessage _message;

        #endregion

        #region Builders

        public PartnerValidator(INotifier notifier, ILanguageMessage message) : base(notifier, message)
        {
            _message = message;
        }

        public override void ValidateAll(Entities.Partner modelo)
        {
            ToValidate(modelo);
        }

        public override void ToValidate(Entities.Partner modelo, ContextService contexto = ContextService.OtherContext)
        {
            RuleFor(model => model.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(_message.NotGuid());

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

        #endregion
    }
}
