using Jokenpo.Models;

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

        public void CadastrarJogada(string jogadorNome, Jogadas jogada)
        {
            var jogador = _jogadores.First(jgd => jgd.Nome == jogadorNome);
            if (jogador != null)
            {
                jogador.Jogada = jogada;
                _jogadas.Add(jogador);
            }

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
            var jogadasVencedoras = new Dictionary<Jogadas, List<Jogadas>>
            {
                { Jogadas.Pedra, new List<Jogadas> { Jogadas.Tesoura, Jogadas.Lagarto } },
                { Jogadas.Papel, new List<Jogadas> { Jogadas.Pedra, Jogadas.Spock } },
                { Jogadas.Tesoura, new List<Jogadas> { Jogadas.Papel, Jogadas.Lagarto } },
                { Jogadas.Lagarto, new List<Jogadas> { Jogadas.Papel, Jogadas.Spock } },
                { Jogadas.Spock, new List<Jogadas> { Jogadas.Pedra, Jogadas.Tesoura } }
            };

            var vitorias = new Dictionary<Jogador, int>();

            foreach (var jogador in jogadas)
            {
                vitorias[jogador] = 0;
            }

            foreach (var jogador1 in jogadas)
            {
                foreach (var jogador2 in jogadas)
                {
                    if (jogador1 == jogador2) continue;

                    if (jogadasVencedoras[jogador1.Jogada].Contains(jogador2.Jogada))
                    {
                        vitorias[jogador1]++;
                    }
                }
            }

            return null;
        }

    }
}
