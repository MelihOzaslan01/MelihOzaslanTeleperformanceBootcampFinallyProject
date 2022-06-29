using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Persistence.Repositories;

public class UserRepository:GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}