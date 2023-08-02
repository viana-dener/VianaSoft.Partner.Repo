using VianaSoft.BuildingBlocks.Core.Pagination;

namespace VianaSoft.Partner.App.Filters
{
    public class PhoneFilterViewModel : Pager
    {
        public string ContactId { get; set; }
        public string Number { get; set; }
        public bool? IsWhatsapp { get; set; }
        public bool? IsEnable { get; set; }
    }
}
