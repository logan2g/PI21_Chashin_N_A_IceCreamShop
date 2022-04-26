﻿using System.ComponentModel;
using System.Runtime.Serialization;

namespace IceCreamShopContracts.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        [DisplayName("ФИО")]
        public string ClientFIO { get; set; }

        [DataMember]
        [DisplayName("Логин")]
        public string Email { get; set; }

        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
