namespace Easybook.Api.Core.Model.EasyWallet.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WalletEntities : DbContext
    {
        public WalletEntities()
            : base("name=WalletEntities")
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<SubTransaction> SubTransactions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wallet_Account> Wallet_Account { get; set; }
        public virtual DbSet<Wallet_Logs> Wallet_Logs { get; set; }
        public virtual DbSet<Wallet_Snapshot> Wallet_Snapshot { get; set; }
        public virtual DbSet<Wallet_User> Wallet_User { get; set; }
        public virtual DbSet<Wallet_Checksum> Wallet_Checksum { get; set; }
        public virtual DbSet<Wallet_Rule> Wallet_Rule { get; set; }
        public virtual DbSet<User_Card> User_Card { get; set; }
        public virtual DbSet<Wallet_Interest> Wallet_Interest { get; set; }
        public virtual DbSet<Transaction_Interest_Snapshot> Snapshot_Transaction_Interest { get; set; }
        public virtual DbSet<Wallet_Account_Reward> Transaction_Rewards { get; set; }
        public virtual DbSet<User_Bank_Account> User_Bank_Account { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubTransaction>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SubTransaction>()
                .Property(e => e.Direction)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Source_Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Destination_Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Status)
                .IsUnicode(false);      

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Wallet_User)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wallet_Account>()
                .Property(e => e.Available_Balance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Wallet_Account>()
                .Property(e => e.Total_Balance)
                .HasPrecision(19, 4);        

            modelBuilder.Entity<Wallet_Logs>()
                .Property(e => e.AspNetUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Wallet_Logs>()
                .Property(e => e.ApiCallerType)
                .IsUnicode(false);

            modelBuilder.Entity<Wallet_Logs>()
                .Property(e => e.ApiMethod)
                .IsUnicode(false);       

            modelBuilder.Entity<Wallet_Rule>()
                .Property(e => e.Maximum_Topup_Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Wallet_Rule>()
                .Property(e => e.Maximum_Withdraw_Amount)
                .HasPrecision(19, 4);
        }
    }
}
