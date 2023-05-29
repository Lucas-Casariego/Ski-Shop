namespace API.DTOs;

public class BasketDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public List<BasketItemDto> Items { get; set; }

    // este Dto no tiene las 2 funciones AddItem() and RemoveItem()
}
