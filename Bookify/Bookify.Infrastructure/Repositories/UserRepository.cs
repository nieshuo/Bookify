using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override void Add(User entity)
        {
            base.Add(entity);
        }
    }
}
