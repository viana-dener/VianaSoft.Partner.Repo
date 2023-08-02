using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.Partner.Domain.Filters;

namespace VianaSoft.Partner.Domain.Interfaces
{
    public interface IPartnerService
    {
        Task<ListPage<Entities.Partner>> GetAllPagedAsync(PartnerFilter filter);
        Task<IEnumerable<Entities.Partner>> GetAllAsync();
        Task<Entities.Partner> GetByIdAsync(string id);
        Task<ListPage<Entities.Partner>> GetByDocumentAsync(DocumentFilter filter);
        Task<Entities.Partner> GetByNameAsync(string document);
        Task<Entities.Partner> ExistDocument(string document);
        Task<bool> InsertAsync(Entities.Partner partner);
        Task<bool> UpdateAsync(Entities.Partner partner);
    }
}
