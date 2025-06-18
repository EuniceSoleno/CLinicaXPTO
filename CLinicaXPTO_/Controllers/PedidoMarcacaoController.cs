using CLinicaXPTO.DTO;
using CLinicaXPTO.Interface.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLinicaXPTO_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoMarcacaoController : ControllerBase
    {
        private IPedidoMarcacaoService _pedidoService;
        public PedidoMarcacaoController(IPedidoMarcacaoService pedido)
        {
            _pedidoService = pedido;
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
        
        
    }
}
