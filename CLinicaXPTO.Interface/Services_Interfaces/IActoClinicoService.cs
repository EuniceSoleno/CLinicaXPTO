using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Share.Services_Interfaces
{
    public interface IActoClinicoService
    {
        Task<ActoClinicoDTO> CriarActoClinico(ActoClinicoDTO actoClinico);
        Task<ActoClinicoDTO> ActualizarActoClinico(ActoClinicoDTO actoClinico);
        Task<ActoClinicoDTO> BuscarActoClinico(int idActoClinico);
        Task<ActoClinicoDTO> DeleteActoClinico(int idActoClinico);
        Task<List<ActoClinicoDTO>> ListarActosClinico();

    }
}
