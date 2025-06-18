using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Interface.Repositories_Interface;
using CLinicaXPTO.Model;
using CLinicaXPTO.Model.Enumerados;
using CLinicaXPTO.Share.Repositories_Interface;
using CLinicaXPTO.Share.Services_Interfaces;

namespace CLinicaXPTO.Services
{
    public class ActoClinicoService : IActoClinicoService
    {
        private readonly IActoClinicoRepository _repository;
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IPedidoMarcacaoRepository _marcacaoRepository;
        public ActoClinicoService(IActoClinicoRepository repository, IProfissionalRepository profissionalRepository, IPedidoMarcacaoRepository marcacaoRepository)
        {
            _repository = repository;
            _profissionalRepository = profissionalRepository;
            _marcacaoRepository = marcacaoRepository;
        }

        public async Task<ActoClinicoDTO> ActualizarActoClinico(ActoClinicoDTO actoClinico)
        {
            try
            {
                if (actoClinico.Id <= 0)
                    throw new ArgumentException("O Id do Acto Clínico precisa ser maior que zero.");

                var profissional = await _profissionalRepository.BuscarProfissional(actoClinico.idProfissional);
                if (profissional == null)
                    throw new Exception($"O id {actoClinico.idProfissional} do Profissional deste Acto Clínico não consta nos registros!");

                var actoClinicoExistente = await _repository.BuscarActoClinico(actoClinico.Id);
                if (actoClinicoExistente == null)
                    throw new Exception($"O Acto Clínico com Id {actoClinico.Id} não existe, logo não pode ser actualizado.");

                // Aqui atualiza os campos do modelo existente com os valores do DTO
                actoClinicoExistente.idProfissional = actoClinico.idProfissional;
                actoClinicoExistente.PedidoMarcacaoId = actoClinico.PedidoMarcacaoId;
                actoClinicoExistente._TipoConsulta = actoClinico._TipoConsulta;
                //actoClinicoExistente.Observacoes = actoClinico.Observacoes;

                // Atualiza no repositório
                await _repository.ActualizarActoClinico(actoClinicoExistente);

                return MapToDTO(actoClinicoExistente);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao actualizar o Acto Clínico no repositório: " + ex.Message, ex);
            }
        }


        public async Task<ActoClinicoDTO> BuscarActoClinico(int idActoClinico)
        {
            try
            {
                if (idActoClinico <= 0) throw new Exception("O id do Acto Clinico precisa ser maior que zero");

                var actoClinico = await _repository.BuscarActoClinico(idActoClinico);
                if(actoClinico != null)
                    return MapToDTO(actoClinico);
                return null;

            }
            catch (Exception ex) { 
                throw new Exception("Erro ao buscar o Acto Clinico no repositorio!");
            }
        }

        public async Task<ActoClinicoDTO> CriarActoClinico(ActoClinicoDTO actoClinico)
        {
            //Buscar o Profissional associado ao ActoClinico
            var profissional = await _profissionalRepository.BuscarProfissional(actoClinico.idProfissional);
            if (profissional == null)
                throw new Exception($"O profissional com id {actoClinico.idProfissional} não existe!");

            //Buscar a marcação associada ao ActoClinico
            var marcacao = await _marcacaoRepository.BuscarMarcacao(actoClinico.PedidoMarcacaoId);
            if (marcacao == null)
                throw new Exception($"O Pedido com id {actoClinico.PedidoMarcacaoId} não existe!");

            var actoClinicoModel = MapToModel(actoClinico);
            var actoCriado = await _repository.CriarActoClinico(actoClinicoModel);
            return MapToDTO(actoCriado);

        }

        public async Task<ActoClinicoDTO> DeleteActoClinico(int idActoClinico)
        {
            if (idActoClinico <= 0)
                throw new Exception("O id do Acto Clinico precisa ser maior que zero");
            var actoClinico = await _repository.DeleteActoClinico(idActoClinico);
            if (actoClinico == null)
                return null;
            return MapToDTO(actoClinico);
        }

        public async Task<List<ActoClinicoDTO>> ListarActosClinico()
        {
            try
            {
                var actos = await _repository.ListarActosClinico();
                return actos.Select(MapToDTO).ToList();
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao listar marcações: {ex.Message}", ex);
            }
            throw new NotImplementedException();
        }

        private ActoClinicoDTO MapToDTO(ActoClinico actoClinico)
        {
            if (actoClinico == null) return null;
            return new ActoClinicoDTO
            {
                Id = actoClinico.Id,
                PedidoMarcacaoId = actoClinico.PedidoMarcacaoId,
                idProfissional = actoClinico.idProfissional,
                _TipoConsulta = actoClinico._TipoConsulta,
                _Subsistema = actoClinico._Subsistema,

            };
        }
        private ActoClinico MapToModel(ActoClinicoDTO actoClinico)
        {
            if (actoClinico == null)
            {
                return null;

            }

            return new ActoClinico
            {
                Id = actoClinico.Id,
                idProfissional = actoClinico.idProfissional,
                PedidoMarcacaoId = actoClinico.PedidoMarcacaoId,
                _TipoConsulta = actoClinico._TipoConsulta,
                _Subsistema = actoClinico._Subsistema

            };
        }
    }
}
