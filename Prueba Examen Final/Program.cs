using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;

namespace Prueba_Examen_Final
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SocioRepositorio repo = new SocioRepositorio();
            string Opcion;
            do
            {
                Opcion = Menu();
                
                switch(Opcion)
                {
                    case "1":
                        Insertar(repo);
                        break;
                    case "2":
                        Listar(repo);
                        break;
                    case "3":
                        Buscar(repo);
                        break;
                    case "4":
                        Eliminar(repo);
                        break;
                    case "5":
                        Actualizar(repo);
                        break;
                    case "6":
                        Console.WriteLine("Saliendo");
                        break;
                    default:
                        Console.WriteLine("No Valida");
                        break;
                }
            }
            while (Opcion != "6");
        }
        public static string Menu()
        {
            Console.WriteLine("Sistema de Socios del Gimnasio");
            Console.WriteLine("Opcion 1: Insertar Socio");
            Console.WriteLine("Opcion 2: Listar Socios");
            Console.WriteLine("Opcion 3: Buscar Socio Por Id");
            Console.WriteLine("Opcion 4: Eliminar Socio"); 
            Console.WriteLine("Opcion 5: Actualizar Membresia");
            Console.WriteLine("Opcion 6: Salir");
            Console.Write("Escoge Una Opcion: ");
            return Console.ReadLine();
        }
        public static void Insertar(SocioRepositorio repo)
        {
            Console.Write("Nombre: ");
            string Nombre = Console.ReadLine();
            Console.Write("Membresia: ");
            string Membresia = Console.ReadLine();
            Console.Write("Cuota: ");
            bool Cok = decimal.TryParse(Console.ReadLine(), out decimal Cuota);

            if (!Cok || Cuota <= 0 || string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Membresia))
            {
                Console.WriteLine("Datos No validos");
                return;
            }
            try
            {        
            repo.Insertar(new Socio
                 {
                     Nombre = Nombre,
                     Membresia = Membresia,
                     Cuota = Cuota
                });
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"Error De Base De Datos: {ex.Message}");
            }
        }
        public static void Listar(SocioRepositorio repo)
        {
            try
            {
            List<Socio> lista = repo.Listar();
            if(lista.Count == 0)
                {
                Console.WriteLine("No Hay Productos");
                return;
                }
                decimal Total = 0;
            foreach(Socio i in lista)
                {
                Console.WriteLine(i);
                    Total += i.Cuota;
                }
            Console.WriteLine($"Total Cuotas {Total}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error De Base De Datos: {ex.Message}");
            }
        }
        public static void Buscar(SocioRepositorio repo)
        {
            Console.Write("Id del Socio: ");
            if(!int.TryParse(Console.ReadLine(), out int Id) || Id <= 0)
            {
                Console.WriteLine("Error");
                return;
            }
            try
            {
            Socio? producto = repo.Buscar(Id);
            Console.WriteLine(producto == null ? "No Existe Ese Id" : producto.ToString());
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"Error De Base De Datos: {ex.Message}");
            }
        }
        public static void Eliminar(SocioRepositorio repo)
        {
            Console.Write("Id del Socio a Eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int Id) || Id <= 0)
            {
                Console.WriteLine("Error");
                return;
            }
            try
            {
            Console.WriteLine(repo.Eliminar(Id) ? "Eliminado" : "No Se Elimino");
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"Error De Base De Datos: {ex.Message}");
            }
        }
        public static void Actualizar(SocioRepositorio repo)
        {
            Console.Write("Id del Socio a Actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int Id) || Id <= 0)
            {
                Console.WriteLine("Error");
                return;
            }
            try
            {
            Socio? socio = repo.Buscar(Id);
            Console.WriteLine("Socio Seleccionado");
            Console.WriteLine(socio);

            Console.Write("Nuevo Precio: ");
            string membresia = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(membresia))
            {
                Console.WriteLine("Error");
                return;
            }
            socio.Membresia = membresia;
            Console.WriteLine(repo.Actualizar(socio) ? "Socio Actualizado" : "No se Actualizo");
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"Error De Base De Datos: {ex.Message}");
            }
        }
    }
}
