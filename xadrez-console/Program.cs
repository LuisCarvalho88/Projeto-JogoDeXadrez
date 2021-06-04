using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Tabuleiro tab = new Tabuleiro(8, 8); // instaciar um tabuleiro e colocar pecas nele =>
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao(); // metodo origem vai ler do teclado um posicao do xadrez

                    partida.executaMovimento(origem, destino);
                }

                Tela.imprimirTabuleiro(partida.tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

            //PosicaoXadrez pos = new PosicaoXadrez('c', 7);

            //Console.WriteLine(pos);
            //Console.WriteLine(pos.toPosicao());
        }
    }
}












//try
//{
//    Tabuleiro tab = new Tabuleiro(8, 8); // instaciar um tabuleiro e colocar pecas nele =>

//    tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
//    tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
//    tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 9));

//    Tela.imprimirTabuleiro(tab);
//}
//catch (TabuleiroException e)
//{
//    Console.WriteLine(e.Message);
//}