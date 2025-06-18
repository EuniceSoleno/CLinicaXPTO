
using System.Runtime.InteropServices;
using ClinicaXPTO.DAL;
using CLinicaXPTO.Interface.Repositories_Interface;
using CLinicaXPTO.Model;
using Microsoft.EntityFrameworkCore;


namespace CLinicaXPTO.DAL.Repositories
{
    public class UtenteRepository : IUtenteRepository
    {
        public readonly CLinicaXPTODBContext _dbContext;
        public UtenteRepository(CLinicaXPTODBContext dbContext) 
        { _dbContext = dbContext; }

        public async Task<Utente> Buscar_Email(string email)
        {
            return await _dbContext.Utentes.Include(u => u.Pedidos)
                 .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<Utente> Buscar_Id(int idUtente)
        {
            return await _dbContext.Utentes.Include(u => u.Pedidos)
                .FirstOrDefaultAsync(u => u.Id == idUtente);
        }

        public async Task<Utente> Buscar_Nome(string nome)
        {
            return await _dbContext.Utentes.Include(u => u.Pedidos)
                .FirstOrDefaultAsync(u => u.NomeCompleto == nome);
        }

        public async Task<Utente> CriarUtente(Utente utente)
        {
            await _dbContext.Utentes.AddAsync(utente);
            await _dbContext.SaveChangesAsync();
            return utente;
        }

        public async Task<List<Utente>> ListarUtentes()
        {
            var Utentes = await _dbContext.Utentes.ToListAsync();
            return Utentes.Select(u => new Utente {
                NomeCompleto = u.NomeCompleto,
                DataNascimento = u.DataNascimento,
                Genero = u.Genero,
                Telemovel = u.Telemovel,
                Email = u.Email,
                Morada = u.Morada,
                Pedidos = u.Pedidos,
            }).ToList();
        }

        public async Task<bool> RemoverUtente(int idUtente)
        {
            var utente = await Buscar_Id(idUtente);
            if (utente != null)
            {
                _dbContext.Utentes.Remove(utente);
                await _dbContext.SaveChangesAsync(); 
                return true;
            }                
            return false;
        }

        public async Task<bool> RemoverUtente(string email)
        {
            var utente = await Buscar_Email(email);
            if(utente != null)
            {
                _dbContext.Utentes.Remove(utente);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoverUtente_ID(int id)
        {
            var utente = await Buscar_Id(id);
            if (utente != null)
            {
                _dbContext.Utentes.Remove(utente);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Utente> UpdateUtente(Utente utente)
        {
            _dbContext.Utentes.Update(utente);
            await _dbContext.SaveChangesAsync();
            return utente;
        }
    }
}
