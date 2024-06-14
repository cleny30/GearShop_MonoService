using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ReceiptProductRepository : IReceiptProductRepository
    {
        public async Task<bool> AddReceiptProductAsync(List<ReceiptProductModel> receiptProducts, int receiptID)
        {
            try
            {
                using (var dbContext = new PrndatabaseContext())
                {
                    foreach (var item in receiptProducts)
                    {
                        var receiptProduct = new ReceiptProduct
                        {
                            Amount = item.Amount,
                            Price = item.Price,
                            ProId = item.ProId,
                            ProName = item.ProName,
                            ReceiptId = receiptID
                        };
                        await dbContext.AddAsync(receiptProduct);
                    }

                    int result = await dbContext.SaveChangesAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return false;
            }
        }

    }
}
