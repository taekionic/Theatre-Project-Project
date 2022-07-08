using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Areas.Rent.Models
{
    public class RentalItem
    {
        public int RentalItemId { get; set; }
        public string Item { get; set; }
        public string ItemDescription { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Byte[] ItemPhoto { get; set; }

    }
}


