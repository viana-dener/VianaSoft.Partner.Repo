using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VianaSoft.BuildingBlocks.Core.Controllers;
using VianaSoft.BuildingBlocks.Core.Identity;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Notifications;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Interfaces;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;

namespace VianaSoft.Partner.Api.Controllers
{
    [Route("v1/Phones")]
    [Authorize]
    public class PhoneController : MainControllerBase
    {
        #region Properties

        private readonly INotifier _notifier;
        private readonly ILanguageMessage _message;
        private readonly IPhoneApplication _application;

        #endregion

        #region Builders

        public PhoneController(INotifier notifier,
                               ILanguageMessage message,
                               IPhoneApplication application) : base(notifier)
        {
            _notifier = notifier;
            _message = message;
            _application = application;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("paged")]
        [ClaimsAuthorize("BackOffice", "Read")]
        [ProducesResponseType(typeof(ListPage<PhoneResponseViewModel>), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get a paginated list of phones.")]
        public async Task<IActionResult> GetAllPagedAsync([FromQuery] PhoneFilterViewModel filter)
        {
            var result = await _application.GetAllPagedAsync(filter);
            if (result == null) _notifier.Add(_message.NotFound());

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("")]
        [ClaimsAuthorize("BackOffice", "Read")]
        [ProducesResponseType(typeof(IEnumerable<PhoneResponseViewModel>), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get all list of phones.")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _application.GetAllAsync();
            if (result == null) _notifier.Add(_message.NotFound());

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ClaimsAuthorize("BackOffice", "Read")]
        [ProducesResponseType(typeof(PhoneResponseViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get by Id")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _application.GetByIdAsync(id);
            if (result == null) _notifier.Add(_message.NotFound("Phone"));

            return CustomResponse(result);
        }

        [HttpPost]
        [Route("")]
        [ClaimsAuthorize("BackOffice", "Create")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PhoneRequestViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Insert new")]
        public async Task<IActionResult> InsertAsync([FromBody] PhoneRequestViewModel model)
        {
            var result = await _application.InsertAsync(model);
            return CustomResponse(result);
        }

        [HttpPut]
        [Route("{id}")]
        [ClaimsAuthorize("BackOffice", "Update")]
        [ProducesResponseType(typeof(PhoneUpdateRequestViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Update by Id")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] PhoneUpdateRequestViewModel model)
        {
            var result = await _application.UpdateAsync(id, model);
            return CustomResponse(result);
        }

        [HttpPut]
        [Route("{id}/enable")]
        [ClaimsAuthorize("BackOffice", "Update")]
        [ProducesResponseType(typeof(PhoneRequestViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Enable by Id")]
        public async Task<IActionResult> EnableAsync(string id)
        {
            var result = await _application.EnableAsync(id);
            return CustomResponse(result);
        }

        [HttpPut]
        [Route("{id}/disable")]
        [ClaimsAuthorize("BackOffice", "Update")]
        [ProducesResponseType(typeof(PhoneRequestViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Disable by Id")]
        public async Task<IActionResult> DisableAsync(string id)
        {
            var result = await _application.DisableAsync(id);
            return CustomResponse(result);
        }

        #endregion
    }
}
