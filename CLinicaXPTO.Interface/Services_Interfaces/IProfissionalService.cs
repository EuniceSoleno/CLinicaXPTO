using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;

namespace CLinicaXPTO.Share.Services_Interfaces
{
    public interface IProfissionalService
    {
        Task<List<ProfissionalDTO>> ListarProfissional();
        Task<ProfissionalDTO> AdicionarProficional(ProfissionalDTO profifionalDTO);
        Task<ProfissionalDTO> BuscarProfissional(int idProficional);
        Task<ProfissionalDTO> ElieminarProfissional(int idProficional);
        Task<ProfissionalDTO> ActualizarProfissional(ProfissionalDTO profifionalDTO);
    }
}
