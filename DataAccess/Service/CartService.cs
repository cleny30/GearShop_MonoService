using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Service
{
    public class CartService
    {
        private readonly ICartRepository _repo;
        private readonly ProductService _productService;

        public CartService(ICartRepository repo, ProductService productService)
        {
            _repo = repo;
            _productService = productService;
        }

        public List<UserCartData> GetCartsByUserName(string username) {
            var products = _productService.GetProducts();
            var carts = _repo.GetCarts().Where(u=>u.Username==username);
            List<UserCartData> list = new List<UserCartData>();
            foreach(CartModel item in carts)
            {
                UserCartData cartData = new UserCartData
                {
                    model = item,
                    Product = products.FirstOrDefault(p => p.ProId == item.ProId)
                };
                list.Add(cartData);
            }

            return list;
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

        public Tuple<bool,double> UpdateCart(string username, string proId, int amount)
        {
            var _cart = _repo.GetCarts().FirstOrDefault(c => c.Username == username && c.ProId == proId);
            _cart.Quantity = amount;
          
            var p = _productService.GetProducts().FirstOrDefault(p => p.ProId == proId);

            _cart.Price = amount * (p.ProPrice - (p.ProPrice * p.Discount) / 100);
            
            bool rs =_repo.UpdateCartData(_cart);

            var carts = _repo.GetCarts().Where(c => c.Username == username);
            double total = 0;
            foreach( var c in carts )
            {
                total += c.Price;
            }

            return Tuple.Create(rs, total);
        }

        public List<UserCartData> GetCheckedProduct(string username, List<string> proIds)
        {
			List<UserCartData> cartItems = new List<UserCartData>();
			foreach (var i in proIds)
			{
				cartItems.Add(GetCartsByUserName("cleny30").FirstOrDefault(p => p.Product.ProId == i));
			}
            return cartItems;
		}
    }
}
