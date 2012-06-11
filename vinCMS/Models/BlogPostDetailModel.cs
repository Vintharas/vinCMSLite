using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace vinCMS.Models
{
    public class BlogPostDetailModel
    {
        public BlogPost BlogPost { get; set; }
        public IList<BlogPost> RelatedBlogPosts { get; set; }
        public bool HasRelatedBlogPosts
        {
            get 
            {
                return RelatedBlogPosts != null && RelatedBlogPosts.Count != 0;
            }
        }
        public Comment NewComment { get; set; }
    }
}