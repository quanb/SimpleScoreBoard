using ScoreBoard.Core.DAL;
using System.IO;
using System.Threading.Tasks;

namespace ScoreBoard.IO
{
	public class FileHandler : IFileHandler
	{
		public FileHandler ()
		{
		}

		#region IFileHandler implementation

		public bool FileExists (string filename)
		{
			return File.Exists (filename);
		}

		public string ReadAllText (string filename)
		{
			return File.ReadAllText(filename); ;
		}

		public void WriteAllText (string filename, string content)
		{
            File.WriteAllText(filename, content);
			
		}

        public void DeleteFile(string filename)
        {
            File.Delete(filename);
        }

        public string[] GetFileSet(string _storagePath)
        {
            return Directory.GetFiles(_storagePath, "*.json");
        }

        public void CreatePathIfNotExist(string _storagePath)
        {
            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);
        }
        #endregion
    }
}

