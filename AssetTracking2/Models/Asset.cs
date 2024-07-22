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

        
        public string Type { get; set; }

       
        public string? Brand { get; set; }

        public DateTime? PurchaseDate { get; set; }

       
        public string? Model { get; set; }

        public double? PriceInDollar { get; set; }

        public double? LocalPrice { get; set; }

        //public Office Office { get; set; }


        public Asset(int id, string type, string? brand, DateTime? purchaseDate, string? model, double? priceInDollar,
            double? localPrice)
        {
            Id = id;
            Type = type;
            Brand = brand;
            PurchaseDate = purchaseDate;
            Model = model;
            PriceInDollar = priceInDollar;
            LocalPrice = localPrice;

            // Office = office;
        }

    }
}
