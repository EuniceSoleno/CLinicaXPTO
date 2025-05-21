using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLinicaXPTO.Model
{
    public class PedidoMarcacao
    {
        public PedidoMarcacao()
        {
            DataPedido = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int UtenteId { get; set; }
        public Utente Utente { get; set; }
       /* public EstadoMarcacao EstadoMarcacao { get; set; } = EstadoMarcacao.Pedido;
        public List<ActoClinico>? ActosClinicos { get; set; } = new List<ActoClinico>();*/
        public DateTime? DataAgendada { get; set; }

        [Required]
        public DateTime DataPedido { get; set; } = DateTime.Now; //Data em que a solicitação de agendamento foi feita
        [Required]
        public string? IntervaloData { get; set; } // Intervalo solicitado para a marcacao
        public string? Observacoes { get; set; }
        
    }
}
