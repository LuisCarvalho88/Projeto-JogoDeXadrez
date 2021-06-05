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

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos); //pega e retorna a peca que está na posição, se for null a casa ta livra ou se a cor da peça for diferente da cor do torre ou seja, uma peça advesária
            return p == null || p.cor != cor;
        }

        public override bool[,] MovimentosPossiveis() //movimentos possiveis do torre Henrança e sobreposição
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas]; //estrutura de dados matriz
            Posicao pos = new Posicao(0, 0);

            //verificando acima da posicao atual do torre
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha - 1;
            }
            //verificando abaixo da posicao atual do torre
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.linha = pos.linha + 1;
            }
            //verificando direita da posicao atual do torre
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.coluna = pos.coluna + 1;
            }
            //verificando esquerda da posicao atual do torre
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.coluna = pos.coluna - 1;
            }

            return mat;
        }
    }
}
