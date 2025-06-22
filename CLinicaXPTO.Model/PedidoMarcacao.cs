using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model.Enumerados;

namespace CLinicaXPTO.Model
{
    public class PedidoMarcacao
    {
        public TimeOnly horarioSolicitado;

        public PedidoMarcacao()
        {
            DataPedido = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public int? UtenteId { get; set; }
        public EstadoMarcacao EstadoMarcacao { get; set; } = EstadoMarcacao.PEDIDO;
        public List<ActoClinico>? ActosClinicos { get; set; } = new List<ActoClinico>();
        public DateTime? DataAgendada { get; set; } //Data determinada pelo administrador
        public DateTime DataPedido { get; set; } = DateTime.Now; //Data em que a solicitação de agendamento foi feita
        public string? Observacoes { get; set; }

        /*Intervalo de Data e Data Solicitada*/
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim {  get; set; }
        public TimeSpan HorarioSolicitado { get; set; }

        /*O intervalo Solicitado representa algo como: 22/06/2025 - 23/06/2025
         E o horario Solicitado algo como : 10h:30*/
        /*Acredito que seja uma boa ideia definir um limite no intervalo das dastas
         isto é, a diferença entre a dataFim pela datInicio não pode ser superior a
        3 dias!*/

    }
}
