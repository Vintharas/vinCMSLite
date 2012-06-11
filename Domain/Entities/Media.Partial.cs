using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    /// <summary>
    /// Media partia class
    /// </summary>
    [MetadataType(typeof(MediaMetadata))]
    public partial class Media
    {
        /// <summary>
        /// Media buddy class that holds its metadata
        /// </summary>
        public class MediaMetadata
        {
            [HiddenInput(DisplayValue = false)]
            public int MediaID { get; set; }

        }
    }
}
