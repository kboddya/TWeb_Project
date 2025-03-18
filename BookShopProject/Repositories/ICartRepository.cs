using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Models;

namespace BookShopProject.Repositories
{
    public interface ICartRepository
    {
        IEnumerable<CartItem> GetAllCartItems();
        CartItem GetCartItemById(int id);
        void AddCartItem(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);
        void DeleteCartItem(int id);
    }
}

