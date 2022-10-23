using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.Data.DatabaseContext
{
    public class OfficeSupplyDB : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<ProductInMenu> ProductInMenus { get; set; }
        //public DbSet<SupplierInArea> SupplierInAreas { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<Period> Periods { get; set; }
        //public DbSet<CategoryQuota> CategoryQuotas { get; set; }
        //public DbSet<Token> Tokens { get; set; }

        public OfficeSupplyDB(DbContextOptions<OfficeSupplyDB> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInMenu>().HasKey(p => new { p.MenuID, p.ProductID });

            modelBuilder.Entity<SupplierInArea>().HasKey(s => new { s.AreaID, s.SupplierID });

            modelBuilder.Entity<User>().Property(u => u.DepartmentID).IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.DateOfBirth).IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.IsMale).IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.TokenDevice).IsRequired(false);

            modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.OrderID, o.ProductID, o.MenuID });

            modelBuilder.Entity<CategoryQuota>().HasKey(c => new { c.PeriodID, c.CategoryID });

            modelBuilder.Entity<Order>().Property(o => o.UserOrderID).IsRequired(false);
            modelBuilder.Entity<Order>().Property(o => o.UserApproveID).IsRequired(false);
            modelBuilder.Entity<Order>().Property(o => o.ApproveTime).IsRequired(false);

            modelBuilder.Entity<OrderHistory>().Property(o => o.FromStatus).IsRequired(false);
            modelBuilder.Entity<OrderHistory>().Property(o => o.ToStatus).IsRequired(false);

            modelBuilder.Entity<Company>().Property(c => c.ManagerID).IsRequired(false);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.ProductInMenu)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => new { o.MenuID, o.ProductID })
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
