namespace AssetTracking2.Models
{
    public class Computer : Asset
    {
        public Computer(int id, string? brand, string? model, DateTime? purchaseDate, double? priceD, double? localPrice) :
            base(id, "Computer", brand, purchaseDate, model, priceD, localPrice)
        {
        }
    }
}
