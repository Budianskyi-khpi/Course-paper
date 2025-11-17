using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PolynomialLib.DataManager
{
    /// <summary>
    /// Class for writing/reading data to\from XML file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class XmlManager<T>
    {
        private string _path;

        public XmlManager() { }

        public XmlManager(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Read data from XML file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public T Read(string path = null)
        {
            string actualPath = path ?? _path;

            if (string.IsNullOrEmpty(actualPath))
            {
                throw new InvalidOperationException("Path must be provided either in the constructor or in the Read method.");
            }

            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(T));

                using (TextReader textReader = new StreamReader(actualPath))
                {
                    T readedInstance = (T)deserializer.Deserialize(textReader);
                    return readedInstance;
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"XML file not found at path: {actualPath}", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize type {typeof(T).Name}. See inner exception.", ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Write data to XML file
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public void Write(T obj, string path = null)
        {
            string actualPath = path ?? _path;

            if (string.IsNullOrEmpty(actualPath))
            {
                throw new InvalidOperationException("Path must be provided either in the constructor or in the Write method.");
            }

            try
            {
                string directoryPath = Path.GetDirectoryName(actualPath);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (TextWriter textWriter = new StreamWriter(actualPath))
                {
                    serializer.Serialize(textWriter, obj);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Error: The directory path is invalid. Path: {actualPath}");
                throw new InvalidOperationException($"Invalid directory path: {actualPath}", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Error: No permission to write to file. Path: {actualPath}");
                throw new UnauthorizedAccessException($"Access denied to path: {actualPath}", ex);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: Failed to serialize object of type {typeof(T).Name}.");
                throw new InvalidOperationException($"Failed to serialize type {typeof(T).Name}. See inner exception.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
