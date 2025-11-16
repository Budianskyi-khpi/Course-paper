using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialLib.ReportGeneration
{
    public abstract class AbstractGenerator
    {
        protected const string DEFAULT_PATH = "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Reports\\";
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

        protected abstract string Create(string path, string name, bool autoGenereatedPath);
        public abstract string Generate(string path = null, string name = null, bool autoGenereatedPath = true);
    }
}
