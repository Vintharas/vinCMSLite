using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace vinCMS.Models
{
    public class AdminTagViewModel
    {
        public Tag NewTag { get; set; }
        public PagedList.IPagedList<Tag> PagedListTags { get; set; }
    }
}