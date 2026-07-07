using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Infrastructure.Persistence;

public class UnitOfWork(RecipeDbContext context): IUnitOfWork, IDisposable
{
    private bool disposed;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }

            this.disposed = true;
        }
    }
}
