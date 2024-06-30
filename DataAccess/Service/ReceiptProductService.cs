using BusinessObject.Model.Page;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ReceiptProductService
    {
        private readonly IReceiptProductRepository _receiptProductRepository;

        public ReceiptProductService(IReceiptProductRepository receiptProductRepository)
        {
            _receiptProductRepository = receiptProductRepository;
        }
        public async Task<bool> AddReceiptProductAsync(List<ReceiptProductModel> list, int ID)
        {
            return await _receiptProductRepository.AddReceiptProductAsync(list, ID);
        }
        public List<ReceiptProductModel> GetReceiptProducts(int ReceiptID)
        {
            return _receiptProductRepository.GetReceiptProducts(ReceiptID);
        }

        public List<ReceiptProductModel> GetAllReceiptProducts()
        {
            return _receiptProductRepository.GetAllReceiptProducts();
        }

    }
}