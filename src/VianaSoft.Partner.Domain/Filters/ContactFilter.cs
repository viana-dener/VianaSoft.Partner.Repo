using VianaSoft.BuildingBlocks.Core.Pagination;

namespace VianaSoft.Partner.Domain.Filters
{
    public class ContactFilter : Pager
    {
        public string PartnerId { get; set; }
        public string Name { get; set; }
        public bool? IsEnable { get; set; }
    }
}
