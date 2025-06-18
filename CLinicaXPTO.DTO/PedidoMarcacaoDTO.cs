using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model.Enumerados;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.DTO
{
    public class PedidoMarcacaoDTO
    {
        public int Id { get; set; }
        public int UtenteId { get; set; }
        public EstadoMarcacao EstadoMarcacao { get; set; } = EstadoMarcacao.PEDIDO;
        public List<ActoClinico>? ActosClinicos { get; set; } = new List<ActoClinico>();
        public DateTime? DataAgendada { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now; //Data em que a solicitação de agendamento foi feita
        [Required]
        public string? IntervaloData { get; set; } // Intervalo solicitado para a marcacao
        public string? Observacoes { get; set; }
    }
}
