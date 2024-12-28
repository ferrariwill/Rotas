
using Domain;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Rotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : ControllerBase
    {
        private readonly IRotaService _rotaService;

        public RotaController(IRotaService rotaService)
        {
            _rotaService = rotaService;
        }

        [HttpGet("melhor-rota")]
        public IActionResult ObterMelhorRota(string origem, string destino)
        {
            var resultado = _rotaService.ObterMelhorRota(origem, destino);

            if (resultado == null)
            {
                return NotFound("Rota não encontrada");
            }

            return Ok(resultado);
        }

        [HttpPost("registrar-rota")]
        public IActionResult RegistrarRota([FromBody] Rota novaRota)
        {
            if (novaRota.Valor <= 0)
            {
                return BadRequest("Valor deve ser maior que 0!");
            }

            if (novaRota.Destino.ToUpper() == novaRota.Origem.ToUpper())
            {
                return BadRequest("Origem e destino são iguais!");
            }

            if (_rotaService.RotaExiste(novaRota.Origem.ToUpper(), novaRota.Destino.ToUpper()))
            {
                return BadRequest("Essa rota já está registrada");
            }
            _rotaService.RegistrarRota(novaRota);
            return Ok("Rota registrada com sucess");
        }
    }
}
