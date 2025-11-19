using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using cafe_management.Helpers;
using cafe_management.Models;

public class ShoppingCartSummaryViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");
        var cartItemCount = (cartItems != null) ? cartItems.Count : 0;

        return View(cartItemCount);
    }
}
