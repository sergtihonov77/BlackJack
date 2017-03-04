using System.Collections.Generic;

namespace BlackJack.Models
{
    public class CardDeck
    {
        public Stack<Card> Deck { get; set; }
        public double Value { get; set; }
    }
}
