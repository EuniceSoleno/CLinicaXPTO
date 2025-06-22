using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaXPTO.DAL;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Interface.Repositories_Interface;
using CLinicaXPTO.Model;
using Microsoft.EntityFrameworkCore;

namespace CLinicaXPTO.DAL.Repositories
{
    public class PedidoMarcacaoRepository : IPedidoMarcacaoRepository
    {
        private readonly CLinicaXPTODBContext _dbContext;
        public PedidoMarcacaoRepository(CLinicaXPTODBContext dbContext)
        { 
            _dbContext = dbContext; 
        }
        public async Task<PedidoMarcacao> ActualizarMarcacao(PedidoMarcacao pedidoMarcacao)
        {
            try
            {
                _dbContext.Pedidos.Update(pedidoMarcacao);
                await _dbContext.SaveChangesAsync();
                return await BuscarMarcacao(pedidoMarcacao.Id);
            }catch (Exception ex)
            {
                throw new Exception($"Erro ao actualizar a marcação no repositorio: {ex.Message}", ex);
            }
        }

        public async Task<PedidoMarcacao> BuscarMarcacao(int idMarcacao)
        {
            return await _dbContext.Pedidos
                .Include(u => u.ActosClinicos)
                .FirstOrDefaultAsync(u => u.Id == idMarcacao);
        }

        public async Task<bool> ElieminarMarcacao(int idMarcacao)
        {
            try
            {
                var marcacao = await _dbContext.Pedidos
                .Include(p => p.ActosClinicos)
                .FirstOrDefaultAsync(p => p.Id == idMarcacao);

                if (marcacao == null) return false;

                _dbContext.Actos.RemoveRange(marcacao.ActosClinicos);
                _dbContext.Pedidos.Remove(marcacao);

                await _dbContext.SaveChangesAsync();
                return true;
            }catch (Exception ex) { 
                throw new Exception($"Erro ao eliminar marcação no repositorio: {ex.Message}");
            }
            return false;

        }

        public async Task<List<PedidoMarcacao>> ListarMarcacoes()
        {
            var pedidos = await _dbContext.Pedidos
                .Include(p  => p.ActosClinicos) 
                /*.Include(p => p.Utente)*/
                .ToListAsync();
            return pedidos.ToList();

        }

        public async Task<PedidoMarcacao> RegistrarMarcacao(PedidoMarcacao pedidoMarcacao)
        {
            try
            {
                await _dbContext.Pedidos.AddAsync(pedidoMarcacao);
                await _dbContext.SaveChangesAsync();
                return await BuscarMarcacao(pedidoMarcacao.Id);
                /*return new PedidoMarcacao();*/
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao registrar marcação no repositório: {ex.Message}", ex);
            }
        }
    }
}
