using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {

        public static void Main()
        {
            BlackJackGame newgame = new BlackJackGame(20);
            newgame.UserInterface.Game();
        }
    }
}
