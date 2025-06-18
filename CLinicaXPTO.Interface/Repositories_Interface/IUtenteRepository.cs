using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Interface.Repositories_Interface
{
    public interface IUtenteRepository
    {
        Task<Utente> CriarUtente(Utente utente);
        Task<List<Utente>> ListarUtentes();
        Task<Utente> Buscar_Id(int idUtente);
        Task<Utente> Buscar_Email(string email);
        Task<Utente> Buscar_Nome(string nome);
        Task<Utente> UpdateUtente(Utente utente);
        Task<bool> RemoverUtente(int idUtente);
        Task<bool> RemoverUtente(string email);
        Task<bool> RemoverUtente_ID(int id);

    }
}
