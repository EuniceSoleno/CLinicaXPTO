using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;
using CLinicaXPTO.Model.Enumerados;

namespace CLinicaXPTO.Share.Services_Interfaces
{
    public interface ITabelaHorarioService
    {
        Task<TabelaDeHorarioDTO> BuscarHorarioProfissional(string nomeProfissional);
        Task<bool> DefinirHorario(TabelaDeHorario tabelaDeHorario);
        Task<bool> AlterarHorarioInicioFim(string nomeProfissional, TimeSpan inicio, TimeSpan fim);
        Task<bool> AlterarIntervalo(string nomeProfissional, TimeSpan intervalo);
        Task<bool> AdicionarDiaSemana(string nomeProfissional, DayOfWeek novoDia);
        Task<bool> RemoverDiaSemana(string nomeProfissional, DayOfWeek diaRemover);
        Task<bool> EliminarHorario(string nomeProfissional);
    }
}
