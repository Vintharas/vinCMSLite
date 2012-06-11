using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using Domain.Entities;

namespace vinCMS.Models
{
    public class AdminViewModel
    {
        public IPagedList<BlogPost> PagedListBlogPosts { get; set; }
        public IEnumerable<BlogPost> PostDraftsList { get; set; }
        public IEnumerable<Page> PagesList { get; set; }
    }
}