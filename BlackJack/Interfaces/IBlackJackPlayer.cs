using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Interfaces
{
    public interface IBlackJackPlayer
    {
        CardDeck Hand { get; set; }
    }
}
