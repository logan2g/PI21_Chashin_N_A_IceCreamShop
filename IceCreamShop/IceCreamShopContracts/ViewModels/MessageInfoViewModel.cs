using System;
using IceCreamShopContracts.Attributes;

namespace IceCreamShopContracts.ViewModels
{
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }

        [Column(title: "Прочитано", width: 60)]
        public bool IsRead { get; set; }

        [Column(title: "Отправитель", width: 100)]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", width: 50)]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }

        [Column(title: "Ответ", width: 100)]
        public string Reply { get; set; }
    }
}
