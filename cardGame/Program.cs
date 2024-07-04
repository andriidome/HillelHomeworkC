namespace cardGame;


class Program
{
    static void Main(string[] args)
    {
        PlayingDeck rawDeck = new PlayingDeck(9);
        Stack<PlayingCard> playableDeck1 = rawDeck.PlayableDeck;
        foreach (var card in playableDeck1)
        {
            Console.WriteLine($"RAW: {card}");
        }

        Console.WriteLine();
        rawDeck.Shuffle();
        Stack<PlayingCard> playableDeck2 = rawDeck.PlayableDeck;
        foreach (var card in playableDeck2)
        {
            Console.WriteLine($"SHUFFLE: {card}");
        }
        Console.WriteLine();

        PlayingCard[] justABunchOfCards = rawDeck.Cards;

        int[] aces = new int[4];
        int aceIndex = 0;
        for (int i = 0; i < justABunchOfCards.Length; i++)
        {
            if (justABunchOfCards[i].Value == CardValue.Ace)
            {
                aces[aceIndex++] = i;
            }
        }

        int spadeStartPosition = 0;

        for (int i = 0; i < justABunchOfCards.Length; i++)
        {
            if (justABunchOfCards[i].Suit == CardSuit.Spade)
            {
                (justABunchOfCards[i], justABunchOfCards[spadeStartPosition]) = (justABunchOfCards[spadeStartPosition], justABunchOfCards[i]);
                spadeStartPosition++;
            }
        }
        foreach (var card in justABunchOfCards)
        {
            Console.WriteLine($"SPADED: {card}");
        }

        rawDeck.Sort();
        Stack<PlayingCard> playableDeck3 = rawDeck.PlayableDeck;

        foreach (var card in playableDeck3)
        {
            Console.WriteLine($"SORTED: {card}");
        }
        Console.WriteLine();



        BlackJackGameHost BJHost = new BlackJackGameHost(9);
        BJHost.Play();
        Console.ReadLine();
    }
}