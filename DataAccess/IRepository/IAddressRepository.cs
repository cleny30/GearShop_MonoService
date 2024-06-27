using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IAddressRepository
    {
        public bool AddNewAddress(DeliveryAddressModel deliAddressModel);
        public List<DeliveryAddressModel> GetAddressByUsername(string username);
        public bool UpdateAddress(DeliveryAddressModel deliveryAddressModel);
        public bool DeleteAddress(string username, int id);
        public DeliveryAddressModel? FindExistingAddressItem(string username, string phoneNumber, string fullname, string address);
    }
}
