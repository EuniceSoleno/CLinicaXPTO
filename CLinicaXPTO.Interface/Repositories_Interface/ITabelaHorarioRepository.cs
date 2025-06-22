using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;
using CLinicaXPTO.Model.Enumerados;

namespace CLinicaXPTO.Share.Repositories_Interface
{
    public interface ITabelaHorarioRepository
    {
        Task<TabelaDeHorario?> BuscarPorProfissionalId(int profissionalId);
        //Task<TabelaDeHorario> BuscarHorarioProfissional(string nomeProfissional);
        Task<bool> CriarHorario(TabelaDeHorario horario);
        Task<bool> AtualizarHorario(TabelaDeHorario horario);
        Task<bool> RemoverHorario(TabelaDeHorario horario);

    }
}
