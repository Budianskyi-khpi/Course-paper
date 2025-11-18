using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialLib.DataManager
{
    internal interface ISerializer<T>
    {
        public T Read(string path);
        public void Write(T obj, string path);
    }
}
