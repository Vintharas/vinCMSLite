using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace vinCMS.Models
{
    public class AdminMediaViewModel
    {
        public Media NewMedia { get; set; }
        public PagedList.IPagedList<Media> PagedListMedia { get; set; }
    }
}