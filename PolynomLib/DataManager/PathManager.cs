using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialLib.DataManager
{
    public static class PathManager
    {
        /// <summary>
        /// Search whether provided path exists or no, and which part exists if there is some uncreated files
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="existingPathPart"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool Search(string fullPath, out string existingPathPart)
        {
            DirectoryInfo dir = new DirectoryInfo(fullPath);
            existingPathPart = "";

            if (dir.Exists)
            {
                existingPathPart = fullPath;
                return true;
            }
            else
            {
                string[] parts = fullPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                var passedPath = "";

                foreach (string part in parts)
                {
                    if (passedPath == "")
                    {
                        passedPath += part;
                    }
                    else
                    {
                        passedPath += "\\" + part;

                    }

                    dir = new DirectoryInfo(passedPath);
                    if (dir.Exists)
                    {
                        existingPathPart = passedPath;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (existingPathPart == "")
            {
                throw new ArgumentException("Incorrect path! There isn't any  existing folders in it!");
            }

            return false;
        }

        /// <summary>
        /// Method for creating files in specified directory
        /// </summary>
        /// <param name="path">Path to where file will be stored</param>
        /// <param name="fileName">How file will be named</param>
        /// <param name="extension">With which extension file will be stored</param>
        /// <param name="autoCreate">Whether program will be creating new directories, which were mensioned in file path, but don't exist in reality</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string Create(string path, string fileName, string extension, bool autoCreate = true)
        {
            string existingPath;

            if (fileName.Length < extension.Length || fileName[^extension.Length..] != extension)
            {
                fileName += extension;
            }

            if (Search(path, out existingPath))
            {
                FileInfo fileInfo = new FileInfo(path + "\\" + fileName);
                if (!fileInfo.Exists)
                {
                    fileInfo.Create().Dispose();
                }
            }
            else if (autoCreate)
            {
                string noExistingPath = path[existingPath.Length..];
                DirectoryInfo dir = new DirectoryInfo(path);
                dir.Create();
                FileInfo file = new FileInfo(path + "\\" + fileName);
                file.Create().Dispose();

                return noExistingPath;
            }
            else
            {
                throw new ArgumentException();
            }
            return "";
        }

        public static string Create(string fullPath, bool autoCreate = true)
        {
            string existingPath;

            string path = Path.GetDirectoryName(fullPath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);

            return Create(path, fileNameWithoutExt, extension, autoCreate);
        }
    }
}
