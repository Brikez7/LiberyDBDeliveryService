using LiberyDBDeliveryService.Models.DB.Table;
using Microsoft.EntityFrameworkCore;

namespace LiberyDBDeliveryService.Models.DB.Context
{
    public partial class DeliveryServiceContext : DbContext
    {
        private static ConnectionBase _connection;
        static DeliveryServiceContext() 
        {
            _connection = new PathConnection();
        }
        public DeliveryServiceContext()
        {
        }
        public DeliveryServiceContext(DbContextOptions<DeliveryServiceContext> options)
            : base(options)
        {
        }
        static public void UpdateConnetion(ConnectionBase newConnection) 
        {
            _connection = newConnection;
        }
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<WorkShift> WorkShifts { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(_connection.GetPathConnection());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => new { e.IdTelegram, e.Post, e.Name });

                entity.ToTable("Account");

                entity.HasIndex(e => e.IdTelegram, "KEY_Account_idTelegram")
                    .IsUnique();

                entity.Property(e => e.IdTelegram).HasColumnName("idTelegram");

                entity.Property(e => e.Post).HasColumnName("post");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Life).HasColumnName("life");

                entity.Property(e => e.LoginTelegram)
                    .HasMaxLength(50)
                    .HasColumnName("loginTelegram")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK_Order_Id");

                entity.ToTable("Order");

                entity.Property(e => e.IdOrder).HasColumnName("idOrder");

                entity.Property(e => e.AddresClient)
                    .HasMaxLength(100)
                    .HasColumnName("addresClient")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AddresWarehouse)
                    .HasMaxLength(100)
                    .HasColumnName("addresWarehouse")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DateOrder)
                    .HasMaxLength(50)
                    .HasColumnName("dateOrder")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DeliveryTime)
                    .HasMaxLength(30)
                    .HasColumnName("deliveryTime")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Deposit).HasColumnName("deposit");

                entity.Property(e => e.Describe)
                    .HasMaxLength(100)
                    .HasColumnName("describe")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FenceTime)
                    .HasMaxLength(30)
                    .HasColumnName("fenceTime")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.IdTelegramDeliver).HasColumnName("idTelegramDeliver");

                entity.Property(e => e.IdTelegramShop).HasColumnName("idTelegramShop");

                entity.Property(e => e.PhoneClient)
                    .HasMaxLength(40)
                    .HasColumnName("phoneClient")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PhoneSenders)
                    .HasMaxLength(30)
                    .HasColumnName("phoneSenders")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Product)
                    .HasColumnName("product")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.StatusOrder).HasColumnName("statusOrder");

                entity.Property(e => e.TypeMovement)
                    .HasMaxLength(30)
                    .HasColumnName("typeMovement")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.IdTelegramDeliverNavigation)
                    .WithMany(p => p.OrderIdTelegramDeliverNavigations)
                    .HasPrincipalKey(p => p.IdTelegram)
                    .HasForeignKey(d => d.IdTelegramDeliver)
                    .HasConstraintName("FK_Order_idTelegramEmploye");

                entity.HasOne(d => d.IdTelegramShopNavigation)
                    .WithMany(p => p.OrderIdTelegramShopNavigations)
                    .HasPrincipalKey(p => p.IdTelegram)
                    .HasForeignKey(d => d.IdTelegramShop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_idTelegramShop");
            });

            modelBuilder.Entity<WorkShift>(entity =>
            {
                entity.ToTable("WorkShift");

                entity.HasIndex(e => e.IdWorkShift, "PK_WorkShift")
                    .IsClustered();

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.IdTelegramEmploye)
                .HasColumnName("idTelegramEmploye");

                entity.Property(e => e.Status)
                .HasColumnName("status");

                entity.HasOne(d => d.IdTelegramEmployeNavigation)
                    .WithMany()
                    .HasPrincipalKey(p => p.IdTelegram)
                    .HasForeignKey(d => d.IdTelegramEmploye)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkShift_idTelegramEmploye");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
