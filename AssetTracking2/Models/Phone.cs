namespace AssetTracking2.Models
{
    public class Phone : Asset
    {
        public Phone(int id, string? brand, string? model, DateTime? purchaseDate, double? priceD, double? localPrice) :
            base(id, "Phone", brand, purchaseDate, model, priceD, localPrice)
        {
        }


        //public string ConnectedToOperator { get; set; }
    }
}
