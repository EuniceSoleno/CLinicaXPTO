using System.ComponentModel;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Interface.Repositories_Interface;
using CLinicaXPTO.Interface.Services_Interfaces;
using CLinicaXPTO.Model;
using Microsoft.AspNetCore.Identity;


namespace CLinicaXPTO.Services
{
    public class UtenteService : IUtenteServiceInterface
    {
        private readonly IUtenteRepository _utenteRepository;
        public UtenteService(IUtenteRepository utenteRepository)
        {
            _utenteRepository = utenteRepository;
        }

        private UtenteDTO MapToDTO(Utente utente)
        {
            return new UtenteDTO
            {
                NomeCompleto = utente.NomeCompleto,
                Genero = utente.Genero,
                DataNascimento = utente.DataNascimento,
                Fotografia = utente.Fotografia,
                Telemovel = utente.Telemovel,
                Email = utente.Email,
                Morada = utente.Morada,
            };
        }

        public async Task<UtenteDTO> Buscar_Email(string email)
        {
            var utente = await _utenteRepository.Buscar_Email(email);
            if(utente != null)
                return MapToDTO(utente);
            return null;
        }

        public async Task<UtenteDTO> Buscar_Id(int idUtente)
        {
            var utente = await _utenteRepository.Buscar_Id(idUtente);
            if(utente != null) return MapToDTO(utente);
            return null;

        }

        public async Task<UtenteDTO> Buscar_Nome(string nome)
        {
            var utente = await _utenteRepository.Buscar_Nome(nome);
            if(utente != null) { return MapToDTO(utente); }
            return null;
        }

        public async Task<UtenteDTO> CriarUtente(UtenteDTO utente)
        {
            var hasher = new PasswordHasher<Utente>();

            var utente_ = new Utente
            {
                Email = utente.Email,
                NomeCompleto = utente.NomeCompleto,
                DataNascimento = utente.DataNascimento,
                Genero = utente.Genero,
                Telemovel = utente.Telemovel,
                Morada = utente.Morada,
            };

            utente_.Password = hasher.HashPassword(utente_, utente.Password);

            await _utenteRepository.CriarUtente(utente_);

            var dto = MapToDTO(utente_);
            return dto;
        }


        public async Task<List<UtenteDTO>> ListarUtentes()
        {
            var utentes = await _utenteRepository.ListarUtentes();

            var dtoList = utentes.Select(u => new UtenteDTO
            {
                NomeCompleto = u.NomeCompleto,
                DataNascimento = u.DataNascimento,
                Genero = u.Genero,
                Fotografia = u.Fotografia,
                Telemovel = u.Telemovel,
                Email = u.Email,
                Morada = u.Morada,
            }).ToList();

            return dtoList;
        }


        public async Task<bool> RemoverUtente(int idUtente)
        {
            return await _utenteRepository.RemoverUtente(idUtente);

        }

        public async Task<bool> RemoverUtente(string email)
        {
            return await _utenteRepository.RemoverUtente(email);
        }
        public async Task<bool> RemoverUtente_ID(int id)
        {
            return await _utenteRepository.RemoverUtente_ID(id);

        }

        public async Task<UtenteDTO> UpdateUtente(UtenteDTO utenteAtualizado)
        {
            var utenteExistente = await _utenteRepository.Buscar_Email(utenteAtualizado.Email);

            if (utenteExistente == null)
            {
                return null;
            }

            // Atualizar os campos
            utenteExistente.NomeCompleto = utenteAtualizado.NomeCompleto;
            utenteExistente.DataNascimento = utenteAtualizado.DataNascimento;
            utenteExistente.Genero = utenteAtualizado.Genero;
            utenteExistente.Fotografia = utenteAtualizado.Fotografia;
            utenteExistente.Telemovel = utenteAtualizado.Telemovel;
            utenteExistente.Email = utenteAtualizado.Email;
            utenteExistente.Morada = utenteAtualizado.Morada;

            // Atualizar a password, se vier preenchida 
            if (!string.IsNullOrWhiteSpace(utenteAtualizado.Password))
            {
                var hasher = new PasswordHasher<Utente>();
                utenteExistente.Password = hasher.HashPassword(utenteExistente, utenteAtualizado.Password);
            }

            // Salvar alterações
            await _utenteRepository.UpdateUtente(utenteExistente);

            return MapToDTO(utenteExistente);
        }

      
    }
}
