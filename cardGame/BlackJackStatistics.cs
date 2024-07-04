using System;
namespace cardGame
{
    public class BlackJackStatistics
    {
        //private int wins;
        //private int gamesPlayed;
        //private int bestScore;
        //private bool lastGameWon;
        //private int lastGamePoints;

        public BlackJackStatistics()
        {
            Wins = 0;
            GamesPlayed = 0;
            BestScore = 0;
            LastGameWon = false;
            LastGamePoints = 0;
        }

        public int Wins
        {
            get; private set;
        }

        public int Loses
        {
            get { return GamesPlayed - Wins; }
        }

        public int GamesPlayed
        {
            get; private set;
        }

        public int BestScore
        {
            get; private set;
        }

        public bool LastGameWon { get; private set; }

        public int LastGamePoints { get; private set; }

        public string AfterRoundReport
        {
            get
            {
                return $"{(LastGameWon ? "WON" : "LOST")} WITH {LastGamePoints} pts.";
            }
        }

        public string SummaryReport
        {
            get
            {
                return $"TOTAL: {GamesPlayed} PLAYED, {Wins} WON, BEST RESULT {BestScore} pts.";
            }
        }

        public void RecordResult(bool win, int points)
        {
            if (win)
            {
                Wins += 1;
            }
            GamesPlayed += 1;
            LastGameWon = win;
            LastGamePoints = points;
            if (points == 21 || (points < 21 && BestScore > 21) || (points > 21 && BestScore > 21 && points < BestScore) || BestScore == 0)
            {
                BestScore = points;
            }
        }
    }
}

