using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;

namespace VianaSoft.Partner.App.Interfaces
{
    public interface IContactApplication
    {
        Task<ListPage<ContactResponseViewModel>> GetAllPagedAsync(ContactFilterViewModel filter);
        Task<IEnumerable<ContactResponseViewModel>> GetAllAsync();
        Task<ContactResponseViewModel> GetByIdAsync(string id);
        Task<ContactResponseViewModel> GetByNameAsync(string name);
        Task<bool> InsertAsync(ContactRequestViewModel contact);
        Task<bool> UpdateAsync(string id, ContactUpdateRequestViewModel contact);
        Task<bool> EnableAsync(string id);
        Task<bool> DisableAsync(string id);
    }
}
