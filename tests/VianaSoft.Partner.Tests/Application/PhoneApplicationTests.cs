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
    public class PhoneApplicationTests
    {
        #region Properties

        private readonly Mock<IMapper> _mapper;
        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IAspNetUser> _aspNetUser;
        private readonly Mock<ILanguageMessage> _message;
        private readonly Mock<IPhoneService> _service;

        public readonly PhoneApplication _application;

        #endregion

        #region Builders
        public PhoneApplicationTests()
        {
            _mapper = new Mock<IMapper>();
            _notifier = new Mock<INotifier>();
            _aspNetUser = new Mock<IAspNetUser>();
            _message = new Mock<ILanguageMessage>();
            _service = new Mock<IPhoneService>();

            _application = new PhoneApplication(_mapper.Object, _notifier.Object, _aspNetUser.Object, _message.Object, _service.Object);
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Get All Paged - Success")]
        [Trait("Application", "Phone")]
        public async void GetAllPagedAsync_Success()
        {
            // Scenario
            var filter1 = Builder<PhoneFilterViewModel>.CreateNew().Build();
            var filter2 = Builder<PhoneFilter>.CreateNew().Build();

            var items1 = Builder<Partner.Domain.Entities.Phone>.CreateListOfSize(10).Build();
            var result1 = Builder<ListPage<Partner.Domain.Entities.Phone>>.CreateNew().With(x => x.Items, items1).Build();

            var items2 = Builder<PhoneResponseViewModel>.CreateListOfSize(10).Build();
            var result2 = Builder<ListPage<PhoneResponseViewModel>>.CreateNew().With(x => x.Items, items2).Build();

            _mapper.Setup(x => x.Map<PhoneFilter>(filter1)).Returns(filter2);
            _mapper.Setup(x => x.Map<ListPage<PhoneResponseViewModel>>(result1)).Returns(result2);
            _service.Setup(x => x.GetAllPagedAsync(It.IsAny<PhoneFilter>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetAllPagedAsync(filter1);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result.Items.Any());

            _mapper.Verify(x => x.Map<PhoneFilter>(filter1), Times.Once);
            _mapper.Verify(x => x.Map<ListPage<PhoneResponseViewModel>>(result1), Times.Once);
            _service.Verify(x => x.GetAllPagedAsync(It.IsAny<PhoneFilter>()), Times.Once);
        }

        [Fact(DisplayName = "Get All - Success")]
        [Trait("Application", "Phone")]
        public async void GetAllAsync_Success()
        {
            // Scenario
            var result1 = Builder<Partner.Domain.Entities.Phone>.CreateListOfSize(10).Build();
            var result2 = Builder<PhoneResponseViewModel>.CreateListOfSize(10).Build();

            _mapper.Setup(x => x.Map<IEnumerable<PhoneResponseViewModel>>(result1)).Returns(result2);
            _service.Setup(x => x.GetAllAsync()).ReturnsAsync(result1);

            // Action
            var result = await _application.GetAllAsync();

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result.Any());

            _mapper.Verify(x => x.Map<IEnumerable<PhoneResponseViewModel>>(result1), Times.Once);
            _service.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact(DisplayName = "Get by id - Success")]
        [Trait("Application", "Phone")]
        public async void GetByIdAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var result1 = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();
            var result2 = Builder<PhoneResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<PhoneResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByIdAsync(filter);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result != null);

            _mapper.Verify(x => x.Map<PhoneResponseViewModel>(result1), Times.Once);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Get by number - Success")]
        [Trait("Application", "Phone")]
        public async void GetByNumberAsync_Success()
        {
            // Scenario
            var filter = "Kathundha";
            var result1 = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();
            var result2 = Builder<PhoneResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<PhoneResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByNumberAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByNumberAsync(filter);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result != null);

            _mapper.Verify(x => x.Map<PhoneResponseViewModel>(result1), Times.Once);
            _service.Verify(x => x.GetByNumberAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Get by number - Failure")]
        [Trait("Application", "Phone")]
        public async void GetByNumberAsync_Failure()
        {
            // Scenario
            var filter = "";
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();
            var result2 = Builder<PhoneResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<PhoneResponseViewModel>(result1));
            _service.Setup(x => x.GetByNumberAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByNumberAsync(filter);

            // Affirmations
            Assert.Null(result);

            _mapper.Verify(x => x.Map<PhoneResponseViewModel>(result1), Times.Once);
            _service.Verify(x => x.GetByNumberAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Get by id - Failure")]
        [Trait("Application", "Phone")]
        public async void GetByIdAsync_Failure()
        {
            // Scenario
            var filter = string.Empty;
            var result1 = Builder<Partner.Domain.Entities.Phone>.CreateNew().Build();
            var result2 = Builder<PhoneResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<PhoneResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByIdAsync(filter);

            // Affirmations
            Assert.Null(result);

            _mapper.Verify(x => x.Map<PhoneResponseViewModel>(result1), Times.Never);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact(DisplayName = "Insert - Success")]
        [Trait("Application", "Phone")]
        public async void InsertAsync_Success()
        {
            // Scenario
            var request = Builder<PhoneRequestViewModel>.CreateNew().With(x => x.ContactId, Guid.NewGuid().ToString()).Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _service.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.InsertAsync(request);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.InsertAsync(It.IsAny<Domain.Entities.Phone>()), Times.Once);
        }

        [Fact(DisplayName = "Insert - Failure")]
        [Trait("Application", "Phone")]
        public async void InsertAsync_Failure()
        {
            // Scenario
            var request = Builder<PhoneRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.InsertAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.InsertAsync(request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.InsertAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("Application", "Phone")]
        public async void Update_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Once);
        }

        [Fact(DisplayName = "Update - Failure")]
        [Trait("Application", "Phone")]
        public async void Update_Failure()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Update - Exists")]
        [Trait("Application", "Phone")]
        public async void Update_Exists()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Update - NotGuid")]
        [Trait("Application", "Phone")]
        public async void Update_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Update - NotFound")]
        [Trait("Application", "Phone")]
        public async void Update_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Enable - Success")]
        [Trait("Application", "Phone")]
        public async void EnableAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Once);
        }

        [Fact(DisplayName = "Enable - NotFound")]
        [Trait("Application", "Phone")]
        public async void EnableAsync_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Enable - NotGuid")]
        [Trait("Application", "Phone")]
        public async void EnableAsync_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Disable - Success")]
        [Trait("Application", "Phone")]
        public async void DisableAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Once);
        }

        [Fact(DisplayName = "Disable - NotFound")]
        [Trait("Application", "Phone")]
        public async void DisableAsync_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        [Fact(DisplayName = "Disable - NotGuid")]
        [Trait("Application", "Phone")]
        public async void DisableAsync_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Domain.Entities.Phone>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Domain.Entities.Phone>()), Times.Never);
        }

        #endregion
    }
}
