﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class HomeModel: HeaderModel
    {
        public List<ProductData> products { get; set; }
    }
}
