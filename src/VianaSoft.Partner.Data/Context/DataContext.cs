using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using VianaSoft.BuildingBlocks.Core.Repository;

namespace VianaSoft.Partner.Data.Context
{
    public class DataContext : DbContext, IUnitOfWork
    {
        #region Builders

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        #endregion

        #region Public Methods

        public DbSet<Domain.Entities.Partner> Partners { get; set; }
        public DbSet<Domain.Entities.Contact> Contacts { get; set; }
        public DbSet<Domain.Entities.Phone> Phones { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        #endregion

        #region Pretected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(255)");

            foreach (var relarionShip in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetForeignKeys()))
                relarionShip.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

        #endregion
    }
}
