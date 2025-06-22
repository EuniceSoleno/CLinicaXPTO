using CLinicaXPTO.Model.Enumerados;
using CLinicaXPTO.Model;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public TipoUtilizador Tipo { get; set; } = TipoUtilizador.ANONIMO;
        public List<PedidoMarcacao>? Pedidos { get; set; } = new List<PedidoMarcacao>();
    }
}
