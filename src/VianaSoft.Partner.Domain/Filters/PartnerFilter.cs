using VianaSoft.BuildingBlocks.Core.Pagination;

namespace VianaSoft.Partner.Domain.Filters
{
    public class PartnerFilter : Pager
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public bool? Enable { get; set; }
    }
}
