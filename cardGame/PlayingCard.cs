using System;
namespace cardGame
{
    public struct PlayingCard
    {
        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }

        public PlayingCard(CardSuit suit, CardValue value)
        {
            Value = value;
            Suit = suit;
        }

        public int ComparingValue
        {
            get { return ((int)this.Suit - 1) * 13 + (int)this.Value; }
        }

        public override string ToString()
        {
            return $"{(this.Value.ToString().StartsWith("v") ? this.Value.ToString().Substring(1) : this.Value)} of {this.Suit}s";
        }
    }
}

