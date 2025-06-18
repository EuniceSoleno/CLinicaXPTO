using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLinicaXPTO.Model
{
    public class Profissional
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public string Especialidade { get; set; } = string.Empty;

    }
}
