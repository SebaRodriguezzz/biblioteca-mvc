using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_MVC.Models
{
    public class Libro
    {

        public const int CANT_DIAS_ALQUILER = 30;
        [DisplayName("Precio de compra")]
        public double PrecioCompra { get; set; }
        [DisplayName("Precio de alquiler")]
        public double PrecioAlquiler { get; set; }
        [Key]
        public int ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        [DisplayName("Cantidad de páginas")]
        public int CantPaginas { get; set; }
        [DisplayName("Está alquilado?")]
        public bool Alquilado { get; set; }
        [DisplayName("Dias para su devolución")]
        public int FechaDevolucionEnDias { get; set; }

        public Libro(double precioCompra, double precioAlquiler, int iSBN, string titulo, string autor, int cantPaginas, bool alquilado)
        {
            PrecioCompra = precioCompra;
            PrecioAlquiler = precioAlquiler;
            ISBN = iSBN;
            Titulo = titulo;
            Autor = autor;
            CantPaginas = cantPaginas;
            Alquilado = alquilado;
            FechaDevolucionEnDias = 0;
        }

        public Libro()
        {
            
        }

        public bool estaAlquilado()
        {
            return Alquilado;
        }

        public double getPrecioCompra()
        {
            return PrecioCompra;
        }

        public double getPrecioAlquiler()
        {
            return PrecioAlquiler;
        }

        public void actualizarDevolucion()
        {
            this.FechaDevolucionEnDias = CANT_DIAS_ALQUILER;
        }
    }
}
