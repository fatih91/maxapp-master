using System.Threading.Tasks;
using maxapp.Core.Interfaces;

namespace maxapp.Persistence
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly MaxPanelContext context;

    public UnitOfWork(MaxPanelContext context)
    {
      this.context = context;
    }

    public async Task CompleteAsync()
    {
      await context.SaveChangesAsync();
    }
  }
}