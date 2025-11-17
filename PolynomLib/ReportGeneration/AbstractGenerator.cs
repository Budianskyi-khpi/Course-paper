namespace PolynomialLib.ReportGeneration
{
    /// <summary>
    /// Abstract class for report generating
    /// </summary>
    public abstract class AbstractGenerator
    {
        protected const string DEFAULT_PATH = "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Reports\\";

        /// <summary>
        /// Make path unique. If such pass already exists, baseName will be changed to givenName(number of such names)
        /// </summary>
        /// <param name="directory">Path to directory where file will be stored</param>
        /// <param name="baseName">File name</param>
        /// <param name="extension">Extension</param>
        /// <returns>Unique path</returns>
        public string GetUniqueFullPath(string directory, string baseName, string extension)
        {
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string fullPath = Path.Combine(directory, baseName + extension);

            if (!File.Exists(fullPath))
            {
                return fullPath;
            }

            int counter = 1;
            string newFullPath;
            string newFileName;

            do
            {
                newFileName = $"{baseName} ({counter}){extension}";

                newFullPath = Path.Combine(directory, newFileName);

                counter++;
            }
            while (File.Exists(newFullPath));

            return newFullPath;
        }

        /// <summary>
        /// Create path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="autoGenereatedPath"></param>
        /// <returns></returns>
        protected abstract string Create(string path, string name, bool autoGenereatedPath);

        /// <summary>
        /// Generate report
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="autoGenereatedPath"></param>
        /// <returns></returns>
        public abstract string Generate(string path = null, string name = null, bool autoGenereatedPath = true);
    }
}
