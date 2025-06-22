using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model.Enumerados;

namespace CLinicaXPTO.Model
{
    public class TabelaDeHorario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int  ProfissionalId {  get; set; }
        /*public DiaSemana[] DiasSemana {  get; set; } = new DiaSemana[6];*/
        public List<DayOfWeek> DiasSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public TimeSpan Intervalo {  get; set; } // intervalo para o almoço por exemplo
    }
}
