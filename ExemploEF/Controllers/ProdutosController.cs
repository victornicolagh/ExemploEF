
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFexemplo.Models;
using Exemplo_EF.Data;

public class ProdutosController : Controller
{
    private readonly Context _context;

    public ProdutosController(Context context)
    {
        _context = context;
    }

    // Carregar categorias pelo nome
    private void CarregarCategorias(object categoriaSelecionada = null)
    {
        ViewData["CategoriaId"] = new SelectList(
            _context.Categorias,
            "CategoriaId",
            "Nome",
            categoriaSelecionada);
    }

    // GET: Produtos
    public async Task<IActionResult> Index()
    {
        var produtos = await _context.Produtos
            .Include(p => p.Categoria)
            .ToListAsync();

        return View(produtos);
    }

    // GET: Produtos/Details
    public async Task<IActionResult> Details(Guid? produtoid)
    {
        if (produtoid == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.ProdutoId == produtoid);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // GET: Produtos/Create
    public IActionResult Create()
    {
        CarregarCategorias();
        return View();
    }

    // POST: Produtos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("ProdutoId,Nome,Descricao,Preco,CategoriaId")] Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        CarregarCategorias(produto.CategoriaId);
        return View(produto);
    }

    // GET: Produtos/Edit
    public async Task<IActionResult> Edit(Guid? produtoid)
    {
        if (produtoid == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos.FindAsync(produtoid);

        if (produto == null)
        {
            return NotFound();
        }

        CarregarCategorias(produto.CategoriaId);
        return View(produto);
    }

    // POST: Produtos/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid? produtoid,
        [Bind("ProdutoId,Nome,Descricao,Preco,CategoriaId")] Produto produto)
    {
        if (produtoid != produto.ProdutoId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.ProdutoId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        CarregarCategorias(produto.CategoriaId);
        return View(produto);
    }

    // GET: Produtos/Delete
    public async Task<IActionResult> Delete(Guid? produtoid)
    {
        if (produtoid == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.ProdutoId == produtoid);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // POST: Produtos/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid? produtoid)
    {
        var produto = await _context.Produtos.FindAsync(produtoid);

        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ProdutoExists(Guid produtoid)
    {
        return _context.Produtos.Any(p => p.ProdutoId == produtoid);
    }
}
