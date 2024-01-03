using Microsoft.EntityFrameworkCore;
using QuickStartWebApi2.DBContext;
using QuickStartWebApi2.Interfaces;
using QuickStartWebApi2.Models;

namespace QuickStartWebApi2.Repositories
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Pessoa>> GetByNameAsync(string name)
        {
            return await _context.Pessoas
                                 .Where(p => p.Nome.Contains(name))
                                 .ToListAsync();
        }
    }
}
