using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CartRepository : ICartRepository
    {
        public List<CartModel> GetCarts()
        {
            List<Cart> cart;
            try
            {
                var dbContext = new PrndatabaseContext();
                cart = dbContext.Carts.ToList();

                List<CartModel> CartModels = new List<CartModel>();

                foreach (var item in cart)
                {
                    CartModel CartModel = new CartModel();
                    CartModel.CopyProperties(item);
                    CartModels.Add(CartModel);
                }
                return CartModels;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CartModel> GetCartsByUsername(string username)
        {
            List<Cart> cart;
            try
            {
                var dbContext = new PrndatabaseContext();
                cart = dbContext.Carts.ToList();

                List<CartModel> CartModels = new List<CartModel>();

                foreach (var item in cart)
                {
                    CartModel CartModel = new CartModel();
                    CartModel.CopyProperties(item);
                    CartModels.Add(CartModel);
                }
                return CartModels.Where(u=>u.Username==username).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AddCart(CartModel _cart)
        {
            try
            {
                Cart cart = new Cart
                {
                    Username = _cart.Username,
                    ProId = _cart.ProId,
                    ProName = _cart.ProName,
                    Quantity = _cart.Quantity,
                    Price = _cart.Price,
                };

                var dbContext = new PrndatabaseContext();
                dbContext.Carts.Add(cart);
                int result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool UpdateCartData(CartModel _cart)
        {
            Cart cart = new Cart
            {
                Username = _cart.Username,
                ProId = _cart.ProId,
                ProName = _cart.ProName,
                Price = _cart.Price,
                Quantity = _cart.Quantity
            };
            try
            {
                var dbContext = new PrndatabaseContext();
                dbContext.Entry<Cart>(cart).State = EntityState.Modified;
                int result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
