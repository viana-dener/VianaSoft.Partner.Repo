using FizzWare.NBuilder;
using FizzWare.NBuilder.Extensions;
using Moq;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.Partner.Domain.Validations;
using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Validations
{
    public class ContactValidatorTests
    {
        #region Properties

        private readonly Mock<INotifier> _notifier;
        private readonly Mock<ILanguageMessage> _message;

        private readonly ContactValidator _validator;

        #endregion

        #region Builders

        public ContactValidatorTests()
        {
            _notifier = new Mock<INotifier>();
            _message = new Mock<ILanguageMessage>();

            _validator = new ContactValidator(_notifier.Object, _message.Object);
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Validate - Success")]
        [Trait("Domain", "Validate.Contact")]
        public void Validate_Success()
        {
            // Scenario
            var _partners = Builder<Partner.Domain.Entities.Contact>.CreateNew()
                .With(x => x.Id, Guid.NewGuid())
                .With(x => x.PartnerId, Guid.NewGuid())
                .Build();

            // Action

            var result = _validator.Validate(_partners).IsValid;

            // Affirmations
            Assert.True(result);
        }

        [Fact(DisplayName = "Validate all - Success")]
        [Trait("Domain", "Validate.Contact")]
        public void ValidateAll_Success()
        {
            // Scenario
            var _partners = Builder<Partner.Domain.Entities.Contact>.CreateNew()
                .With(x => x.Id, Guid.NewGuid())
                .With(x => x.PartnerId, Guid.NewGuid())
                .Build();

            _message.Setup(x => x.NotGuid(false)).Returns("O valor informado no campo não é do tipo Guid.");
            _message.Setup(x => x.Required("Name", false)).Returns("O campo Name é obrigatório.");
            _message.Setup(x => x.MaxValueCharacters("255", false)).Returns("O valor máximo permitido é de 255 caracteres.");
            _message.Setup(x => x.Required("BusinessEmail", false)).Returns("O campo BusinessEmail é obrigatório.");
            _message.Setup(x => x.MaxValueCharacters("255", false)).Returns("O valor máximo permitido é de 255 caracteres.");
            _message.Setup(x => x.Required("PersonalEmail", false)).Returns("O campo PersonalEmail é obrigatório.");
            _message.Setup(x => x.MaxValueCharacters("255", false)).Returns("O valor máximo permitido é de 255 caracteres.");

            // Action
            _validator.ValidateAll(_partners);

            // Affirmations
            Assert.False(_validator.IsDefaultValue());
        }

        [Fact(DisplayName = "Validate - Failure")]
        [Trait("Domain", "Validate.Contact")]
        public void Error_Failure()
        {
            // Scenario
            var _partners = Builder<Partner.Domain.Entities.Contact>.CreateNew()
                .With(x => x.Name, null)
                .With(x => x.BusinessEmail, null)
                .With(x => x.PersonalEmail, null)
                .Build();

            // Action

            var result = _validator.Validate(_partners).IsValid;

            // Affirmations
            Assert.False(_validator.IsDefaultValue());
        }

        #endregion
    }
}
