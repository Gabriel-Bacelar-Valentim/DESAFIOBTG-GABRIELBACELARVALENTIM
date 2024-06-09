namespace Jokenpo.Models
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Jogadas Jogada { get; set; }
    }
}
