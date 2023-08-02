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
    public class PhoneApplication : IPhoneApplication
    {
        #region Properties

        private readonly IMapper _mapper;
        private readonly INotifier _notifier;
        private readonly IAspNetUser _aspNetUser;
        private readonly ILanguageMessage _message;
        private readonly IPhoneService _service;

        #endregion

        #region Builders

        public PhoneApplication(IMapper mapper,
                                INotifier notifier,
                                IAspNetUser aspNetUser,
                                ILanguageMessage message,
                                IPhoneService service)
        {
            _mapper = mapper;
            _notifier = notifier;
            _aspNetUser = aspNetUser;
            _message = message;
            _service = service;
        }

        #endregion

        #region Public Methods

        public async Task<ListPage<PhoneResponseViewModel>> GetAllPagedAsync(PhoneFilterViewModel filter)
        {
            return _mapper.Map<ListPage<PhoneResponseViewModel>>(await _service.GetAllPagedAsync(_mapper.Map<PhoneFilter>(filter)));
        }
        public async Task<IEnumerable<PhoneResponseViewModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<PhoneResponseViewModel>>(await _service.GetAllAsync());
        }
        public async Task<PhoneResponseViewModel> GetByIdAsync(string id)
        {
            if (!await ValidId(id)) return default;

            return _mapper.Map<PhoneResponseViewModel>(await _service.GetByIdAsync(id));
        }
        public async Task<PhoneResponseViewModel> GetByNumberAsync(string number)
        {
            return _mapper.Map<PhoneResponseViewModel>(await _service.GetByNumberAsync(number));
        }

        public async Task<bool> InsertAsync(PhoneRequestViewModel contact)
        {
            return await Create(contact);
        }
        public async Task<bool> UpdateAsync(string id, PhoneUpdateRequestViewModel contact)
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

        private async Task<bool> Create(PhoneRequestViewModel request)
        {
            if (await Exists(request.DDICode, request.DDDCode, request.Number))
            {
                _notifier.Add(_message.Exists("Number"));
                return false;
            }

            var phone = new Domain.Entities.Phone(Guid.Parse(request.ContactId), request.DDICode, request.DDDCode, request.Number, request.IsCellPhone, request.IsWhatsapp, request.IsTelegram, _aspNetUser.Name);

            return await _service.InsertAsync(phone);
        }
        private async Task<bool> Update(string id, PhoneUpdateRequestViewModel request)
        {
            if (await Exists(request.DDICode, request.DDDCode, request.Number))
            {
                _notifier.Add(_message.Exists("Number"));
                return false;
            }

            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _notifier.Add(_message.NotFound("Phone"));
                return false;
            }
            result.Update(request.DDICode, request.DDDCode, request.Number, request.IsCellPhone, request.IsWhatsapp, request.IsTelegram, _aspNetUser.Name);

            return await _service.UpdateAsync(result);
        }
        private async Task<bool> Enable(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _notifier.Add(_message.NotFound("Phone"));
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
                _notifier.Add(_message.NotFound("Phone"));
                return false;
            }
            result?.Disable(_aspNetUser.Name);

            return await _service.UpdateAsync(result);
        }
        private async Task<bool> Exists(string ddiCode, string dddCode, string number)
        {
            var country = await _service.ExistsAsync(ddiCode, dddCode,number);
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
