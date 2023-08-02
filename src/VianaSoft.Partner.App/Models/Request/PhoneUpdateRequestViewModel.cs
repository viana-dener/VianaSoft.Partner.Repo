namespace VianaSoft.Partner.App.Models.Request
{
    public class PhoneUpdateRequestViewModel
    {
        public string DDICode { get; set; }
        public string DDDCode { get; set; }
        public string Number { get; set; }
        public bool IsCellPhone { get; set; }
        public bool IsWhatsapp { get; set; }
        public bool IsTelegram { get; set; }
    }
}
