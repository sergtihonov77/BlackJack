using BlackJack.Models;
using System.Collections.Generic;

namespace BlackJack
{
    public class BlackJackGame

    {
        private const int _startCards = 2;

        private const int _dealerStandLim = 18;

        public Player Player { get; set; }

        public Player Dealer { get; set; }

        public GameResult Result { get; set; }

        public ConsoleBlackjackUI UserInterface { get; set; }

        public CardDeck MainDeck;

        public BlackJackGame()
        {
            Result = GameResult.Pending;
            UserInterface = new ConsoleBlackjackUI(this);

            MainDeck = new CardDeck();
            MainDeck.Deck = new Stack<Card>();
            MainDeck.Deck = BlackJackHelper.ShoffledDeck();

            Dealer = new Player();
            Dealer.Hand = new CardDeck();
            Dealer.Hand.Deck = new Stack<Card>();
            Player = new Player();
            Player.Hand = new CardDeck();
            Player.Hand.Deck = new Stack<Card>();
            GetStartCards();
        }

        public void GetStartCards()
        {
            for (int i = 0; i++ < _startCards;)
            {
                Dealer.Hand.Deck.Push(MainDeck.Deck.Pop());
                Player.Hand.Deck.Push(MainDeck.Deck.Pop());
            }
        }

        public void Hit()
        {
            if (BlackJackHelper.CanPlayerHit(Player.Hand) && Result == GameResult.Pending)
            {
                Player.Hand.Deck.Push(MainDeck.Deck.Pop());
            }
        }

        public void Stand()
        {          
                while (BlackJackHelper.CanDealerHit(Dealer.Hand, _dealerStandLim))
                {
                    if (Result == GameResult.Pending)
                    {
                        Dealer.Hand.Deck.Push(MainDeck.Deck.Pop());
                    }
                }

                Result = BlackJackHelper.Result(Player, Dealer);
        }

    }
}
