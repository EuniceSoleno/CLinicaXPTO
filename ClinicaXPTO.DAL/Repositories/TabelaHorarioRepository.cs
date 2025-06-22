using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaXPTO.DAL;
using CLinicaXPTO.Model;
using CLinicaXPTO.Model.Enumerados;
using CLinicaXPTO.Share.Repositories_Interface;
using Microsoft.EntityFrameworkCore;

namespace CLinicaXPTO.DAL.Repositories
{
    public class TabelaHorarioRepository : ITabelaHorarioRepository
    {
        private readonly CLinicaXPTODBContext _dbContext;

        public TabelaHorarioRepository(CLinicaXPTODBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TabelaDeHorario?> BuscarPorProfissionalId(int profissionalId)
        {
            return await _dbContext.horarios.FirstOrDefaultAsync(h => h.ProfissionalId == profissionalId);
        }

        public async Task<bool> CriarHorario(TabelaDeHorario horario)
        {
            _dbContext.horarios.Add(horario);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> AtualizarHorario(TabelaDeHorario horario)
        {
            _dbContext.horarios.Update(horario);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoverHorario(TabelaDeHorario horario)
        {
            _dbContext.horarios.Remove(horario);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /*public Task<TabelaDeHorario> BuscarHorarioProfissional(string nomeProfissional)
        {
            return _dbContext.horarios.FirstOrDefaultAsync(h => )
        }*/
    }
}
