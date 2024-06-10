using Jokenpo.Models;
using Jokenpo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jokenpo.Controllers
{

    [ApiController]
    [Route("jokenpo")]
    public class JokenpoController : Controller
    {
        private readonly JokenpoService _jokenpoService;

        public JokenpoController(JokenpoService jokenpoService)
        {
            _jokenpoService = jokenpoService;
        }

        [HttpPost("jogador")]
        public ActionResult<Jogador> CadastrarJogador([FromBody] string nome)
        {
            var jogador = _jokenpoService.CadastrarJogador(nome);
            return Ok(jogador);
        }

        [HttpDelete("jogador/{jogadorNome}")]
        public ActionResult RemoverJogador(string jogadorNome)
        {
            _jokenpoService.RemoverJogador(jogadorNome);
            return NoContent();
        }

        [HttpPost("jogada")]
        public ActionResult Post( string jogadorNome, Jogadas jogada)
        {
            _jokenpoService.CadastrarJogada(jogadorNome, jogada);
            return Ok(jogada);
        }

        [HttpGet("jogadores")]
        public ActionResult<List<Jogador>> JogadoresCadastrados()
        {
            var jogadores = _jokenpoService.JogadoresCadastrados();
            return Ok(jogadores);
        }

        [HttpGet("jogadas")]
        public ActionResult<List<Jogador>> JogadasRealizadas()
        {
            var jogadas = _jokenpoService.JogadasRealizadas();
            return Ok(jogadas);
        }

        [HttpGet("rodada")]
        public ActionResult<string> FinalizarRodada()
        {
            var resultadoRodada = _jokenpoService.FinalizarRodada();
            return Ok(resultadoRodada);
        }
    }
}
