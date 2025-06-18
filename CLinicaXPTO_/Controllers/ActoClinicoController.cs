using CLinicaXPTO.DTO;
using CLinicaXPTO.Share.Services_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CLinicaXPTO_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoClinicoController : ControllerBase
    {
        private readonly IActoClinicoService _actoClinicoService;
        public ActoClinicoController(IActoClinicoService actoClinicoService)
        {
            _actoClinicoService = actoClinicoService;
        }
        [HttpGet("listar_actosClinicos")]
        public async Task<List<ActoClinicoDTO>> ListarActosClinicos()
        {
            try
            {
                var actosClinicos = await _actoClinicoService.ListarActosClinico();
                return actosClinicos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        [HttpGet("buscar_actoClinico")]
        public async Task<ActionResult<ActoClinicoDTO>> BuscarActoClinico(int idActoClinico)
        {
            try
            {
                var acto = await _actoClinicoService.BuscarActoClinico(idActoClinico);
                if(acto == null)
                    return NotFound($"Acto clínico com id {idActoClinico} não encontrado.");
                // return await _actoClinicoService.BuscarActoClinico(idActoClinico);

                return Ok(acto);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar o Acto Clinico no repositorio!", ex);
            }
        }

        [HttpPut("actualizar_actoClinico")]
        public async Task<ActionResult<ActoClinicoDTO>> ActualizarActoClinico([FromBody] ActoClinicoDTO actoClinico)
        {
            try
            {
                var _actoClinico = await _actoClinicoService.ActualizarActoClinico(actoClinico);
                return _actoClinico;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        [HttpDelete("eliminar_actoclinico")]
        public async Task<ActionResult<ActoClinicoDTO>> EliminarActoClinico(int idActoClinico)
        {
            try
            {
                var actoEliminado = await _actoClinicoService.DeleteActoClinico(idActoClinico);
                return actoEliminado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        [HttpPost("criar_actoClinico")]
        public async Task<ActionResult<ActoClinicoDTO>> AdicionarActoClinico([FromBody] ActoClinicoDTO actoClinico)
        {
            try
            {
                var _actoClinico = await _actoClinicoService.CriarActoClinico(actoClinico);
                return _actoClinico;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }



        }
    }
}