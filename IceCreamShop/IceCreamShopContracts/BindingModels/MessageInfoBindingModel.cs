using System;

namespace IceCreamShopContracts.BindingModels
{
    public class MessageInfoBindingModel
    {
        public int? ClientId { get; set; }
        
        public int? PageNumber { get; set; }

        public string MessageId { get; set; }

        public string FromMailAddress { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Reply { get; set; }

        public bool? IsRead { get; set; }

        public DateTime DateDelivery { get; set; }
    }
}
