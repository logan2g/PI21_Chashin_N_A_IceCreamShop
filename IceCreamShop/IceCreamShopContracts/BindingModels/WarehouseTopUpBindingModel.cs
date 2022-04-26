namespace IceCreamShopContracts.BindingModels
{
    public class WarehouseTopUpBindingModel
    {
        public int ComponentId { get; set; }
        public int WarehouseId { get; set; }
        public int Count { get; set; }
    }
}
