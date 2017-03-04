using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    public static class BlackJackHelper
    {
        private const int _cardDeckSize = 52;
        private const int _acesValue = 11;
        private const int _jqkValue = 10;

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

        public static GameResult Result(Player player, Player dealer)
        {
            GameResult res = GameResult.Win;

            double playerVal = HandValue(player.Hand);
            double dealerVal = HandValue(dealer.Hand);

            if (playerVal <= 21 && dealerVal <= 21 )
            {
                res = (playerVal > dealerVal) ? GameResult.Win : (playerVal == dealerVal) ? GameResult.Draw : GameResult.Lose;
            }

            if (playerVal > 21 && dealerVal > 21 )
            {
                res = (playerVal < dealerVal) ? GameResult.Win : (playerVal == dealerVal) ? GameResult.Draw : GameResult.Lose;
            }

            if (playerVal > 21 && dealerVal <= 21)
            {
                res = GameResult.Lose;
            }

            if (dealerVal > 21 && playerVal <= 21)
            {
                res = GameResult.Win;
            }
   
            return res;
        }

        #region Helpers

        private static CardDeck FillDeck()
        {

            CardDeck d = new CardDeck();
            d.Deck = new Stack<Card>(_cardDeckSize);
            int value;
            string nam;

            foreach (var suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (var name in Enum.GetValues(typeof(CardName)))
                {
                    value = (int)name;
                    nam = ((int)name > 10) ? name.ToString() : value.ToString();
                    value = ((int)name == 14) ? _acesValue : ((int)name < 11) ? value : _jqkValue;
                    d.Deck.Push(FillCard(nam, suit.ToString(), value));
                }
            }
            return d;
        }

        private static Card FillCard(string name, string suit, int value)
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

        #endregion Helpers
    }
}
