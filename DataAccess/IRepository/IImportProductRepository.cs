using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IImportProductRepository
    {
        public Task<ImportProductModel> CreateImportReceiptAsync(ImportProductModel _ImportProduct);

        public int GetNewestImportReceiptID();
    }
}
