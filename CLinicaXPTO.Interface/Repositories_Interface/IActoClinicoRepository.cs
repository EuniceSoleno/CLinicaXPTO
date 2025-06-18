using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Share.Repositories_Interface
{
    public interface IActoClinicoRepository
    {
        Task<ActoClinico> CriarActoClinico(ActoClinico actoClinico);
        Task<ActoClinico> ActualizarActoClinico(ActoClinico actoClinico);
        Task<ActoClinico> BuscarActoClinico(int idActoClinico);
        Task<ActoClinico> DeleteActoClinico(int idActoClinico);
        Task<List<ActoClinico>> ListarActosClinico();
    }
}
