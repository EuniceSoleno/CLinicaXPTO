using CLinicaXPTO.DTO;
using CLinicaXPTO.Interface.Services_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CLinicaXPTO.Services;


namespace CLinicaXPTO_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtenteController : ControllerBase
    {
        private readonly IUtenteServiceInterface _utenteServiceInterface;
        public UtenteController(IUtenteServiceInterface utenteServiceInterface)
        {
            _utenteServiceInterface = utenteServiceInterface;
        }
        [HttpPost("registrar")]
        public async Task<ActionResult<UtenteDTO>> RegistarUtente([FromBody] UtenteDTO utenteDTO)
        {
            try
            {
                var utenteCriado = await _utenteServiceInterface.CriarUtente(utenteDTO);
                return Ok(utenteCriado);
            }
            catch (Exception ex) 
            {
                return BadRequest(new
                {
                    erro = ex.Message,
                    detalhe = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("buscar_email")]
        public async Task<ActionResult<UtenteDTO>> BuscarPeloEmail(string email)
        {
            var utente = await _utenteServiceInterface.Buscar_Email(email);
            if (utente == null)
                return BadRequest($"Utente com email'{email}' não encontrado.");

            return Ok(utente);
        }
        [HttpGet("buscar_nome")]
        public async Task<ActionResult<UtenteDTO>> BuscarPeloNome(string nome)
        {
            var utente = await _utenteServiceInterface.Buscar_Nome(nome);
            if (utente == null)
                return BadRequest($"Utente com nome'{nome}' não encontrado.");

            return Ok(utente);
        }
        [HttpGet("buscar_idUtente")]
        public async Task<ActionResult<UtenteDTO>> BuscarPeloId(int idUtente)
        {
            var utente = await _utenteServiceInterface.Buscar_Id(idUtente);
            if (utente == null)
                return BadRequest($"Utente com o id'{idUtente}' não encontrado.");

            return Ok(utente);
        }

        [HttpPut]
        public async Task<ActionResult<UtenteDTO>> ActualizarUtente([FromBody] UtenteDTO utenteAtualizado)
        {
            var utenteExistente = await _utenteServiceInterface.UpdateUtente(utenteAtualizado);
            if (utenteExistente == null)
                return NotFound("Utente não encontrado para atualização.");

            return Ok(utenteExistente);
        }

        [HttpGet("listar_utentes")]
        public async Task<ActionResult<List<UtenteDTO>>> ListarUtente()
        {
            var utentes = await _utenteServiceInterface.ListarUtentes();
            return Ok(utentes);
        }

        [HttpDelete("remover_pelo_email")]
        public async Task<ActionResult<UtenteDTO>> RemoverPeloEmail (string email)
        {
            var removido = await _utenteServiceInterface.RemoverUtente(email);
            if (!removido)
                return NotFound($"Utente com email '{email}' não encontrado.");

            return NoContent();
        }
        [HttpDelete("remover_pelo_id")]
        public async Task<ActionResult<UtenteDTO>> RemoverPeloId(int id)
        {
            var removido = await _utenteServiceInterface.RemoverUtente_ID(id);
            if (!removido)
                return NotFound($"Utente com id '{id}' não encontrado.");

            return NoContent();
        }


    }
}
