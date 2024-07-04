using System;
namespace cardGame
{
    public class CardSorter : IComparer<PlayingCard>
    {
        public int Compare(PlayingCard x, PlayingCard y)
        {
            return y.ComparingValue - x.ComparingValue;
        }
    }
}

