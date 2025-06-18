using CLinicaXPTO.DTO;
using CLinicaXPTO.Services;
using CLinicaXPTO.Share.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLinicaXPTO_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalService _service;

        public ProfissionalController(IProfissionalService service)
        {
            _service = service;
        }
        [HttpGet("listar_Profissionais")]
        public async Task<ActionResult<List<ProfissionalDTO>>> ListarProfissionais()
        {
            try
            {
                var profissionais = await _service.ListarProfissional();
                return Ok(profissionais);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("buscar_profissional")]
        public async Task<ActionResult<ProfissionalDTO>> BuscarProfissional(int idProfissional)
        {
            try
            {
                var profissional = await _service.BuscarProfissional(idProfissional);
                return Ok(profissional);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("adicionar_profissional")]
        public async Task<ActionResult<ProfissionalDTO>> RegistarProfissional([FromBody] ProfissionalDTO profissionalDTO)
        {
            try
            {
                var profissionalCriado = await _service.AdicionarProficional(profissionalDTO);
                return Ok(profissionalCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("actualizar_profissional")]
        public async Task<ActionResult<ProfissionalDTO>> ActualizarProfissional([FromBody] ProfissionalDTO profissional)
        {
            try
            {
                var profissionalActualizado = await _service.ActualizarProfissional(profissional);
                return Ok(profissionalActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar profissional: " + ex.Message);
            }
        }

        [HttpDelete("delete_profissional")]
        public async Task<ActionResult<ProfissionalDTO>> EliminarProfissiona(int idProfissional)
        {
            try
            {
                var profissionalEliminado = await _service.ElieminarProfissional(idProfissional);
                return Ok(profissionalEliminado);

            }catch(Exception ex)
            {
                return  BadRequest(ex.ToString());
            }
        }

    }
}