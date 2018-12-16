using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Entidades
{
    public class CentralitaDAO
    {
        private string cadenaDeConexion = "Data Source = .\\SQLEXPRESS; Initial Catalog = Centralita; Integrated Security = True";
        private SqlCommand comando;
        private SqlConnection conexion;
        private SqlDataReader datareader;

        public CentralitaDAO()
    {
        this.comando = new SqlCommand();
        this.conexion = new SqlConnection(this.cadenaDeConexion);
        this.comando.CommandType = System.Data.CommandType.Text;
        this.comando.Connection = this.conexion;
    }

        public bool Guardar(Llamada nuevaLlamada)
        {
            bool isOk = false;
            try
            {
                if (nuevaLlamada is Local)
                {
                    comando.CommandText = String.Format("INSERT INTO [dbo].[ListaLlamados] (Tipo, NumeroOrigen, NumeroDestino, Duracion, Costo, Franja) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", "Local", nuevaLlamada.NroOrigen, nuevaLlamada.NroDestino, nuevaLlamada.Duracion, nuevaLlamada.CostoLlamada, null);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
                else
                {
                    comando.CommandText = String.Format("INSERT INTO [dbo].[ListaLlamados] (Tipo, NumeroOrigen, NumeroDestino, Duracion, Costo, Franja) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", "Provincial", nuevaLlamada.NroOrigen, nuevaLlamada.NroDestino, nuevaLlamada.Duracion, nuevaLlamada.CostoLlamada, ((Provincial)nuevaLlamada).FranjaHoraria);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }                
            }

            catch (Exception)
            {
                throw new Exception("Error al registrar la llamada");
            }

            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return isOk;
        }

        public List<Llamada> Leer(string archivo)
        {
            List<Llamada> listaDeLlamadas = new List<Llamada>();
            string numeroOrigen; 
            string numeroDestino;
            float duracion;
            float costo;
            Provincial.Franja franja;
            try
            {
                conexion.Open();
                comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;
                comando.Connection = conexion;
                comando.CommandText = "SELECT * FROM [dbo].[" + archivo + "]";
                datareader = comando.ExecuteReader();

                while (datareader.Read())
                {
                    numeroOrigen = datareader["NumeroOrigen"].ToString();
                    numeroDestino = datareader["NumeroDestino"].ToString();
                    duracion = float.Parse((datareader["Duracion"].ToString()), System.Globalization.CultureInfo.InvariantCulture);
                    costo = float.Parse((datareader["Costo"].ToString()), System.Globalization.CultureInfo.InvariantCulture);                    

                    if (datareader["Tipo"].ToString() == "Local")
                        listaDeLlamadas.Add(new Local(numeroOrigen, duracion, numeroDestino, costo));

                    else
                    {
                        franja = (Provincial.Franja)Enum.Parse(typeof(Provincial.Franja), datareader["Franja"].ToString(), true);
                        listaDeLlamadas.Add(new Provincial(numeroOrigen, franja, duracion, numeroDestino));
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al intentar recuperar la base de datos");
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return listaDeLlamadas;
        }
    }
}