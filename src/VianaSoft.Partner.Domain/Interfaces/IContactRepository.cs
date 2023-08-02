using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.Domain.Filters;

namespace VianaSoft.Partner.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<ListPage<Entities.Contact>> GetAllPagedAsync(ContactFilter filter);
        Task<IEnumerable<Entities.Contact>> GetAllAsync();
        Task<Entities.Contact> GetByIdAsync(Guid id);
        Task<Entities.Contact> GetByNameAsync(string name);

        void InsertAsync(Entities.Contact entity);
        void UpdateAsync(Entities.Contact entity);
        void DeleteAsync(Entities.Contact entity);
        Task<bool> Commit();
    }
}
