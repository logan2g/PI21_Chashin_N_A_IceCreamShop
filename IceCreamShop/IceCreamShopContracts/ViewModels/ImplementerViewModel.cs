using IceCreamShopContracts.Attributes;

namespace IceCreamShopContracts.ViewModels
{
    public class ImplementerViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ImplementerFIO { get; set; }

        [Column(title: "Время на заказ", width: 50)]
        public int WorkingTime { get; set; }

        [Column(title: "Время на перерыв", width: 50)]
        public int PauseTime { get; set; }
    }
}
