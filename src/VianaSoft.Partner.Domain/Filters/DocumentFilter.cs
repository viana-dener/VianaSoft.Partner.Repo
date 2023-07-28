using VianaSoft.BuildingBlocks.Core.Pagination;

namespace VianaSoft.Partner.Domain.Filters
{
    public class DocumentFilter : Pager
    {
        public string? Document { get; set; }
    }
}
