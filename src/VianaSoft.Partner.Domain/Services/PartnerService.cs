using VianaSoft.BuildingBlocks.Core.Notifications.Interfaces;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Resources.Interfaces;
using VianaSoft.BuildingBlocks.Core.Validations.Interfaces;
using VianaSoft.Partner.Domain.Filters;
using VianaSoft.Partner.Domain.Interfaces;

namespace VianaSoft.Partner.Domain.Services
{
    public class PartnerService : IPartnerService
    {
        #region Properties

        private readonly INotifier _notifier;
        private readonly IMyValidator<Entities.Partner> _validator;
        private readonly ILanguageMessage _message;
        private readonly IPartnerRepository _repository;

        #endregion

        #region Builders

        public PartnerService(INotifier notifier,
                              IMyValidator<Entities.Partner> validator,
                              ILanguageMessage message,
                              IPartnerRepository repository)
        {
            _notifier = notifier;
            _validator = validator;
            _message = message;
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<ListPage<Entities.Partner>> GetAllPagedAsync(PartnerFilter filter)
        {
            return await _repository.GetAllPagedAsync(filter);
        }
        public async Task<IEnumerable<Entities.Partner>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Entities.Partner> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(Guid.Parse(id));
        }
        public async Task<ListPage<Entities.Partner>> GetByDocumentAsync(DocumentFilter filter)
        {
            return await _repository.GetByDocumentAsync(filter);
        }
        public async Task<Entities.Partner> GetByNameAsync(string document)
        {
            return await _repository.GetByNameAsync(document);
        }
        public async Task<Entities.Partner> ExistDocument(string document)
        {
            return await _repository.ExistDocument(document);
        }

        public async Task<bool> InsertAsync(Entities.Partner request)
        {
            if (!_validator.IsValid(request))
            {
                _notifier.Add(_message.NotValid("Partner"));
                return default;
            }

            _repository.InsertAsync(request);

            return await _repository.Commit();
        }
        public async Task<bool> UpdateAsync(Entities.Partner partner)
        {
            if (!_validator.IsValid(partner))
            {
                _notifier.Add(_message.NotValid("Partner"));
                return default;
            }

            _repository.UpdateAsync(partner);

            return await _repository.Commit();
        }

        #endregion
    }
}
