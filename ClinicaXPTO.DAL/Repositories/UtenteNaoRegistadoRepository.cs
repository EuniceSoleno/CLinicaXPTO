using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaXPTO.DAL;
using CLinicaXPTO.Model;
using CLinicaXPTO.Share.Repositories_Interface;
using Microsoft.EntityFrameworkCore;

namespace CLinicaXPTO.DAL.Repositories
{
    public class UtenteNaoRegistadoRepository : IUtenteNaoRegistadoRepository
    {
        private readonly CLinicaXPTODBContext _dbContext;

        public UtenteNaoRegistadoRepository(CLinicaXPTODBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UtenteNaoRegistado> AdicionarUtente(UtenteNaoRegistado utente)
        {

            _dbContext.utentesNaoRegistados.Add(utente);
            await _dbContext.SaveChangesAsync();
            return utente;

        }

        public async Task<List<UtenteNaoRegistado>> ListarTodos()
        {
            return await _dbContext.utentesNaoRegistados.ToListAsync();
        }

        public async Task<UtenteNaoRegistado?> ObterPorUtenteNaoRegistado(int id)
        {
            return await _dbContext.utentesNaoRegistados.FindAsync(id);
        }
    }
}
