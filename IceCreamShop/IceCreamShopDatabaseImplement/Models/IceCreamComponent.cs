using System.ComponentModel.DataAnnotations; 

namespace IceCreamShopDatabaseImplement.Models
{
    public class IceCreamComponent
    {
        public int Id { get; set; }

        public int IceCreamId { get; set; }

        public int ComponentId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Component Component { get; set; }

        public virtual IceCream IceCream { get; set; }
    }
}
