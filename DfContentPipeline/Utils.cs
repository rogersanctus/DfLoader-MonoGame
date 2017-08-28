using System;
using System.IO;

namespace DfContentPipeline
{
    internal class Utils
    {
        public static string MakeRelativePathToFile(string from, string toFile)
        {
            var root = Path.GetFullPath(Path.GetDirectoryName(from));
            var filePath = Path.GetDirectoryName(toFile);

            var relativePath = "";

            try
            {
                Uri rootUri = new Uri(root);

                // Folder must have relative path, even if to the current directory
                if (filePath.Length == 0)
                {
                    filePath = "./";
                }

                filePath = Path.GetFullPath(filePath);

                // Folders must end in a slash
                if (!filePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    filePath += Path.DirectorySeparatorChar;
                }

                Uri fileUri = new Uri(filePath);
                relativePath = Uri.UnescapeDataString(fileUri.MakeRelativeUri(rootUri).ToString().Replace('/', Path.DirectorySeparatorChar));
            }
            catch( Exception e )
            {
                throw new Exception("dFLoader Utils MakeRelativePathToFile error. " + e.ToString());
            }

            return relativePath;
        }
    }
}
