using CLinicaXPTO.Model.Enumerados;
using CLinicaXPTO.Model;
using CLinicaXPTO.Share.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLinicaXPTO_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TabelaHorarioController : ControllerBase
    {
        private readonly ITabelaHorarioService _service;

        public TabelaHorarioController(ITabelaHorarioService service)
        {
            _service = service;
        }

        // GET: api/TabelaHorario/profissional/nome
        [HttpGet("profissional/{nome}")]
        public async Task<IActionResult> BuscarHorario(string nome)
        {
            var result = await _service.BuscarHorarioProfissional(nome);
            if (result == null)
                return NotFound("Horário não encontrado.");
            return Ok(result);
        }

        // POST: api/TabelaHorario
        [HttpPost]
        public async Task<IActionResult> DefinirHorario([FromBody] TabelaDeHorario horario)
        {
            var result = await _service.DefinirHorario(horario);
            if (!result)
                return BadRequest("Erro ao definir horário.");
            return Ok("Horário definido com sucesso.");
        }

        // PUT: api/TabelaHorario/horario-inicio-fim
        [HttpPut("horario-inicio-fim")]
        public async Task<IActionResult> AlterarInicioFim([FromQuery] string nomeProfissional, [FromQuery] TimeSpan inicio, [FromQuery] TimeSpan fim)
        {
            var result = await _service.AlterarHorarioInicioFim(nomeProfissional, inicio, fim);
            return result ? Ok("Horário atualizado.") : NotFound("Horário não encontrado.");
        }

        // PUT: api/TabelaHorario/intervalo
        [HttpPut("intervalo")]
        public async Task<IActionResult> AlterarIntervalo([FromQuery] string nomeProfissional, [FromQuery] TimeSpan intervalo)
        {
            var result = await _service.AlterarIntervalo(nomeProfissional, intervalo);
            return result ? Ok("Intervalo alterado.") : NotFound("Horário não encontrado.");
        }

        // PUT: api/TabelaHorario/dia
        [HttpPut("dia/adicionar")]
        public async Task<IActionResult> AdicionarDiaSemana([FromQuery] string nomeProfissional, [FromQuery] DayOfWeek dia)
        {
            var result = await _service.AdicionarDiaSemana(nomeProfissional, dia);
            return result ? Ok("Dia adicionado.") : BadRequest("Dia já existente ou erro.");
        }

        [HttpPut("dia/remover")]
        public async Task<IActionResult> RemoverDiaSemana([FromQuery] string nomeProfissional, [FromQuery] DayOfWeek dia)
        {
            var result = await _service.RemoverDiaSemana(nomeProfissional, dia);
            return result ? Ok("Dia removido.") : BadRequest("Erro ao remover dia.");
        }

        // DELETE: api/TabelaHorario/profissional/nome
        [HttpDelete("profissional/{nome}")]
        public async Task<IActionResult> EliminarHorario(string nome)
        {
            var result = await _service.EliminarHorario(nome);
            return result ? Ok("Horário eliminado.") : NotFound("Horário não encontrado.");
        }
    }
}
