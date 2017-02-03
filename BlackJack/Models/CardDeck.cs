using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
{
    public class CardDeck
    {
        public Stack<Card> Deck { get; set; }
        public double Value { get; set; }
    }
}
