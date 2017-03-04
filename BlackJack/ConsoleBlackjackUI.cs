using BlackJack.Models;
using System;

namespace BlackJack
{
    public class ConsoleBlackjackUI 
    {
        public BlackJackGame BlackJack { get; set; }

        public ConsoleBlackjackUI(BlackJackGame game)
        {
            BlackJack = game;
        }  
             
        public void StartGame()
        {
            Show(BlackJack);
            Console.Title = "BLACKJACK";
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

        #region Helpers

        private void Show(BlackJackGame bj)
        {
            Console.WriteLine("-----Dealer-----");
            foreach (Card c in bj.Dealer.Hand.Deck)
            {
                Console.WriteLine(string.Format("{0}{1}", c.Name, ShowSuit(c.Suit)));
            }
            Console.WriteLine("Total score: " + BlackJackHelper.HandValue(bj.Dealer.Hand));

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("-----Player-----");
            foreach (Card c in bj.Player.Hand.Deck)
            {
                Console.WriteLine(string.Format("{0}{1}", c.Name, ShowSuit(c.Suit)));
            }
            Console.WriteLine("Total score: " + BlackJackHelper.HandValue(bj.Player.Hand));

            Console.WriteLine(Environment.NewLine);
        }

        private string ShowSuit(string suit)
        {
            string res = "";
            switch (suit)
            {
                case "Spades":
                    res = "♠";
                    break;
                case "Hearts":
                    res = "♥";
                    break;
                case "Diamonds":
                    res = "♦";
                    break;
                case "Clubs":
                    res = "♣";
                    break;
            }
            return res;


        }

        #endregion Helpers

    }
}
