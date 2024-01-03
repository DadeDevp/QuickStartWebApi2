using QuickStartWebApi2.Models;

namespace QuickStartWebApi2.Interfaces
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<IEnumerable<Pessoa>> GetByNameAsync(string name);
    }
}
