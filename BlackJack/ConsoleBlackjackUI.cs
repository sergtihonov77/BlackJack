using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
        public class ConsoleBlackjackUI : IBlackjackUI
    {
        public BlackJackGame BlackJack { get; set; }

        public ConsoleBlackjackUI(BlackJackGame game)
        {
            BlackJack = game;
        }  
           
        private void Show(BlackJackGame bj)
        {
            Console.WriteLine("-----Diller-----");
            foreach (Card c in bj.Dealer.Hand.Deck)
            {
                Console.WriteLine(string.Format("{0}{1}", c.Name, c.Suit));
            }
            Console.WriteLine("Total score: " + BlackJackHelper.HandValue(bj.Dealer.Hand));

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("-----Player-----");
            foreach (Card c in bj.Player.Hand.Deck)
            {
                Console.WriteLine(string.Format("{0}{1}", c.Name, c.Suit));
            }
            Console.WriteLine("Total score: " + BlackJackHelper.HandValue(bj.Player.Hand));

            Console.WriteLine(Environment.NewLine);
        }

        public void StartGame()
        {
            Show(BlackJack);
        }

        public void  Game()
        {
            string input = "";
            StartGame();
            Console.WriteLine("Take the next card - Press Y / y to stop - press any key!");
            while (BlackJack.Result == GameResult.Pending)
            {

                input = Console.ReadLine();

                if (input.ToLower() == "y")
                {
                    BlackJack.Hit();
                    Show(BlackJack);
                    Console.WriteLine("Take the next card - Press 'y' to stop - press any key!");
                }
                else
                {
                    BlackJack.Stand();
                    Show(BlackJack);
                }
            }

            EndGame(BlackJack.Result);
            Console.ReadLine();
        }

        public void EndGame(GameResult res)
        {
            switch (res)
            {
                case GameResult.Win:
                    Console.WriteLine("You Win!!!!");
                    break;

                case GameResult.Lose:
                    Console.WriteLine("Sorry, You Lose");
                    break;

                case GameResult.Draw:
                    Console.WriteLine("Draw!");
                    break;                         
            }
            
        }

    }
}
