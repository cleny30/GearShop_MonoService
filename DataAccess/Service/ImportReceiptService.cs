using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ImportReceiptService
    {
        private readonly IImportProductRepository _IRRepository;
        private readonly ProductService _productService;
        private readonly ReceiptProductService _receiptProductService;  

        public ImportReceiptService(IImportProductRepository IRRepository, ProductService productService, ReceiptProductService receiptProductService)
        {
            _IRRepository = IRRepository;
            _productService = productService;
            _receiptProductService = receiptProductService;
        }

        public async Task<bool> ImportProduct(ImportProductModel _IRproduct, List<ReceiptProductModel> list)
        {
            ImportProductModel IR = await _IRRepository.CreateImportReceiptAsync(_IRproduct);
            if(IR != null)
            {
                bool finish = await _receiptProductService.AddReceiptProductAsync(list, IR.ReceiptId);
                if(finish == true)
                {
                    bool complete = await _productService.AddQuantityToProduct(list);
                    return complete;
                }
            } else
            {
                return false;
            }

            return false;
        }
    }
}
