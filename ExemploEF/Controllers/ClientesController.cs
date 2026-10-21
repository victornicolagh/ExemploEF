
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExemploEF.Models;
using Exemplo_EF.Data;

public class ClientesController : Controller
{
    private readonly Context _context;

    public ClientesController(Context context)
    {
        _context = context;
    }

    // GET: CLIENTES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Clientes.ToListAsync());
    }

    // GET: CLIENTES/Details/5
    public async Task<IActionResult> Details(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(m => m.ID == id);
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    // GET: CLIENTES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CLIENTES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Nome,Nascimento")] Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    // GET: CLIENTES/Edit/5
    public async Task<IActionResult> Edit(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }

    // POST: CLIENTES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(System.Guid? id, [Bind("ID,Nome,Nascimento")] Cliente cliente)
    {
        if (id != cliente.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.ID))
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
        return View(cliente);
    }

    // GET: CLIENTES/Delete/5
    public async Task<IActionResult> Delete(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(m => m.ID == id);
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    // POST: CLIENTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(System.Guid? id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClienteExists(System.Guid? id)
    {
        return _context.Clientes.Any(e => e.ID == id);
    }
}
