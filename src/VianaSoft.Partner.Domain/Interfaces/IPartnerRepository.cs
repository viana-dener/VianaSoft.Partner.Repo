using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.Domain.Filters;

namespace VianaSoft.Partner.Domain.Interfaces
{
    public interface IPartnerRepository
    {
        Task<ListPage<Entities.Partner>> GetAllPagedAsync(ContactFilter filter);
        Task<IEnumerable<Entities.Partner>> GetAllAsync();
        Task<Entities.Partner> GetByIdAsync(Guid id);
        Task<ListPage<Entities.Partner>> GetByDocumentAsync(DocumentFilter filter);
        Task<Entities.Partner> ExistDocument(string document);
        Task<Entities.Partner> GetByNameAsync(string name);

        void InsertAsync(Entities.Partner entity);
        void UpdateAsync(Entities.Partner entity);
        void DeleteAsync(Entities.Partner entity);
        Task<bool> Commit();
    }
}
