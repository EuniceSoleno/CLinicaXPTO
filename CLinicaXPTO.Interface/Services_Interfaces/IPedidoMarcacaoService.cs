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
        Task<List<PedidoMarcacaoDTO>> ListarMarcacoesDeUtentesNaoRegistados();
        Task<List<PedidoMarcacaoDTO>> ListarMarcacoesDeUtentesRegistados();
        Task<PedidoMarcacaoDTO> RegistrarMarcacao(PedidoMarcacaoDTO pedidoMarcacao);
        Task<PedidoMarcacaoDTO> BuscarMarcacao(int idMarcacao);
        Task<PedidoMarcacaoDTO> ElieminarMarcacao(int idMarcacao);
        Task<PedidoMarcacaoDTO> ActualizarMarcacao(PedidoMarcacaoDTO pedidoMarcacao);
        Task<PedidoMarcacaoDTO> RegistrarMarcacaoNaoRegistado(PedidoMarcacaNaoRegistadoDTO dto);
        Task<bool> AgendarMarcacao(int idMarcacao);
        Task<bool> RealizarMarcacao(int idMarcaca);



    }
}
