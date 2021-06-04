using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez  //Classe
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha) // contrutor
        {
            this.coluna = coluna; // autoreferencia
            this.linha = linha;
        }

        public Posicao toPosicao() //metodo que converte para posicao
        {
            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString() //sobreposicao tostring
        {
            return ""
                + coluna
                + linha;
        }
    }
}
