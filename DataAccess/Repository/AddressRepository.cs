using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AddressRepository : IAddressRepository
    {  
        /// <summary>
        /// Method to Add new Address for customer
        /// </summary>
        /// <param name="deliAddressModel"></param>
        public bool AddNewAddress(DeliveryAddressModel deliAddressModel)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    DeliveryAddress deliveryAddress = new DeliveryAddress();
                    deliveryAddress.CopyProperties(deliAddressModel);

               
                    dbContext.DeliveryAddresses.Add(deliveryAddress);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get User's address list 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<DeliveryAddressModel> GetAddressByUsername(string username)
        {
            using (var context = new PrndatabaseContext())
            {
                var addresses = context.DeliveryAddresses.Where(c => c.Username == username).ToList();
                List<DeliveryAddressModel> addressModels = new List<DeliveryAddressModel>();
                foreach (var address in addresses)
                {
                    var addressModel = new DeliveryAddressModel();
                    addressModel.CopyProperties(address);
                    addressModels.Add(addressModel);
                }
                return addressModels;
            }
        }

        /// <summary>
        /// Method use to check existed address in user's address list
        /// </summary>
        /// <param name="username"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="address"></param>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public DeliveryAddressModel? FindExistingAddressItem(string username, string phoneNumber, string fullname, string address, bool isdefault)
        {
            using (var context = new PrndatabaseContext())
            {
                var deliveryAddress = context.DeliveryAddresses.FirstOrDefault(c => c.Username == username && c.Phone == phoneNumber && c.Fullname == fullname && c.Address == address && c.IsDefault == isdefault);
                if (deliveryAddress != null)
                {
                    DeliveryAddressModel deliveryAddressModel = new DeliveryAddressModel();
                    deliveryAddressModel.CopyProperties(deliveryAddress);
                    return deliveryAddressModel;
                }
                return null;
            }
        }

        public void CheckAllFalse(string username)
        {
            using (var context = new PrndatabaseContext())
            {
                bool hasDefaultAddress = context.DeliveryAddresses.Any(c => c.Username == username && c.IsDefault == true);
                if (!hasDefaultAddress)
                {
                    var firstAddress = context.DeliveryAddresses
                                              .Where(c => c.Username == username)
                                              .OrderBy(c => c.Id)
                                              .FirstOrDefault();

                    if (firstAddress != null)
                    {
                        firstAddress.IsDefault = true;
                        context.SaveChanges();
                    }
                }
            }
        }



        /// <summary>
        /// Update user's Address
        /// </summary>
        /// <param name="deliveryAddressModel"></param>
        public bool UpdateAddress(DeliveryAddressModel deliveryAddressModel)
        {
            try
            {
                using (var context = new PrndatabaseContext())
                {
                    var existingAddress = context.DeliveryAddresses.FirstOrDefault(p => p.Id == deliveryAddressModel.Id);
                    if (existingAddress != null)
                    {
                        // Update product information
                        existingAddress.Fullname = deliveryAddressModel.Fullname;
                        existingAddress.Phone = deliveryAddressModel.Phone;
                        existingAddress.Address = deliveryAddressModel.Address;
                        existingAddress.Specific = deliveryAddressModel.Specific;
                        // Check if isDefault is being set to true
                        if (deliveryAddressModel.IsDefault)
                        {
                            // Find all addresses with the same username and set their isDefault to false
                            var otherAddresses = context.DeliveryAddresses.Where(p => p.Username == deliveryAddressModel.Username && p.Id != existingAddress.Id).ToList();
                            foreach (var address in otherAddresses)
                            {
                                address.IsDefault = false;
                            }
                        }

                        existingAddress.IsDefault = deliveryAddressModel.IsDefault;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Delete user's address.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
        public bool DeleteAddress(string username, int id)
        {
            try
            {
                using (var context = new PrndatabaseContext())
                {
                    var deliveryAddress = context.DeliveryAddresses.FirstOrDefault(ca => ca.Username == username && ca.Id == id);
                    if (deliveryAddress != null)
                    {
                        context.DeliveryAddresses.Remove(deliveryAddress);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
