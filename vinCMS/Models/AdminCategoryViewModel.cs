using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace vinCMS.Models
{
    public class AdminCategoryViewModel
    {
        public Category NewCategory { get; set; }
        public PagedList.IPagedList<Category> PagedListCategories { get; set; }
    }
}