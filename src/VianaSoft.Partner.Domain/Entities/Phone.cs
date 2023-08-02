using VianaSoft.BuildingBlocks.Core.DomainObjects;

namespace VianaSoft.Partner.Domain.Entities
{
    public class Phone : Entity
    {
        #region Properties
        public string DDICode { get; private set; }
        public string DDDCode { get; private set; }
        public string Number { get; private set; }
        public bool IsCellPhone { get; private set; }
        public bool IsWhatsapp { get; private set; }
        public bool IsTelegram { get; private set; }
        
        public bool IsEnable { get; private set; }
        public bool IsExclude { get; private set; }
        public string CreateBy { get; private set; }
        public DateTime CreateAt { get; private set; }
        public string UpdateBy { get; private set; }
        public DateTime? UpdateAt { get; private set; }

        // EF Relation
        public Guid ContactId { get; private set; }

        #endregion

        #region Builders

        protected Phone()
        {

        }

        public Phone(Guid contactId, string dDICode, string dDDCode, string number, bool isCellPhone, bool isWhatsapp, bool isTelegram, string createdBy = null)
        {
            ContactId = contactId;
            DDICode = dDICode;
            DDDCode = dDDCode;
            Number = number;
            IsCellPhone = isCellPhone;
            IsWhatsapp = isWhatsapp;
            IsTelegram = isTelegram;
            IsEnable = true;
            IsExclude = false;
            CreateBy = createdBy;
            CreateAt = DateTime.Now;
        }
        #endregion

        #region Public Methods
        public void AddCreatedBy(string createdBy)
        {
            CreateBy = createdBy;
            CreateAt = DateTime.Now;
        }

        public void Update(string dDICode, string dDDCode, string number, bool isCellPhone, bool isWhatsapp, bool isTelegram, string updateBy)
        {
            DDICode = dDICode;
            DDDCode = dDDCode;
            Number = number;
            IsCellPhone = isCellPhone;
            IsWhatsapp = isWhatsapp;
            IsTelegram = isTelegram;
            UpdateBy = updateBy;
            UpdateAt = DateTime.Now;
        }

        public void Enable(string updateBy)
        {
            IsEnable = true;
            UpdateBy = updateBy;
            UpdateAt = DateTime.Now;
        }

        public void Disable(string updateBy)
        {
            IsEnable = false;
            UpdateBy = updateBy;
            UpdateAt = DateTime.Now;
        }

        public void Delete(string updateBy)
        {
            IsExclude = true;
            UpdateBy = updateBy;
            UpdateAt = DateTime.Now;
        }

        #endregion
    }
}
