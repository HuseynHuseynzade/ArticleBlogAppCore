﻿using ArticleBlogAppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Areas.Admin.Models
{
    public class SubcategoryCreateModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
