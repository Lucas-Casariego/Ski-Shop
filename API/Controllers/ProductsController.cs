using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    #region contructor and field
    private readonly StoreContext _context;
    public ProductsController(StoreContext context)
    {
        _context = context;
    }
    #endregion

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) return NotFound();

        return product;
    }

}
