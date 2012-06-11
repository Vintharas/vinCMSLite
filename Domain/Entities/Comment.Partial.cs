using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    /// <summary>
    /// Comment partial class
    /// </summary>
    [MetadataType(typeof(CommentMetadata))]
    public partial class Comment
    {
        /// <summary>
        /// Comment buddy class for adding metadata info
        /// </summary>
        public class CommentMetadata
        {
            [HiddenInput(DisplayValue = false)]
            public int CommentID { get; set; }

            [Required(ErrorMessage = "You need to set an author name")]
            [StringLength(50, ErrorMessage = "You can use 50 characters tops")]
            [RegularExpression(@"([a-zA-Z0-9\s])*", ErrorMessage = "Use alphanumeric characters only")]
            [DisplayName("Author")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "You need to write a comment")]
            [StringLength(500, ErrorMessage = "You can use 500 characters tops")]
            [DisplayName("Comment")]
            [UIHint("MediumTextArea")]
            public string CommentContent { get; set; }

            [StringLength(50, ErrorMessage = "You can use 50 characters tops")]
            [DisplayName("Website")]
            public string UserWebsite { get; set; }

            [HiddenInput(DisplayValue = false)]
            public Nullable<int> BlogPostID { get; set; }

            [ScaffoldColumn(false)]
            public System.DateTime CommentDate { get; set; }
            
        }
    }
}
