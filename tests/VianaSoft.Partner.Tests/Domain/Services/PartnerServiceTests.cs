using FizzWare.NBuilder;
using Moq;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Validations.Interfaces;
using VianaSoft.Partner.Domain.Filters;
using VianaSoft.Partner.Domain.Interfaces;
using VianaSoft.Partner.Domain.Services;
using Xunit;

namespace VianaSoft.Partner.Tests.Domain.Services
{
    public class PartnerServiceTests
    {
        #region Properties

        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IMyValidator<Partner.Domain.Entities.Partner>> _validator;
        private readonly Mock<ILanguageMessage> _message;
        private readonly Mock<IPartnerRepository> _repository;

        private readonly PartnerService _service;

        #endregion

        #region Builders

        public PartnerServiceTests()
        {
            _notifier = new Mock<INotifier>();
            _validator = new Mock<IMyValidator<Partner.Domain.Entities.Partner>>();
            _message = new Mock<ILanguageMessage>();
            _repository = new Mock<IPartnerRepository>();

            _service = new PartnerService(_notifier.Object, _validator.Object, _message.Object, _repository.Object);
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Get all paged - Success")]
        [Trait("Domain", "Services.Partner")]
        public async void GetAllPagedAsync_Success()
        {
            // Scenario
            var _filter = Builder<PartnerFilter>.CreateNew().Build();
            var _data = Builder<ListPage<Partner.Domain.Entities.Partner>>.CreateNew().Build();

            _repository.Setup(x => x.GetAllPagedAsync(It.IsAny<PartnerFilter>())).ReturnsAsync(_data);

            // Action
            var result = await _service.GetAllPagedAsync(_filter);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetAllPagedAsync(It.IsAny<PartnerFilter>()), Times.Once);
        }

        [Fact(DisplayName = "Get all - Success")]
        [Trait("Domain", "Services.Partner")]
        public async void GetAllAsync_Success()
        {
            // Scenario
            var _data = Builder<Partner.Domain.Entities.Partner>.CreateListOfSize(10).Build();

            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(_data);

            // Action
            var result = await _service.GetAllAsync();

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact(DisplayName = "Get by id - Success")]
        [Trait("Domain", "Services.Partner")]
        public async void GetByIdAsync_Success()
        {
            // Scenario
            var _filter = Guid.NewGuid().ToString();
            var _data = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_data);

            // Action
            var result = await _service.GetByIdAsync(_filter);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Get by document - Success")]
        [Trait("Domain", "Services.Partner")]
        public async void GetByDocumentAsync_Success()
        {
            // Scenario
            var _filter = Builder<DocumentFilter>.CreateNew().Build();
            var _data = Builder<ListPage<Partner.Domain.Entities.Partner>>.CreateNew().Build();

            _repository.Setup(x => x.GetByDocumentAsync(It.IsAny<DocumentFilter>())).ReturnsAsync(_data);

            // Action
            var result = await _service.GetByDocumentAsync(_filter);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetByDocumentAsync(It.IsAny<DocumentFilter>()), Times.Once);
        }

        [Fact(DisplayName = "Get by name - Success")]
        [Trait("Domain", "Services.Partner")]
        public async void GetByNameAsync_Success()
        {
            // Scenario
            var _filter = "VianaSoft Ltda.";
            var _data = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _repository.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(_data);

            // Action
            var result = await _service.GetByNameAsync(_filter);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Exist document - Success")]
        [Trait("Domain", "Services.Partner")]
        public async void ExistDocument_Success()
        {
            // Scenario
            var _filter = "VianaSoft Ltda.";
            var _data = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _repository.Setup(x => x.ExistDocument(It.IsAny<string>())).ReturnsAsync(_data);

            // Action
            var result = await _service.ExistDocument(_filter);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.ExistDocument(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Insert - Success")]
        [Trait("Domain", "Services.Partner")]
        public async void InsertAsync_Success()
        {
            // Scenario
            var _request = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _validator.Setup(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Partner>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>())).Returns(true);
            _repository.Setup(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Partner>()));
            _repository.Setup(x => x.Commit()).ReturnsAsync(true);

            // Action
            var result = await _service.InsertAsync(_request);

            // Affirmations
            Assert.True(result);

            _validator.Verify(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Partner>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>()), Times.Once);
            _repository.Verify(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Once);
            _repository.Verify(x => x.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Insert - Failure")]
        [Trait("Domain", "Services.Partner")]
        public async void InsertAsync_Failure()
        {
            // Scenario
            var _request = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _validator.Setup(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Partner>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>())).Returns(false);
            _repository.Setup(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Partner>()));
            _repository.Setup(x => x.Commit()).ReturnsAsync(true);

            // Action
            var result = await _service.InsertAsync(_request);

            // Affirmations
            Assert.False(result);

            _validator.Verify(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Partner>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>()), Times.Once);
            _repository.Verify(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
            _repository.Verify(x => x.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("Domain", "Services.Partner")]
        public async void UpdateAsync_Success()
        {
            // Scenario
            var _request = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _validator.Setup(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Partner>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>())).Returns(true);
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()));
            _repository.Setup(x => x.Commit()).ReturnsAsync(true);

            // Action
            var result = await _service.UpdateAsync(_request);

            // Affirmations
            Assert.True(result);

            _validator.Verify(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Partner>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Once);
            _repository.Verify(x => x.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Update - Failure")]
        [Trait("Domain", "Services.Partner")]
        public async void UpdateAsync_Failure()
        {
            // Scenario
            var _request = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _validator.Setup(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Partner>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>())).Returns(false);
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()));
            _repository.Setup(x => x.Commit()).ReturnsAsync(true);

            // Action
            var result = await _service.UpdateAsync(_request);

            // Affirmations
            Assert.False(result);

            _validator.Verify(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Partner>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>()), Times.Once);
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
            _repository.Verify(x => x.Commit(), Times.Never);
        }

        #endregion
    }
}
