using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Rock.Repository.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Rock.Repository.Repositories
{
    public class ProductRepository
    {
        private readonly CarvedRockContext _dbContext;

        public ProductRepository(CarvedRockContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetOne(int id)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
