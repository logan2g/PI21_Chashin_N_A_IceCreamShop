using System;
using System.ComponentModel;

namespace IceCreamShopContracts.ViewModels
{
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }

        [DisplayName("Прочитано")]
        public bool IsRead { get; set; }

        [DisplayName("Отправитель")]
        public string SenderName { get; set; }

        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }

        [DisplayName("Заголовок")]
        public string Subject { get; set; }

        [DisplayName("Текст")]
        public string Body { get; set; }

        [DisplayName("Ответ")]
        public string Reply { get; set; }
    }
}
