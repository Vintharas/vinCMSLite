using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Entities
{

    [MetadataType(typeof(PageMetadata))]
    public partial class Page
    {

        /// <summary>
        /// Buddy class that contains the metadata of the class Page
        /// </summary>
        public class PageMetadata
        {
            [DisplayName("Is it the Home Page?")]
            public bool IsHomePage { get; set; }

            [DisplayName("Add a link in the navigation menu?")]
            public bool IsNavigablePage { get; set; }
            
        }
    }
}
