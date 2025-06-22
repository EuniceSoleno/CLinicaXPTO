using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;
using CLinicaXPTO.Share.Repositories_Interface;
using CLinicaXPTO.Share.Services_Interfaces;

namespace CLinicaXPTO.Services
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository _repository;
        public ProfissionalService(IProfissionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProfissionalDTO> ActualizarProfissional(ProfissionalDTO profissionalDTO)
        {
            try
            {
                if (profissionalDTO.Id <= 0)
                    throw new Exception("O Id do profissional precisa ser maior que zero");

                // Buscar o profissional existente no banco
                var profissional = await _repository.BuscarProfissional(profissionalDTO.Id);
                if (profissional == null)
                    throw new Exception($"O profissional com o id {profissionalDTO.Id} não existe no sistema");

                // Aplicar as alterações
                profissional.NomeCompleto = profissionalDTO.NomeCompleto;
                profissional.Especialidade = profissionalDTO.Especialidade;

                // Salvar alterações
                var actualizado = await _repository.ActualizarProfissional(profissional);

                return MapToDTO(actualizado);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar profissional: " + ex.Message);
            }
        }



        public async Task<ProfissionalDTO> AdicionarProficional(ProfissionalDTO profissionalDTO)
        {
            try
            {
                // Mapear o DTO para a entidade (model)
                var profissional = MapToModel(profissionalDTO);

                // Adicionar o profissional e aguardar a operação assíncrona
                await _repository.AdicionarProficional(profissional);

                // Após salvar, o profissional.Id deve estar preenchido
                var profissionalModel = await _repository.BuscarProfissional(profissional.Id);

                if (profissionalModel == null)
                    throw new Exception("Profissional não foi adicionado com sucesso.");

                // Mapear de volta para DTO e retornar
                return MapToDTO(profissionalModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao adicionar profissional: {ex.Message}");
            }
        }


        public async Task<ProfissionalDTO> BuscarProfissional(int idProfissional)
        {
            try
            {
                if (idProfissional <= 0)
                    throw new Exception($"O id do Profissional precisa ser maior que zero!");

                var profissional = await _repository.BuscarProfissional(idProfissional);
                if (profissional == null)
                    return null;

                return MapToDTO(profissional);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProfissionalDTO> BuscarProfissionalNome(string profissionalNome)
        {
            try
            {
                if (profissionalNome.Equals(string.Empty));
                    throw new Exception($"Insira o nome do Profissional!");

                var profissional = await _repository.BuscarProfissionalNome(profissionalNome);
                if (profissional == null)
                    return null;

                return MapToDTO(profissional);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public async Task<ProfissionalDTO> ElieminarProfissional(int idProficional)
        {
            try
            {
                if (idProficional <= 0)
                    throw new Exception($"O id do profissional não pode ser menor ou igual a zero");

                var profissional = await BuscarProfissional(idProficional);
                if (profissional == null)
                {
                    throw new Exception($"Profissional com id {idProficional} não encontrado");

                }
                else
                {
                    var profissionalRemovido = await _repository.EliminarProfissional(idProficional);
                    return profissional;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }    
            
        }

        public async Task<List<ProfissionalDTO>> ListarProfissional()
        {
            try
            {
                var profissionais = await _repository.ListarProfissionai();
                return profissionais.Select(p => MapToDTO(p)).ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private Profissional MapToModel(ProfissionalDTO profissionalDTO)
        {
           if(profissionalDTO == null)
            {
                return null;
            }
            else
            {
                return new Profissional
                {
                    Id = profissionalDTO.Id,
                    NomeCompleto = profissionalDTO.NomeCompleto,
                    Especialidade = profissionalDTO.Especialidade
                };
            }
        }
        private ProfissionalDTO MapToDTO(Profissional proficional)
        {
            if (proficional == null)
                return null;
            return new ProfissionalDTO
            {
                Id = proficional.Id,
                NomeCompleto = proficional.NomeCompleto,
                Especialidade = proficional.Especialidade,

            };

        }

        
    }
}
