using amazon_customer_reviews_data.dbcontext;
using amazon_customer_reviews_data.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace amazon_customer_reviews_data.repositories
{
    public interface IGenericRepository
    {
        Task<IList<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : Base;
        Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Base;
        Task<TEntity> InsertAsync<TEntity>(TEntity model) where TEntity : Base;
    }

    public class GenericRepository : IGenericRepository
    {
        private readonly AmazonCustomerReviewsDbContext _dbContext;

        public GenericRepository(AmazonCustomerReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : Base
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Base
        {
            return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity model) where TEntity : Base
        {
            var utcNow = DateTime.UtcNow;
            model.Created = utcNow;
            model.Updated = utcNow;
            await _dbContext.Set<TEntity>().AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }
    }
}
