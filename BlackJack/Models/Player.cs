using BlackJack.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
{
    public class Player: IBlackJackPlayer
    {
        public CardDeck Hand { get; set; }
    }
}
