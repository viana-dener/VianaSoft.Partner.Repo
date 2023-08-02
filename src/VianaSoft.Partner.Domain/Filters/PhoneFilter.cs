using VianaSoft.BuildingBlocks.Core.Pagination;

namespace VianaSoft.Partner.Domain.Filters
{
    public class PhoneFilter : Pager
    {
        public string ContactId { get; set; }
        public string Number { get; set; }
        public bool? IsWhatsapp { get; set; }
        public bool? IsEnable { get; set; }
    }
}
