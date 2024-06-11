using BusinessObject.Model.Page;
using DataAccess.IRepository;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ProductAttributeService
    {

        private readonly IProductAttributeRepository _repo;

        public ProductAttributeService(IProductAttributeRepository repo)
        {
            _repo = repo;
        }

        public List<ProductAttributeModel> GetProductAttributeList()
        {
            return _repo.GetProductAttribute();
        }
    }
}
