using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;

namespace VianaSoft.Partner.App.Interfaces
{
    public interface IPartnerApplication
    {
        Task<ListPage<PartnerResponseViewModel>> GetAllPagedAsync(FilterIViewModel filter);
        Task<IEnumerable<PartnerResponseViewModel>> GetAllAsync();
        Task<PartnerResponseViewModel> GetByIdAsync(string id);
        Task<ListPage<PartnerResponseViewModel>> GetByDocumentAsync(DocumentFilterIViewModel filter);
        Task<bool> InsertAsync(PartnerRequestViewModel partner);
        Task<bool> UpdateAsync(string id, PartnerRequestViewModel partner);
        Task<bool> EnableAsync(string id);
        Task<bool> DisableAsync(string id);
    }
}
