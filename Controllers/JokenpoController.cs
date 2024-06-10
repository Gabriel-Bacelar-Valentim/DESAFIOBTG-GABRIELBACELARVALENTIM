using Jokenpo.Models;
using Jokenpo.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

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

        [HttpPost("Cadastrar jogador")]
        public ActionResult<Jogador> Post( string nome)
        {
            var jogador = _jokenpoService.CadastrarJogador(nome);
            return Ok(jogador);
        }

        [HttpPost("Inserir jogada")]
        public ActionResult Post(string jogadorNome, string jogada)
        {
            if (!Enum.TryParse<Jogadas>(jogada, true, out var jogadaEnum))
            {
                return BadRequest("Jogada inválida.");
            }

            var resultadoJogada = _jokenpoService.CadastrarJogada(jogadorNome, jogadaEnum);
            if (resultadoJogada != default(Jogadas))
            {
                return Ok(JsonConvert.SerializeObject (resultadoJogada.ToString()));
            }
            return BadRequest("Jogador não encontrado.");
        }

        [HttpDelete("Deletar jogador/{jogadorNome}")]
        public ActionResult RemoverJogador(string jogadorNome)
        {
            _jokenpoService.RemoverJogador(jogadorNome);
            return NoContent();
        }

        [HttpGet("Listar jogadores")]
        public ActionResult<List<Jogador>> JogadoresCadastrados()
        {
            var jogadores = _jokenpoService.JogadoresCadastrados();
            return Ok(jogadores);
        }

        [HttpGet("Listar jogadas")]
        public ActionResult <Jogador> JogadasRealizadas()
        {
            var jogadas = _jokenpoService.JogadasRealizadas();
            return Ok(JsonConvert.SerializeObject(jogadas));
        }

        [HttpGet("Listar Rodada")]
        public ActionResult<string> FinalizarRodada()
        {
            var resultadoRodada = _jokenpoService.FinalizarRodada();
            _jokenpoService.ResetarJogadas();
            return Ok(JsonConvert.SerializeObject(resultadoRodada));
        }
    }
}
