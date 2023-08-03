using AutoMapper;
using FizzWare.NBuilder;
using Moq;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.User.Interfaces;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;
using VianaSoft.Partner.App.Services;
using VianaSoft.Partner.Domain.Filters;
using VianaSoft.Partner.Domain.Interfaces;
using Xunit;

namespace VianaSoft.Partner.Tests.Application
{
    public class PartnerApplicationTests
    {
        #region Properties

        private readonly Mock<IMapper> _mapper;
        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IAspNetUser> _aspNetUser;
        private readonly Mock<ILanguageMessage> _message;
        private readonly Mock<IPartnerService> _service;

        public readonly PartnerApplication _application;

        #endregion

        #region Builders
        public PartnerApplicationTests()
        {
            _mapper = new Mock<IMapper>();
            _notifier = new Mock<INotifier>();
            _aspNetUser = new Mock<IAspNetUser>();
            _message = new Mock<ILanguageMessage>();
            _service = new Mock<IPartnerService>();

            _application = new PartnerApplication(_mapper.Object, _notifier.Object, _aspNetUser.Object, _message.Object, _service.Object);
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Get All Paged - Success")]
        [Trait("Application", "Partner")]
        public async void GetAllPagedAsync_Success()
        {
            // Scenario
            var filter1 = Builder<PartnerFilterViewModel>.CreateNew().Build();
            var filter2 = Builder<PartnerFilter>.CreateNew().Build();
            
            var items1 = Builder<Partner.Domain.Entities.Partner>.CreateListOfSize(10).Build();
            var result1 = Builder<ListPage<Partner.Domain.Entities.Partner>>.CreateNew().With(x => x.Items, items1).Build();

            var items2 = Builder<PartnerResponseViewModel>.CreateListOfSize(10).Build();
            var result2 = Builder<ListPage<PartnerResponseViewModel>>.CreateNew().With(x => x.Items, items2).Build();

            _mapper.Setup(x => x.Map<PartnerFilter>(filter1)).Returns(filter2);
            _mapper.Setup(x => x.Map<ListPage<PartnerResponseViewModel>>(result1)).Returns(result2);
            _service.Setup(x => x.GetAllPagedAsync(It.IsAny<PartnerFilter>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetAllPagedAsync(filter1);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result.Items.Any());

            _mapper.Verify(x => x.Map<PartnerFilter>(filter1), Times.Once);
            _mapper.Verify(x => x.Map<ListPage<PartnerResponseViewModel>>(result1), Times.Once);
            _service.Verify(x => x.GetAllPagedAsync(It.IsAny<PartnerFilter>()), Times.Once);
        }

        [Fact(DisplayName = "Get All - Success")]
        [Trait("Application", "Partner")]
        public async void GetAllAsync_Success()
        {
            // Scenario
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateListOfSize(10).Build();
            var result2 = Builder<PartnerResponseViewModel>.CreateListOfSize(10).Build();

            _mapper.Setup(x => x.Map<IEnumerable<PartnerResponseViewModel>>(result1)).Returns(result2);
            _service.Setup(x => x.GetAllAsync()).ReturnsAsync(result1);

            // Action
            var result = await _application.GetAllAsync();

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result.Any());

            _mapper.Verify(x => x.Map<IEnumerable<PartnerResponseViewModel>>(result1), Times.Once);
            _service.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact(DisplayName = "Get by id - Success")]
        [Trait("Application", "Partner")]
        public async void GetByIdAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();
            var result2 = Builder<PartnerResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<PartnerResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByIdAsync(filter);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result != null);

            _mapper.Verify(x => x.Map<PartnerResponseViewModel>(result1), Times.Once);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Get by id - Failure")]
        [Trait("Application", "Partner")]
        public async void GetByIdAsync_Failure()
        {
            // Scenario
            var filter = string.Empty;
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();
            var result2 = Builder<PartnerResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<PartnerResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByIdAsync(filter);

            // Affirmations
            Assert.Null(result);

            _mapper.Verify(x => x.Map<PartnerResponseViewModel>(result1), Times.Never);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact(DisplayName = "Get by document - Success")]
        [Trait("Application", "Partner")]
        public async void GetByDocumentAsync_Success()
        {
            // Scenario
            var filter1 = Builder<DocumentFilterViewModel>.CreateNew().Build();
            var filter2 = Builder<DocumentFilter>.CreateNew().Build();

            var items1 = Builder<Partner.Domain.Entities.Partner>.CreateListOfSize(10).Build();
            var result1 = Builder<ListPage<Partner.Domain.Entities.Partner>>.CreateNew().With(x => x.Items, items1).Build();

            var items2 = Builder<PartnerResponseViewModel>.CreateListOfSize(10).Build();
            var result2 = Builder<ListPage<PartnerResponseViewModel>>.CreateNew().With(x => x.Items, items2).Build();

            _mapper.Setup(x => x.Map<DocumentFilter>(filter1)).Returns(filter2);
            _mapper.Setup(x => x.Map<ListPage<PartnerResponseViewModel>>(result1)).Returns(result2);
            _service.Setup(x => x.GetByDocumentAsync(It.IsAny<DocumentFilter>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByDocumentAsync(filter1);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result.Items.Any());

            _mapper.Verify(x => x.Map<DocumentFilter>(filter1), Times.Once);
            _mapper.Verify(x => x.Map<ListPage<PartnerResponseViewModel>>(result1), Times.Once);
            _service.Verify(x => x.GetByDocumentAsync(It.IsAny<DocumentFilter>()), Times.Once);
        }

        [Fact(DisplayName = "Insert - Success")]
        [Trait("Application", "Partner")]
        public async void InsertAsync_Success()
        {
            // Scenario
            var request = Builder<PartnerRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.ExistDocument(It.IsAny<string>()));
            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>()));
            _service.Setup(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.InsertAsync(request);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.ExistDocument(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Once);
        }

        [Fact(DisplayName = "Insert - Failure")]
        [Trait("Application", "Partner")]
        public async void InsertAsync_Failure()
        {
            // Scenario
            var request = Builder<PartnerRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.ExistDocument(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.InsertAsync(request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.ExistDocument(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("Application", "Partner")]
        public async void Update_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Once);
        }

        [Fact(DisplayName = "Update - Failure")]
        [Trait("Application", "Partner")]
        public async void Update_Failure()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
        }

        [Fact(DisplayName = "Update - NotFound")]
        [Trait("Application", "Partner")]
        public async void Update_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>()));
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
        }

        [Fact(DisplayName = "Update - NotGuid")]
        [Trait("Application", "Partner")]
        public async void Update_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>()));
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
        }

        [Fact(DisplayName = "Enable - Success")]
        [Trait("Application", "Partner")]
        public async void EnableAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Once);
        }

        [Fact(DisplayName = "Enable - NotFound")]
        [Trait("Application", "Partner")]
        public async void EnableAsync_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
        }

        [Fact(DisplayName = "Enable - NotGuid")]
        [Trait("Application", "Partner")]
        public async void EnableAsync_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
        }

        [Fact(DisplayName = "Disable - Success")]
        [Trait("Application", "Partner")]
        public async void DisableAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Once);
        }

        [Fact(DisplayName = "Disable - NotFound")]
        [Trait("Application", "Partner")]
        public async void DisableAsync_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
        }

        [Fact(DisplayName = "Disable - NotGuid")]
        [Trait("Application", "Partner")]
        public async void DisableAsync_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<PartnerUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Partner>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Partner>()), Times.Never);
        }

        #endregion
    }
}
