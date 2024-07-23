//implicit using-directives added by compiler in all console-classes .net8:
//using System;
//using System.IO
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net.Http:
//using System.Threading;
//using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace AssetTracking2.Models
{
    public class Asset
    {
        
        public int Id { get; set; }


        [Required,MaxLength(10)]
        public string Type { get; set; }


        [MaxLength(50)]
        public string? Brand { get; set; }

        [MaxLength(50)]
        public DateTime? PurchaseDate { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        public double? PriceInDollar { get; set; }

        public double? LocalPrice { get; set; }

        public int OfficeId { get; set; }

        public Office Office { get; set; }     // Nav.property


        //public Asset(int id, string type, string? brand, DateTime? purchaseDate, string? model, double? priceInDollar)
        //{
        //    Id = id;
        //    Type = type;
        //    Brand = brand;
        //    PurchaseDate = purchaseDate;
        //    Model = model;
        //    PriceInDollar = priceInDollar;

        //    //LocalPrice = localPrice;

        //    // Office = office;
        //}

    }
}
