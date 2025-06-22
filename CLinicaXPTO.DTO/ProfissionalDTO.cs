using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.DTO
{
    public class ProfissionalDTO
    {
        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public string Especialidade { get; set; } = string.Empty;
        public ICollection<TabelaDeHorario> TabelaDeHorario { get; set; }

    }
}
