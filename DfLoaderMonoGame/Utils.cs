using System;
using System.IO;

namespace DfLoader
{
    internal class Utils
    {
        /// <summary>
        /// Make a relative path from one file path to other, relative to the pwd folder.
        /// </summary>
        /// <param name="root">The root folder for the files or the working directory.</param>
        /// <param name="wd">The file location of the first file.</param>
        /// <param name="toFile">The location of the other file, relative to the first.</param>
        /// <returns>The relative path.</returns>
        public static string MakeRelativePathToFile(string root, string wd, string toFile)
        {
            // Root folder must be set
            if (root == null || root.Length == 0)
            {
                throw new ArgumentNullException("dFloader Util error. The root path must be set.");
            }

            var relativePath = "";
            var wdPath = "";
            var filePath = "";

            try
            {
                wdPath = Path.GetDirectoryName(wd);
                filePath = Path.GetDirectoryName(toFile);
            }
            catch (Exception e)
            {
                throw new Exception("dfLoader Utils error. Could not get WD or file directory name. " + e.ToString());
            }

            // Uri will not work properly if the root directory do not ends with '/'
            if (!root.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                root += Path.DirectorySeparatorChar;
            }

            // Same thing here, the folder must ends with '/'. Otherwise Uri will think its a file.
            if (filePath.Length > 0 && !filePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                filePath += Path.DirectorySeparatorChar;
            }

            wdPath = Path.Combine(root, wdPath);

            relativePath = Path.Combine(wdPath, filePath);
            relativePath = Path.GetFullPath(relativePath);

            if (!relativePath.Contains(root))
            {
                throw new Exception("dFloader Utils error. The given relative path ended up not pointing to the root directory.");
            }

            try
            {
                var rootUri = new Uri(Path.GetFullPath(root));     // The Uri for the absolute path of the root folder
                var fileUri = new Uri(relativePath);

                relativePath = Uri.UnescapeDataString(rootUri.MakeRelativeUri(fileUri).ToString().Replace('/', Path.DirectorySeparatorChar));
            }
            catch (Exception e)
            {
                throw new Exception("dFLoader Utils MakeRelativePathToFile error. " + e.ToString());
            }

            return relativePath;
        }
    }
}
