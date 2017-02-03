using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public interface IBlackjackUI
    {
        BlackJackGame BlackJack { get; set; }
        void StartGame();
        void Game();
        void EndGame(GameResult rs);
    }
}
