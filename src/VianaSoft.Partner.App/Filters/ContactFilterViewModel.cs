using VianaSoft.BuildingBlocks.Core.Pagination;

namespace VianaSoft.Partner.App.Filters
{
    public class ContactFilterViewModel : Pager
    {
        public string PartnerId { get; set; }
        public string Name { get; set; }
        public bool? IsEnable { get; set; }
    }
}
