using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    [MetadataType(typeof(TagMetadata))]
    public partial class Tag
    {

        /// <summary>
        /// Buddy class that contains Tag metadata
        /// </summary>
        public class TagMetadata
        {
            [HiddenInput(DisplayValue = false)]
            public int TagID { get; set; }

            [Required(ErrorMessage = "You must set a name for the tag")]
            [StringLength(50, ErrorMessage = "Use less than 50 characters")]
            [RegularExpression(@"([a-z0-9\s])*", ErrorMessage = "Use alphanumeric characters only")]
            [DisplayName("Tag Name")]
            public string Name { get; set; }
            
        }
    }
}
