﻿namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            this.cor = cor;
            this.tab = tab;
            this.qtdMovimentos = 0;
        }
        public void incrementarQtdMovimentos()
        {
            qtdMovimentos++;
        }
        public void decrementarQtdMovimentos()
        {
            qtdMovimentos--;
        }

        public bool existeMovimentoPossiveis() //metodo existe movimentos possiveis
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// metodo para validar a posicao de destino
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool movimentoPossivel(Posicao pos)
        {
            return MovimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] MovimentosPossiveis();

    }
}
