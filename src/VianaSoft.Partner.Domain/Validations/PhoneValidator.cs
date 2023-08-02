using FluentValidation;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Validations;
using VianaSoft.BuildingBlocks.Core.Validations.Enumerators;

namespace VianaSoft.Partner.Domain.Validations
{
    public class PhoneValidator : MyValidatorBase<Entities.Phone>
    {
        #region Properties

        private readonly ILanguageMessage _message;

        #endregion

        #region Builders

        public PhoneValidator(INotifier notifier, ILanguageMessage message) : base(notifier, message)
        {
            _message = message;
        }

        public override void ValidateAll(Entities.Phone modelo)
        {
            ToValidate(modelo);
        }

        public override void ToValidate(Entities.Phone modelo, ContextService contexto = ContextService.OtherContext)
        {
            RuleFor(model => model.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(_message.NotGuid());

            RuleFor(model => model.ContactId)
                .NotEqual(Guid.Empty)
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
        }

        #endregion
    }
}
