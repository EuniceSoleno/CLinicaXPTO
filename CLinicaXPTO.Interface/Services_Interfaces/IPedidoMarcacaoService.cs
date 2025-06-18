using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Interface.Services_Interfaces
{
    public interface IPedidoMarcacaoService
    {
        Task<List<PedidoMarcacaoDTO>> ListarMarcacoes();
        Task<PedidoMarcacaoDTO> RegistrarMarcacao(PedidoMarcacaoDTO pedidoMarcacao);
        Task<PedidoMarcacaoDTO> BuscarMarcacao(int idMarcacao);
        Task<PedidoMarcacaoDTO> ElieminarMarcacao(int idMarcacao);
        Task<PedidoMarcacaoDTO> ActualizarMarcacao(PedidoMarcacaoDTO pedidoMarcacao);


    }
}
