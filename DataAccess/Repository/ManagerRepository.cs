using DataAccess.IRepository;
using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISUZU_NEXT.Server.Core.Extentions;
using System.Security.Cryptography;

namespace DataAccess.Repository
{
    public class ManagerRepository: IManagerRepository
    {
        public bool CheckUsernameExisted(string username)
        {
            try
            {
                var dbContext = new PrndatabaseContext();
                Manager _manager = new Manager();
                _manager = dbContext.Managers.FirstOrDefault(m => m.Username.Equals(username));
                if(_manager == null)
                {
                    return false;
                } else
                {
                    return true;
                }    
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckManagerExisted(string username, string password)
        {
            try
            {
                var dbContext = new PrndatabaseContext();
                Manager _manager = new Manager();
                _manager = dbContext.Managers.FirstOrDefault(m => m.Username.Equals(username) && m.Password.Equals(GetMD5Hash(password)));
                if (_manager == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input); // Convert the input string to bytes
                byte[] hashBytes = md5.ComputeHash(inputBytes); // Compute the MD5 hash

                // Convert the byte array to a hexadecimal string representation of the hash
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" means lowercase hexadecimal
                }
                return sb.ToString();
            }
        }
    }
}
