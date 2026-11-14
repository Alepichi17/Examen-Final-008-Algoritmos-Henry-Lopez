using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_Examen_Final
{
    public class Socio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Membresia { get; set; }
        public decimal Cuota { get; set; }
        public override string ToString()
        {
            return $"{Id} | Nombre: {Nombre} | Membresia: {Membresia} | Cuota: {Cuota}";
        }
    }
}
