using System;
using System.Collections.Generic;
using ScoreBoard.Core.Entity;

namespace ScoreBoard.Core.DAL
{
    public class FakeGameService : IGameDataService
    {
        private List<Game> _games = new List<Game>();

        public FakeGameService()
        {
            RefreshCache();
        }

        public IReadOnlyList<Game> Games
        {
            get { return _games; }
        }

        public void DeleteGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Game GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public void RefreshCache()
        {
            _games = new List<Game>()
            {
                new Game { Id = 1, Name = "bobama" },
                new Game { Id = 2, Name = "bobloblaw" },
                new Game { Id = 3, Name = "gmichael" },
            };
        }

        public void SaveGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
