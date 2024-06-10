using Jokenpo.Models;
using System.Linq;

namespace Jokenpo.Services
{
    public class JokenpoService
    {
        private readonly List<Jogador> _jogadores = new List<Jogador>();
        private readonly List<Jogador> _jogadas = new List<Jogador>();
        private int _proximoId = 1;
        private int _numeroRodadas = 0;
        public int NumeroRodadas => _numeroRodadas;


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
            var jogador = _jogadores.LastOrDefault(jgd => jgd.Nome == jogadorNome);
            if (jogador != null)
            {
                jogador.Jogada = jogada;
                _jogadas.Add(jogador);
                return jogada;
            }
            return default(Jogadas);
        }
        public List<Jogador> JogadoresCadastrados()
        {

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
            return vencedor == null ? $"Empate" : $"Vencedor : {vencedor.Nome}";
        }

        public void ResetarJogadas()
        {
            _jogadas.Clear();
        }
        public void IncrementarRodadas()
        {
            _numeroRodadas++;
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
            Jogador vencedor = null;
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
                if (vencedor == null || vitorias[jogador1] > vitorias[vencedor])
                {
                    vencedor = jogador1;
                }
            }
            if (vencedor != null)
            {
                vencedor.Vitorias++;
            }
            return vencedor;
        }
    }
}