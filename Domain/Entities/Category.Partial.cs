using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {

        /// <summary>
        /// Metadata buddy class
        /// </summary>
        public class CategoryMetadata
        {
            [HiddenInput(DisplayValue = false)]
            public int CategoryID { get; set; }

            [Required(ErrorMessage = "You must set a name for the category")]
            [StringLength(50, ErrorMessage= "Use less than 50 characters")]
            [RegularExpression(@"([a-z0-9\s])*", ErrorMessage="Use alphanumeric characters only")]
            [DisplayName("Category Name")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public State State { get; set; }
        }
    }
}

