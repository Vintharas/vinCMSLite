using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Domain.Entities
{
    [MetadataType(typeof(ContentContainerMetadata))]
    public partial class ContentContainer
    {

        /// <summary>
        /// Method that obtains a list of those tags that are associated with the blogpost
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetListOfTags()
        {
            var listOfTags = new List<string>();
            if (Tags.Count > 0)
            {
                // build a list of tag names from the tags associated to the blogpost
                listOfTags.AddRange(Tags.Select(pageTag => pageTag.Name));
            }
            return listOfTags;
        }

        /// <summary>
        /// Method that obtains a list of those categories the blogpost belongs to
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetListOfCategories()
        {
            var listOfCategories = new List<string>();
            if (Categories.Count > 0)
            {
                // build a list of category names from the categories the blogpost belongs to
                listOfCategories.AddRange(Categories.Select(pageCategory => pageCategory.Name));
            }
            return listOfCategories;
        }

        /// <summary>
        /// Gets a comma separated list of tags within a string
        /// </summary>
        /// <returns></returns>
        public string GetStringOfTags()
        {
            // explanation of this tricky linQ
            // 1) Concatenates all the tagNames in a single string in this format (tag1 tag2 tag3 )
            // 2) If there are spaces between words of a tagname they are replaced by a -
            // 3) It trims the string, eliminating the last space
            // 4) It replaces spaces for commas
            // 5) It replaces - for spaces
            return Tags.ToList().
                Aggregate(string.Empty, (current, tag) => current + (tag.Name.Replace(" ", "-") + " ")).Trim().Replace(" ", ",").Replace("-", " ");
        }

        /// <summary>
        /// Gets the formatted name of the author of the blog post
        /// </summary>
        /// <returns></returns>
        virtual public string GetAuthorName()
        {
            return MetaAuthor == null ? string.Empty : MetaAuthor.Trim();
        }

        /// <summary>
        /// Class that defines the metadata to be used with objects that belong to the ContentContainer Class
        /// </summary>
        public class ContentContainerMetadata
        {
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

            [Required(ErrorMessage = "You need to set a path")]
            [DisplayName("Path")]
            [StringLength(100, ErrorMessage = "The path cannot be longer than 100 characters")]
            [RegularExpression(@"(\d|[a-z]|\-)+", ErrorMessage = "Use only lower case letters, numbers and dashes")]
            public string Path { get; set; }

        }

    }
}
