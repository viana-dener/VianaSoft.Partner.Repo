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

namespace VianaSoft.Contact.Tests.Application
{
    public class ContactApplicationTests
    {
        #region Properties

        private readonly Mock<IMapper> _mapper;
        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IAspNetUser> _aspNetUser;
        private readonly Mock<ILanguageMessage> _message;
        private readonly Mock<IContactService> _service;

        public readonly ContactApplication _application;

        #endregion

        #region Builders
        public ContactApplicationTests()
        {
            _mapper = new Mock<IMapper>();
            _notifier = new Mock<INotifier>();
            _aspNetUser = new Mock<IAspNetUser>();
            _message = new Mock<ILanguageMessage>();
            _service = new Mock<IContactService>();

            _application = new ContactApplication(_mapper.Object, _notifier.Object, _aspNetUser.Object, _message.Object, _service.Object);
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Get All Paged - Success")]
        [Trait("Application", "Contact")]
        public async void GetAllPagedAsync_Success()
        {
            // Scenario
            var filter1 = Builder<ContactFilterViewModel>.CreateNew().Build();
            var filter2 = Builder<ContactFilter>.CreateNew().Build();

            var items1 = Builder<Partner.Domain.Entities.Contact>.CreateListOfSize(10).Build();
            var result1 = Builder<ListPage<Partner.Domain.Entities.Contact>>.CreateNew().With(x => x.Items, items1).Build();

            var items2 = Builder<ContactResponseViewModel>.CreateListOfSize(10).Build();
            var result2 = Builder<ListPage<ContactResponseViewModel>>.CreateNew().With(x => x.Items, items2).Build();

            _mapper.Setup(x => x.Map<ContactFilter>(filter1)).Returns(filter2);
            _mapper.Setup(x => x.Map<ListPage<ContactResponseViewModel>>(result1)).Returns(result2);
            _service.Setup(x => x.GetAllPagedAsync(It.IsAny<ContactFilter>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetAllPagedAsync(filter1);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result.Items.Any());

            _mapper.Verify(x => x.Map<ContactFilter>(filter1), Times.Once);
            _mapper.Verify(x => x.Map<ListPage<ContactResponseViewModel>>(result1), Times.Once);
            _service.Verify(x => x.GetAllPagedAsync(It.IsAny<ContactFilter>()), Times.Once);
        }

        [Fact(DisplayName = "Get All - Success")]
        [Trait("Application", "Contact")]
        public async void GetAllAsync_Success()
        {
            // Scenario
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateListOfSize(10).Build();
            var result2 = Builder<ContactResponseViewModel>.CreateListOfSize(10).Build();

            _mapper.Setup(x => x.Map<IEnumerable<ContactResponseViewModel>>(result1)).Returns(result2);
            _service.Setup(x => x.GetAllAsync()).ReturnsAsync(result1);

            // Action
            var result = await _application.GetAllAsync();

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result.Any());

            _mapper.Verify(x => x.Map<IEnumerable<ContactResponseViewModel>>(result1), Times.Once);
            _service.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact(DisplayName = "Get by id - Success")]
        [Trait("Application", "Contact")]
        public async void GetByIdAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();
            var result2 = Builder<ContactResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<ContactResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByIdAsync(filter);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result != null);

            _mapper.Verify(x => x.Map<ContactResponseViewModel>(result1), Times.Once);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Get by name - Success")]
        [Trait("Application", "Contact")]
        public async void GetByNameAsync_Success()
        {
            // Scenario
            var filter = "Kathundha";
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();
            var result2 = Builder<ContactResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<ContactResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByNameAsync(filter);

            // Affirmations
            Assert.NotNull(result);
            Assert.True(result != null);

            _mapper.Verify(x => x.Map<ContactResponseViewModel>(result1), Times.Once);
            _service.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Get by name - Failure")]
        [Trait("Application", "Contact")]
        public async void GetByNameAsync_Failure()
        {
            // Scenario
            var filter = "";
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();
            var result2 = Builder<ContactResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<ContactResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByNameAsync(filter);

            // Affirmations
            Assert.Null(result);

            _mapper.Verify(x => x.Map<ContactResponseViewModel>(result1), Times.Never);
            _service.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact(DisplayName = "Get by id - Failure")]
        [Trait("Application", "Contact")]
        public async void GetByIdAsync_Failure()
        {
            // Scenario
            var filter = string.Empty;
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();
            var result2 = Builder<ContactResponseViewModel>.CreateNew().Build();

            _mapper.Setup(x => x.Map<ContactResponseViewModel>(result1)).Returns(result2);
            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);

            // Action
            var result = await _application.GetByIdAsync(filter);

            // Affirmations
            Assert.Null(result);

            _mapper.Verify(x => x.Map<ContactResponseViewModel>(result1), Times.Never);
            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact(DisplayName = "Insert - Success")]
        [Trait("Application", "Contact")]
        public async void InsertAsync_Success()
        {
            // Scenario
            var request = Builder<ContactRequestViewModel>.CreateNew().With(x => x.PartnerId, Guid.NewGuid().ToString()).Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>()));
            _service.Setup(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.InsertAsync(request);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByNameAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Once);
        }

        [Fact(DisplayName = "Insert - Failure")]
        [Trait("Application", "Contact")]
        public async void InsertAsync_Failure()
        {
            // Scenario
            var request = Builder<ContactRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.InsertAsync(request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.InsertAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Never);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("Application", "Contact")]
        public async void Update_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Once);
        }

        [Fact(DisplayName = "Update - Failure")]
        [Trait("Application", "Contact")]
        public async void Update_Failure()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Never);
        }

        [Fact(DisplayName = "Update - NotGuid")]
        [Trait("Application", "Contact")]
        public async void Update_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Never);
        }

        [Fact(DisplayName = "Update - NotFound")]
        [Trait("Application", "Contact")]
        public async void Update_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.UpdateAsync(filter, request);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Never);
        }

        [Fact(DisplayName = "Enable - Success")]
        [Trait("Application", "Contact")]
        public async void EnableAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Once);
        }

        [Fact(DisplayName = "Enable - NotFound")]
        [Trait("Application", "Contact")]
        public async void EnableAsync_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Never);
        }

        [Fact(DisplayName = "Enable - NotGuid")]
        [Trait("Application", "Contact")]
        public async void EnableAsync_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.EnableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Never);
        }

        [Fact(DisplayName = "Disable - Success")]
        [Trait("Application", "Contact")]
        public async void DisableAsync_Success()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(result1);
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.True(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Once);
        }

        [Fact(DisplayName = "Disable - NotFound")]
        [Trait("Application", "Contact")]
        public async void DisableAsync_NotFound()
        {
            // Scenario
            var filter = Guid.NewGuid().ToString();
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Never);
        }

        [Fact(DisplayName = "Disable - NotGuid")]
        [Trait("Application", "Contact")]
        public async void DisableAsync_NotGuid()
        {
            // Scenario
            var filter = string.Empty;
            var request = Builder<ContactUpdateRequestViewModel>.CreateNew().Build();
            var result1 = Builder<Partner.Domain.Entities.Contact>.CreateNew().Build();

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>()));
            _service.Setup(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>())).ReturnsAsync(true);

            // Action
            var result = await _application.DisableAsync(filter);

            // Affirmations
            Assert.False(result);

            _service.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Never);
            _service.Verify(x => x.UpdateAsync(It.IsAny<Partner.Domain.Entities.Contact>()), Times.Never);
        }

        #endregion
    }
}
