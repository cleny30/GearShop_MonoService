using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.IRepository;
using ISUZU_NEXT.Server.Core.Extentions;

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
                    _ImportProduct.ReceiptId = _receipt.ReceiptId;
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
        public List<ImportProductModel> GetImportProductsList()
        {
            List <ImportProduct> list = new List<ImportProduct>(); 
            try
            {
                var dbContext = new PrndatabaseContext();
                list = dbContext.ImportProducts.ToList();
                List<ImportProductModel> _list = new List<ImportProductModel>();
                foreach (var item in list)
                {
                    ImportProductModel model = new ImportProductModel();
                    model.CopyProperties(item);
                    _list.Add(model);
                }
                return _list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ImportProductModel GetImportProduct(int receiptID)
        {
            ImportProduct importProduct = new ImportProduct();
            try
            {
                var dbContext = new PrndatabaseContext();
                importProduct = dbContext.ImportProducts.Where(p => p.ReceiptId == receiptID).FirstOrDefault();
                if(importProduct != null)
                {
                    ImportProductModel _importProduct = new ImportProductModel
                    {
                        ReceiptId = receiptID,  
                        DateImport = importProduct.DateImport,
                        Payment = importProduct.Payment,
                        PersonChange = importProduct.PersonChange
                    };
                    return _importProduct;
                } else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
