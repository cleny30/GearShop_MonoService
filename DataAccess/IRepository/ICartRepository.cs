using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface ICartRepository
    {
        public List<CartModel> GetCarts();
        public List<CartModel> GetCartsByUsername(string username);
        public bool AddCart(CartModel _cart);
        public bool UpdateCartData(CartModel _cart);
    }
}
