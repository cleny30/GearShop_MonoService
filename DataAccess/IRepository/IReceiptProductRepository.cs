using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IReceiptProductRepository
    {
        public Task<bool> AddReceiptProductAsync(List<ReceiptProductModel> receiptProducts, int receiptID);
    }
}
