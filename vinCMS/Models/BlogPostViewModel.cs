using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;
using PagedList;

namespace vinCMS.Models
{
    public class BlogPostViewModel
    {
        public IPagedList<BlogPost> BlogPostPagedList { get; set; }
        public string Title { get; set; }
    }
}