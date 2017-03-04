namespace BlackJack
{
    public enum GameResult
    {
        Win = 1, Lose = -1, Draw = 0, Pending = 2
    };

    public enum CardName
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        J = 11,
        Q = 12,
        K = 13,
        A = 14       
    };

    public enum CardSuit
    {
        Spades, Hearts, Clubs, Diamonds
    };
}