using Microsoft.VisualBasic;

namespace PrestamosNordestanaApp.Models
{
    public class Prestamo
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string Monto { get; set; }

        public int FechaPrestamo { get; set; }

        public int ClientId { get; set; }

        public Cliente? Cliente { get; set; }

    }
}
