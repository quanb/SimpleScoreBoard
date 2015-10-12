using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ScoreBoard.Core.Entity;

namespace ScoreBoard.Core.DAL
{
	public class PlayerJsonService : IPlayerDataService
    {
		private string _storagePath;
		private List<Player> _players = new List<Player>(); 
        private IFileHandler _fileHandler;

        public PlayerJsonService(string storagePath, IFileHandler fileHandler)
		{
			_storagePath = Path.Combine(storagePath, "Players.json"); ;
            _fileHandler = fileHandler;
            // create the storage path if it does not exist
            fileHandler.CreatePathIfNotExist(storagePath);

            RefreshCache ();
		}

        #region IPlayerDataService implementation

        public IReadOnlyList<Player> Players
        {
            get { return _players; }
        }

        public Player GetPlayer (int id)
		{
            Player player = _players.Find (p => p.Id == id);
			return player;
		}

		private int GetNextId()
		{
			if (_players.Count == 0)
				return 1;
			else
				return _players.Max (p => p.Id.Value) + 1;
		}

		public void SavePlayer(Player player)
		{
			Boolean newPlayer = false;
			if (!player.Id.HasValue) {
                player.Id = GetNextId ();
                newPlayer = true;
			}

            if (newPlayer)
                _players.Add(player);
            string playerString = JsonConvert.SerializeObject (_players);
            _fileHandler.WriteAllText (_storagePath, playerString);
		}

		public void DeletePlayer(Player player)
		{
            if (player == null)
                throw new Exception("Cannot delete null player.");

            _players.Remove(player);
            string serializedParks = JsonConvert.SerializeObject(_players);
            _fileHandler.WriteAllText(_storagePath, serializedParks);
        }
		
		public void RefreshCache()
		{
            _players.Clear ();

            if (_fileHandler.FileExists(_storagePath))
            {
                string serializedPlayers = _fileHandler.ReadAllText(_storagePath);
                _players = JsonConvert.DeserializeObject<List<Player>>(serializedPlayers);
            }
        }

		#endregion
	}
}

