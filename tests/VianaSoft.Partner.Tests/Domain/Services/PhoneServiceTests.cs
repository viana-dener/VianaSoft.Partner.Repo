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
    public class PhoneServiceTests
    {
        #region Properties

        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IMyValidator<Partner.Domain.Entities.Phone>> _validator;
        private readonly Mock<ILanguageMessage> _message;
        private readonly Mock<IPhoneRepository> _repository;

        private readonly PhoneService _service;

        #endregion

        #region Builders

        public PhoneServiceTests()
        {
            _notifier = new Mock<INotifier>();
            _validator = new Mock<IMyValidator<Partner.Domain.Entities.Phone>>();
            _message = new Mock<ILanguageMessage>();
            _repository = new Mock<IPhoneRepository>();

            _service = new PhoneService(_notifier.Object, _validator.Object, _message.Object, _repository.Object);
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Get all paged - Success")]
        [Trait("Domain", "Services.Phone")]
        public async void GetAllPagedAsync_Success()
        {
            // Scenario
            var _filter = Builder<PhoneFilter>.CreateNew().Build();
            var _data = Builder<ListPage<Partner.Domain.Entities.Phone>>.CreateNew().Build();

            _repository.Setup(x => x.GetAllPagedAsync(It.IsAny<PhoneFilter>())).ReturnsAsync(_data);

            // Action
            var result = await _service.GetAllPagedAsync(_filter);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetAllPagedAsync(It.IsAny<PhoneFilter>()), Times.Once);
        }

        [Fact(DisplayName = "Get all - Success")]
        [Trait("Domain", "Services.Phone")]
        public async void GetAllAsync_Success()
        {
            // Scenario
            var _data = Builder<Partner.Domain.Entities.Phone>.CreateListOfSize(10).Build();

            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(_data);

            // Action
            var result = await _service.GetAllAsync();

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact(DisplayName = "Get by id - Success")]
        [Trait("Domain", "Services.Phone")]
        public async void GetByIdAsync_Success()
        {
            // Scenario
            var _filter = Guid.NewGuid().ToString();
            var _data = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();

            _repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_data);

            // Action
            var result = await _service.GetByIdAsync(_filter);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "Get by number - Success")]
        [Trait("Domain", "Services.Phone")]
        public async void GetByNumberAsync_Success()
        {
            // Scenario
            var _filter = "VianaSoft Ltda.";
            var _data = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();

            _repository.Setup(x => x.GetByNumberAsync(It.IsAny<string>())).ReturnsAsync(_data);

            // Action
            var result = await _service.GetByNumberAsync(_filter);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.GetByNumberAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Exists - Success")]
        [Trait("Domain", "Services.Phone")]
        public async void ExistsAsync_Success()
        {
            // Scenario
            var _ddiCode = "+55";
            var _dddCode = "31";
            var _number = "997411315";
            var _data = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();

            _repository.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(_data);

            // Action
            var result = await _service.ExistsAsync(_ddiCode, _dddCode, _number);

            // Affirmations
            Assert.NotNull(result);

            _repository.Verify(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Insert - Success")]
        [Trait("Domain", "Services.Phone")]
        public async void InsertAsync_Success()
        {
            // Scenario
            var _request = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();

            _validator.Setup(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Phone>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>())).Returns(true);
            _repository.Setup(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Phone>()));
            _repository.Setup(x => x.Commit()).ReturnsAsync(true);

            // Action
            var result = await _service.InsertAsync(_request);

            // Affirmations
            Assert.True(result);

            _repository.Verify(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Phone>()), Times.Once);
        }

        [Fact(DisplayName = "Insert - Failure")]
        [Trait("Domain", "Services.Phone")]
        public async void InsertAsync_Failure()
        {
            // Scenario
            var _request = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();

            _validator.Setup(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Phone>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>())).Returns(false);
            _repository.Setup(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Phone>()));
            _repository.Setup(x => x.Commit()).ReturnsAsync(true);

            // Action
            var result = await _service.InsertAsync(_request);

            // Affirmations
            Assert.False(result);

            _repository.Verify(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("Domain", "Services.Phone")]
        public async void UpdateAsync_Success()
        {
            // Scenario
            var _request = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();

            _validator.Setup(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Phone>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>())).Returns(true);
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Phone>()));
            _repository.Setup(x => x.Commit()).ReturnsAsync(true);

            // Action
            var result = await _service.UpdateAsync(_request);

            // Affirmations
            Assert.True(result);

            _repository.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Phone>()), Times.Once);
        }

        [Fact(DisplayName = "Update - Failure")]
        [Trait("Domain", "Services.Phone")]
        public async void UpdateAsync_Failure()
        {
            // Scenario
            var _request = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();

            _validator.Setup(x => x.IsValid(It.IsAny<Partner.Domain.Entities.Phone>(), It.IsAny<BuildingBlocks.Core.Validations.Enumerators.ContextService>())).Returns(false);
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Phone>()));
            _repository.Setup(x => x.Commit()).ReturnsAsync(true);

            // Action
            var result = await _service.UpdateAsync(_request);

            // Affirmations
            Assert.False(result);

            _repository.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Phone>()), Times.Never);
        }

        #endregion
    }
}
