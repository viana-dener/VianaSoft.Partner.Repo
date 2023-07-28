using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace VianaSoft.Partner.Data.Mappings
{
    public class PartnerMapping : IEntityTypeConfiguration<Domain.Entities.Partner>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<Domain.Entities.Partner> builder)
        {
            builder.ToTable("Partners");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Document).HasColumnType("varchar(14)").IsRequired();
            builder.Property(p => p.Name).HasColumnType("varchar(255)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("varchar(500)").IsRequired();

            builder.Property(p => p.IsEnable).HasColumnType("bit").IsRequired();
            builder.Property(p => p.IsExclude).HasColumnType("bit").IsRequired();
            builder.Property(p => p.CreateBy).HasColumnType("varchar(255)").IsRequired();
            builder.Property(p => p.CreateAt).HasColumnType("datetime2(7)").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.UpdateBy).HasColumnType("varchar(255)");
            builder.Property(p => p.UpdateAt).HasColumnType("datetime2(7)");
        }

        #endregion
    }
}
