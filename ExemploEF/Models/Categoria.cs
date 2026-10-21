
using System.ComponentModel.DataAnnotations;

namespace EFexemplo.Models
{
    public class Categoria
    {
        public Guid CategoriaId { get; set; }

        public string Nome { get; set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
