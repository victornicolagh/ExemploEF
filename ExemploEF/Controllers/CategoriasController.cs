
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFexemplo.Models;
using Exemplo_EF.Data;

public class CategoriasController : Controller
{
    private readonly Context _context;

    public CategoriasController(Context context)
    {
        _context = context;
    }

    // GET: Categorias
    public async Task<IActionResult> Index()
    {
        return View(await _context.Categorias.ToListAsync());
    }

    // GET: Categorias/Details
    public async Task<IActionResult> Details(Guid? categoriaid)
    {
        if (categoriaid == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.CategoriaId == categoriaid);

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // GET: Categorias/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categorias/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Nome")] Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            categoria.CategoriaId = Guid.NewGuid();

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(categoria);
    }

    // GET: Categorias/Edit
    public async Task<IActionResult> Edit(Guid? categoriaid)
    {
        if (categoriaid == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias
            .FindAsync(categoriaid);

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // POST: Categorias/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid categoriaid,
        [Bind("CategoriaId,Nome")] Categoria categoria)
    {
        if (categoriaid != categoria.CategoriaId)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(categoria);
        }

        var categoriaExistente = await _context.Categorias
            .FindAsync(categoriaid);

        if (categoriaExistente == null)
        {
            return NotFound();
        }

        categoriaExistente.Nome = categoria.Nome;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Categorias/Delete
    public async Task<IActionResult> Delete(Guid? categoriaid)
    {
        if (categoriaid == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.CategoriaId == categoriaid);

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // POST: Categorias/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid categoriaid)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Produtos)
            .FirstOrDefaultAsync(c => c.CategoriaId == categoriaid);

        if (categoria == null)
        {
            return NotFound();
        }

        // Não excluir categorias que possuem produtos relacionados.
        if (categoria.Produtos != null && categoria.Produtos.Any())
        {
            ModelState.AddModelError(
                string.Empty,
                "Não é possível excluir esta categoria porque existem produtos relacionados.");

            return View("Delete", categoria);
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
