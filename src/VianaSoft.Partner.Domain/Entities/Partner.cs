using VianaSoft.BuildingBlocks.Core.DomainObjects;

namespace VianaSoft.Partner.Domain.Entities
{
    public class Partner : Entity, IAggregateRoot
    {
        #region Properties

        public string? Document { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsEnable { get; private set; }
        public bool IsExclude { get; private set; }
        public string? CreateBy { get; private set; }
        public DateTime CreateAt { get; private set; }
        public string? UpdateBy { get; private set; }
        public DateTime? UpdateAt { get; private set; }

        #endregion

        #region Builders

        public Partner() { }

        public Partner(Guid id, string? document, string? name, string? description, string? createBy = null)
        {
            Id = id;
            Document = document;
            Name = name;
            Description = description;
            IsEnable = true;
            IsExclude = false;
            CreateBy = createBy;
            CreateAt = DateTime.Now;
        }

        #endregion

        #region Public Methods

        public string GetByDocument()
        {
            return Document;
        }

        public void AddCreatedBy(string? createdBy)
        {
            CreateBy = createdBy;
            CreateAt = DateTime.Now;
        }

        public void UpdatePartner(string? document, string? name, string? description, string? updateBy)
        {
            Document = document;
            Name = name;
            Description = description;
            UpdateBy = updateBy;
            UpdateAt = DateTime.Now;
        }

        public void EnablePartner(string? updateBy)
        {
            IsEnable = true;
            UpdateBy = updateBy;
            UpdateAt = DateTime.Now;
        }

        public void DisablePartner(string? updateBy)
        {
            IsEnable = false;
            UpdateBy = updateBy;
            UpdateAt = DateTime.Now;
        }

        public void DeletePartner(string? updateBy)
        {
            IsExclude = true;
            UpdateBy = updateBy;
            UpdateAt = DateTime.Now;
        }

        #endregion
    }
}
