using CLinicaXPTO.DTO;
using CLinicaXPTO.Share.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLinicaXPTO_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtenteNaoRegistadoController : ControllerBase
    {
        private readonly IUtenteNaoRegistadoService _service;

        public UtenteNaoRegistadoController(IUtenteNaoRegistadoService service)
        {
            _service = service;
        }

        // POST: api/UtenteNaoRegistado
        [HttpPost]
        public async Task<ActionResult<UtenteNaoRegistadoDTO>> Post([FromBody] UtenteNaoRegistadoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AdicionarUtente(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // GET: api/UtenteNaoRegistado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtenteNaoRegistadoDTO>> GetById(int id)
        {
            var utente = await _service.BuscarUentente(id);
            if (utente == null)
                return NotFound();

            return Ok(utente);
        }

        // ✅ GET: api/UtenteNaoRegistado
        [HttpGet]
        public async Task<ActionResult<List<UtenteNaoRegistadoDTO>>> GetTodos()
        {
            var utentes = await _service.ListarTodos();
            return Ok(utentes);
        }
    }

}
