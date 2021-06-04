using tabuleiro;

namespace xadrez
{
    class Rei : Peca // rei é um herança de peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }
        public override string ToString()
        {
            return "R";
        }

    }
}
