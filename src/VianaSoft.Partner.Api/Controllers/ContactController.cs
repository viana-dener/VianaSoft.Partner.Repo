using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VianaSoft.BuildingBlocks.Core.Controllers;
using VianaSoft.BuildingBlocks.Core.Identity;
using VianaSoft.BuildingBlocks.Core.Notifications;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Interfaces;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;

namespace VianaSoft.Partner.Api.Controllers
{
    [Route("v1/Contacts")]
    [Authorize]
    public class ContactController : MainControllerBase
    {
        #region Properties

        private readonly INotifier _notifier;
        private readonly ILanguageMessage _message;
        private readonly IContactApplication _application;

        #endregion

        #region Builders

        public ContactController(INotifier notifier,
                                 ILanguageMessage message,
                                 IContactApplication application) : base(notifier)
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
        [ProducesResponseType(typeof(ListPage<ContactResponseViewModel>), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get a paginated list of contacts.")]
        public async Task<IActionResult> GetAllPagedAsync([FromQuery] ContactFilterViewModel filter)
        {
            var result = await _application.GetAllPagedAsync(filter);
            if (result == null) _notifier.Add(_message.NotFound());

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("")]
        [ClaimsAuthorize("BackOffice", "Read")]
        [ProducesResponseType(typeof(IEnumerable<ContactResponseViewModel>), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get all list of contacts.")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _application.GetAllAsync();
            if (result == null) _notifier.Add(_message.NotFound());

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ClaimsAuthorize("BackOffice", "Read")]
        [ProducesResponseType(typeof(ContactResponseViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get by Id")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _application.GetByIdAsync(id);
            if (result == null) _notifier.Add(_message.NotFound("Contact"));

            return CustomResponse(result);
        }

        [HttpPost]
        [Route("")]
        [ClaimsAuthorize("BackOffice", "Create")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ContactRequestViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Insert new")]
        public async Task<IActionResult> InsertAsync([FromBody] ContactRequestViewModel model)
        {
            var result = await _application.InsertAsync(model);
            return CustomResponse(result);
        }

        [HttpPut]
        [Route("{id}")]
        [ClaimsAuthorize("BackOffice", "Update")]
        [ProducesResponseType(typeof(ContactUpdateRequestViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Update by Id")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] ContactUpdateRequestViewModel model)
        {
            var result = await _application.UpdateAsync(id, model);
            return CustomResponse(result);
        }

        [HttpPut]
        [Route("{id}/enable")]
        [ClaimsAuthorize("BackOffice", "Update")]
        [ProducesResponseType(typeof(ContactRequestViewModel), 200)]
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
        [ProducesResponseType(typeof(ContactRequestViewModel), 200)]
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
