using System;
using IceCreamShopContracts.Attributes;

namespace IceCreamShopContracts.ViewModels
{
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }

        [Column(title: "Отправитель", width: 100)]
        [DisplayName("Прочитано")]
        public bool IsRead { get; set; }

        [DisplayName("Отправитель")]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", width: 50)]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }

        [DisplayName("Ответ")]
        public string Reply { get; set; }
    }
}
