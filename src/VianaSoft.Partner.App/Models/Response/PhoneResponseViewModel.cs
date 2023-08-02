namespace VianaSoft.Partner.App.Models.Response
{
    public class PhoneResponseViewModel
    {
        public string Id { get; set; }
        public string DDICode { get; private set; }
        public string DDDCode { get; private set; }
        public string Number { get; private set; }
        public bool IsCellPhone { get; private set; }
        public bool IsWhatsapp { get; private set; }
        public bool IsTelegram { get; private set; }
        public bool IsEnable { get; private set; }
    }
}
