using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.Domain.Filters;

namespace VianaSoft.Partner.Domain.Interfaces
{
    public interface IPhoneService
    {
        Task<ListPage<Entities.Phone>> GetAllPagedAsync(PhoneFilter filter);
        Task<IEnumerable<Entities.Phone>> GetAllAsync();
        Task<Entities.Phone> GetByIdAsync(string id);
        Task<Entities.Phone> GetByNumberAsync(string number);
        Task<Entities.Phone> ExistsAsync(string ddiCode, string dddCode, string number);
        Task<bool> InsertAsync(Entities.Phone partner);
        Task<bool> UpdateAsync(Entities.Phone partner);
    }
}
