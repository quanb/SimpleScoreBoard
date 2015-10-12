using System.Collections.Generic;

namespace ScoreBoard.Core.Entity
{
    public class Game
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<GamePlayer> Players { get; set; }
    }
}
