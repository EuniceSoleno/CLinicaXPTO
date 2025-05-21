namespace CLinicaXPTO.DTO
{
    public class UtenteDTO
    {
        public string NomeCompleto { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Genero { get; set; }
        public byte[]? Fotografia { get; set; }
        public string Telemovel { get; set; }
        public string Email { get; set; }
        public string Morada { get; set; }
        public string Password { get; set; }
    }
}
