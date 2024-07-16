using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Conexion
    {
        public static string cadena = "Data Source=DESKTOP-DEL1U7K;Initial Catalog=Proy_Despelote;Integrated Security=True;";

        public SqlConnection ObtenerConexion()
        {
            SqlConnection connection = new SqlConnection(cadena);
            try
            {
                connection.Open(); // Intentar abrir la conexión
                connection.Close(); // Cerrar la conexión si se abre correctamente
            }
            catch (Exception ex)
            {
                // Manejar la excepción y mostrar un mensaje de error
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
            return connection;
        }

    }
}
