namespace cardGame;

public enum CardSuit
{
    blank = 0,
    Diamond,
    Heart,
    Club,
    Spade,
    Red,
    Black
}

public enum CardValue
{
    blank = 0,
    v2 = 2,
    v3,
    v4,
    v5,
    v6,
    v7,
    v8,
    v9,
    v10,
    Jack,
    Queen,
    King,
    Ace,
    Joker
}

public struct PlayingCard
{
    public CardSuit Suit;
    public CardValue Value;

    public PlayingCard(CardSuit suit, CardValue value)
    {
        Value = value;

        if (Value == CardValue.Joker)
        {
            switch (Suit)
            {
                case CardSuit.Heart:
                case CardSuit.Diamond:
                    Suit = CardSuit.Red;
                    break;
                case CardSuit.Spade:
                case CardSuit.Club:
                    Suit = CardSuit.Black;
                    break;
                default:
                    Suit = suit;
                    break;
            }
        }
        else
        {
            Suit = suit;
        }
    }

    public int ComparingValue
    {
        get { return ((int)this.Suit - 1) * 13 + (int)this.Value; }
    }

    override public string ToString()
    {
        if (this.Value == CardValue.Joker)
        {
            return $"{this.Suit} {this.Value}";

        }
        else
        {
            return $"{(this.Value.ToString().StartsWith("v") ? this.Value.ToString().Substring(1) : this.Value)} of {this.Suit}s";
        }
    }
}

