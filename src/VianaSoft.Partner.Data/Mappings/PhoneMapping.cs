using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VianaSoft.Partner.Domain.Entities;

namespace VianaSoft.Partner.Data.Mappings
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phones");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ContactId).HasColumnType("uniqueidentifier");
            builder.Property(x => x.DDICode).HasColumnType("varchar(5)").IsRequired();
            builder.Property(x => x.DDDCode).HasColumnType("varchar(5)");
            builder.Property(x => x.Number).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.IsCellPhone).HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsWhatsapp).HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsTelegram).HasColumnType("bit").IsRequired();

            builder.Property(x => x.IsEnable).HasColumnType("bit").IsRequired();
            builder.Property(x => x.IsExclude).HasColumnType("bit").IsRequired();
            builder.Property(x => x.CreateBy).HasColumnType("varchar(255)").IsRequired();
            builder.Property(x => x.CreateAt).ValueGeneratedOnAdd().HasColumnType("datetime2(7)").IsRequired();
            builder.Property(x => x.UpdateBy).HasColumnType("varchar(255)");
            builder.Property(x => x.UpdateAt).HasColumnType("datetime2(7)");

        }

        #endregion
    }
}
