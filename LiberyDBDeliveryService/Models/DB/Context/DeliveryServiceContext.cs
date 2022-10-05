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
            Database.EnsureDeleted();
            Database.EnsureCreated();
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

                entity.Property(e => e.IdTelegram)
                    .HasColumnType("bigint")
                    .HasColumnName("idTelegram");

                entity.Property(e => e.Post)
                .HasColumnType("smallint")
                .HasColumnName("post");

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Life)
                    .HasColumnType("bit")
                    .HasColumnName("life");

                entity.Property(e => e.LoginTelegram)
                    .HasMaxLength(50)
                    .HasColumnName("loginTelegram")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.WorkNow)
                    .HasColumnType("bit")
                    .HasColumnName("workNow");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK_Order_Id");

                entity.ToTable("Order");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("bigint")
                    .HasColumnName("idOrder");

                entity.Property(e => e.AddresClient)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(100)
                    .HasColumnName("addresClient")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AddresWarehouse)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(100)
                    .HasColumnName("addresWarehouse")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DateOrder)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50)
                    .HasColumnName("dateOrder")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DeliveryTime)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(30)
                    .HasColumnName("deliveryTime")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Deposit)
                    .HasColumnType("float")
                    .HasColumnName("deposit");

                entity.Property(e => e.Describe)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(100)
                    .HasColumnName("describe")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.FenceTime)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50)
                    .HasColumnName("fenceTime")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.IdTelegramDeliver)
                    .HasColumnType("bigint")
                    .HasColumnName("idTelegramDeliver");

                entity.Property(e => e.IdTelegramShop)
                    .HasColumnType("bigint")
                    .HasColumnName("idTelegramShop");

                entity.Property(e => e.PhoneClient)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(40)
                    .HasColumnName("phoneClient")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.PhoneSenders)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(30)
                    .HasColumnName("phoneSenders")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Price)
                    .HasColumnType("float")
                    .HasColumnName("price");

                entity.Property(e => e.Product)
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar")
                    .HasColumnName("product")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.StatusOrder)
                    .HasColumnType("smallint")
                    .HasColumnName("statusOrder");

                entity.Property(e => e.TypeMovement)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(30)
                    .HasColumnName("typeMovement")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Weight)
                    .HasColumnType("float")
                    .HasColumnName("weight");

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

                entity.HasKey(e => e.IdWorkShift)
                    .HasName("PK_WorkShift_Id");

                entity.HasIndex(e => e.IdWorkShift, "PK_WorkShift")
                    .IsClustered();

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.IdTelegramEmploye)
                    .HasColumnType("bigint")
                    .HasColumnName("idTelegramEmploye");

                entity.Property(e => e.Status)
                    .HasColumnType("bit")
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