public class CardSorter : IComparer<PlayingCard>
{
    public int Compare(PlayingCard x, PlayingCard y)
    {
        if (x.ComparingValue > y.ComparingValue)
        {
            return -1;
        }
        else if (x.ComparingValue < y.ComparingValue)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

public class PlayingDeck
{
    private PlayingCard[] cards;

    public PlayingCard[] Cards
    {
        get { return cards; }
    }

    public PlayingDeck(uint cardsInSuite = 13, bool addJokers = false)
    {
        if (cardsInSuite > 13) cardsInSuite = 13;

        cards = new PlayingCard[cardsInSuite * 4 + (addJokers ? 2 : 0)];
        int index = 0;

        for (int s = 4; s >= 1; s--)
        {
            for (int v = 14; v > 14 - cardsInSuite; v--)
            {
                PlayingCard newCard = new PlayingCard((CardSuit)s, (CardValue)v);
                cards[index++] = newCard;
            }
        }

        if (addJokers)
        {
            cards[cards.Length - 1] = new PlayingCard(CardSuit.Red, CardValue.Joker);
            cards[cards.Length - 2] = new PlayingCard(CardSuit.Black, CardValue.Joker);
        }
    }

    public void Sort()
    {
        Array.Sort(cards, new CardSorter());
    }

    public void Shuffle()
    {
        Random rnd = new Random();
        int n = cards.Length;

        while (n > 1)
        {
            int k = rnd.Next(n--);
            (cards[n], cards[k]) = (cards[k], cards[n]);
        }
    }

    public Stack<PlayingCard> PlayableDeck
    {
        get { return new Stack<PlayingCard>(cards); }
    }
}


class BlackJackGameHost
{
    private PlayingDeck blackjackDeck;
    private Stack<PlayingCard> playingDeck;
    private Dictionary<CardValue, int> blackjackPoints;

    private List<PlayingCard> dealersHand = new List<PlayingCard>();
    private List<PlayingCard> playersHand = new List<PlayingCard>();

    private BlackJackStatistics statistics;

    public bool DealerFirst;

    public BlackJackGameHost(uint cardsInSuite)
    {
        blackjackPoints = new Dictionary<CardValue, int> {
            { CardValue.Ace, 11 },
            { CardValue.King, 4 },
            { CardValue.Queen, 3 },
            { CardValue.Jack, 2},
            { CardValue.v10, 10 },
            { CardValue.v9, 9 },
            { CardValue.v8, 8 },
            { CardValue.v7, 7 },
            { CardValue.v6, 6 },
            { CardValue.v5, 5 },
            { CardValue.v4, 4 },
            { CardValue.v3, 3 },
            { CardValue.v2, 2 },
            { CardValue.Joker, 0 },
        };

        blackjackDeck = new PlayingDeck(cardsInSuite);

        blackjackDeck.Shuffle();
        playingDeck = blackjackDeck.PlayableDeck;
        statistics = new BlackJackStatistics();

        DealerFirst = true;

        Console.WriteLine("Choose the first player to draw (P = player, D = dealer): ");
        char key = Console.ReadKey().KeyChar;

        if (key == 'd')
        {
            Console.WriteLine();
            Console.WriteLine("Dealer goes first");
            DealerFirst = true;
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Player goes first");
            DealerFirst = false;
        }
    }

    internal class BlackJackStatistics
    {
        private int wins;
        private int numOfGames;
        private int bestScore;
        private bool lastGameWon;
        private int lastGamePoints;

        public BlackJackStatistics()
        {
            wins = 0;
            numOfGames = 0;
            bestScore = 0;
            lastGameWon = false;
            lastGamePoints = 0;
        }

        public int Wins
        {
            get { return wins; }
            private set { wins = value; }
        }

        public int Loses
        {
            get { return numOfGames - wins; }
        }

        public int GamesPlayed
        {
            get { return numOfGames; }
            private set { numOfGames = value; }
        }

        public int BestScore
        {
            get { return bestScore; }
            private set { bestScore = value; }
        }

        public string AfterRoundReport
        {
            get
            {
                return $"{(lastGameWon ? "WON" : "LOST")} WITH {lastGamePoints} pts.";
            }
        }

        public string SummaryReport
        {
            get
            {
                return $"TOTAL: {numOfGames} PLAYED, {wins} WON, BEST RESULT {bestScore} pts.";
            }
        }

        public void RecordResult(bool win, int points)
        {
            if (win)
            {
                Wins += 1;
            }
            GamesPlayed += 1;
            lastGameWon = win;
            lastGamePoints = points;
            if (points == 21 || (points < 21 && BestScore > 21) || (points > 21 && BestScore > 21 && points < BestScore) || BestScore == 0)
            {
                BestScore = points;
            }
        }
    }

    private bool DealerDrawsMore()
    {
        if (DealersHandPoints() >= 17)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool PlayerWonCheck()
    {
        int playerPoints = PlayersHandPoints();
        int dealerPoints = DealersHandPoints();

        return (playerPoints == 21 ||
            (playersHand.Count == 2 && playerPoints > 20) ||
            (dealerPoints > 21 && playerPoints < 21) ||
            (dealerPoints > 21 && playerPoints > 21 && playerPoints < dealerPoints) ||
            (dealerPoints < 21 && playerPoints < 21 && playerPoints > dealerPoints));
    }

    private bool IntermediateSummary()
    {
        int dealerPoints = DealersHandPoints();
        int playerPoints = PlayersHandPoints();

        Console.WriteLine("DEALER: ");
        foreach (PlayingCard card in dealersHand)
        {
            Console.WriteLine($"{card}: {blackjackPoints[card.Value]} pts.");
        }
        Console.WriteLine($"Cards: {dealersHand.Count} / Points: {dealerPoints}");
        Console.WriteLine();
        Console.WriteLine("PLAYER: ");
        foreach (PlayingCard card in playersHand)
        {
            Console.WriteLine($"{card}: {blackjackPoints[card.Value]} pts.");
        }
        Console.WriteLine($"Cards: {playersHand.Count} / Points: {playerPoints}");
        return false;
    }

    private bool SingleRound()
    {
        bool playerStays = false;
        bool dealerStays = false;

        if (DealerFirst)
        {
            if (DealerDrawsMore())
            {
                Console.WriteLine("Dealer draws 1 more...");
                dealersHand.Add(playingDeck.Pop());
            }
            else
            {
                Console.WriteLine("Dealer stays...");
                dealerStays = true;
            }

            Console.WriteLine("Press M to draw more, any other button to stay...");
            if (Console.ReadKey().KeyChar == 'm')
            {
                Console.WriteLine("Player draws 1 more...");
                playersHand.Add(playingDeck.Pop());
            }
            else
            {
                Console.WriteLine("Player stays...");
                playerStays = true;
            }
        }
        else
        {
            Console.WriteLine("Press M to draw more, any other button to stay...");
            if (Console.ReadKey().KeyChar == 'm')
            {
                Console.WriteLine("Player draws 1 more...");
                playersHand.Add(playingDeck.Pop());
            }
            else
            {
                Console.WriteLine("Player stays...");
                playerStays = true;
            }

            if (DealerDrawsMore())
            {
                Console.WriteLine("Dealer draws 1 more...");
                dealersHand.Add(playingDeck.Pop());
            }
            else
            {
                Console.WriteLine("Dealer stays...");
                dealerStays = true;
            }
        }

        if (PlayersHandPoints() > 21 || DealersHandPoints() > 21)
        {
            return true;
        }

        return dealerStays && playerStays;
    }

    private void GameStart()
    {
        do
        {
            if (playingDeck.Count <= 10)
            {
                Console.WriteLine("Players have depleted the deck. Press any button to close...");
                Console.ReadKey();
                return;
            }

            dealersHand.Clear();
            playersHand.Clear();

            dealersHand.Add(playingDeck.Pop());
            dealersHand.Add(playingDeck.Pop());

            playersHand.Add(playingDeck.Pop());

            playersHand.Add(playingDeck.Pop());

            while (!IntermediateSummary())
            {
                if (SingleRound())
                {
                    break;
                }
            }

            IntermediateSummary();
            if (PlayerWonCheck())
            {
                statistics.RecordResult(true, PlayersHandPoints());
            }
            else
            {
                statistics.RecordResult(false, PlayersHandPoints());
            }


            Console.WriteLine($"ROUND RESULTS: {statistics.AfterRoundReport}");
            Console.WriteLine($"SUMMARY: {statistics.SummaryReport}");

            Console.WriteLine("Press Enter for another round, any button to close...");

        } while (Console.ReadKey().Key == ConsoleKey.Enter);
    }

    public void Play()
    {
        GameStart();
    }

    private int PlayersHandPoints()
    {
        int sum = 0;
        foreach (PlayingCard card in playersHand)
        {
            sum += blackjackPoints[card.Value];
        }
        return sum;
    }

    private int DealersHandPoints()
    {
        int sum = 0;
        foreach (PlayingCard card in dealersHand)
        {
            sum += blackjackPoints[card.Value];
        }
        return sum;
    }

    public void ShowStatistics()
    {
        Console.WriteLine(statistics.SummaryReport);
    }
}

class Program
{
    static void Main(string[] args)
    {
        PlayingDeck rawDeck = new PlayingDeck(9, false);
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