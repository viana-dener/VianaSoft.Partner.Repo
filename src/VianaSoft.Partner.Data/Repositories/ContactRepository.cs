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
    public class ContactRepository : IContactRepository
    {
        #region Properties

        private readonly IDynamicFilter _filter;
        private readonly DataContext _context;

        #endregion

        #region Builders

        public ContactRepository(IDynamicFilter filter, DataContext context)
        {
            _filter = filter;
            _context = context;
        }

        #endregion

        #region Public Methods

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ListPage<Domain.Entities.Contact>> GetAllPagedAsync(ContactFilter filter)
        {
            return ExistFilters(filter) ? await WithFilter(filter) : await NoFilter(filter);
        }
        public async Task<IEnumerable<Domain.Entities.Contact>> GetAllAsync()
        {
            return await GetDataAsync();
        }
        public async Task<Domain.Entities.Contact> GetByIdAsync(Guid id)
        {
            return await _context.Contacts.AsNoTracking().Include(x => x.Phones).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Domain.Entities.Contact> GetByNameAsync(string name)
        {
            return await _context.Contacts.AsNoTracking().Where((x => x.Name.ToLower() == name.ToLower())).FirstOrDefaultAsync();
        }

        public void InsertAsync(Domain.Entities.Contact entity)
        {
            _context.Contacts.Add(entity);
        }
        public void UpdateAsync(Domain.Entities.Contact entity)
        {
            _context.Contacts.Update(entity);
        }
        public void DeleteAsync(Domain.Entities.Contact entity)
        {
            _context.Contacts.Remove(entity);
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

        private async Task<ListPage<Domain.Entities.Contact>> WithFilter(ContactFilter contactFilter)
        {
            int count = _context.Contacts.AsNoTracking().Count();
            var filter = CreateFilter(contactFilter);
            var sortExpr = CreateOrder(contactFilter);
            var contacts = await GetDataAsync(contactFilter, filter, sortExpr);

            return new ListPage<Domain.Entities.Contact>(contacts, contactFilter.Page, contactFilter.ItemsPerPage, count, (count + contactFilter.ItemsPerPage - 1) / contactFilter.ItemsPerPage);
        }
        private async Task<ListPage<Domain.Entities.Contact>> NoFilter(ContactFilter contactFilter)
        {
            int count = _context.Contacts.AsNoTracking().Count();
            var sortExpr = CreateOrder(contactFilter);
            var contacts = await GetDataAsync(contactFilter, sortExpr);

            return new ListPage<Domain.Entities.Contact>(contacts, contactFilter.Page, contactFilter.ItemsPerPage, count, (count + contactFilter.ItemsPerPage - 1) / contactFilter.ItemsPerPage);
        }
        private async Task<IEnumerable<Domain.Entities.Contact>> GetDataAsync()
        {
            return await _context.Contacts.AsNoTracking().Include(x => x.Phones).ToListAsync();
        }
        private async Task<List<Domain.Entities.Contact>> GetDataAsync(ContactFilter filterBase, Expression<Func<Domain.Entities.Contact, bool>> filter, Expression<Func<Domain.Entities.Contact, object>> sortExpr)
        {
            List<Domain.Entities.Contact> partners;
            if (filterBase.OrderType.ToLower().Equals("asc"))
                partners = await _context.Contacts.AsNoTracking().Include(x => x.Phones).Where(filter).OrderBy(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();
            else
                partners = await _context.Contacts.AsNoTracking().Include(x => x.Phones).Where(filter).OrderByDescending(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();

            return partners;
        }
        private async Task<List<Domain.Entities.Contact>> GetDataAsync(ContactFilter filterBase, Expression<Func<Domain.Entities.Contact, object>> sortExpr)
        {
            List<Domain.Entities.Contact> addresses;
            if (filterBase.OrderType.ToLower().Equals("asc"))
                addresses = await _context.Contacts.AsNoTracking().Include(x => x.Phones).OrderBy(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();
            else
                addresses = await _context.Contacts.AsNoTracking().Include(x => x.Phones).OrderByDescending(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();

            return addresses;
        }

        private Expression<Func<Domain.Entities.Contact, bool>> CreateFilter(ContactFilter filter)
        {
            List<FilterItem> filters = new();
            if (!string.IsNullOrWhiteSpace(filter.PartnerId))
                filters.Add(new FilterItem { Property = "PartnerId", FilterType = "contains", Value = filter.PartnerId });
            if (!string.IsNullOrWhiteSpace(filter.Name))
                filters.Add(new FilterItem { Property = "Name", FilterType = "contains", Value = filter.Name });
            if (filter.IsEnable.HasValue)
                filters.Add(new FilterItem { Property = "Enable", FilterType = "equals", Value = filter.IsEnable });

            return _filter.FromFiltroItemList<Domain.Entities.Contact>(filters);
        }
        private static Expression<Func<Domain.Entities.Contact, object>> CreateOrder(ContactFilter filterBase)
        {
            var param = Expression.Parameter(typeof(Domain.Entities.Contact), "u");
            var property = Expression.Convert(Expression.Property(param, filterBase.OrderBy), typeof(object));
            return Expression.Lambda<Func<Domain.Entities.Contact, object>>(property, param);
        }
        private static bool ExistFilters(ContactFilter filter)
        {
            return !string.IsNullOrWhiteSpace(filter.PartnerId) ||
                   !string.IsNullOrWhiteSpace(filter.Name) ||
                   filter.IsEnable.HasValue;
        }

        #endregion
    }
}
