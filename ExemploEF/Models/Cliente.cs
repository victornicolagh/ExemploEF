namespace ExemploEF.Models
{
    public class Cliente
    {
        public Guid ID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime Nascimento { get; set; }

    }
}
