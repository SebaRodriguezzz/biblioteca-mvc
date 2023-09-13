using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca_MVC.DAL;
using Biblioteca_MVC.Models;
using System.Net.Mail;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Biblioteca_MVC.Controllers
{
    public class LibrosController : Controller
    {
        private readonly Biblioteca_MVCContext _context;

        public LibrosController(Biblioteca_MVCContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Stock(string buscar, string filtro)
        {
            var libros = from libro in _context.Libros where libro.Alquilado == false select libro;

            if (!String.IsNullOrEmpty(buscar))
            {
                libros = libros.Where(s => s.Titulo!.Contains(buscar) || s.Autor!.Contains(buscar));

            }

            ViewData["FiltroNombre"] = String.IsNullOrEmpty(filtro) ? "NombreDescendente" : "";
            if (filtro == "NombreDescendente")
            {
                libros = libros.OrderByDescending(libro => libro.Titulo);
            } else
            {
                libros = libros.OrderBy(libro => libro.Titulo);
            }


            return View(await libros.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.ISBN == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrecioCompra,PrecioAlquiler,ISBN,Titulo,Autor,CantPaginas,Alquilado, FechaDevolucionEnDias")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Stock));
            }
            return View(libro);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrecioCompra,PrecioAlquiler,ISBN,Titulo,Autor,CantPaginas,Alquilado,FechaDevolucionEnDias")] Libro libro)
        {
            if (id != libro.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.ISBN))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Stock));
            }
            return View(libro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.ISBN == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Libros == null)
            {
                return Problem("Entity set 'Biblioteca_MVCContext.Libros'  is null.");
            }
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Stock));
        }

        private bool LibroExists(int id)
        {
          return (_context.Libros?.Any(e => e.ISBN == id)).GetValueOrDefault();
        }

        public IActionResult Alquilados(string buscar, string filtro)
        {
            var libros = from libro in _context.Libros where libro.Alquilado == true select libro;

            if (!String.IsNullOrEmpty(buscar))
            {
                libros = libros.Where(s => s.Titulo!.Contains(buscar) || s.Autor!.Contains(buscar));

            }

            ViewData["FiltroNombre"] = String.IsNullOrEmpty(filtro) ? "NombreDescendente" : "";

            if (filtro == "NombreDescendente")
            {
                libros = libros.OrderByDescending(libro => libro.Titulo);
            }
            else
            {
                libros = libros.OrderBy(libro => libro.Titulo);
            }

            return View(libros);
        }

        public IActionResult Solicitar()
        {

            return View();
        }
        public async Task<IActionResult> Vender(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        public async Task<IActionResult> ConfirmarVenta(int? id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }

            await _context.SaveChangesAsync();

            return View(libro);
        }

        public IActionResult Estadisticas()
        {
            int cantidadStock = _context.Libros.Count(l => !l.Alquilado);
            int cantidadAlquilados = _context.Libros.Count(l => l.Alquilado);
            double valorTotalStock = _context.Libros.Where(l => !l.Alquilado).Sum(l => l.PrecioCompra);
            ViewBag.CantidadStock = cantidadStock;
            ViewBag.CantidadAlquilados = cantidadAlquilados;
            ViewBag.ValorTotalStock = valorTotalStock;

            return View();
        }

    }
}
