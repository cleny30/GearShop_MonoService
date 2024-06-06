﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model.Page
{
    public class Category
    {
        public int CateId { get; set; }

        public string CateName { get; set; } = null!;

        public string Keyword { get; set; } = null!;

        public bool IsAvailable { get; set; }
    }
}
