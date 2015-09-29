using ScoreBoard.Core.DAL;

namespace ScoreBoard.Droid
{
    public class GameData
	{
		public static readonly IGameDataService Service = new FakeGameService();
	}
}