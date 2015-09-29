using System.Threading.Tasks;

namespace ScoreBoard.Core.DAL
{
	public interface IFileHandler
	{
		bool FileExists (string filename);
        string ReadAllText (string filename);
		void WriteAllText (string filename, string content);
        void DeleteFile(string filename);
        string[] GetFileSet(string _storagePath);
    }
}

