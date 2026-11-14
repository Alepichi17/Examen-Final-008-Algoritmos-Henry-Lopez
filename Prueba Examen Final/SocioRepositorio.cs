using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_Examen_Final
{
    public class SocioRepositorio
    {
        private const string CadenaConexion =
           @"Server=(localdb)\MSSQLLocalDB;" +
           @"Database=GimnasioDB;" +
           @"Trusted_Connection=True;" +
           @"TrustServerCertificate=True;";
        public void Insertar(Socio p)
        {
            string sql = "INSERT INTO dbo.Socios (Nombre, Membresia, Cuota) " +
                "VALUES (@nombre, @membresia, @cuota)";
            using(SqlConnection conexion = new SqlConnection(CadenaConexion))
            using(SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", p.Nombre);
                comando.Parameters.AddWithValue("@membresia", p.Membresia);
                comando.Parameters.AddWithValue("@cuota", p.Cuota);

                conexion.Open();
                int filas = comando.ExecuteNonQuery();
                Console.WriteLine(filas == 1 ? "Socio Insertado Correctamente" : "Error Al Insertar");
            }
        }
        public List<Socio> Listar()
        {
            List<Socio> lista = new List<Socio>();
            string sql = "SELECT Id, Nombre, Membresia, Cuota " +
                "FROM dbo.socios";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                conexion.Open();
                using(SqlDataReader lector = comando.ExecuteReader())
                {
                    while(lector.Read())
                    {
                        lista.Add(new Socio
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Membresia = lector.GetString(2),
                            Cuota = lector.GetDecimal(3)
                        });
                    }
                }
            }
            return lista;
        }
        public Socio? Buscar(int Id)
        {
            string sql = "SELECT Id, Nombre, Membresia, Cuota " +
                "FROM dbo.Socios " +
                "WHERE Id = @id";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", Id);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new Socio
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Membresia = lector.GetString(2),
                            Cuota = lector.GetDecimal(3)
                        };
                    }
                }
                return null;
            }
        }
        public bool Eliminar(int Id)
        {
            string sql = "DELETE FROM dbo.Socios WHERE Id = @id";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", Id);
                conexion.Open();
                return comando.ExecuteNonQuery() == 1;
            }
        }
        public bool Actualizar(Socio s)
        {
            string sql = "UPDATE dbo.Socios " +
                "SET Nombre = @nombre, Membresia = @membresia, Cuota = @cuota " +
                "WHERE Id = @id";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", s.Id);
                comando.Parameters.AddWithValue("@nombre", s.Nombre);
                comando.Parameters.AddWithValue("@membresia", s.Membresia);
                comando.Parameters.AddWithValue("@cuota", s.Cuota);

                conexion.Open();
                return comando.ExecuteNonQuery() == 1;
            }
        }
    }
}
