using VianaSoft.BuildingBlocks.Core.Pagination;

namespace VianaSoft.Partner.Domain.Filters
{
    public class FilterBase : Pager
    {
        public string? Document { get; set; }
        public string? Name { get; set; }
        public bool? IsEnable { get; set; }
    }
}
