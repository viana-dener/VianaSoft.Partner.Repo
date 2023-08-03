using System.Reflection.Metadata;
using VianaSoft.BuildingBlocks.Core.DomainObjects;

namespace VianaSoft.Partner.Domain.Entities
{
    public class Contact : Entity
    {
        #region Properties

        public string Name { get; private set; }
        public string BusinessEmail { get; private set; }
        public string PersonalEmail { get; private set; }

        public bool IsEnable { get; private set; }
        public bool IsExclude { get; private set; }
        public string CreateBy { get; private set; }
        public DateTime CreateAt { get; private set; }
        public string UpdateBy { get; private set; }
        public DateTime? UpdateAt { get; private set; }


        // EF Relation
        public Guid PartnerId { get; private set; }
        public IEnumerable<Phone> Phones { get; protected set; }

        #endregion

        #region Builders

        protected Contact() { }

        public Contact(Guid partnerId, string name, string businessEmail, string personalEmail, string createdBy = null)
        {
            PartnerId = partnerId;
            Name = name;
            BusinessEmail = businessEmail;
            PersonalEmail = personalEmail;
            IsEnable = true;
            IsExclude = false;
            CreateBy = createdBy;
            CreateAt = DateTime.Now;
        }

        #endregion

        #region Public Methods

        public void AddCreateBy(string createdBy)
        {
            CreateBy = createdBy;
            CreateAt = DateTime.Now;
        }

        public void Update(string businessEmail, string personalEmail, string updateBy)
        {
            BusinessEmail = businessEmail;
            PersonalEmail = personalEmail;
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
