using Microsoft.EntityFrameworkCore;

namespace cafe_management.Models
{
    public class CafeDBContext : DbContext
    {
        public CafeDBContext()
        {
        }

        public CafeDBContext(DbContextOptions<CafeDBContext> options)
            : base(options)
        {
        }

        public DbSet<TbBill> TbBills { get; set; }
        public DbSet<TbBillDetail> TbBillDetails { get; set; }
        public DbSet<TbAccount> TbAccounts { get; set; }
        public DbSet<TbCategory> TbCategories { get; set; }
        public DbSet<TbProduct> TbProducts { get; set; }
        public DbSet<TbCustomer> TbCustomers { get; set; }

        public DbSet<TbNews> TbNews { get; set; }
        public DbSet<TbFeedback> TbFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbBillDetail>(entity =>
            {
                // [Key] không làm được
                //Data Annotation không thể định nghĩa khóa chính bằng 2 cột.
                entity.HasKey(e => new { e.BillId, e.ProductId });
            });
        }
    }
}