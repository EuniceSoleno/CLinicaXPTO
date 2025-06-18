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
    public class ActoClinicoRepository : IActoClinicoRepository
    {
        private readonly CLinicaXPTODBContext _context;
        public ActoClinicoRepository (CLinicaXPTODBContext context)
        {
            _context = context;
        }
        public async Task<ActoClinico> ActualizarActoClinico(ActoClinico actoClinico)
        {
            try
            {
               // _context.Actos.Update(actoClinico);
                await _context.SaveChangesAsync();
                return actoClinico;
            }
            catch (Exception ex) {
                throw new Exception("Erro ao actualizar o Acto Clínico");
            }
        }

        public async Task<ActoClinico> BuscarActoClinico(int idActoClinico)
        {
            try
            {
                return await _context.Actos.FirstOrDefaultAsync(u => u.Id == idActoClinico);
            }
            catch (Exception ex) {
                throw new Exception("Erro ao buscar o Acto Clínico no Repositorio!");
            }
        }

        public async Task<ActoClinico> CriarActoClinico(ActoClinico actoClinico)
        {
            try
            {
                await _context.Actos.AddAsync(actoClinico);
                await _context.SaveChangesAsync();
                return actoClinico;
            }
            catch (Exception ex) {
                throw new Exception("Erro ao Criar o Acto Clinico no Repositorio");
            }
            throw new NotImplementedException();
        }

        public
            async Task<ActoClinico> DeleteActoClinico(int idActoClinico)
        {
            try
            {
                var acto = await BuscarActoClinico(idActoClinico);
                if(acto != null)
                {
                    _context.Actos.Remove(acto);
                    await _context.SaveChangesAsync();
                    return acto;
                }
                return null;

            }
            catch (Exception ex) {
                throw new Exception("Erro ao eliminar o Acto clinico do Repositorio!");
            }
        }

        public async Task<List<ActoClinico>> ListarActosClinico()
        {
            var actosClinicos = await _context.Actos.ToListAsync();
            return actosClinicos;
        }
    }
}
