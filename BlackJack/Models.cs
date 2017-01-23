
using BlackJack;
using System.Collections.Generic;

namespace BlackJack
{       
        /// <summary>
        /// Перечисление состояний игры
        /// </summary>
        public enum GameResult { Win = 1, Lose = -1, Draw = 0, Pending = 2 };

        /// <summary>
        /// Класс карт, параметры имя, масть и количество очков!
        /// </summary>
        public class Card
        {
            public string Name { get; set; }
            public string Suit { get; set; }
            public int Value { get; set; }
            
            /// <summary>
            /// Конструктор карты
            /// </summary>
            /// <param name="name">Название</param>
            /// <param name="suit">Масть</param>
            /// <param name="val">Количество очков</param>
            public Card(string name, string suit, int val)
            {
                Name = name;
                Suit = suit;
                Value = val;
            }
        }

        /// <summary>
        /// Класс колоды, основан на стеке
        /// </summary>
        public class CardDeck : Stack<Card>
        {
            /// <summary>
            /// Базовый конструктор, принимает на вход последовательность карт
            /// </summary>
            /// <param name="collection">Колода карт</param>
            public CardDeck(IEnumerable<Card> collection) : base(collection) { }
            
            /// <summary>
            /// Базовый конструктор, создает колоду размером 52 карты
            /// </summary>
            public CardDeck() : base(52) { }
            
            /// <summary>
            /// Индексатор по колоде
            /// </summary>
            /// <param name="index">Индекс</param>
            /// <returns></returns>
            public Card this[int index]
            {
                get
                {
                    Card item;

                    if (index >= 0 && index <= this.Count - 1)
                    {
                        item = this.ToArray()[index];
                    }
                    else
                    {
                        item = null;
                    }

                    return item;
                }
            }

            /// <summary>
            /// Подсчет суммы очков в колоде
            /// </summary>
            public double Value
            {
                get
                {
                    return BlackJackHelper.HandValue(this);
                }
            }

        }

        /// <summary>
        /// Класс игрока и диллера
        /// </summary>
        public class Player
        {
            public CardDeck Hand;

            public Player()
            {
                Hand = new CardDeck();
            }
        }
}