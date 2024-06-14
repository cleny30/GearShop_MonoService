using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class ImportProductRepository : IImportProductRepository
    {
        public async Task<ImportProductModel> CreateImportReceiptAsync(ImportProductModel _ImportProduct)
        {
            try
            {
                var dbContext = new PrndatabaseContext();
                ImportProduct _receipt = new ImportProduct
                {
                    DateImport = _ImportProduct.DateImport,
                    Payment = _ImportProduct.Payment,
                    PersonChange = _ImportProduct.PersonChange,
                    ReceiptId = GetNewestImportReceiptID() + 1
                };

                await dbContext.ImportProducts.AddAsync(_receipt);
                int check = await dbContext.SaveChangesAsync();

                if (check > 0)
                {
                    return _ImportProduct;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return null;
            }
        }

        public int GetNewestImportReceiptID()
        {
            try
            {
                var dbContext = new PrndatabaseContext();
                int NewestIRID = dbContext.ImportProducts.OrderByDescending(p => p.ReceiptId)
                        .Select(p => p.ReceiptId)
                        .FirstOrDefault();
                if(NewestIRID == null)
                {
                    return 1;
                } else
                {
                    return NewestIRID;
                }
            } catch (Exception ex)
            {

                return 0;
            }

            return 0;
        }
    }
}
