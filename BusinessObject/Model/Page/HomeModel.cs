﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class HomeModel: HeaderModel
    {
        public List<ProductCard> products { get; set; }
    }
}
