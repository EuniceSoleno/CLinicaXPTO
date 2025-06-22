using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CLinicaXPTO.DTO
{
    public class UtenteNaoRegistadoDTO
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Genero { get; set; }
        public string Telemovel { get; set; }
        public string Email { get; set; }
        public string Morada { get; set; }
        public string? Password { get; set; }

    }
}
