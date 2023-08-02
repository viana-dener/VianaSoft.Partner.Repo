using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Validations.Interfaces;
using VianaSoft.Partner.Domain.Filters;
using VianaSoft.Partner.Domain.Interfaces;

namespace VianaSoft.Partner.Domain.Services
{
    public class ContactService : IContactService
    {
        #region Properties

        private readonly INotifier _notifier;
        private readonly IMyValidator<Entities.Contact> _validator;
        private readonly ILanguageMessage _message;
        private readonly IContactRepository _repository;

        #endregion

        #region Builders

        public ContactService(INotifier notifier,
                              IMyValidator<Entities.Contact> validator,
                              ILanguageMessage message,
                              IContactRepository repository)
        {
            _notifier = notifier;
            _validator = validator;
            _message = message;
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<ListPage<Entities.Contact>> GetAllPagedAsync(ContactFilter filter)
        {
            return await _repository.GetAllPagedAsync(filter);
        }
        public async Task<IEnumerable<Entities.Contact>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Entities.Contact> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(Guid.Parse(id));
        }
        public async Task<Entities.Contact> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<bool> InsertAsync(Entities.Contact partner)
        {
            if (!_validator.IsValid(partner))
            {
                _notifier.Add(_message.NotValid("Contact"));
                return default;
            }

            _repository.InsertAsync(partner);

            return await _repository.Commit();
        }
        public async Task<bool> UpdateAsync(Entities.Contact partner)
        {
            if (!_validator.IsValid(partner))
            {
                _notifier.Add(_message.NotValid("Contact"));
                return default;
            }

            _repository.UpdateAsync(partner);

            return await _repository.Commit();
        }

        #endregion
    }
}
