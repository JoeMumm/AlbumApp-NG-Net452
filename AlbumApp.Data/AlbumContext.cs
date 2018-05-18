using AlbumApp.Business.Entities;
using AlbumApp.Core.Common.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Serialization;

namespace AlbumApp.Data
{
  public class AlbumContext : IdentityDbContext<ApplicationUser>
  {
    public AlbumContext() : base("name=AlbumApp")
    { Database.SetInitializer<AlbumContext>(null); }

    public virtual DbSet<Album> AlbumSet { get; set; }
    public virtual DbSet<Track> TrackSet { get; set; }
    public virtual DbSet<Order> OrderSet { get; set; }
    public virtual DbSet<CartItem> CartItemSet { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

      modelBuilder.Ignore<PropertyChangedEventHandler>();
      modelBuilder.Ignore<ExtensionDataObject>();
      modelBuilder.Ignore<IIdentifiableEntity>();

      modelBuilder.Entity<Account>().HasKey<string>(e => e.Id).Ignore(e => e.EntityId);
      modelBuilder.Entity<Album>().HasKey<int>(e => e.AlbumId).Ignore(e => e.EntityId);
      modelBuilder.Entity<Track>().HasKey<int>(e => e.TrackId).Ignore(e => e.EntityId);
      modelBuilder.Entity<Order>().HasKey<int>(e => e.OrderId).Ignore(e => e.EntityId);
      modelBuilder.Entity<CartItem>().HasKey<int>(e => e.CartItemId).Ignore(e => e.EntityId);

      modelBuilder.Entity<Account>().Property(x => x.FirstName).IsRequired().HasMaxLength(40);
      modelBuilder.Entity<Account>().Property(x => x.LastName).IsRequired().HasMaxLength(50);
      modelBuilder.Entity<Account>().Property(x => x.Address).IsRequired().HasMaxLength(50);
      modelBuilder.Entity<Account>().Property(x => x.City).IsRequired().HasMaxLength(30);
      modelBuilder.Entity<Account>().Property(x => x.State).IsRequired().HasMaxLength(30);
      modelBuilder.Entity<Account>().Property(x => x.ZipCode).IsRequired().HasMaxLength(15);
      modelBuilder.Entity<Account>().Property(x => x.CreditCard).IsRequired().HasMaxLength(16);
      modelBuilder.Entity<Account>().Property(x => x.ExpDate).IsRequired().HasMaxLength(5);

      modelBuilder.Entity<Album>().Property(x => x.AlbumNumber).IsRequired().HasMaxLength(15);
      modelBuilder.Entity<Album>().Property(x => x.Title).IsRequired().HasMaxLength(60);
      modelBuilder.Entity<Album>().Property(x => x.Artist).IsRequired().HasMaxLength(50);

      modelBuilder.Entity<Track>().Property(x => x.Name).IsRequired().HasMaxLength(50);

    }

    public static AlbumContext Create()
    { return new AlbumContext(); }
  }
}
