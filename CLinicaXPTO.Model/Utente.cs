using CLinicaXPTO.Model.Enumerados;
using System.ComponentModel.DataAnnotations;

namespace CLinicaXPTO.Model
{
    public class Utente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomeCompleto { get; set; }
        [Required]
        public DateOnly DataNascimento { get; set; }
        [Required]
        public string Genero { get; set; }
        public byte[]? Fotografia { get; set; }
        [Required]
        public string Telemovel { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Morada { get; set; }
        [Required]
        public string Password { get; set; }
       [Required]
        public TipoUtilizador Tipo { get; set; } = TipoUtilizador.ANONIMO;
        public List<PedidoMarcacao>? Pedidos { get; set; } = new List<PedidoMarcacao>();

    }
}
