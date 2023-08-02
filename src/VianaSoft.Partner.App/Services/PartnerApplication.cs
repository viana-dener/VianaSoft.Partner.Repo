using AutoMapper;
using VianaSoft.BuildingBlocks.Core.Extensions;
using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.User.Interfaces;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Interfaces;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;
using VianaSoft.Partner.Domain.Filters;
using VianaSoft.Partner.Domain.Interfaces;

namespace VianaSoft.Partner.App.Services
{
    public class PartnerApplication : IPartnerApplication
    {
        #region Properties

        private readonly IMapper _mapper;
        private readonly INotifier _notifier;
        private readonly IAspNetUser _aspNetUser;
        private readonly ILanguageMessage _message;
        private readonly IPartnerService _service;

        #endregion

        #region Builders

        public PartnerApplication(IMapper mapper,
                                  INotifier notifier,
                                  IAspNetUser aspNetUser,
                                  ILanguageMessage message,
                                  IPartnerService service)
        {
            _mapper = mapper;
            _notifier = notifier;
            _aspNetUser = aspNetUser;
            _message = message;
            _service = service;
        }

        #endregion

        #region Public Methods

        public async Task<ListPage<PartnerResponseViewModel>> GetAllPagedAsync(PartnerFilterViewModel filter)
        {
            return _mapper.Map<ListPage<PartnerResponseViewModel>>(await _service.GetAllPagedAsync(_mapper.Map<PartnerFilter>(filter)));
        }
        public async Task<IEnumerable<PartnerResponseViewModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<PartnerResponseViewModel>>(await _service.GetAllAsync());
        }
        public async Task<PartnerResponseViewModel> GetByIdAsync(string id)
        {
            if (!await ValidId(id)) return default;

            return _mapper.Map<PartnerResponseViewModel>(await _service.GetByIdAsync(id));
        }
        public async Task<ListPage<PartnerResponseViewModel>> GetByDocumentAsync(DocumentFilterViewModel filter)
        {
            return _mapper.Map<ListPage<PartnerResponseViewModel>>(await _service.GetByDocumentAsync(_mapper.Map<DocumentFilter>(filter)));
        }

        public async Task<bool> InsertAsync(PartnerRequestViewModel request)
        {
            return await CreatePartner(request);
        }
        public async Task<bool> UpdateAsync(string id, PartnerUpdateRequestViewModel request)
        {
            if (!await ValidId(id)) return false;
            return await Update(id, request);
        }
        public async Task<bool> EnableAsync(string id)
        {
            if (!await ValidId(id)) return false;

            return await Enable(id);
        }
        public async Task<bool> DisableAsync(string id)
        {
            if (!await ValidId(id)) return false;

            return await Disable(id);
        }

        #endregion

        #region Private Methods

        private async Task<bool> CreatePartner(PartnerRequestViewModel request)
        {
            if (await ExistDocument(request.Document) || await ExistName(request.Name))
            {
                _notifier.Add(_message.Exists("Document or Name"));
                return false;
            }

            var parther = new Domain.Entities.Partner(request.Document, request.Name, request.Description, _aspNetUser.Name);

            return await _service.InsertAsync(parther);
        }
        private async Task<bool> Update(string id, PartnerUpdateRequestViewModel request)
        {
            if (await ExistName(request.Name))
            {
                _notifier.Add(_message.Exists("Name"));
                return false;
            }

            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _notifier.Add(_message.NotFound("Partner"));
                return false;
            }
            result.Update(request.Name, request.Description, _aspNetUser.Name);

            return await _service.UpdateAsync(result);
        }
        private async Task<bool> Enable(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _notifier.Add(_message.NotFound("Partner"));
                return false;
            }
            result?.Enable(_aspNetUser.Name);

            return await _service.UpdateAsync(result);
        }
        private async Task<bool> Disable(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _notifier.Add(_message.NotFound("Partner"));
                return false;
            }
            result?.Disable(_aspNetUser.Name);

            return await _service.UpdateAsync(result);
        }
        private async Task<bool> ExistDocument(string document)
        {
            var country = await _service.ExistDocument(document);
            if (country != null)
                return true;

            return false;
        }
        private async Task<bool> ExistName(string name)
        {
            var country = await _service.GetByNameAsync(name);
            if (country != null)
                return true;

            return false;
        }
        private async Task<bool> ValidId(string id)
        {
            if (!await id.IsGuid())
            {
                _notifier.Add(_message.NotGuid());
                return false;
            }

            return true;
        }
        #endregion
    }
}
