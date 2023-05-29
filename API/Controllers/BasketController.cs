using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class BasketController : BaseApiController
{
    private readonly StoreContext _context;

    public BasketController(StoreContext context)
	{
        _context = context;
    }

    [HttpGet(Name = "GetBasket")]
    public async Task<ActionResult<BasketDto>> GetBasket()
    {
        var basket = await RetriveBasket();

        if (basket == null)
            return NotFound();

        return MapBasketToDto(basket);
    }


    [HttpPost]
    public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
    {
        // get basket
        var basket = await RetriveBasket();
        // create basket
        if (basket == null)
            basket = CreateBasket();
        // get product
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return NotFound();
        }
        // add item
        basket.AddItem(product, quantity);
        // save changes
        var result = await _context.SaveChangesAsync() > 0;

        if (result)
            return CreatedAtRoute("GetBasket", MapBasketToDto(basket));

        return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
    {
        var basket = await RetriveBasket();

        if(basket == null)
            return NotFound();

        basket.RemoveItem(productId, quantity);

        var result = await _context.SaveChangesAsync() > 0;

        if (result)
            return Ok();

        return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
    }

    // métodos utility

    private async Task<Basket> RetriveBasket()
    {
        return await _context.Baskets
            .Include(i => i.Items)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);   // bc we're inside the controller. we have access to the http response we're gonna send back
    }

    private Basket CreateBasket()
    {
        var buyerId = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
        Response.Cookies.Append("buyerId", buyerId, cookieOptions);
        var basket = new Basket { BuyerId = buyerId };
        _context.Baskets.Add(basket);
        return basket;
    }

    private BasketDto MapBasketToDto(Basket basket)
    {
        return new BasketDto
        {
            Id = basket.Id,
            BuyerId = basket.BuyerId,
            Items = basket.Items.Select(item => new BasketItemDto
            {
                ProductId = item.ProductId,
                Name = item.Product.Name,
                Price = item.Product.Price,
                PictureUrl = item.Product.PictureUrl,
                Type = item.Product.Type,
                Brand = item.Product.Brand,
                Quantity = item.Quantity,
            }).ToList()
        };
    }
}
