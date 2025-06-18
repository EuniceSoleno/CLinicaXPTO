using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;
using CLinicaXPTO.Share.Repositories_Interface;
using CLinicaXPTO.Share.Services_Interfaces;

namespace CLinicaXPTO.Services
{
    public class UtenteNaoRegistadoService : IUtenteNaoRegistadoService
    {

        private readonly IUtenteNaoRegistadoRepository _repository;

        public UtenteNaoRegistadoService(IUtenteNaoRegistadoRepository repository)
        {
            _repository = repository;
        }



        public async Task<UtenteNaoRegistadoDTO> AdicionarUtente(UtenteNaoRegistadoDTO dto)
        {
            var utenteNR = new UtenteNaoRegistado
            {
                NomeCompleto = dto.NomeCompleto,
                DataNascimento = dto.DataNascimento,
                Genero = dto.Genero,
                Email = dto.Email,
                Telemovel = dto.Telemovel,
                Morada = dto.Morada,


            };
            var novoUtente = await _repository.AdicionarUtente(utenteNR);

            //converter a entidade de volta para DTO
            return new UtenteNaoRegistadoDTO
            {
                Id = novoUtente.Id,
                NomeCompleto = dto.NomeCompleto,
                Genero = dto.Genero,
                Email = dto.Email,
                Telemovel = dto.Telemovel,
                Morada = dto.Morada
            };

        }
        public async Task<UtenteNaoRegistadoDTO?> BuscarUentente(int idUtentNR)
        {
            var utente = await _repository.ObterPorUtenteNaoRegistado(idUtentNR);
            if(utente == null) return null;

            return new UtenteNaoRegistadoDTO
            {
                Id = utente.Id,
                NomeCompleto = utente.NomeCompleto,
                DataNascimento = utente.DataNascimento,
                Genero = utente.Genero,
                Telemovel = utente.Telemovel,
                Email = utente.Email,
                Morada = utente.Morada
            };

        }

        public async Task<List<UtenteNaoRegistadoDTO>> ListarTodos()
        {
            var lista = await _repository.ListarTodos();

            return lista.Select(utente => new UtenteNaoRegistadoDTO
            {
                Id = utente.Id,
                NomeCompleto = utente.NomeCompleto,
                DataNascimento = utente.DataNascimento,
                Genero = utente.Genero,
                Telemovel = utente.Telemovel,
                Email = utente.Email,
                Morada = utente.Morada
            }).ToList();
        }
    }
}
