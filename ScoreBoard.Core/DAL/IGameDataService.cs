using ScoreBoard.Core.Entity;
using System.Collections.Generic;

namespace ScoreBoard.Core.DAL
{
    public interface IGameDataService
    {
		IReadOnlyList<Game> Games { get; }
		void RefreshCache();
        Game GetGame(int id);
		void SaveGame(Game game);
        void DeleteGame(Game game);
	}
}

