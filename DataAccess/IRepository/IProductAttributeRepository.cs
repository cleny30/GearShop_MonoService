using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IProductAttributeRepository
    {
        public List<ProductAttributeModel> GetProductAttribute();
    }
}
