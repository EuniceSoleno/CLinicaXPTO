using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Interface.Repositories_Interface
{
    public interface IPedidoMarcacaoRepository
    {
        Task<List<PedidoMarcacao>> ListarMarcacoes();
        Task<PedidoMarcacao> RegistrarMarcacao(PedidoMarcacao utente);
        Task<PedidoMarcacao> BuscarMarcacao(int idMarcacao);
        Task<bool> ElieminarMarcacao(int idMarcacao);
        Task<PedidoMarcacao> ActualizarMarcacao(PedidoMarcacao pedidoMarcacao);

    }
}
