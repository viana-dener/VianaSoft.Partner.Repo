using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VianaSoft.Partner.Domain.Entities;

namespace VianaSoft.Partner.Data.Mappings
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PartnerId).HasColumnType("uniqueidentifier");
            builder.Property(x => x.Name).HasColumnType("varchar(255)").IsRequired();
            builder.Property(x => x.BusinessEmail).HasColumnType("varchar(255)");
            builder.Property(x => x.PersonalEmail).HasColumnType("varchar(255)");

            builder.Property(x => x.IsEnable).HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsExclude).HasColumnType("bit").IsRequired();
            builder.Property(x => x.CreateBy).HasColumnType("varchar(255)").IsRequired();
            builder.Property(x => x.CreateAt).HasColumnType("datetime2(7)").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.UpdateBy).HasColumnType("varchar(255)");
            builder.Property(x => x.UpdateAt).HasColumnType("datetime2(7)");
        }

        #endregion
    }
}
