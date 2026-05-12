using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecipeDbContext _context;

        public UnitOfWork(RecipeDbContext context) => _context = context;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _context.SaveChangesAsync(cancellationToken);
    }
}
