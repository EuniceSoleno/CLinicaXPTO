using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Interface.Repositories_Interface;
using CLinicaXPTO.Interface.Services_Interfaces;
using CLinicaXPTO.Model;

namespace CLinicaXPTO.Services
{
    public class PedidoMarcacaoService : IPedidoMarcacaoService
    {
        private readonly IPedidoMarcacaoRepository _marcacaoRepository;
        private readonly IUtenteRepository _utenteRepository;
        public PedidoMarcacaoService(IPedidoMarcacaoRepository marcacaoRepository, IUtenteRepository utenteRepository)
        {
            _marcacaoRepository = marcacaoRepository;
            _utenteRepository = utenteRepository;
        }

        public async Task<PedidoMarcacaoDTO> ActualizarMarcacao(PedidoMarcacaoDTO pedidoMarcacaoDTO)
        {
            //verificar se o id é válido
            if (pedidoMarcacaoDTO.Id <= 0)
                throw new ArgumentException("ID da marcacao inválido");
            //Buscar a marcacao existente
            var marcacaoExistente = await _marcacaoRepository.BuscarMarcacao(pedidoMarcacaoDTO.Id);
            if (marcacaoExistente == null)
                throw new InvalidOperationException($"Marcacao comId {pedidoMarcacaoDTO.Id} não encontrada");

            //verificar se o Utente existe
            var utente = await _utenteRepository.Buscar_Id(pedidoMarcacaoDTO.UtenteId);
            if (utente == null)
                throw new InvalidOperationException($"Utente com o Id {pedidoMarcacaoDTO.UtenteId} não encontrado");

            //Mapear DTO para Model
            var pedidoMarcao = MapToModel(pedidoMarcacaoDTO, marcacaoExistente);


            //Actualizar o Repositorio
            var marcacaoActualizada = await _marcacaoRepository.ActualizarMarcacao(pedidoMarcao);

            //Mapear para DTO
            return MapToDTO(marcacaoActualizada);

        }

        public async Task<PedidoMarcacaoDTO> BuscarMarcacao(int idMarcacao)
        {
            try
            {
                if (idMarcacao <= 0)
                    throw new ArgumentException("O Id da marcação deve ser maior que zero");

                var marcacao = await _marcacaoRepository.BuscarMarcacao(idMarcacao);
                if (marcacao == null)
                    return null;

                return MapToDTO(marcacao);
            }catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar marcacao: {ex.Message}", ex);
            }
        }

        public async Task<PedidoMarcacaoDTO> ElieminarMarcacao(int idMarcacao)
        {
            try
            {
                if (idMarcacao <= 0)
                    throw new ArgumentException("O Id da marcação deve ser maior que zero");

                //buscar a marcacao antes de eliminar para retornar os dados

                var marcacao = await _marcacaoRepository.BuscarMarcacao(idMarcacao);
                if(marcacao == null)
                    throw new InvalidOperationException($"Marcacao com ID{idMarcacao} não encontrado!");

                //eliminar a marcacao
                var marcacaoEliminada = await _marcacaoRepository.ElieminarMarcacao(idMarcacao);

                if (!marcacaoEliminada)
                    throw new InvalidOperationException("Falha ao eliminar a marcação");

                return MapToDTO(marcacao);


            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao eliminar Marcacao");
            }
            throw new NotImplementedException();
        }

        public async Task<List<PedidoMarcacaoDTO>> ListarMarcacoes()
        {
            try
            {
                var marcacoes = await _marcacaoRepository.ListarMarcacoes();

                return marcacoes.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar marcações: {ex.Message}", ex);

            }
        }

        public async Task<PedidoMarcacaoDTO> RegistrarMarcacao(PedidoMarcacaoDTO marcacaoDTO)
        {
            //Buscar o Utente
            var utente = await _utenteRepository.Buscar_Id(marcacaoDTO.UtenteId);
            if (utente == null) throw new InvalidOperationException($"Utente com ID{marcacaoDTO
                ?.UtenteId} não encontrado");

            var pedidoMarcacaoModel = MapToModel(marcacaoDTO);

            var marcacaoRegistada = await _marcacaoRepository.RegistrarMarcacao(pedidoMarcacaoModel);
            return MapToDTO(marcacaoRegistada);
        }

        private PedidoMarcacaoDTO MapToDTO(PedidoMarcacao model)
        {
            if (model == null) return null;

            return new PedidoMarcacaoDTO
            {
                Id = model.Id,
                UtenteId = model.UtenteId,
                //O utente já possui um MapToDTO
                //Mas não sei como chamar aqui
               /* UtenteDTO = */
               EstadoMarcacao = model.EstadoMarcacao,
               ActosClinicos = model.ActosClinicos?.ToList()?? new List<ActoClinico>(),
               DataAgendada = model.DataAgendada,
               DataPedido = model.DataPedido,
               IntervaloData = model.IntervaloData,
               Observacoes = model.Observacoes

            };
        }

        private PedidoMarcacao MapToModel(PedidoMarcacaoDTO dto , PedidoMarcacao modeloExistente = null)
        {
            var model = modeloExistente ?? new PedidoMarcacao();
            model.Id = dto.Id;
            model.UtenteId= dto.UtenteId;
            model.EstadoMarcacao= dto.EstadoMarcacao;
            model.DataAgendada = dto.DataAgendada;
            model.IntervaloData = dto.IntervaloData;
            model.Observacoes= dto.Observacoes;

            if (modeloExistente == null)
                model.DataPedido = DateTime.Now;

            if (dto.ActosClinicos != null)
                model.ActosClinicos = dto.ActosClinicos.ToList();

            return model;

        }
        
    }
}
