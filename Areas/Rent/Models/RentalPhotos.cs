using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Areas.Rent.Models
{
    public class RentalPhotos
    {
        public int RentalPhotosId { get; set; }
        public string RentalsName { get; set; }
        public bool Damaged { get; set; }
        public byte[] RentalPhoto { get; set; }
        public string Details { get; set; }

    }

}
