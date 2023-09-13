using Biblioteca_MVC.Controllers;
using Biblioteca_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_MVC.DAL
{
    public class Biblioteca_MVCContext : DbContext
    {
        public Biblioteca_MVCContext(DbContextOptions<Biblioteca_MVCContext> options):base(options)
        {
        }

        public DbSet<Biblioteca> Biblioteca { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }

    
}
