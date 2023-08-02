namespace VianaSoft.Partner.App.Models.Response
{
    public class ContactResponseViewModel
    {
        public string Id { get; set; }
        public string Name { get; private set; }
        public string BusinessEmail { get; private set; }
        public string PersonalEmail { get; private set; }
        public IEnumerable<PhoneResponseViewModel> Phones { get; set; }
        public bool IsEnable { get; private set; }
    }
}
