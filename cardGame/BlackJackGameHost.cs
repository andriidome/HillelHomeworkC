using System;
namespace cardGame
{
    class BlackJackGameHost
    {
        private PlayingDeck blackjackDeck;
        private Stack<PlayingCard> playingDeck;
        private Dictionary<CardValue, int> blackjackPoints;
        //private List<PlayingCard> dealersHand = new List<PlayingCard>();
        //private List<PlayingCard> playersHand = new List<PlayingCard>();
        //private BlackJackStatistics statistics;
        private bool dealerFirst;
        //private Player player;
        //private Player dealer = new Player("DEALER");

        public BlackJackGameHost(int cardsInSuite)
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
                { CardValue.v2, 2 }
            };

            blackjackDeck = new PlayingDeck(cardsInSuite);
        }

        public Player Player { get; private set; }

        public Player Dealer { get; private set; }


        private bool DealerDrawsMore()
        {
            return DealersHandPoints() < 17;
        }

        private bool PlayerWonCheck()
        {
            int playerPoints = PlayersHandPoints();
            int dealerPoints = DealersHandPoints();

            return (playerPoints == 21 ||
                (Player.Hand.Count == 2 && playerPoints > 20) ||
                (dealerPoints > 21 && playerPoints < 21) ||
                (dealerPoints > 21 && playerPoints > 21 && playerPoints < dealerPoints) ||
                (dealerPoints < 21 && playerPoints < 21 && playerPoints > dealerPoints));
        }

        private void IntermediateSummary()
        {
            int dealerPoints = DealersHandPoints();
            int playerPoints = PlayersHandPoints();

            Console.WriteLine("DEALER: ");
            foreach (PlayingCard card in Dealer.Hand)
            {
                Console.WriteLine($"{card}: {blackjackPoints[card.Value]} pts.");
            }
            Console.WriteLine($"Cards: {Dealer.Hand.Count} / Points: {dealerPoints}");
            Console.WriteLine();
            Console.WriteLine("PLAYER: ");
            foreach (PlayingCard card in Player.Hand)
            {
                Console.WriteLine($"{card}: {blackjackPoints[card.Value]} pts.");
            }
            Console.WriteLine($"Cards: {Player.Hand.Count} / Points: {playerPoints}");
        }

        private bool SingleRound()
        {
            bool playerStays = false;
            bool dealerStays = false;

            if (dealerFirst)
            {
                if (DealerDrawsMore())
                {
                    Console.WriteLine("Dealer draws 1 more...");
                    Dealer.Hand.Add(playingDeck.Pop());
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
                    Player.Hand.Add(playingDeck.Pop());
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
                    Player.Hand.Add(playingDeck.Pop());
                }
                else
                {
                    Console.WriteLine("Player stays...");
                    playerStays = true;
                }

                if (DealerDrawsMore())
                {
                    Console.WriteLine("Dealer draws 1 more...");
                    Dealer.Hand.Add(playingDeck.Pop());
                }
                else
                {
                    Console.WriteLine("Dealer stays...");
                    dealerStays = true;
                }
            }

            return PlayersHandPoints() > 21 || DealersHandPoints() > 21 || (dealerStays && playerStays);
        }

        private void GameStart()
        {
            Console.Write("Enter player's name: ");
            string? playerName = Console.ReadLine();
            playerName = playerName is null ? "DEFAULT" : playerName;
            Player = new Player(playerName);
            Dealer = new Player("DEALER");

            blackjackDeck.Shuffle();
            playingDeck = blackjackDeck.PlayableDeck;
            //statistics = new BlackJackStatistics();

            dealerFirst = true;

            Console.WriteLine("Choose the first player to draw (P = player, D = dealer): ");
            char key = Console.ReadKey().KeyChar;

            if (key == 'd')
            {
                Console.WriteLine();
                Console.WriteLine("Dealer goes first");
                dealerFirst = true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Player goes first");
                dealerFirst = false;
            }

            do
            {
                if (playingDeck.Count <= 10)
                {
                    Console.WriteLine("Players have depleted the deck. Press any button to close...");
                    Console.ReadKey();
                    return;
                }

                Dealer.Hand.Clear();
                Player.Hand.Clear();

                Dealer.Hand.Add(playingDeck.Pop());
                Dealer.Hand.Add(playingDeck.Pop());

                Player.Hand.Add(playingDeck.Pop());
                Player.Hand.Add(playingDeck.Pop());

                while (true)
                {
                    IntermediateSummary();
                    if (SingleRound())
                    {
                        break;
                    }
                }

                IntermediateSummary();
                if (PlayerWonCheck())
                {
                    Player.Statistics.RecordResult(true, PlayersHandPoints());
                }
                else
                {
                    Player.Statistics.RecordResult(false, PlayersHandPoints());
                }


                Console.WriteLine($"ROUND RESULTS: {Player.Statistics.AfterRoundReport}");
                Console.WriteLine($"SUMMARY: {Player.Statistics.SummaryReport}");

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
            foreach (PlayingCard card in Player.Hand)
            {
                sum += blackjackPoints[card.Value];
            }
            return sum;
        }

        private int DealersHandPoints()
        {
            int sum = 0;
            foreach (PlayingCard card in Dealer.Hand)
            {
                sum += blackjackPoints[card.Value];
            }
            return sum;
        }

        public void ShowStatistics()
        {
            Console.WriteLine(Player.Statistics.SummaryReport);
        }
    }
}

