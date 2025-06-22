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
using CLinicaXPTO.Model.Enumerados;
using CLinicaXPTO.Share.Repositories_Interface;

namespace CLinicaXPTO.Services
{
    public class PedidoMarcacaoService : IPedidoMarcacaoService
    {
        private readonly IPedidoMarcacaoRepository _marcacaoRepository;
        private readonly IUtenteRepository _utenteRepository;
        private readonly IUtenteNaoRegistadoRepository _utenteNaoRegistadoRepository;
        private readonly ITabelaHorarioRepository _tabelaHorarioRepository;
        private readonly IProfissionalRepository _profissionalRepository;

        public PedidoMarcacaoService(
        IPedidoMarcacaoRepository marcacaoRepository,
        IUtenteRepository utenteRepository,
        IUtenteNaoRegistadoRepository utenteNaoRegistadoRepository,
        ITabelaHorarioRepository tabelaHorarioRepository,
        IProfissionalRepository profissionalRepository)
            {
            _marcacaoRepository = marcacaoRepository;
            _utenteRepository = utenteRepository;
            _utenteNaoRegistadoRepository = utenteNaoRegistadoRepository;
            _tabelaHorarioRepository = tabelaHorarioRepository;
            _profissionalRepository = profissionalRepository;
        }

        public async Task<PedidoMarcacaoDTO> ActualizarMarcacao(PedidoMarcacaoDTO pedidoMarcacaoDTO)
        {
            //Repartir a actualização da Marcação
            //verificar se o id é válido
            if (pedidoMarcacaoDTO.Id <= 0)
                throw new ArgumentException("ID da marcacao inválido");
            //Buscar a marcacao existente
            var marcacaoExistente = await _marcacaoRepository.BuscarMarcacao(pedidoMarcacaoDTO.Id);
            if (marcacaoExistente == null)
                throw new InvalidOperationException($"Marcacao comId {pedidoMarcacaoDTO.Id} não encontrada");

            //verificar se o Utente existe
            var utente = await _utenteRepository.Buscar_Id(pedidoMarcacaoDTO.UtenteId ?? 0);
            if (utente == null)
                throw new InvalidOperationException($"Utente com o Id {pedidoMarcacaoDTO.UtenteId} não encontrado");

            //Mapear DTO para Model
            var pedidoMarcao = MapToModel(pedidoMarcacaoDTO, marcacaoExistente);


            //Actualizar o Repositorio
            var marcacaoActualizada = await _marcacaoRepository.ActualizarMarcacao(pedidoMarcao);

            //Mapear para DTO
            return MapToDTO(marcacaoActualizada);

        }

