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
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly CLinicaXPTODBContext _dbContext;
        public ProfissionalRepository(CLinicaXPTODBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Profissional> ActualizarProfissional(Profissional profissional)
        {
            try
            {
                // Aqui não é necessário chamar .Update(profissional), pois a entidade já está sendo rastreada
                await _dbContext.SaveChangesAsync();
                return await BuscarProfissional(profissional.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar os dados do Profissional: {ex.Message}", ex);
            }
        }


        public async Task<Profissional> AdicionarProficional(Profissional profissional)
        {
            try
            {
                await _dbContext.profissionais.AddAsync(profissional);
                await _dbContext.SaveChangesAsync();
                return await BuscarProfissional(profissional.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar o Profissional no repositório: {ex.Message}", ex);
            }
        }

        public async Task<Profissional> BuscarProfissional(int idProfissional)
        {
            return await _dbContext.profissionais
                .FirstOrDefaultAsync(u => u.Id == idProfissional);
        }

        public async Task<Profissional> BuscarProfissionalNome(string profissionalNome)
        {
            return await _dbContext.profissionais
                .FirstOrDefaultAsync(u => u.NomeCompleto.Equals(profissionalNome));
        }

        public async Task<Profissional> EliminarProfissional(int idProfissional)
        {
            try
            {
                var profissional = await BuscarProfissional(idProfissional);
                if (profissional != null)
                {
                    _dbContext.profissionais.Remove(profissional);
                    _dbContext.SaveChangesAsync();
                    return profissional;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao eliminar o profissional no repositorio: {ex.Message}");
            }
        }

        public async Task<List<Profissional>> ListarProfissionai()
        {
            var profissionais = await _dbContext.profissionais.ToListAsync();
            return profissionais;

        }
    }
}
