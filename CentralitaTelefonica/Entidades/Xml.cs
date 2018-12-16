using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Entidades
{
    public class Xml <T> : IArchivo<T>
    {

        public bool Guardar(string archivo, T datos)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                TextWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + archivo, true);
                serializer.Serialize(writer, datos);
                writer.Close();
                return true;
                
            }
            catch (Exception)
            {
                throw new Exception("Error al guardar el archivo xml");
            }
        }

        public bool Leer(string archivo, out T datos)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                TextReader reader = new StreamReader(archivo);
                datos = (T)serializer.Deserialize(reader);
                reader.Close();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Error al cargar el archivo xml");
            }
        }
    }
}
