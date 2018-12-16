using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    public class Texto : IArchivo<string>  
    {
        public bool Guardar(string archivo, string datos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + archivo, false))
                {
                    sw.WriteLine(datos);
                }
                return true;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }   
        }

        public bool Leer(string archivo, out string datos)
        {
            throw new NotImplementedException();
        }
    }
}