        public async Task<bool> AgendarMarcacao(int idMarcacao)
        {
            //Buscar Marcacao
            var marcacao = await _marcacaoRepository.BuscarMarcacao(idMarcacao);
            if(marcacao == null)
                throw new Exception("Marcacao não encontrada");

            //Verificar se é uma solicitaçao de Utente Registado
            if (marcacao.UtenteId == null)
                throw new Exception("So é permitido agendar Macação de utentes Registados");


            //validar intervalo de datas, maximo de 3 dias permitidos
            var diferenca = marcacao.DataFim.DayNumber - marcacao.DataInicio.DayNumber;
            if (diferenca > 3)
                throw new ArgumentException("Só é permitido solicitar num intervalo de até 3 dias");

            //Buscar todos os profissionais e seus horarios
            var profissionais = await _profissionalRepository.ListarProfissionai();
            if (profissionais == null || profissionais.Count == 0)
                throw new Exception("Nenhum Profissional Registado");

            DateTime? dataAgendada = null;
            Profissional? profissionalSelecionado = null;
            foreach(var profissional in profissionais)
            {
                var horario = await _tabelaHorarioRepository.BuscarPorProfissionalId(profissional.Id);
                if (horario == null)
                    continue;

                //Procurar uma data disponivel dentro do intervalo solicitado
                for (var data = marcacao.DataInicio; data <= marcacao.DataFim; data = data.AddDays(1))
                {
                    var diaSemana = data.DayOfWeek;

                    //verificar se o Profissional trabalha nesse dia
                    if (!horario.DiasSemana.Contains(diaSemana))
                        continue;

                    //verificar se o horario solicitado está dentro do horario do profissional
                    if(marcacao.HorarioSolicitado >= horario.HoraInicio && marcacao.HorarioSolicitado <= horario.HoraInicio)
                    {
                        profissionalSelecionado = profissional;
                        var horarioSolicitado = TimeOnly.FromTimeSpan(marcacao.HorarioSolicitado);
                        dataAgendada = data.ToDateTime(marcacao.horarioSolicitado);
                        break;

                    }
                }
                if(profissionalSelecionado != null && dataAgendada != null) 
                    break;
            }
            //Actualizar a marcacao
            marcacao.EstadoMarcacao = EstadoMarcacao.AGENDADO;
            marcacao.DataAgendada = dataAgendada;

            var resultado = await _marcacaoRepository.ActualizarMarcacao(marcacao);
            return resultado != null;


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
        public async Task<List<PedidoMarcacaoDTO>> ListarMarcacoesDeUtentesNaoRegistados()
        {
            var marcacoes = await _marcacaoRepository.ListarMarcacoes();
            var filtradas = marcacoes
                .Where(m => m.UtenteId == null)
                .Select(MapToDTO)
                .ToList();

            return filtradas;
            
        }

        public async Task<List<PedidoMarcacaoDTO>> ListarMarcacoesDeUtentesRegistados()
        {
            var marcacoes = await _marcacaoRepository.ListarMarcacoes();
            var filtradas = marcacoes
                .Where(m => m.UtenteId != null)
                .Select(MapToDTO)
                .ToList();
            return filtradas;
        }

        public async Task<bool> RealizarMarcacao(int idMarcacao)
        {
            if (idMarcacao == 0)
                throw new ArgumentException("O ID da Marcacao deve ser maior que zero");

            //Buscar a marcacao no Banco de Dados
            var marcacao = await _marcacaoRepository.BuscarMarcacao(idMarcacao);
            if (marcacao == null) throw new ArgumentException("Marcacao não encontrada");

            //Verificar se a marcacao já foi agengada
            if (marcacao.EstadoMarcacao == EstadoMarcacao.REALIZADO)
                throw new ArgumentException("A Marcacao já foi realizada");

            //actualizar o estado da Macacao
            if(marcacao.EstadoMarcacao == EstadoMarcacao.AGENDADO)
                marcacao.EstadoMarcacao = EstadoMarcacao.REALIZADO;

            //Salvar as alterações Feitas
            var realizado = await _marcacaoRepository.ActualizarMarcacao(marcacao);
            return realizado != null;

        }

        //O metodo registar Marcacao não sera utilizado!

        public async Task<PedidoMarcacaoDTO> RegistrarMarcacao(PedidoMarcacaoDTO marcacaoDTO)
        {
            //Buscar o Utente
            var utente = await _utenteRepository.Buscar_Id(marcacaoDTO.UtenteId ?? 0);
            if (utente == null) throw new InvalidOperationException($"Utente com ID{marcacaoDTO
                ?.UtenteId} não encontrado");

            var pedidoMarcacaoModel = MapToModel(marcacaoDTO);

            var marcacaoRegistada = await _marcacaoRepository.RegistrarMarcacao(pedidoMarcacaoModel);
            return MapToDTO(marcacaoRegistada);
        }

        public async Task<PedidoMarcacaoDTO> RegistrarMarcacaoNaoRegistado(PedidoMarcacaNaoRegistadoDTO dto)
        {
        
            // Criar e guardar o utente não registado
            var utenteNR = new UtenteNaoRegistado
            {
                NomeCompleto = dto.NomeCompleto,
                DataNascimento = dto.DataNascimento,
                Genero = dto.Genero,
                Telemovel = dto.Telemovel,
                Email = dto.Email,
                Morada = dto.Morada
            };

            var utenteCriado = await _utenteNaoRegistadoRepository.AdicionarUtente(utenteNR);

            // Criar e guardar o pedido de marcação do  utente não registado
            var pedido = new PedidoMarcacao
            {
                EstadoMarcacao = dto.EstadoMarcacao,
                Observacoes = dto.Observacoes,
                DataPedido = dto.DataPedido,
                ActosClinicos = dto.ActosClinicos?.ToList() ?? new List<ActoClinico>(),
                DataAgendada = dto.DataAgendada

            };

            var marcacaoCriada = await _marcacaoRepository.RegistrarMarcacao(pedido);

            return MapToDTO(marcacaoCriada);

        }

        private PedidoMarcacaoDTO MapToDTO(PedidoMarcacao model)
        {
            if (model == null) return null;

            return new PedidoMarcacaoDTO
            {
               Id = model.Id,
               UtenteId = model.UtenteId,
               EstadoMarcacao = model.EstadoMarcacao,
               ActosClinicos = model.ActosClinicos?.ToList()?? new List<ActoClinico>(),
               DataAgendada = model.DataAgendada,
               DataPedido = model.DataPedido,
               Observacoes = model.Observacoes

            };
        }

        private PedidoMarcacao MapToModel(PedidoMarcacaoDTO dto , PedidoMarcacao modeloExistente = null)
        {
            var model = modeloExistente ?? new PedidoMarcacao();
            model.Id = dto.Id;
            model.UtenteId= dto.UtenteId ?? 0;
            model.EstadoMarcacao= dto.EstadoMarcacao;
            model.DataAgendada = dto.DataAgendada;
            model.Observacoes= dto.Observacoes;

            if (modeloExistente == null)
                model.DataPedido = DateTime.Now;

            if (dto.ActosClinicos != null)
                model.ActosClinicos = dto.ActosClinicos.ToList();

            return model;

        }
        
    }
}
