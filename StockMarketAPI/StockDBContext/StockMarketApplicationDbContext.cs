using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StockMarketAPI.Models;

namespace StockMarketAPI.StockDBContext;

public partial class StockMarketApplicationDbContext : DbContext
{
    public StockMarketApplicationDbContext()
    {
    }

    public StockMarketApplicationDbContext(DbContextOptions<StockMarketApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockPrice> StockPrices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserHolding> UserHoldings { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    public virtual DbSet<Watchlist> Watchlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Mycon");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stocks__2C83A9C2779D0CD5");

            entity.HasIndex(e => e.TickerSymbol, "UQ__Stocks__F144591B1F244FDA").IsUnique();

            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Exchange)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sector)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TickerSymbol)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StockPrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__StockPri__49575BAF4C0B1EB9");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriceDate).HasColumnType("datetime");

            entity.HasOne(d => d.Stock).WithMany(p => p.StockPrices)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__StockPric__Stock__4CA06362");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CDF9AB420");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4BCE0ACA1").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534D19CCC31").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("User");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserHolding>(entity =>
        {
            entity.HasKey(e => e.HoldingId).HasName("PK__UserHold__E524B56D11142EF3");

            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Stock).WithMany(p => p.UserHoldings)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__UserHoldi__Stock__44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.UserHoldings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserHoldi__UserI__440B1D61");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__UserToke__658FEEEA41DF6091");

            entity.Property(e => e.Expiry).HasColumnType("datetime");
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);
            entity.Property(e => e.Token).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserToken__UserI__3D5E1FD2");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasKey(e => e.WatchlistId).HasName("PK__Watchlis__48DE55CB5EB0A1C7");

            entity.ToTable("Watchlist");

            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Stock).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__Watchlist__Stock__48CFD27E");

            entity.HasOne(d => d.User).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Watchlist__UserI__47DBAE45");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
