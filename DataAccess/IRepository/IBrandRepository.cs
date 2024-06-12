using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IBrandRepository
    {
        public List<BrandModel> Getbrand();

        public bool ChangeBrandStatus(int ID, bool availability);

        public bool AddBrand(BrandModel _brand);

        public bool UpdateBrand(BrandModel _brand);
    }
}
