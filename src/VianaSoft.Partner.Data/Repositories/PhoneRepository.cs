using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Math.EC.Rfc7748;
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
    public class PhoneRepository : IPhoneRepository
    {
        #region Properties

        private readonly IDynamicFilter _filter;
        private readonly DataContext _context;

        #endregion

        #region Builders

        public PhoneRepository(IDynamicFilter filter, DataContext context)
        {
            _filter = filter;
            _context = context;
        }

        #endregion

        #region Public Methods

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ListPage<Domain.Entities.Phone>> GetAllPagedAsync(PhoneFilter filter)
        {
            return ExistFilters(filter) ? await WithFilter(filter) : await NoFilter(filter);
        }
        public async Task<IEnumerable<Domain.Entities.Phone>> GetAllAsync()
        {
            return await GetDataAsync();
        }
        public async Task<Domain.Entities.Phone> GetByIdAsync(Guid id)
        {
            return await _context.Phones.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Domain.Entities.Phone> GetByNumberAsync(string number)
        {
            return await _context.Phones.AsNoTracking().Where((x => x.Number.ToLower() == number.ToLower())).FirstOrDefaultAsync();
        }
        public async Task<Domain.Entities.Phone> ExistsAsync(string ddiCode, string dddCode, string number)
        {
            return await _context.Phones.AsNoTracking().FirstOrDefaultAsync(x => x.DDICode == ddiCode &&
                                                                                 x.DDDCode == dddCode &&
                                                                                 x.Number == number);
        }

        public void InsertAsync(Domain.Entities.Phone entity)
        {
            _context.Phones.Add(entity);
        }
        public void UpdateAsync(Domain.Entities.Phone entity)
        {
            _context.Phones.Update(entity);
        }
        public void DeleteAsync(Domain.Entities.Phone entity)
        {
            _context.Phones.Remove(entity);
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

        private async Task<ListPage<Domain.Entities.Phone>> WithFilter(PhoneFilter phoneFilter)
        {
            int count = _context.Phones.AsNoTracking().Count();
            var filter = CreateFilter(phoneFilter);
            var sortExpr = CreateOrder(phoneFilter);
            var contacts = await GetData(phoneFilter, filter, sortExpr);

            return new ListPage<Domain.Entities.Phone>(contacts, phoneFilter.Page, phoneFilter.ItemsPerPage, count, (count + phoneFilter.ItemsPerPage - 1) / phoneFilter.ItemsPerPage);
        }
        private async Task<ListPage<Domain.Entities.Phone>> NoFilter(PhoneFilter phoneFilter)
        {
            int count = _context.Phones.AsNoTracking().Count();
            var sortExpr = CreateOrder(phoneFilter);
            var contacts = await GetData(phoneFilter, sortExpr);

            return new ListPage<Domain.Entities.Phone>(contacts, phoneFilter.Page, phoneFilter.ItemsPerPage, count, (count + phoneFilter.ItemsPerPage - 1) / phoneFilter.ItemsPerPage);
        }
        private async Task<IEnumerable<Domain.Entities.Phone>> GetDataAsync()
        {
            return await _context.Phones.AsNoTracking().ToListAsync();
        }
        private async Task<List<Domain.Entities.Phone>> GetData(PhoneFilter filterBase, Expression<Func<Domain.Entities.Phone, bool>> filter, Expression<Func<Domain.Entities.Phone, object>> sortExpr)
        {
            List<Domain.Entities.Phone> partners;
            if (filterBase.OrderType.ToLower().Equals("asc"))
                partners = await _context.Phones.AsNoTracking().Where(filter).OrderBy(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();
            else
                partners = await _context.Phones.AsNoTracking().Where(filter).OrderByDescending(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();

            return partners;
        }
        private async Task<List<Domain.Entities.Phone>> GetData(PhoneFilter filterBase, Expression<Func<Domain.Entities.Phone, object>> sortExpr)
        {
            List<Domain.Entities.Phone> addresses;
            if (filterBase.OrderType.ToLower().Equals("asc"))
                addresses = await _context.Phones.AsNoTracking().OrderBy(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();
            else
                addresses = await _context.Phones.AsNoTracking().OrderByDescending(sortExpr).Skip((filterBase.Page - 1) * filterBase.ItemsPerPage).Take(filterBase.ItemsPerPage).ToListAsync();

            return addresses;
        }

        private Expression<Func<Domain.Entities.Phone, bool>> CreateFilter(PhoneFilter filter)
        {
            List<FilterItem> filters = new();
            if (!string.IsNullOrWhiteSpace(filter.ContactId))
                filters.Add(new FilterItem { Property = "ContactId", FilterType = "contains", Value = filter.ContactId });
            if (!string.IsNullOrWhiteSpace(filter.Number))
                filters.Add(new FilterItem { Property = "Number", FilterType = "contains", Value = filter.Number });
            if (filter.IsWhatsapp.HasValue)
                filters.Add(new FilterItem { Property = "IsWhatsapp", FilterType = "equals", Value = filter.IsWhatsapp });
            if (filter.IsEnable.HasValue)
                filters.Add(new FilterItem { Property = "IsEnable", FilterType = "equals", Value = filter.IsEnable });

            return _filter.FromFiltroItemList<Domain.Entities.Phone>(filters);
        }
        private static Expression<Func<Domain.Entities.Phone, object>> CreateOrder(PhoneFilter filterBase)
        {
            var param = Expression.Parameter(typeof(Domain.Entities.Phone), "u");
            var property = Expression.Convert(Expression.Property(param, filterBase.OrderBy), typeof(object));
            return Expression.Lambda<Func<Domain.Entities.Phone, object>>(property, param);
        }
        private static bool ExistFilters(PhoneFilter filter)
        {
            return !string.IsNullOrWhiteSpace(filter.ContactId) ||
                   !string.IsNullOrWhiteSpace(filter.Number) ||
                   filter.IsWhatsapp.HasValue ||
                   filter.IsEnable.HasValue;
        }

        #endregion
    }
}
