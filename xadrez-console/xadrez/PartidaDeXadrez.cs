using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez // classe partida de xadrez
    {
        public Tabuleiro tab { get; private set; }//encapsulamento método privado 
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        /// <summary>
        /// metodo executa movimento, tira a peca da origem e envia para o destino
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            //p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) // valida se a peca capturada for diferente de null eu insiro ela em pecacapturada
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos) // metodo validar posiçao de origem
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida ");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua ");
            }
            if (!tab.peca(pos).existeMovimentoPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida! ");
            }
        }

        /// <summary>
        /// Metodo que valida se a posição de destino da peca é válida
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) // metodo validar posiçao de origem
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida ");
            }
        }

        /// <summary>
        /// metodo privado que muda o jogador pela cor
        /// </summary>
        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        /// <summary>
        /// metodo para ver quais as pecas capturadas de uma cor especifica
        /// </summary>
        /// <param name="cor"></param>
        /// <returns></returns>
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) // percorre todas as pecas capturadas
            {
                if (x.cor == cor) // se x for igual a cor eu adiciono a pexa no aux
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        /// <summary>
        /// metodo que irá retornar todas as pecas em jogo de uma determinada cor
        /// </summary>
        /// <param name="cor"></param>
        /// <returns></returns>
        public HashSet<Peca> pecasEmjogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) // percorre todas as pecas capturadas
            {
                if (x.cor == cor) // se x for igual a cor eu adiciono a pexa no aux
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        /// <summary>
        /// metodo vai colocar no tabuleiro a peca em uma nova posicao
        /// </summary>
        /// <param name="coluna"></param>
        /// <param name="linha"></param>
        /// <param name="peca"></param>
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        /// <summary>
        /// metodo colocarpecas inseri as pecas nas posições no tabuleiro
        /// </summary>
        private void colocarPecas() //Delega a instanciação das peças da partida para a classe Partida de Xadrez
        {

            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
        }
    }
}
