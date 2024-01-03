namespace QuickStartWebApi2.Models
{
    public class Pessoa
    {
        //Entity Framework já reconhece o padrao "NomedaclasseID" como a chave primária, ou pode usar a data annotation [Key]
        public int PessoaId { get; set; }
        public required string Nome { get; set; }
        public required DateTime DataNascimento { get; set; }
        public string? Endereco { get; set; }

    }
}
