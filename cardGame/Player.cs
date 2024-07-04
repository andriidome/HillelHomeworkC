using System;
namespace cardGame
{
	public class Player
	{
        public string Name { get; private set; }
        public List<PlayingCard> Hand { get; private set; }
        public BlackJackStatistics Statistics { get; private set; }

        public Player(string name)
		{
			Name = name;
			Hand = new List<PlayingCard>();
			Statistics = new BlackJackStatistics();
		}
	}
}

