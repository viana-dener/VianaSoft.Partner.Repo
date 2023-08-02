using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VianaSoft.BuildingBlocks.Core.Identity;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Notifications;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Interfaces;
using VianaSoft.BuildingBlocks.Core.Controllers;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.Partner.App.Models.Response;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.App.Models.Request;

namespace VianaSoft.Partner.Api.Controllers
{
    [Route("v1/Partners")]
    [Authorize]
    public class PartnerController : MainControllerBase
    {
        #region Properties

        private readonly INotifier _notifier;
        private readonly ILanguageMessage _message;
        private readonly IPartnerApplication _application;

        #endregion

        #region Builders

        public PartnerController(INotifier notifier,
                                 ILanguageMessage message,
                                 IPartnerApplication application) : base(notifier)
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
        [ProducesResponseType(typeof(ListPage<PartnerResponseViewModel>), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get a paginated list of partners.")]
        public async Task<IActionResult> GetAllPagedAsync([FromQuery] ContactFilterViewModel filter)
        {
            var result = await _application.GetAllPagedAsync(filter);
            if (result == null) _notifier.Add(_message.NotFound());

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("")]
        [ClaimsAuthorize("BackOffice", "Read")]
        [ProducesResponseType(typeof(IEnumerable<PartnerResponseViewModel>), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get all list of Partner.")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _application.GetAllAsync();
            if (result == null) _notifier.Add(_message.NotFound());

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("{id}")]
        [ClaimsAuthorize("BackOffice", "Read")]
        [ProducesResponseType(typeof(PartnerResponseViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get by Id")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _application.GetByIdAsync(id);
            if (result == null) _notifier.Add(_message.NotFound("Partner"));

            return CustomResponse(result);
        }

        [HttpGet]
        [Route("Document")]
        [ClaimsAuthorize("BackOffice", "Read")]
        [ProducesResponseType(typeof(PartnerResponseViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Get by Document")]
        public async Task<IActionResult> GetByDocumentAsync([FromQuery] DocumentFilterViewModel filter)
        {
            var result = await _application.GetByDocumentAsync(filter);
            if (result == null) _notifier.Add(_message.NotFound("Partner"));

            return CustomResponse(result);
        }

        [HttpPost]
        [Route("")]
        [ClaimsAuthorize("BackOffice", "Create")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PartnerRequestViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Insert new")]
        public async Task<IActionResult> InsertAsync([FromBody] PartnerRequestViewModel model)
        {
            var result = await _application.InsertAsync(model);
            return CustomResponse(result);
        }

        [HttpPut]
        [Route("{id}")]
        [ClaimsAuthorize("BackOffice", "Update")]
        [ProducesResponseType(typeof(PartnerUpdateRequestViewModel), 200)]
        [ProducesResponseType(typeof(MessageErrors), 400)]
        [SwaggerOperation(Summary = "Update by Id")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] PartnerUpdateRequestViewModel model)
        {
            var result = await _application.UpdateAsync(id, model);
            return CustomResponse(result);
        }

        [HttpPut]
        [Route("{id}/enable")]
        [ClaimsAuthorize("BackOffice", "Update")]
        [ProducesResponseType(typeof(PartnerRequestViewModel), 200)]
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
        [ProducesResponseType(typeof(PartnerRequestViewModel), 200)]
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