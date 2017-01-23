using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        /// <summary>
        /// Метод для отрисовки игры в консоли 
        /// </summary>
        /// <param name="bj">Игра</param>
        public static void Show(BlackJackGame bj)
        {
            Console.WriteLine("-----Диллер-----");
            foreach (Card c in bj.Dealer.Hand)
            {
                Console.WriteLine(string.Format("{0}{1}",c.Name, c.Suit));
            }
            Console.WriteLine("Всего очков: " + bj.Dealer.Hand.Value);

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("-----Игрок-----");
            foreach (Card c in bj.Player.Hand)
            {
                Console.WriteLine(string.Format("{0}{1}", c.Name, c.Suit));
            }
            Console.WriteLine("Всего очков: " + bj.Player.Hand.Value);

            Console.WriteLine(Environment.NewLine);


        }

        /// <summary>
        /// Точка входа в игру и взаимодействие с пользователем
        /// </summary>
        public static void Main()
        {
            string input = "";

            BlackJackGame blj = new BlackJackGame(18);
            Show(blj);
            Console.WriteLine("Взять следующую карту - нажмите Y/y, чтобы остановиться - нажмите любую клавишу!");
            while (blj.Result == GameResult.Pending)
            {
                
                input = Console.ReadLine();

                if (input.ToLower() == "y")
                {
                    blj.Hit();
                    Show(blj);
                    Console.WriteLine("Взять следующую карту - нажмите Y/y, чтобы остановиться - нажмите любую клавишу!");
                }
                else
                {
                    blj.Stand();
                    Show(blj);   
                }
            }

            Console.WriteLine(blj.Result);
            Console.WriteLine("***Игра окончена***");
            Console.ReadLine();
        }
    }
}
