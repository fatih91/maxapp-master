using System.Threading.Tasks;

namespace maxapp.Core.Interfaces
{
  public interface IUnitOfWork
  {
    Task CompleteAsync();
  }
}