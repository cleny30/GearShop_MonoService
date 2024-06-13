using BusinessObject.Model.Page;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class CartService
    {
        private readonly ICartRepository _repo;

        public CartService(ICartRepository repo)
        {
            _repo = repo;
        }

        public List<CartModel> GetCartsByUserName(string username) { 
            return _repo.GetCartsByUsername(username);
        }

        public bool AddOrUpdateCart(string username, ProductData data, int amount)
        {
            var existingCart = _repo.GetCarts().FirstOrDefault(c => c.Username == username && c.ProId == data.ProId);

            if (existingCart != null)
            {
                // Product exists in the cart, update the quantity and price
                existingCart.Quantity += amount;
                existingCart.Price = (data.ProPrice-(data.ProPrice*data.Discount)/100)*existingCart.Quantity; // Assuming you want to update the price to the latest one
                return _repo.UpdateCartData(existingCart);
            }
            else
            {
                CartModel cartModel = new CartModel()
                {
                    Username = username,
                    Price = (data.ProPrice - (data.ProPrice * data.Discount) / 100)*amount,
                    ProId = data.ProId,
                    ProName = data.ProName,
                    Quantity = amount,
                };

                return _repo.AddCart(cartModel);
            }
        }
    }
}
