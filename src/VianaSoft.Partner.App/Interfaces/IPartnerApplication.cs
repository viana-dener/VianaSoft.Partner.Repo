using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.App.Filters;
using VianaSoft.Partner.App.Models.Request;
using VianaSoft.Partner.App.Models.Response;

namespace VianaSoft.Partner.App.Interfaces
{
    public interface IPartnerApplication
    {
        Task<ListPage<PartnerResponseViewModel>> GetAllPagedAsync(PartnerFilterViewModel filter);
        Task<IEnumerable<PartnerResponseViewModel>> GetAllAsync();
        Task<PartnerResponseViewModel> GetByIdAsync(string id);
        Task<ListPage<PartnerResponseViewModel>> GetByDocumentAsync(DocumentFilterViewModel filter);
        Task<bool> InsertAsync(PartnerRequestViewModel partner);
        Task<bool> UpdateAsync(string id, PartnerUpdateRequestViewModel partner);
        Task<bool> EnableAsync(string id);
        Task<bool> DisableAsync(string id);
    }
}
