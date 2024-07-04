using System;
namespace cardGame
{
    public class PlayingDeck
    {
        private PlayingCard[] cards;

        public PlayingCard[] Cards
        {
            get { return cards; }
        }

        public PlayingDeck(int cardsInSuite = 13)
        {
            if (cardsInSuite <= 0)
            {
                cardsInSuite = 4; // only the face cards and aces
            }

            if (cardsInSuite > 13) cardsInSuite = 13;

            cards = new PlayingCard[cardsInSuite * 4];
            int index = 0;

            for (int s = 4; s >= 1; s--)
            {
                for (int v = 14; v > 14 - cardsInSuite; v--)
                {
                    PlayingCard newCard = new PlayingCard((CardSuit)s, (CardValue)v);
                    cards[index++] = newCard;
                }
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
}

