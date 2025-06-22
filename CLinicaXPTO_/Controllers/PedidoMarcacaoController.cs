using CLinicaXPTO.DAL.Migrations;
using CLinicaXPTO.DTO;
using CLinicaXPTO.Interface.Services_Interfaces;
using CLinicaXPTO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLinicaXPTO_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoMarcacaoController : ControllerBase
    {
        private IPedidoMarcacaoService _pedidoService;
        private IUtenteServiceInterface _utenteService;
        public PedidoMarcacaoController(IPedidoMarcacaoService pedido, IUtenteServiceInterface utenteService)
        {
            _pedidoService = pedido;
            _utenteService = utenteService;
        }

        [HttpPost("registrar_pedido")]
        public async Task<ActionResult<PedidoMarcacaoDTO>> RegistarPedido([FromBody] PedidoMarcacaoDTO pedidoDto)
        {
            try
            {
                var pedidoCriado = await _pedidoService.RegistrarMarcacao(pedidoDto);
                return Ok(pedidoCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<PedidoMarcacaoDTO>> BuscarMarcacao(int idMarcacao)
        {
            try
            {
                var pedidoObtido = await _pedidoService.BuscarMarcacao(idMarcacao);
                return Ok(pedidoObtido);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPut("actualizar_pedido")]
        public async Task<ActionResult<PedidoMarcacaoDTO>> ActualizarMarcaca([FromBody] PedidoMarcacaoDTO pedidoDto)
        {
            try
            {
                var pedidoActualizado = await _pedidoService.ActualizarMarcacao(pedidoDto);
                return Ok(pedidoActualizado);

            }catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
        [HttpDelete("eliminar_pedido")]
        public async Task<ActionResult<PedidoMarcacaoDTO>> EliminarPedido(int idMarcacao)
        {
            try
            {
                var pedidoEliminado = await _pedidoService.ElieminarMarcacao(idMarcacao);
                return Ok(pedidoEliminado);

            }catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("listar_pedido")]
        public async Task<ActionResult<List<PedidoMarcacaoDTO>>> ListarPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.ListarMarcacoes();
                return Ok(pedidos);
            }catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpPost("registrar_nao_registado")]
        [AllowAnonymous]
        public async Task<ActionResult<PedidoMarcacaoDTO>> RegistarPedidoNaoRegistado([FromBody] PedidoMarcacaNaoRegistadoDTO dto)
        {
            try
            {
                var pedidoCriado = await _pedidoService.RegistrarMarcacaoNaoRegistado(dto);
                return Ok(pedidoCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("converter-nao-registado/{id}")]
        /*[Authorize(Roles = "Administrativo")]*/
        public async Task<IActionResult> ConverterUtenteNaoRegistado(int id)
        {
            try
            {
                var utenteCriado = await _utenteService.ConverterDeNaoRegistadoAsync(id);
                return Ok(utenteCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[Authorize(Roles = "Administrativo")]*/

        [HttpPut("agendar/{id}")]
        public async Task<IActionResult>AgendarMarcacao(int id)
        {
            try
            {
                var agendado = await _pedidoService.AgendarMarcacao(id);
                if(!agendado)
                    return BadRequest("A marcação não pôde ser agendada.");

                return Ok("Marcação agendada com sucesso.");


            }catch(Exception ex)
            {
                return BadRequest($"Erro ao agendar marcação: {ex.Message}");

            }
        }

        [HttpPut("realizar/{idMarcacao}")]
        public async Task<IActionResult>RealizarMarcacao(int idMarcacao)
        {
            try
            {
                var sucesso = await _pedidoService.RealizarMarcacao(idMarcacao);
                if (!sucesso)
                    return BadRequest("Não foi possível realizar a marcação.");

                return Ok("Marcação realizada com sucesso.");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message );
            }
        }


    }
}
