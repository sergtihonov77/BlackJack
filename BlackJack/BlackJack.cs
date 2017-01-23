using System;
using System.Linq;

namespace BlackJack
{
    /// <summary>
    /// Класс игры, конструктор принимает на вход количество очков, при которых диллер будет заканчивать игру!
    /// </summary>
    public class BlackJackGame
    {
        //Игрок и диллер
        public Player Player = new Player();
        public Player Dealer = new Player();

        //Результат игры
        public GameResult Result { get; set; }

        //Главная колода
        public CardDeck MainDeck;

        //Количество очков, при которых диллер будет заканчивать игру
        public int StandLim { get; set; }

        /// <summary>
        /// Конструктор игры, тут инциализируется колода и игрокам раздаются первые 2 карты
        /// </summary>
        /// <param name="dealerStandLim">Количество очков, при которых диллер будет заканчивать игру</param>
        public BlackJackGame(int dealerStandLim)
        {
            Result = GameResult.Pending;

            StandLim = dealerStandLim;

            MainDeck = BlackJackHelper.ShoffledDack;

            Dealer.Hand.Clear();
            Player.Hand.Clear();

            for (int i = 0; ++i < 3;)
            {
                Dealer.Hand.Push(MainDeck.Pop());
                Player.Hand.Push(MainDeck.Pop());
            }
        }

        /// <summary>
        /// Ход игрока
        /// </summary>
        public void Hit()
        {
            if (BlackJackHelper.CanPlayerHit(Player.Hand) && Result == GameResult.Pending)
            {
                Player.Hand.Push(MainDeck.Pop());
            }
        }

        /// <summary>
        /// Остановка хода игрока и очередь игры диллера
        /// </summary>
        public void Stand()
        {
            if (Result == GameResult.Pending)
            {
                while (BlackJackHelper.CanDealerHit(Dealer.Hand, StandLim))
                {
                    Dealer.Hand.Push(MainDeck.Pop());
                }

                Result = BlackJackHelper.Result(Player, Dealer);
            }
        }

    }

    /// <summary>
    /// Класс помощник для игры
    /// </summary>
    public static class BlackJackHelper
    {
        /// <summary>
        /// Массив карт
        /// </summary>
        public static string[] names = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        /// <summary>
        /// Массив мастей
        /// </summary>
        public static string[] suits = { "♣", "♦", "♥", "♠" };

        /// <summary>
        /// Инциализатор новой колоды
        /// </summary>
        public static CardDeck NewDeck
        {
            get
            {
                CardDeck d = new CardDeck();
                int value;
                foreach (string s in suits)
                {
                    foreach (string n in names)
                    {
                        value = Int32.TryParse(n, out value) ? value : n == "A" ? 1 : 10;
                        d.Push(new Card(n, s, value));
                    }
                }
                return d;
            }
        }

        /// <summary>
        /// Инициализатор перетасованой колоды
        /// </summary>
        public static CardDeck ShoffledDack
        {
            get
            {
                return new CardDeck(NewDeck.OrderBy(c => Guid.NewGuid()).ToArray());
            }
        }

        /// <summary>
        /// Подсчет очков на руках
        /// </summary>
        /// <param name="deck">Карты, сумму которых нужно подсчитать</param>
        /// <returns></returns>
        public static double HandValue(CardDeck deck)
        {
            int value1 = deck.Sum(c => c.Value);

            double aces = deck.Count(c => c.Name == "A");

            double value2 = aces > 0 ? value1 + (10 * aces) : value1;

            return new double[] { value1, value2 }
            .Select(handVal => new { handVal, weigth = Math.Abs(handVal - 21) + (handVal > 21 ? 100 : 0) })
            .OrderBy(n => n.weigth)
            .First().handVal;
        }

        /// <summary>
        /// Проверка возможности хода диллера
        /// </summary>
        /// <param name="deck">Имеющиеся карты</param>
        /// <param name="standlim">Число очков при котор нужно останавливатся</param>
        /// <returns></returns>
        public static bool CanDealerHit(CardDeck deck, int standlim)
        {
            return deck.Value < standlim;
        }

        /// <summary>
        /// Проверка возможности хода игрока
        /// </summary>
        /// <param name="deck">Имеющиеся карты</param>
        /// <returns></returns>
        public static bool CanPlayerHit(CardDeck deck)
        {
            return deck.Value < 21;
        }

        /// <summary>
        /// Подсчет результата игры
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="dealer">Диллер</param>
        /// <returns></returns>
        public static GameResult Result(Player player, Player dealer)
        {
            GameResult res = GameResult.Win;

            double playerVal = HandValue(player.Hand);
            double dealerVal = HandValue(dealer.Hand);

            if (playerVal <= 21)
            {
                if (playerVal != dealerVal)
                {
                    double closestVal = new double[] { playerVal, dealerVal }
                    .Select(handVal => new { handVal, weigth = Math.Abs(handVal - 21) + (handVal > 21 ? 100 : 0) })
                    .OrderBy(n => n.weigth)
                    .First().handVal;

                    res = playerVal == closestVal ? GameResult.Win : GameResult.Lose;
                }
                else
                {
                    res = GameResult.Draw;
                }
            }
            else
            {
                res = GameResult.Lose;
            }
            return res;
        }
    }
}