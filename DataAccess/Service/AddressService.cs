using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class AddressService
    {
        private readonly IAddressRepository _deliveryAddressRepository;
        public AddressService()
        {
            _deliveryAddressRepository = new AddressRepository();
        }

        public List<DeliveryAddressModel> GetAddressByUsername(string username)
        {
            return _deliveryAddressRepository.GetAddressByUsername(username);
        }

        public bool AddNewAddress(DeliveryAddressModel deliveryAddressModel,string username)
        {
  
            deliveryAddressModel.Username = username;
            var existingAddressItem = _deliveryAddressRepository.FindExistingAddressItem(deliveryAddressModel.Username, deliveryAddressModel.Phone, deliveryAddressModel.Fullname);
            if (existingAddressItem != null)
            {
                return false; // address already exist 
            }
            else
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    // Query the DeliveryAddress table to get the newest address ID
                    int newestAddressId = dbContext.DeliveryAddresses
                                                  .OrderByDescending(a => a.Id)
                                                  .Select(a => a.Id)
                                                  .FirstOrDefault();

                    deliveryAddressModel.Id = newestAddressId + 1;
                    _deliveryAddressRepository.AddNewAddress(deliveryAddressModel);
                    return true;
                }
            }
        }

        public bool UpdateAddress(DeliveryAddressModel deliveryAddressModel)
        {
            var existingAddressItem = _deliveryAddressRepository.FindExistingAddressItem(deliveryAddressModel.Username, deliveryAddressModel.Phone, deliveryAddressModel.Fullname);
            if (existingAddressItem != null)
            {
                return false; // Address already exist 
            }
            else
            {
                _deliveryAddressRepository.UpdateAddress(deliveryAddressModel);
                return true;
            }
        }

        public bool DeleteAddress(string username, int id)
        {
            return _deliveryAddressRepository.DeleteAddress(username, id);
        }

        public List<DeliveryAddressModel> GetAddressListByUsername(string userName)
        {
            List<DeliveryAddressModel> AddressList = _deliveryAddressRepository.GetAddressByUsername(userName).OrderByDescending(address => address.IsDefault).ToList();
            return AddressList;
        }
    }
}
