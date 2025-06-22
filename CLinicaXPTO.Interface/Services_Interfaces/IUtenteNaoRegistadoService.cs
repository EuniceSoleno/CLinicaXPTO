using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Share.Services_Interfaces
{
     public interface IUtenteNaoRegistadoService
    {
        Task<UtenteNaoRegistadoDTO> AdicionarUtente(UtenteNaoRegistadoDTO utenteNaoRegistadoDTO);
        Task<UtenteNaoRegistadoDTO?> BuscarUentente(int idUtentNR);
        Task<List<UtenteNaoRegistadoDTO>> ListarTodos();
        Task<UtenteNaoRegistadoDTO> Buscar_Email(string email);
        Task<UtenteNaoRegistadoDTO> Telemovel(string nome);


    }
}
