using ScoreBoard.Core.Entity;
using System.Collections.Generic;

namespace ScoreBoard.Core.DAL
{
    public interface IPlayerDataService
    {
		IReadOnlyList<Player> Players { get; }
		void RefreshCache();
        Player GetPlayer(int id);
		void SavePlayer(Player player);
        void DeletePlayer(Player player);
	}
}

