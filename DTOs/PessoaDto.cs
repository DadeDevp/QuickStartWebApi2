namespace QuickStartWebApi2.DTOs
{
    public record PessoaDto
    {
        public required string Nome { get; set; }
        public required DateTime DataNascimento { get; set; }
        public string? Endereco { get; set; }
    }
}
