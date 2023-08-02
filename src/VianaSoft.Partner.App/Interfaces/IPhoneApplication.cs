using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;

namespace VianaSoft.Partner.App.Interfaces
{
    public interface IPhoneApplication
    {
        Task<ListPage<PhoneResponseViewModel>> GetAllPagedAsync(PhoneFilterViewModel filter);
        Task<IEnumerable<PhoneResponseViewModel>> GetAllAsync();
        Task<PhoneResponseViewModel> GetByIdAsync(string id);
        Task<PhoneResponseViewModel> GetByNumberAsync(string number);
        Task<bool> InsertAsync(PhoneRequestViewModel contact);
        Task<bool> UpdateAsync(string id, PhoneUpdateRequestViewModel contact);
        Task<bool> EnableAsync(string id);
        Task<bool> DisableAsync(string id);
    }
}
