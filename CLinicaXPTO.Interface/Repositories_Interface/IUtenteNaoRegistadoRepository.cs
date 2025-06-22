using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Share.Repositories_Interface
{
    public interface IUtenteNaoRegistadoRepository
    {
        Task<UtenteNaoRegistado> AdicionarUtente(UtenteNaoRegistado utente);
        Task<UtenteNaoRegistado?> ObterPorUtenteNaoRegistado(int id);
        Task<List<UtenteNaoRegistado>> ListarTodos();
        Task<bool> RemoverPorId(int id);
        Task<UtenteNaoRegistado> Buscar_Email(string email);
        Task<UtenteNaoRegistado> Telemovel(string nome);

    }
}
