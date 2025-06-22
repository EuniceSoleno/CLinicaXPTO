using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLinicaXPTO.DAL.Repositories;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Model;
using CLinicaXPTO.Model.Enumerados;
using CLinicaXPTO.Share.Repositories_Interface;
using CLinicaXPTO.Share.Services_Interfaces;

namespace CLinicaXPTO.Services
{
    public class TabelaHorarioService : ITabelaHorarioService
    {
        
            private readonly ITabelaHorarioRepository _horarioRepo;
            private readonly IProfissionalRepository _profissionalRepo;

            public TabelaHorarioService(
                ITabelaHorarioRepository horarioRepo,
                IProfissionalRepository profissionalRepo)
            {
                _horarioRepo = horarioRepo;
                _profissionalRepo = profissionalRepo;
            }

            public async Task<TabelaDeHorarioDTO> BuscarHorarioProfissional(string nomeProfissional)
            {
                var profissional = await _profissionalRepo.BuscarProfissionalNome(nomeProfissional);
                if (profissional == null) throw new Exception("Profissional não encontrado.");

                var horario = await _horarioRepo.BuscarPorProfissionalId(profissional.Id);
                if (horario == null) throw new Exception("´Não existe nenhum horario definido para este profissional!");

                return new TabelaDeHorarioDTO
                {
                    HoraInicio = horario.HoraInicio,
                    HoraFim = horario.HoraFim,
                    Intervalo = horario.Intervalo,
                    DiasSemana = horario.DiasSemana
                };
            }

            public async Task<bool> DefinirHorario(TabelaDeHorario tabelaDeHorario)
            {
                return await _horarioRepo.CriarHorario(tabelaDeHorario);
            }

            public async Task<bool> AlterarHorarioInicioFim(string nomeProfissional, TimeSpan inicio, TimeSpan fim)
            {
                var profissional = await _profissionalRepo.BuscarProfissionalNome(nomeProfissional);
                var horario = await _horarioRepo.BuscarPorProfissionalId(profissional.Id);

                horario.HoraInicio = inicio;
                horario.HoraFim = fim;

                return await _horarioRepo.AtualizarHorario(horario);
            }

            public async Task<bool> AlterarIntervalo(string nomeProfissional, TimeSpan intervalo)
            {
                var profissional = await _profissionalRepo.BuscarProfissionalNome(nomeProfissional);
                var horario = await _horarioRepo.BuscarPorProfissionalId(profissional.Id);

                horario.Intervalo = intervalo;
                return await _horarioRepo.AtualizarHorario(horario);
            }

            public async Task<bool> AdicionarDiaSemana(string nomeProfissional, DayOfWeek novoDia)
            {
                var profissional = await _profissionalRepo.BuscarProfissionalNome(nomeProfissional);
                var horario = await _horarioRepo.BuscarPorProfissionalId(profissional.Id);

                if (!horario.DiasSemana.Contains(novoDia))
                {
                    var dias = horario.DiasSemana.ToList();
                    dias.Add(novoDia);
                    horario.DiasSemana = dias.ToList();
                    return await _horarioRepo.AtualizarHorario(horario);
                }

                return false;
            }

            public async Task<bool> RemoverDiaSemana(string nomeProfissional, DayOfWeek diaRemover)
            {
                var profissional = await _profissionalRepo.BuscarProfissionalNome(nomeProfissional);
                var horario = await _horarioRepo.BuscarPorProfissionalId(profissional.Id);

                if (horario.DiasSemana.Contains(diaRemover))
                {
                    horario.DiasSemana = horario.DiasSemana.Where(d => d != diaRemover).ToList();

                    return await _horarioRepo.AtualizarHorario(horario);
                }

                return false;
            }

            public async Task<bool> EliminarHorario(string nomeProfissional)
            {
                var profissional = await _profissionalRepo.BuscarProfissionalNome(nomeProfissional);
                var horario = await _horarioRepo.BuscarPorProfissionalId(profissional.Id);
                return await _horarioRepo.RemoverHorario(horario);
            }
    }
}
