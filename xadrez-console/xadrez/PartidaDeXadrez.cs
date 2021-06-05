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
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        /// <summary>
        /// metodo executa movimento, tira a peca da origem e envia para o destino
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            //p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) // valida se a peca capturada for diferente de null eu insiro ela em pecacapturada
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        /// <summary>
        /// metodo que desfaz o movimento 
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        /// <param name="pecaCapturada"></param>
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }

        /// <summary>
        /// metodo para  verificar se estou colocando meu rei em xeque
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

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

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmjogo(cor)) // Peça é uma super classe, para verificar se uma variavel do 
            {                                    // tipo da super classe é uma instacia da subclasse eu tenho que usar a palavra IS
                if (x is Rei) // Rei/Torre é uma subClasse 
                {
                    return x;
                }
            }
            return null;
        }

        /// <summary>
        /// metodo que testa se o rei está em xeque
        /// /// </summary>
        /// <param name="cor"></param>
        /// <returns></returns>
        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca x in pecasEmjogo(adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Metodo que verifica um possivel xequemate contra mim
        /// </summary>
        /// estrutura de dadaos matriz
        /// <param name="cor"></param>
        /// <returns></returns>
        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmjogo(cor)) // toda peça x no conjunto peças em jogo dessa cor
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, new Posicao(i, j));
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            colocarNovaPeca('h', 7, new Torre(tab, Cor.Branca));

            colocarNovaPeca('a', 8, new Rei(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Torre(tab, Cor.Preta));


            //colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            //colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            //colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            //colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            //colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            //colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            //colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
        }
    }
}
