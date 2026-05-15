using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Infrastructure.Persistence;

public class UnitOfWork(RecipeDbContext context) : IUnitOfWork, IDisposable
{
    private bool _disposed;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
            _disposed = true;
        }
    }
}
