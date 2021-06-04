using tabuleiro;

namespace xadrez
{
    class Torre : Peca // torre é um herança de peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) //repasse da execução para a superclasse usando dois pontos base
        {
        }
        public override string ToString()
        {
            return "T";
        }

    }
}
