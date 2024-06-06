﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class Brand
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string BrandLogo { get; set; } = null!;

        public bool IsAvailable { get; set; }
    }
}
