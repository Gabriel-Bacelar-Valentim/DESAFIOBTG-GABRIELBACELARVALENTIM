using Jokenpo.Models;
using System.Linq;

namespace Jokenpo.Services
{
    public class JokenpoService
    {
        private readonly List<Jogador> _jogadores = new List<Jogador>();
        private readonly List<Jogador> _jogadas = new List<Jogador>();
        private int _proximoId = 1;


        public Jogador CadastrarJogador(string jogadorNome)
        {
            var jogador = new Jogador { Id = _proximoId++, Nome = jogadorNome };
            _jogadores.Add(jogador);
            return jogador;
        }

        public void RemoverJogador(string jogadorNome)
        {
            _jogadores.RemoveAll(jgd => jgd.Nome == jogadorNome);

        }

        public Jogadas CadastrarJogada(string jogadorNome, Jogadas jogada)
        {
            var jogador = _jogadores.First(jgd => jgd.Nome == jogadorNome);
            if (jogador != null)
            {
                jogador.Jogada = jogada;
                _jogadas.Add(jogador);

            return jogada;
            }
            return null;
        }
        public List<Jogador> JogadoresCadastrados()
        {
            _jogadores.Add(new Jogador { Id = 1, Nome = "Gabriel" });

            return _jogadores;
        }

        public List<Jogador> JogadasRealizadas()
        {
            return _jogadas;
        }

        public string FinalizarRodada()
        {
            var jgdSemJogadas = _jogadores.Except(_jogadas).ToList();

            if (jgdSemJogadas.Any())
            {
                var jogadoresSemJogadas = string.Join(", ", jgdSemJogadas.Select(jgd => jgd.Nome));
                return $"Erro: {jogadoresSemJogadas} ainda não jogou(jogaram).";
            }

            if (_jogadas.Count < _jogadores.Count)
                return "Erro: Nem todos os jogadores jogaram.";

            var vencedor = VencedorRodada(_jogadas);
            return vencedor == null ? "Empate" : $"Vencedor : {vencedor.Nome}";
        }

        private Jogador VencedorRodada(List<Jogador> jogadas)
        {
            var jogadasVencedoras = new Dictionary<string, List<string>>
            {
                { Jogadas.Pedra, new List<string> { Jogadas.Tesoura, Jogadas.Lagarto } },
                { Jogadas.Papel, new List<string> { Jogadas.Pedra, Jogadas.Spock } },
                { Jogadas.Tesoura, new List<string> { Jogadas.Papel, Jogadas.Lagarto } },
                { Jogadas.Lagarto, new List<string> { Jogadas.Papel, Jogadas.Spock } },
                { Jogadas.Spock, new List<string> { Jogadas.Pedra, Jogadas.Tesoura } }
            };

            var vitorias = new Dictionary<string, int>();

            foreach (var jogador in jogadas)
            {
                vitorias[jogador.Nome] = 0;
            }

            foreach (var jogador1 in jogadas)
            {
                foreach (var jogador2 in jogadas)
                {
                    if (jogador1 == jogador2) continue;

                    if (jogadasVencedoras[jogador1.Jogada.ToString()].Contains(jogador2.Jogada.ToString()))
                    {
                        vitorias[jogador1.Nome]++;
                    }
                }
            }

            return null;
        }

    }
}
