using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez('c', 7);

            Console.WriteLine(pos);
            Console.WriteLine(pos.toPosicao());

            Console.ReadLine();
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