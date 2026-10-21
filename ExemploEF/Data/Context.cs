using EFexemplo.Models;
using ExemploEF.Models;
using Microsoft.EntityFrameworkCore;

namespace Exemplo_EF.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context>
            options) : base(options)
        { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<Categoria>().ToTable("tbCategorias");
            modelBuilder.Entity<Produto>().ToTable("tbProdutos");
        }
    }
}
