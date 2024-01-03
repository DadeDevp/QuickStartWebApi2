using Microsoft.EntityFrameworkCore;
using QuickStartWebApi2.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace QuickStartWebApi2.DBContext
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais do modelo podem ser definidas aqui
        }
    }

}
