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
    public class ContactApplication : IContactApplication
    {
        #region Properties

        private readonly IMapper _mapper;
        private readonly INotifier _notifier;
        private readonly IAspNetUser _aspNetUser;
        private readonly ILanguageMessage _message;
        private readonly IContactService _service;

        #endregion

        #region Builders

        public ContactApplication(IMapper mapper,
                                  INotifier notifier,
                                  IAspNetUser aspNetUser,
                                  ILanguageMessage message,
                                  IContactService service)
        {
            _mapper = mapper;
            _notifier = notifier;
            _aspNetUser = aspNetUser;
            _message = message;
            _service = service;
        }

        #endregion

        #region Public Methods

        public async Task<ListPage<ContactResponseViewModel>> GetAllPagedAsync(ContactFilterViewModel filter)
        {
            return _mapper.Map<ListPage<ContactResponseViewModel>>(await _service.GetAllPagedAsync(_mapper.Map<ContactFilter>(filter)));
        }
        public async Task<IEnumerable<ContactResponseViewModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ContactResponseViewModel>>(await _service.GetAllAsync());
        }
        public async Task<ContactResponseViewModel> GetByIdAsync(string id)
        {
            if (!await ValidId(id)) return default;

            return _mapper.Map<ContactResponseViewModel>(await _service.GetByIdAsync(id));
        }
        public async Task<ContactResponseViewModel> GetByNameAsync(string name)
        {
            if (!await ValidId(name)) return default;

            return _mapper.Map<ContactResponseViewModel>(await _service.GetByNameAsync(name));
        }

        public async Task<bool> InsertAsync(ContactRequestViewModel contact)
        {
            return await Create(contact);
        }
        public async Task<bool> UpdateAsync(string id, ContactUpdateRequestViewModel contact)
        {
            if (!await ValidId(id)) return false;
            return await Update(id, contact);
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

        private async Task<bool> Create(ContactRequestViewModel request)
        {
            if (await Exists(request.Name))
            {
                _notifier.Add(_message.Exists("Name"));
                return false;
            }

            var parther = new Domain.Entities.Contact(Guid.Parse(request.PartnerId), request.Name, request.BusinessEmail, request.PersonalEmail, _aspNetUser.Name);

            return await _service.InsertAsync(parther);
        }
        private async Task<bool> Update(string id, ContactUpdateRequestViewModel request)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _notifier.Add(_message.NotFound("Contact"));
                return false;
            }
            result.Update(request.BusinessEmail, request.PersonalEmail, _aspNetUser.Name);

            return await _service.UpdateAsync(result);
        }
        private async Task<bool> Enable(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _notifier.Add(_message.NotFound("Contact"));
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
                _notifier.Add(_message.NotFound("Contact"));
                return false;
            }
            result?.Disable(_aspNetUser.Name);

            return await _service.UpdateAsync(result);
        }
        private async Task<bool> Exists(string name)
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
