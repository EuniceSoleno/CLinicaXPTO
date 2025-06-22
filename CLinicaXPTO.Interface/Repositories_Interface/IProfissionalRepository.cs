using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Share.Repositories_Interface
{
    public interface IProfissionalRepository
    {
        Task<Profissional> AdicionarProficional(Profissional profissional);
        Task<Profissional> ActualizarProfissional(Profissional profissional);
        Task<Profissional> BuscarProfissional(int idProfissional);
        Task<Profissional> BuscarProfissionalNome (string  profissionalNome);
        Task<Profissional> EliminarProfissional(int idProfissional);
        Task<List<Profissional>> ListarProfissionai();
    }
}
