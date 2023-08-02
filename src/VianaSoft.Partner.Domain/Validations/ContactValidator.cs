using FluentValidation;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Validations;
using VianaSoft.BuildingBlocks.Core.Validations.Enumerators;

namespace VianaSoft.Partner.Domain.Validations
{
    public class ContactValidator : MyValidatorBase<Entities.Contact>
    {
        #region Properties

        private readonly ILanguageMessage _message;

        #endregion

        #region Builders

        public ContactValidator(INotifier notifier, ILanguageMessage message) : base(notifier, message)
        {
            _message = message;
        }

        public override void ValidateAll(Entities.Contact modelo)
        {
            ToValidate(modelo);
        }

        public override void ToValidate(Entities.Contact modelo, ContextService contexto = ContextService.OtherContext)
        {
            RuleFor(model => model.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(_message.NotGuid());

            RuleFor(model => model.PartnerId)
                .NotEqual(Guid.Empty)
                .WithMessage(_message.NotGuid());

            RuleFor(model => model.Name)
                .NotEmpty()
                .WithMessage(_message.Required("Name"))
                .MaximumLength(255)
                .WithMessage(_message.MaxValueCharacters("255"));

            RuleFor(model => model.BusinessEmail)
                .NotEmpty()
                .WithMessage(_message.Required("BusinessEmail"))
                .MaximumLength(255)
                .WithMessage(_message.MaxValueCharacters("255"));

            RuleFor(model => model.PersonalEmail)
                .NotEmpty()
                .WithMessage(_message.Required("PersonalEmail"))
                .MaximumLength(255)
                .WithMessage(_message.MaxValueCharacters("255"));
        }

        #endregion
    }
}
