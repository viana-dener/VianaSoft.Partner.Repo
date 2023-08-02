namespace VianaSoft.Partner.App.Models.Response
{
    public class PartnerResponseViewModel
    {
        public string Id { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ContactResponseViewModel> Contacts { get; set; }
        public bool IsEnable { get; set; }

    }
}
