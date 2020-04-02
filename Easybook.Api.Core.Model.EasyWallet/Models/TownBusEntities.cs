namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Easybook.Api.Core.Model.EasyWallet.Models.TownBus;

    public partial class TownBusEntities : DbContext
    {
        public TownBusEntities()
            : base("name=TownBusEntities")
        {
        }

        public virtual DbSet<TownBusTrip> TownBusTrips { get; set; }
        public virtual DbSet<TownBus.User> Users { get; set; }
        public virtual DbSet<TownBus.TownBusCoordinate> TownBusCoordinates { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<TownBusNotification> TownBusNotifications { get; set; }
        public virtual DbSet<TownBusCompany> TownBusCompanies { get; set; }
        public virtual DbSet<TownBusType> TownBusType { get; set; }
        public virtual DbSet<TownBus_SupportedArea> TownBus_SupportedArea { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TownBus_SupportedArea>()
                .Property(e => e.TownBusType_Bus_No)
                .IsUnicode(false);

            modelBuilder.Entity<TownBus_SupportedArea>()
                .Property(e => e.Area_Name)
                .IsUnicode(false);

            modelBuilder.Entity<TownBus_SupportedArea>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<TownBus_SupportedArea>()
                .Property(e => e.Create_By)
                .IsUnicode(false);

            modelBuilder.Entity<TownBus_SupportedArea>()
                .Property(e => e.Update_By)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.Create_User)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.Update_User)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.Time_Stamp)
                .IsFixedLength();
            modelBuilder.Entity<TownBus.TownBusCoordinate>()
               .Property(e => e.Latitude)
               .HasPrecision(9, 6);

            modelBuilder.Entity<TownBus.TownBusCoordinate>()
                .Property(e => e.Longtitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<TownBus.TownBusCoordinate>()
                .Property(e => e.Distance)
                .HasPrecision(36, 2);

            modelBuilder.Entity<Company>()
               .Property(e => e.Create_User)
               .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Update_User)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Time_Stamp)
                .IsFixedLength();

            modelBuilder.Entity<Company>()
                .Property(e => e.DataSource)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.LogoFileName)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.AvailableCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.Company_Code)
                .IsUnicode(false);

            modelBuilder.Entity<TownBusNotification>()
                .Property(e => e.NotificationToken)
                .IsUnicode(false);
            modelBuilder.Entity<TownBusType>()
               .Property(e => e.Bus_No)
               .IsUnicode(false);
        }


    }
}
