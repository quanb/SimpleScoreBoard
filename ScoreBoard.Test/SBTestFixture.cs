using NUnit.Framework;
using ScoreBoard.Core.DAL;
using ScoreBoard.Core.Entity;
using ScoreBoard.IO;
using System;
using System.IO;

namespace ScoreBoard.Test
{
    [TestFixture]
    public class SBTestFixture
    {
        IGameDataService _gameService;

        [SetUp]
        public void Setup() {
            string storagePath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _gameService = new GameJsonService(storagePath, new FileHandler());

            // clear any existing json files
            foreach (string filename in Directory.EnumerateFiles(storagePath, "*.json"))
            {
                File.Delete(filename);
            }
        }


        [TearDown]
        public void Tear() { }

        [Test]
        public void CreateGame()
        {
            Game newGame = new Game();
            newGame.Name = "New Game";
            _gameService.SaveGame(newGame);

            int testId = newGame.Id.Value;

            // refresh the cashe to be sure the data was 
            // saved appropriately
            _gameService.RefreshCache();

            // verify the newly create game exists
            Game game = _gameService.GetGame(testId);
            Assert.NotNull(game);
            Assert.AreEqual(game.Name, "New Game");
        }

        [Test]
        public void UpdateGame()
        {
            Game testGame = new Game();
            testGame.Name = "New Game";
            _gameService.SaveGame(testGame);

            int testId = testGame.Id.Value;

            // refresh the cashe to be sure the data was 
            // game was saved appropriately
            _gameService.RefreshCache();

            Game game = _gameService.GetGame(testId);
            game.Name = "Updated Game";
            _gameService.SaveGame(game);

            // refresh the cashe to be sure the data was 
            // updated appropriately
            _gameService.RefreshCache();

            Game findgame = _gameService.GetGame(testId);
            Assert.NotNull(findgame);
            Assert.AreEqual(findgame.Name, "Updated Game");
        }

        [Test]
        public void DeleteGame()
        {
            Game testGame = new Game();
            testGame.Name = "Delete Game";
            _gameService.SaveGame(testGame);

            int testId = testGame.Id.Value;

            // refresh the cashe to be sure the data was 
            // Game was saved appropriately
            _gameService.RefreshCache();

            Game deleteGame = _gameService.GetGame(testId);
            Assert.IsNotNull(deleteGame);
            _gameService.DeleteGame(deleteGame);

            // refresh the cashe to be sure the data was 
            // deleted appropriately
            _gameService.RefreshCache();

            Game findGame = _gameService.GetGame(testId);
            Assert.Null(findGame);
        }
    }
}