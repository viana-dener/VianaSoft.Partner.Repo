using Castle.Components.DictionaryAdapter.Xml;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Extensions;
using Moq;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.Partner.Domain.Validations;
using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Validations
{
    public class PartnerValidatorTests
    {
        #region Properties

        private readonly Mock<INotifier> _notifier;
        private readonly Mock<ILanguageMessage> _message;

        private readonly PartnerValidator _validator;

        #endregion

        #region Builders

        public PartnerValidatorTests()
        {
            _notifier = new Mock<INotifier>();
            _message = new Mock<ILanguageMessage>();

            _validator = new PartnerValidator(_notifier.Object, _message.Object);
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Validate - Success")]
        [Trait("Domain", "Validate.Partner")]
        public void Validate_Success()
        {
            // Scenario
            var _partners = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            // Action

            var result = _validator.Validate(_partners).IsValid;

            // Affirmations
            Assert.True(_validator.IsDefaultValue());
        }

        [Fact(DisplayName = "Validate all - Success")]
        [Trait("Domain", "Validate.Partner")]
        public void ValidateAll_Success()
        {
            // Scenario
            var _partners = Builder<Partner.Domain.Entities.Partner>.CreateNew()
                .With(x => x.Id, Guid.NewGuid())
                .Build();

            _message.Setup(x => x.NotGuid(false)).Returns("O valor informado no campo não é do tipo Guid.");
            _message.Setup(x => x.Required("Document", false)).Returns("O campo Document é obrigatório.");
            _message.Setup(x => x.MaxValueCharacters("14", false)).Returns("O valor máximo permitido é de 14 caracteres.");
            _message.Setup(x => x.Required("Name", false)).Returns("O campo Name é obrigatório.");
            _message.Setup(x => x.MaxValueCharacters("255", false)).Returns("O valor máximo permitido é de 255 caracteres.");
            _message.Setup(x => x.Required("Description", false)).Returns("O campo Description é obrigatório.");
            _message.Setup(x => x.MaxValueCharacters("500", false)).Returns("O valor máximo permitido é de 500 caracteres.");

            // Action
            _validator.ValidateAll(_partners);

            // Affirmations
            Assert.False(_validator.IsDefaultValue());
        }

        [Fact(DisplayName = "Validate - Failure")]
        [Trait("Domain", "Validate.Partner")]
        public void Error_Failure()
        {
            // Scenario
            var _partners = Builder<Partner.Domain.Entities.Partner>.CreateNew()
                .With(x => x.Document, null)
                .With(x => x.Name, null)
                .With(x => x.Description, null)
                .Build();

            // Action

            var result = _validator.Validate(_partners).IsValid;

            // Affirmations
            Assert.False(_validator.IsDefaultValue());
        }

        #endregion
    }
}
