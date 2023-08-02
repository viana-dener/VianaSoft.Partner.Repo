using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.Domain.Filters;

namespace VianaSoft.Partner.Domain.Interfaces
{
    public interface IContactService
    {
        Task<ListPage<Entities.Contact>> GetAllPagedAsync(ContactFilter filter);
        Task<IEnumerable<Entities.Contact>> GetAllAsync();
        Task<Entities.Contact> GetByIdAsync(string id);
        Task<Entities.Contact> GetByNameAsync(string name);
        Task<bool> InsertAsync(Entities.Contact partner);
        Task<bool> UpdateAsync(Entities.Contact partner);
    }
}
