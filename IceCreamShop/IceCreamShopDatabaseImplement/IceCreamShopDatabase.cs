using IceCreamShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace IceCreamShopDatabaseImplement
{
    public class IceCreamShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=IceCreamShopDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Component> Components { set; get; }

        public virtual DbSet<IceCream> IceCreams { set; get; }

        public virtual DbSet<IceCreamComponent> IceCreamComponents { set; get; }

        public virtual DbSet<Order> Orders { set; get; }
    }
}
