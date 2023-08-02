﻿using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Tests.Mocks;
using VianaSoft.Partner.Api.Controllers;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Interfaces;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;
using Xunit;

namespace VianaSoft.Partner.Tests.Api
{
    public class PhoneControllerTests : MockAnswerTest
    {
        #region Properties

        private readonly Mock<INotifier> _notifier;
        private readonly Mock<ILanguageMessage> _message;
        private readonly Mock<IPhoneApplication> _app;
        private readonly PhoneController _controller;

        #endregion

        #region Builders
        public PhoneControllerTests()
        {
            _notifier = new Mock<INotifier>();
            _message = new Mock<ILanguageMessage>();
            _app = new Mock<IPhoneApplication>();

            _controller = new PhoneController(_notifier.Object, _message.Object, _app.Object);
        }

        #endregion

        #region Public Methods

        [Fact(DisplayName = "Get All Paged - Success")]
        [Trait("API", "Phone")]
        public async void GetAllPagedAsync_Success()
        {
            // Scenario
            var _filter = Builder<PhoneFilterViewModel>.CreateNew().Build();
            var _result = Builder<ListPage<PhoneResponseViewModel>>.CreateNew().Build();

            _app.Setup(x => x.GetAllPagedAsync(It.IsAny<PhoneFilterViewModel>())).ReturnsAsync(_result);
            _notifier.Setup(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()))
                .Returns(MockAnswer(true, 200));

            // Action
            var result = await _controller.GetAllPagedAsync(_filter);
            var okResult = result as ObjectResult;

            // Affirmations
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            _app.Verify(x => x.GetAllPagedAsync(It.IsAny<PhoneFilterViewModel>()), Times.Once);
            _notifier.Verify(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()), Times.Once);
        }

        [Fact(DisplayName = "Get All - Success")]
        [Trait("API", "Phone")]
        public async void GetAllAsync_Success()
        {
            // Scenario
            var _result = Builder<PhoneResponseViewModel>.CreateListOfSize(10).Build();

            _app.Setup(x => x.GetAllAsync()).ReturnsAsync(_result);
            _notifier.Setup(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()))
                .Returns(MockAnswer(true, 200));

            // Action
            var result = await _controller.GetAllAsync();
            var okResult = result as ObjectResult;

            // Affirmations
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            _app.Verify(x => x.GetAllAsync(), Times.Once);
            _notifier.Verify(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()), Times.Once);
        }

        [Fact(DisplayName = "Get by id - Success")]
        [Trait("API", "Phone")]
        public async void GetByIdAsync_Success()
        {
            // Scenario
            var _filter = Guid.NewGuid().ToString();
            var _result = Builder<PhoneResponseViewModel>.CreateNew().Build();

            _app.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(_result);
            _notifier.Setup(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()))
                .Returns(MockAnswer(true, 200));

            // Action
            var result = await _controller.GetByIdAsync(_filter);
            var okResult = result as ObjectResult;

            // Affirmations
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            _app.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _notifier.Verify(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()), Times.Once);
        }

        [Fact(DisplayName = "Insert - Success")]
        [Trait("API", "Phone")]
        public async void InsertAsync_Success()
        {
            // Scenario
            var _model = Builder<PhoneRequestViewModel>.CreateNew().Build();

            _app.Setup(x => x.InsertAsync(It.IsAny<PhoneRequestViewModel>())).ReturnsAsync(true);
            _notifier.Setup(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()))
                .Returns(MockAnswer(true, 200));

            // Action
            var result = await _controller.InsertAsync(_model);
            var okResult = result as ObjectResult;

            // Affirmations
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            _app.Verify(x => x.InsertAsync(It.IsAny<PhoneRequestViewModel>()), Times.Once);
            _notifier.Verify(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()), Times.Once);
        }

        [Fact(DisplayName = "Update - Success")]
        [Trait("API", "Phone")]
        public async void UpdateAsync_Success()
        {
            // Scenario
            var _id = Guid.NewGuid().ToString();
            var _model = Builder<PhoneUpdateRequestViewModel>.CreateNew().Build();

            _app.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<PhoneUpdateRequestViewModel>())).ReturnsAsync(true);
            _notifier.Setup(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()))
                .Returns(MockAnswer(true, 200));

            // Action
            var result = await _controller.UpdateAsync(_id, _model);
            var okResult = result as ObjectResult;

            // Affirmations
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            _app.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<PhoneUpdateRequestViewModel>()), Times.Once);
            _notifier.Verify(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()), Times.Once);
        }

        [Fact(DisplayName = "Enable - Success")]
        [Trait("API", "Phone")]
        public async void EnableAsync_Success()
        {
            // Scenario
            var _id = Guid.NewGuid().ToString();

            _app.Setup(x => x.EnableAsync(It.IsAny<string>())).ReturnsAsync(true);
            _notifier.Setup(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()))
                .Returns(MockAnswer(true, 200));

            // Action
            var result = await _controller.EnableAsync(_id);
            var okResult = result as ObjectResult;

            // Affirmations
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            _app.Verify(x => x.EnableAsync(It.IsAny<string>()), Times.Once);
            _notifier.Verify(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()), Times.Once);
        }

        [Fact(DisplayName = "Disable - Success")]
        [Trait("API", "Phone")]
        public async void DisableAsync_Success()
        {
            // Scenario
            var _id = Guid.NewGuid().ToString();

            _app.Setup(x => x.DisableAsync(It.IsAny<string>())).ReturnsAsync(true);
            _notifier.Setup(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()))
                .Returns(MockAnswer(true, 200));

            // Action
            var result = await _controller.DisableAsync(_id);
            var okResult = result as ObjectResult;

            // Affirmations
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            _app.Verify(x => x.DisableAsync(It.IsAny<string>()), Times.Once);
            _notifier.Verify(x => x.CustomResponse(It.IsAny<object>(), It.IsAny<HttpStatusCode>()), Times.Once);
        }

        #endregion
    }
}
