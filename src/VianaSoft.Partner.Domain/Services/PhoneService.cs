using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Validations.Interfaces;
using VianaSoft.Partner.Domain.Filters;
using VianaSoft.Partner.Domain.Interfaces;

namespace VianaSoft.Partner.Domain.Services
{
    public class PhoneService : IPhoneService
    {
        #region Properties

        private readonly INotifier _notifier;
        private readonly IMyValidator<Entities.Phone> _validator;
        private readonly ILanguageMessage _message;
        private readonly IPhoneRepository _repository;

        #endregion

        #region Builders

        public PhoneService(INotifier notifier,
                              IMyValidator<Entities.Phone> validator,
                              ILanguageMessage message,
                              IPhoneRepository repository)
        {
            _notifier = notifier;
            _validator = validator;
            _message = message;
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<ListPage<Entities.Phone>> GetAllPagedAsync(PhoneFilter filter)
        {
            return await _repository.GetAllPagedAsync(filter);
        }
        public async Task<IEnumerable<Entities.Phone>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Entities.Phone> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(Guid.Parse(id));
        }
        public async Task<Entities.Phone> GetByNumberAsync(string number)
        {
            return await _repository.GetByNumberAsync(number);
        }
        public async Task<Entities.Phone> ExistsAsync(string ddiCode, string dddCode, string number)
        {
            return await _repository.ExistsAsync(ddiCode, dddCode, number);
        }

        public async Task<bool> InsertAsync(Entities.Phone partner)
        {
            if (!_validator.IsValid(partner))
            {
                _notifier.Add(_message.NotValid("Phone"));
                return default;
            }

            _repository.InsertAsync(partner);

            return await _repository.Commit();
        }
        public async Task<bool> UpdateAsync(Entities.Phone partner)
        {
            if (!_validator.IsValid(partner))
            {
                _notifier.Add(_message.NotValid("Phone"));
                return default;
            }

            _repository.UpdateAsync(partner);

            return await _repository.Commit();
        }

        #endregion
    }
}
