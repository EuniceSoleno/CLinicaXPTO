using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Interface.Services_Interfaces
{
    public interface IUtenteServiceInterface
    {
        Task<UtenteDTO> CriarUtente(UtenteDTO utente);
        Task<List<UtenteDTO>> ListarUtentes();
        Task<UtenteDTO> Buscar_Id(int idUtente);
        Task<UtenteDTO> Buscar_Email(string email);
        Task<UtenteDTO> Buscar_Nome(string nome);
        Task<UtenteDTO> UpdateUtente(UtenteDTO utente);
        Task<bool> RemoverUtente(int idUtente);
        Task<bool> RemoverUtente(string nome);
        Task<bool> RemoverUtente_ID(int id);
    }
}
