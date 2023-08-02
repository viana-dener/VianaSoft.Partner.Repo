using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VianaSoft.BuildingBlocks.Core.Filters.Interfaces;
using VianaSoft.BuildingBlocks.Core.Filters.Models;
using VianaSoft.BuildingBlocks.Core.Pagination;
using VianaSoft.BuildingBlocks.Core.Repository;
using VianaSoft.Partner.Data.Context;
using VianaSoft.Partner.Domain.Filters;
using VianaSoft.Partner.Domain.Interfaces;

namespace VianaSoft.Partner.Data.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        #region Properties

        private readonly IDynamicFilter _filter;
        private readonly DataContext _context;

        #endregion

        #region Builders

        public PartnerRepository(IDynamicFilter filter, DataContext context)
        {
            _filter = filter;
            _context = context;
        }

        #endregion

        #region Public Methods

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ListPage<Domain.Entities.Partner>> GetAllPagedAsync(PartnerFilter filter)
        {
            return ExistFilters(filter) ? await WithFilter(filter) : await NoFilter(filter);
        }
        public async Task<IEnumerable<Domain.Entities.Partner>> GetAllAsync()
        {
            return await GetDataAsync();
        }
        public async Task<Domain.Entities.Partner> GetByIdAsync(Guid id)
        {
            return await _context.Partners.AsNoTracking().Include(x => x.Contacts).ThenInclude(x => x.Phones).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ListPage<Domain.Entities.Partner>> GetByDocumentAsync(DocumentFilter filter)
        {
            return await WithFilter(filter);
        }
        public async Task<Domain.Entities.Partner> ExistDocument(string document)
        {
            return await _context.Partners.AsNoTracking().Where(x => x.Document == document).FirstOrDefaultAsync();
        }
        public async Task<Domain.Entities.Partner> GetByNameAsync(string name)
        {
            return await _context.Partners.AsNoTracking().Where((x => x.Name.ToLower() == name.ToLower())).FirstOrDefaultAsync();
        }

        public void InsertAsync(Domain.Entities.Partner entity)
        {
            _context.Partners.Add(entity);
        }
        public void UpdateAsync(Domain.Entities.Partner entity)
        {
            _context.Partners.Update(entity);
        }
        public void DeleteAsync(Domain.Entities.Partner entity)
        {
            _context.Partners.Remove(entity);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<bool> Commit()
        {
            return await _context.Commit();
        }

        #endregion

        #region Private Methods

        private async Task<ListPage<Domain.Entities.Partner>> WithFilter(PartnerFilter filterBase)
        {
            int count = _context.Partners.AsNoTracking().Count();
            var filter = CreateFilter(filterBase);
            var sortExpr = CreateOrder(filterBase);
            var partners = await GetData(filterBase, filter, sortExpr);

            return new ListPage<Domain.Entities.Partner>(partners, filterBase.Page, filterBase.ItemsPerPage, count, (count + filterBase.ItemsPerPage - 1) / filterBase.ItemsPerPage);
        }
        private async Task<ListPage<Domain.Entities.Partner>> WithFilter(DocumentFilter filterBase)
        {
            var filter = CreateFilter(filterBase);
            int count = _context.Partners.Where(filter).AsNoTracking().Count();

            var sortExpr = CreateOrder(filterBase);
            var partners = await GetData(filterBase, filter, sortExpr);

            return new ListPage<Domain.Entities.Partner>(partners, filterBase.Page, filterBase.ItemsPerPage, count, (count + filterBase.ItemsPerPage - 1) / filterBase.ItemsPerPage);
        }
        private async Task<ListPage<Domain.Entities.Partner>> NoFilter(PartnerFilter filterBase)
        {
            int count = _context.Partners.AsNoTracking().Count();
            var sortExpr = CreateOrder(filterBase);
            var partners = await GetData(filterBase, sortExpr);

            return new ListPage<Domain.Entities.Partner>(partners, filterBase.Page, filterBase.ItemsPerPage, count, (count + filterBase.ItemsPerPage - 1) / filterBase.ItemsPerPage);
        }
        private async Task<IEnumerable<Domain.Entities.Partner>> GetDataAsync()
        {
            return await _context.Partners.AsNoTracking().Include(x => x.Contacts).ThenInclude(x => x.Phones).ToListAsync();
        }
        private async Task<List<Domain.Entities.Partner>> GetData(PartnerFilter filterBase, Expression<Func<Domain.Entities.Partner, bool>> filter, Expression<Func<Domain.Entities.Partner, object>> sortExpr)
        {
            List<Domain.Entities.Partner> partners;
            if (filterBase.OrderType.ToLower().Equals("asc"))
                partners = await _context.Partners.AsNoTracking().Include(x => x.Contacts).ThenInclude(x => x.Phones).Where(filter).OrderBy(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();
            else
                partners = await _context.Partners.AsNoTracking().Include(x => x.Contacts).ThenInclude(x => x.Phones).Where(filter).OrderByDescending(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();

            return partners;
        }
        private async Task<List<Domain.Entities.Partner>> GetData(PartnerFilter filterBase, Expression<Func<Domain.Entities.Partner, object>> sortExpr)
        {
            List<Domain.Entities.Partner> partners;
            if (filterBase.OrderType.ToLower().Equals("asc"))
                partners = await _context.Partners.AsNoTracking().Include(x => x.Contacts).ThenInclude(x => x.Phones).OrderBy(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();
            else
                partners = await _context.Partners.AsNoTracking().Include(x => x.Contacts).ThenInclude(x => x.Phones).OrderByDescending(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();

            return partners;
        }
        private async Task<List<Domain.Entities.Partner>> GetData(DocumentFilter filterBase, Expression<Func<Domain.Entities.Partner, bool>> filter, Expression<Func<Domain.Entities.Partner, object>> sortExpr)
        {
            List<Domain.Entities.Partner> partners;
            if (filterBase.OrderType.ToLower().Equals("asc"))
                partners = await _context.Partners.AsNoTracking().Include(x => x.Contacts).ThenInclude(x => x.Phones).Where(filter).OrderBy(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();
            else
                partners = await _context.Partners.AsNoTracking().Include(x => x.Contacts).ThenInclude(x => x.Phones).Where(filter).OrderByDescending(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();

            return partners;
        }
        private Expression<Func<Domain.Entities.Partner, bool>> CreateFilter(PartnerFilter filterBase)
        {
            List<FilterItem> filters = new();
            if (!string.IsNullOrWhiteSpace(filterBase.Document))
                filters.Add(new FilterItem { Property = "Document", FilterType = "contains", Value = filterBase.Document });
            if (!string.IsNullOrWhiteSpace(filterBase.Name))
                filters.Add(new FilterItem { Property = "Name", FilterType = "contains", Value = filterBase.Name });
            if (filterBase.IsEnable.HasValue)
                filters.Add(new FilterItem { Property = "Enable", FilterType = "equals", Value = filterBase.IsEnable });

            return _filter.FromFiltroItemList<Domain.Entities.Partner>(filters);
        }
        private Expression<Func<Domain.Entities.Partner, bool>> CreateFilter(DocumentFilter filterBase)
        {
            List<FilterItem> filters = new();
            if (filterBase.Document != null)
                filters.Add(new FilterItem { Property = "Document", FilterType = "contains", Value = filterBase.Document });

            return _filter.FromFiltroItemList<Domain.Entities.Partner>(filters);
        }
        private static Expression<Func<Domain.Entities.Partner, object>> CreateOrder(PartnerFilter filterBase)
        {
            var param = Expression.Parameter(typeof(Domain.Entities.Partner), "u");
            var property = Expression.Convert(Expression.Property(param, filterBase.OrderBy), typeof(object));
            return Expression.Lambda<Func<Domain.Entities.Partner, object>>(property, param);
        }
        private static Expression<Func<Domain.Entities.Partner, object>> CreateOrder(DocumentFilter filterBase)
        {
            var param = Expression.Parameter(typeof(Domain.Entities.Partner), "u");
            var property = Expression.Convert(Expression.Property(param, filterBase.OrderBy), typeof(object));
            return Expression.Lambda<Func<Domain.Entities.Partner, object>>(property, param);
        }
        private static bool ExistFilters(PartnerFilter filter)
        {
            return !string.IsNullOrWhiteSpace(filter.Document) ||
                   !string.IsNullOrWhiteSpace(filter.Name) ||
                   filter.IsEnable.HasValue;
        }

        #endregion
    }
}
