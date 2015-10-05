using ScoreBoard.Core.DAL;
using ScoreBoard.IO;
using System.IO;

namespace ScoreBoard.Droid
{
    public class GameData
	{
		public static readonly IGameDataService Service = new GameJsonService(
                                                  Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path,"ScoreBoard"), new FileHandler());
	}
}