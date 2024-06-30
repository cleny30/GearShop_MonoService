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
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class ManagerRepository: IManagerRepository
    {
        public async Task<bool> CheckUsernameExistedAsync(string username)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    Manager _manager = await dbContext.Managers.FirstOrDefaultAsync(m => m.Username.Equals(username));
                    return _manager != null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
                return false;
            }
        }

        public async Task<bool> CheckManagerExistedAsync(string username, string password)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    string hashedPassword = GetMD5Hash(password);
                    Manager _manager = await dbContext.Managers.FirstOrDefaultAsync(m => m.Username.Equals(username) && m.Password.Equals(hashedPassword));
                    return _manager != null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
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
