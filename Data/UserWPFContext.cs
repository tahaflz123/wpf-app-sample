using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Model;

namespace WpfApp1.Data
{


public class UserWPFContext : DbContext
{


    public DbSet<User> users => Set<User>();
    public DbSet<Patient> patients => Set<Patient>();

    public DbSet<Order> orders => Set<Order>(); 
    public DbSet<Ingredient> ingredient => Set<Ingredient>();
    public DbSet<IngredientUsage> ingredientUsage => Set<IngredientUsage>();



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer("Server=TAHA-COMPUTER\\SQLEXPRESS;Database=patients;Trusted_Connection=true", builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null));
        //optionsBuilder.UseSqlite("Data Source=C:/Users/Taha/source/repos/WpfApp1/patients");
        //optionsBuilder.UseMySql("server=localhost;database=patient;user=root;password=123456", ServerVersion.AutoDetect(new MySqlConnector.MySqlConnection()));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(e => e.Id);
            user.Property(e => e.name).IsRequired();
            user.Property(e => e.surname).IsRequired();
            user.Property(e => e.email).IsRequired();
            user.Property(e => e.phoneNumber).IsRequired();
            user.ToTable("users");
        });

        modelBuilder.Entity<Patient>(patient =>
        {
            patient.HasKey(e => e.Id);
            patient.Property(e => e.name).IsRequired();
            patient.Property(e => e.surname).IsRequired();
            patient.Property(e => e.email).IsRequired();
            patient.Property(e => e.phoneNumber).IsRequired();
            patient.Property(e => e.gender).IsRequired();
            patient.Property(e => e.age).IsRequired();
            patient.HasOne(p => p.creator).WithMany(u => u.patients);
            patient.ToTable("patients");
        });

        modelBuilder.Entity<Order>(order =>
        {
                order.HasKey(o => o.Id);
                order.HasOne(o => o.creator).WithMany(u => u.orders);
                order.HasOne(o => o.createdFor).WithMany(p => p.createdOrders);
                order.Property(o => o.createdDate).IsRequired();
                order.Property(o => o.maxBagSizeWithML).IsRequired();
        });

        modelBuilder.Entity<Ingredient>(ingredient =>
        {
                ingredient.HasKey(i => i.Id);
                ingredient.Property(i => i.name).IsRequired();
                ingredient.Property(i => i.createdDate).IsRequired();

        });

        modelBuilder.Entity<IngredientUsage>(usage =>
        {
                usage.HasKey(i => i.Id);
                usage.HasOne(i => i.Ingredient).WithMany(x => x.usagedBy);
                usage.HasOne(i => i.order).WithMany(o => o.ingredientUsages);
                usage.Property(i => i.usedMl).IsRequired();
        });




        base.OnModelCreating(modelBuilder);
    }
}

}