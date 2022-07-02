using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        
    }
}
