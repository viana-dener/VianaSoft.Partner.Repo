using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.Domain.Filters;

namespace VianaSoft.Partner.Domain.Interfaces
{
    public interface IPhoneRepository
    {
        Task<ListPage<Entities.Phone>> GetAllPagedAsync(PhoneFilter filter);
        Task<IEnumerable<Entities.Phone>> GetAllAsync();
        Task<Entities.Phone> GetByIdAsync(Guid id);
        Task<Entities.Phone> GetByNumberAsync(string number);
        Task<Entities.Phone> ExistsAsync(string ddiCode, string dddCode, string number);

        void InsertAsync(Entities.Phone entity);
        void UpdateAsync(Entities.Phone entity);
        void DeleteAsync(Entities.Phone entity);
        Task<bool> Commit();
    }
}
