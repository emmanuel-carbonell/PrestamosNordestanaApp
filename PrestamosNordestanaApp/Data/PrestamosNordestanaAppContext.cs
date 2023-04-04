using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrestamosNordestanaApp.Models;

namespace PrestamosNordestanaApp.Data
{
    public class PrestamosNordestanaAppContext : DbContext
    {
        public PrestamosNordestanaAppContext (DbContextOptions<PrestamosNordestanaAppContext> options)
            : base(options)
        {
        }

        public DbSet<PrestamosNordestanaApp.Models.Cliente> Clientes { get; set; } = default!;

        public DbSet<PrestamosNordestanaApp.Models.Prestamo> Prestamo { get; set; }
    }
}
