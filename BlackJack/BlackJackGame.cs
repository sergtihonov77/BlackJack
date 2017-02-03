using BlackJack.Interfaces;
using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class BlackJackGame

    {
        public const int startCards = 2;

        public IBlackJackPlayer Player { get; set; }

        public IBlackJackPlayer Dealer { get; set; }

        public GameResult Result { get; set; }

        public IBlackjackUI UserInterface { get; set; }

        public int StandLim { get; set; }

        public CardDeck MainDeck;

        public BlackJackGame(int dealerStandLim)
        {
            Result = GameResult.Pending;
            UserInterface = new ConsoleBlackjackUI(this);
            StandLim = dealerStandLim;

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
            for (int i = 0; i++ < startCards;)
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
            if (Result == GameResult.Pending)
            {
                while (BlackJackHelper.CanDealerHit(Dealer.Hand, StandLim))
                {
                    Dealer.Hand.Deck.Push(MainDeck.Deck.Pop());
                }

                Result = BlackJackHelper.Result(Player, Dealer);
            }
        }

    }
}
