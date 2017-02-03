using BlackJack.Interfaces;
using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public static class BlackJackHelper
    {
        public const int cardDeckSize = 52;
        public const int acesValue = 11;

        public static CardDeck FillDeck()
        {
            string[] names = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            string[] suits = { "♣", "♦", "♥", "♠" };

            CardDeck d = new CardDeck();
            d.Deck = new Stack<Card>(cardDeckSize);
            int value;
            foreach (string s in suits)
            {
                foreach (string n in names)
                {
                    value = Int32.TryParse(n, out value) ? value : n == "A" ? acesValue : 10;
                    d.Deck.Push(FillCard(n, s, value));
                }
            }
            return d;
        }

        static Card FillCard(string name, string suit, int value)
        {
            Card result = new Card();

            if (name != string.Empty && suit != string.Empty && value != 0)
            {

                result.Name = name;
                result.Suit = suit;
                result.Value = value;
            }
            return result;
        }

        public static Stack<Card> ShoffledDeck()
        {
            CardDeck res = new CardDeck();
            res.Deck = new Stack<Card>(FillDeck().Deck.OrderBy(c => Guid.NewGuid()).ToArray());
            return res.Deck;
        }

        public static double HandValue(CardDeck deck)
        {
            double value = deck.Deck.Sum(c => c.Value);
            return value;
        }

        public static bool CanDealerHit(CardDeck deck, int standlim)
        {
            deck.Value = HandValue(deck);
            return deck.Value < standlim;
        }

        public static bool CanPlayerHit(CardDeck deck)
        {
            deck.Value = HandValue(deck);
            return deck.Value < 21;
        }

        public static GameResult Result(IBlackJackPlayer player, IBlackJackPlayer dealer)
        {
            GameResult res = GameResult.Win;

            double playerVal = HandValue(player.Hand);
            double dealerVal = HandValue(dealer.Hand);

            if (playerVal <= 21 && dealerVal <= 21 && playerVal != dealerVal)
            {
                res = playerVal > dealerVal ? GameResult.Win : GameResult.Lose;
            }

            if (playerVal > 21 && dealerVal > 21 && playerVal != dealerVal)
            {
                res = playerVal < dealerVal ? GameResult.Win : GameResult.Lose;
            }

            if (playerVal > 21 && dealerVal <= 21)
            {
                res = GameResult.Lose;
            }

            if (dealerVal > 21 && playerVal <= 21)
            {
                res = GameResult.Win;
            }

            if (playerVal == dealerVal)
            {
                res = GameResult.Draw;
            }
            return res;
        }
    }
}
