using Microsoft.EntityFrameworkCore;

namespace Biblioteca_MVC.Models
{
    [Keyless]
    public class Biblioteca
    {
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }
        public List<Libro> LibrosAlquilados { get; set; }
        public double SaldoTotal { get; set; }
        public int VentasRealizadas { get; set; }

        public Biblioteca(string nombre, double saldoTotal, int ventasRealizadas)
        {
            Nombre = nombre;
            SaldoTotal = saldoTotal;
            VentasRealizadas = ventasRealizadas;
            Libros = new List<Libro>();
            LibrosAlquilados = new List<Libro>();
        }

        public List<Libro> obtenerStock()
        {
            return Libros;
        }
        public List<Libro> obtenerLibrosAlquilados()
        {
            return LibrosAlquilados;
        }
        public bool venderLibro(int isbn)
        {
            Libro libro = buscarLibro(isbn);
            bool estado = false;
            if (libro != null)
            {
                gestionarVenta(libro);
                estado = true;
            }
            return estado;
        }

        public bool alquilarLibro(int isbn)
        {
            Libro libro = buscarLibro(isbn);
            bool estado = false;
            if (libro != null)
            {
                gestionarAlquiler(libro);
                estado = true;
            }
            return estado;
        }

        public void devolverLibro(int isbn)
        {
            Libro libro = buscarLibro(isbn);
            if (libro != null)
            {
                this.LibrosAlquilados.Remove(libro);
                this.Libros.Add(libro);
            }
            
        }

        private void gestionarVenta(Libro libro)
        {
            SaldoTotal += libro.getPrecioCompra();
            VentasRealizadas += 1;
            eliminarLibroDeStock(libro);

        }
        private void gestionarAlquiler(Libro libro)
        {
            libro.actualizarDevolucion();
            eliminarLibroDeStock(libro);
            agregarLibroAlquilado(libro);
        }

        private void agregarLibroAlquilado(Libro libro)
        {
            LibrosAlquilados.Add(libro);
        }

        private Libro buscarLibro(int isbn)
        {
            Libro libro = null;
            int i = 0;
            while (i < Libros.Count && libro == null) { 

                if (Libros[i].ISBN.Equals(isbn)) {
                    libro = Libros[i];
                }

                i++;
            }

            return libro;
        }
        private bool eliminarLibroDeStock(Libro libro)
        {
            return Libros.Remove(libro);
        }

        public void add(Libro libro)
        {
            this.Libros.Add(libro);
        }
    }
}
