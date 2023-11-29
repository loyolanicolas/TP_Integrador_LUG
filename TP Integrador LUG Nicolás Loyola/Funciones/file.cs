using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Funciones
{
    public class file
    {
        public void Escribir(string text)
        {
            File.AppendAllText(@"bitacora.txt", DateTime.Now.ToString() + "-" + text + Environment.NewLine);
        }
    }
}
