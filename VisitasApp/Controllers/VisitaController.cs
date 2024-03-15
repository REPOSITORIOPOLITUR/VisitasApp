using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitasApp.Models;
using Microsoft.AspNetCore.Identity;
using VisitasApp.Data;

namespace VisitasApp.Controllers
{
    public class VisitaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser>? _userManager;
        private readonly SignInManager<IdentityUser>? _signInManager;
        
        public VisitaController(ApplicationDbContext context, UserManager<IdentityUser>? userManager, SignInManager<IdentityUser>? signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Visita
        public async Task<IActionResult> Index(string buscar, int? numpag, string filtroActual)
        {
            var visitas = from v in _context.Visita.Include(i => i.TipoDocumento).Include(i => i.Departamento) select v;

            if (buscar != null)
                numpag = 1;
            else
                buscar = filtroActual;

            if (!String.IsNullOrEmpty(buscar))
            {
                visitas = visitas.Where(b => b.Nombres!.Contains(buscar) 
                                          || b.Apellidos!.Contains(buscar)
                                          || b.Documento!.Contains(buscar)
                                          || b.Departamento.Nombre!.Contains(buscar));
                
            }

            visitas = visitas.OrderByDescending(orden => orden.FechaRegistro);

            ViewData["FiltroActual"] = buscar;

            int cantidadregistros = 5;

            return View(await Paginacion<Visita>.CrearPaginacion(visitas.AsNoTracking(), numpag ?? 1, cantidadregistros));            
        }

        // GET: Visita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Visita == null)
            {
                return NotFound();
            }

            var visita = await _context.Visita
                .Include(v => v.TipoDocumento)
                .Include(v => v.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // GET: Visita/Create
        public IActionResult Create()
        {        
            ViewData["IdTipoDocumento"] = new SelectList(_context.TipoDocumentos, "Id", "ValorS");
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre");
            return View();
        }

        // POST: Visita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visita visita)
        {
            if (ModelState.IsValid)
            {
                visita.FechaRegistro = DateTime.Now;
                //visita.FechaModificado = DateTime.Now;
                visita.Entrada = DateTime.Now;
                visita.IdEstado = 1;

                if (_signInManager.IsSignedIn(User))
                    visita.IdUsuario = _userManager.GetUserId(HttpContext.User);

                _context.Add(visita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoDocumento"] = new SelectList(_context.TipoDocumentos, "Id", "ValorS", visita.IdTipoDocumento);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", visita.IdDepartamento);
            return View(visita);
        }

        // GET: Visita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Visita == null)
            {
                return NotFound();
            }
            
            var visita = await _context.Visita.FindAsync(id);            
            if (visita == null)
            {
                return NotFound();
            }
            visita.FechaModificado = DateTime.Now;
            ViewData["IdTipoDocumento"] = new SelectList(_context.TipoDocumentos, "Id", "ValorS", visita.IdTipoDocumento);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", visita.IdDepartamento);
            return View(visita);
        }

        // POST: Visita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Visita visita)
        {
            if (id != visita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    visita.FechaModificado = DateTime.Now;
                    _context.Update(visita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitaExists(visita.Id))
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
            ViewData["IdTipoDocumento"] = new SelectList(_context.TipoDocumentos, "Id", "ValorS", visita.IdTipoDocumento);
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", visita.IdDepartamento);
            return View(visita);
        }

        // GET: Visita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Visita == null)
            {
                return NotFound();
            }

            var visita = await _context.Visita
                .Include(v => v.TipoDocumento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // POST: Visita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Visita == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Visita'  is null.");
            }
            var visita = await _context.Visita.FindAsync(id);
            if (visita != null)
            {
                _context.Visita.Remove(visita);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitaExists(int id)
        {
          return (_context.Visita?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
