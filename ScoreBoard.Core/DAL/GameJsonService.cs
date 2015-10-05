using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ScoreBoard.Core.Entity;

namespace ScoreBoard.Core.DAL
{
	public class GameJsonService : IGameDataService
    {
		private string _storagePath;
		private List<Game> _games = new List<Game>();
        private IFileHandler _fileHandler;

        public GameJsonService(string storagePath, IFileHandler fileHandler)
		{
			_storagePath = storagePath;
            _fileHandler = fileHandler;

            // create the storage path if it does not exist
            fileHandler.CreatePathIfNotExist(storagePath);

            RefreshCache ();
		}

        #region IGameDataService implementation

        public IReadOnlyList<Game> Games
        {
            get { return _games; }
        }

        public Game GetGame (int id)
		{
            Game game = _games.Find (p => p.Id == id);
			return game;
		}

		private int GetNextId()
		{
			if (_games.Count == 0)
				return 1;
			else
				return _games.Max (p => p.Id.Value) + 1;
		}

		private string GetFilename(int id)
		{
			return Path.Combine (_storagePath, "game" + id.ToString () + ".json");
		}

		public void SaveGame(Game game)
		{
			Boolean newGame = false;
			if (!game.Id.HasValue) {
                game.Id = GetNextId ();
                newGame = true;
			}

			string gameString = JsonConvert.SerializeObject (game);
            _fileHandler.WriteAllText (GetFilename (game.Id.Value), gameString);

			if (newGame)
                _games.Add (game);
		}

		public void DeleteGame(Game game)
		{
            _fileHandler.DeleteFile (GetFilename (game.Id.Value));
            _games.Remove (game);
		}
		
		public void RefreshCache()
		{
            _games.Clear ();

			string[] filenames = _fileHandler.GetFileSet(_storagePath);
			foreach (string filename in filenames) {
				string gameString = _fileHandler.ReadAllText (filename);
				Game game = JsonConvert.DeserializeObject<Game> (gameString);
                _games.Add (game);
			}
		}

		#endregion
	}
}

