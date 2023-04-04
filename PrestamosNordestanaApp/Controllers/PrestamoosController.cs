using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrestamosNordestanaApp.Data;
using PrestamosNordestanaApp.Models;

namespace PrestamosNordestanaApp.Controllers
{
    public class PrestamoosController : Controller
    {
        private readonly PrestamosNordestanaAppContext _context;

        public PrestamoosController(PrestamosNordestanaAppContext context)
        {
            _context = context;
        }

        // GET: Prestamoos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Prestamo.ToListAsync());
        }

        // GET: Prestamoos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prestamo == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamoos/Create
        public async Task<IActionResult> Create()
        {
            await SetClienteSelect();
            return View();
        }

        public async Task SetClienteSelect()
        {
            var clientes = (await _context.Clientes
                .ToListAsync()).Select(s => new SelectListItem(s.Nombre, s.Id.ToString()));
            ViewBag.Clientes = clientes;
        }

        // POST: Prestamoos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Monto,FechaPrestamo,ClientId")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                await SetCliente(prestamo);
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await SetClienteSelect();
            return View(prestamo);
        }

        public async Task SetCliente(Prestamo prestamo)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == prestamo.ClientId);
            prestamo.Cliente = cliente;
        }

        // GET: Prestamoos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prestamo == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamo.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            await SetClienteSelect();
            return View(prestamo);
        }

        // POST: Prestamoos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Monto,FechaPrestamo,ClientId")] Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await SetCliente(prestamo);
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await SetClienteSelect();
            return View(prestamo);
        }

        // GET: Prestamoos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prestamo == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prestamo == null)
            {
                return Problem("Entity set 'PrestamosNordestanaAppContext.Prestamo'  is null.");
            }
            var prestamo = await _context.Prestamo.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamo.Remove(prestamo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
          return _context.Prestamo.Any(e => e.Id == id);
        }
    }
}
