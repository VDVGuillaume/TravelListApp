using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelListModels
{
    public class TravelListItemImage
    {
        [Key]
        public int TravelListItemImageID { get; set; }
        [Required]
        public int TravelListItemID { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        [Required]
        public string ImageName { get; set; }
    }
}
