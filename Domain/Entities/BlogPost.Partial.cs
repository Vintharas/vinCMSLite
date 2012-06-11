using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{

    [MetadataType(typeof(BlogPostMetadata))]
    public partial class BlogPost
    {

        /// <summary>
        /// Gets the formatted name of the author of the blog post
        /// </summary>
        /// <returns></returns>
        public override string GetAuthorName()
        {
            string fullName = (User.RealName ?? string.Empty) + " " + (User.RealSurname ?? string.Empty);
            return fullName.Trim();
        }

        /// <summary>
        /// Class that contains the metadata associated to the BlogPost class
        /// </summary>
        public partial class BlogPostMetadata
        {
            [HiddenInput(DisplayValue=false)]
            public System.DateTime CreationDate { get; set; }

            [HiddenInput(DisplayValue = false)]
            public System.DateTime PublishingDate { get; set; }

            [HiddenInput(DisplayValue = false)]
            public bool IsDraft { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int AuthorID { get; set; }

            [DataType(DataType.MultilineText)]
            [UIHint("BigBox")]
            [Required(ErrorMessage = "You need to set a summary for the blog post")]
            public string Summary { get; set; }

            [HiddenInput(DisplayValue = false)]
            public int ContentID { get; set; }

            [Required(ErrorMessage = "You need to set a title")]
            [StringLength(100, ErrorMessage = "The title cannot be longer that 100 characters")]
            [DisplayName("Title")]
            public string Title { get; set; }

            [Required(ErrorMessage = "You need to set a content")]
            [DisplayName("Body Content")]
            [DataType(DataType.MultilineText)]
            [UIHint("BigBox")]
            public string BodyContent { get; set; }

            [StringLength(200, ErrorMessage = "The meta description cannot be longer than 200 characters")]
            [DisplayName("Meta Description")]
            [DataType(DataType.MultilineText)]
            [UIHint("BigBox")]
            public string MetaDescription { get; set; }

            [ScaffoldColumn(false)]
            public string MetaAuthor { get; set; }

            [Required(ErrorMessage = "You need to set a path for the blog post")]
            [DisplayName("Path")]
            [StringLength(100, ErrorMessage = "The path cannot be longer than 100 characters")]
            [RegularExpression(@"(\d|[a-z]|\-)+", ErrorMessage = "Use only lower case letters, numbers and dashes")]
            public string Path { get; set; }

        }
    }

}
