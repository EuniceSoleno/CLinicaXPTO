using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model;
using CLinicaXPTO.Model.Enumerados;

namespace CLinicaXPTO.DTO
{
    public class PedidoMarcacaNaoRegistadoDTO
    {
        // Dados pessoais
        //Com os dados pessoais neste DTO Posso usá-los para registar os novos utentes
        public string NomeCompleto { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Genero { get; set; }
        public string Telemovel { get; set; }
        public string Email { get; set; }
        public string Morada { get; set; }

        // Dados da marcação
        public EstadoMarcacao EstadoMarcacao { get; set; } = EstadoMarcacao.PEDIDO;
        public string? Observacoes { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public List<ActoClinico>? ActosClinicos { get; set; } = new List<ActoClinico>();
        public DateTime? DataAgendada { get; set; }

        /*Intervalo de Data e Data Solicitada*/
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
        public TimeSpan HorarioSolicitado { get; set; }

    }
}
