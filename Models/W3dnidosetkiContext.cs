using System;
using System.Collections.Generic;
using dotenv.net;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using w3dniDoSetki.Entities;

namespace w3dniDoSetki;

public partial class W3dnidosetkiContext : DbContext
{
    
    public W3dnidosetkiContext()
    {
        
        DotEnv.Load();
    }

    public W3dnidosetkiContext(DbContextOptions<W3dnidosetkiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Car1> Cars1 { get; set; }

    public virtual DbSet<Carmodel> Carmodels { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(Env.GetString("DBCONN"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("bodytypes", new[] { "Hatchback", "Coupe", "Cabriolet", "Kombi", "Sedan", "SUV", "Minivan" })
            .HasPostgresEnum("car_make", new[] { "Acura", "Alfa Romeo", "AMC", "Aston Martin", "Audi", "Avanti", "Bentley", "BMW", "Buick", "Bugatti", "Cadillac", "Chevrolet", "Chrysler", "Daewoo", "Daihatsu", "Datsun", "DeLorean", "Dodge", "Eagle", "Ferrari", "Fiat", "Fisker", "Ford", "Freightliner", "Genesis", "Geo", "GMC", "Honda", "HUMMER", "Hyundai", "Infiniti", "Isuzu", "Jaguar", "Jeep", "Kia", "Lamborghini", "Lancia", "Land Rover", "Lexus", "Lincoln", "Lotus", "Maserati", "Maybach", "Mazda", "McLaren", "Mercedes-Benz", "Mercury", "Mini", "Mitsubishi", "Nissan", "Oldsmobile", "Panoz", "Peugeot", "Plymouth", "Pontiac", "Porsche", "RAM", "Renault", "Rolls-Royce", "Saab", "Saleen", "Saturn", "Scion", "Shelby", "Spyker", "Subaru", "Suzuki", "Tesla", "Toyota", "Triumph", "Volkswagen", "Volvo", "Yugo" })
            .HasPostgresEnum("car_make_temp", new[] { "Acura", "Alfa Romeo", "AMC", "Aston Martin", "Audi", "Avanti", "Bentley", "BMW", "Bugatti", "Buick", "Cadillac", "Chevrolet", "Chrysler", "Daewoo", "Daihatsu", "Datsun", "DeLorean", "Dodge", "Eagle", "Ferrari", "Fiat", "Fisker", "Ford", "Freightliner", "Genesis", "Geo", "GMC", "Honda", "HUMMER", "Hyundai", "Infiniti", "Isuzu", "Jaguar", "Jeep", "Kia", "Lamborghini", "Lancia", "Land Rover", "Lexus", "Lincoln", "Lotus", "Maserati", "Maybach", "Mazda", "McLaren", "Mercedes-Benz", "Mercury", "Mini", "Mitsubishi", "Nissan", "Oldsmobile", "Panoz", "Peugeot", "Plymouth", "Pontiac", "Porsche", "RAM", "Renault", "Rolls-Royce", "Saab", "Saleen", "Saturn", "Scion", "Shelby", "Spyker", "Subaru", "Suzuki", "Tesla", "Toyota", "Triumph", "Volkswagen", "Volvo", "Yugo" })
            .HasPostgresEnum("colortype", new[] { "Metalic", "Mat", "Satyna" })
            .HasPostgresEnum("condition", new[] { "Used", "New" })
            .HasPostgresEnum("fuel", new[] { "Diesel", "Benzyna", "LPG", "Hybrid", "Elektryczny" })
            .HasPostgresEnum("gearbox", new[] { "Manual", "Automat", "Bezstopniowa" });
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("car_pkey");

            entity.ToTable("car");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Make)
                .HasMaxLength(20)
                .HasColumnName("make");
        });

        modelBuilder.Entity<Car1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cars_pk");

            entity.ToTable("Cars");
        
            entity.HasIndex(e => e.Description, "cars_description_uindex").IsUnique();

            entity.HasIndex(e => e.Id, "cars_id_uindex").IsUnique();

            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasColumnName("color");
            entity.Property(e => e.Country)
                .HasMaxLength(12)
                .HasColumnName("country");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EngineCapacity).HasColumnName("engineCapacity");
            entity.Property(e => e.EnginePower).HasColumnName("enginePower");
            entity.Property(e => e.FuelRateInCity).HasColumnName("fuelRateInCity");
            entity.Property(e => e.FuelRateInTrip).HasColumnName("fuelRateInTrip");
            entity.Property(e => e.Milage).HasColumnName("milage");
            entity.Property(e => e.NoAccidents).HasColumnName("noAccidents");
            entity.Property(e => e.NumOfDoors).HasColumnName("numOfDoors");
            entity.Property(e => e.NumOfSeats).HasColumnName("numOfSeats");
            entity.Property(e => e.ProdYear).HasColumnName("prodYear");
            entity.Property(e => e.RegistredInPl).HasColumnName("registredInPl");
            entity.Property(e => e.SellerId).HasColumnName("sellerId");
            entity.Property(e => e.StOwner).HasColumnName("stOwner");
            entity.Property(e => e.StRegistration).HasColumnName("stRegistration");
            entity.Property(e => e.fuel).HasColumnName("fuel");
            entity.Property(e => e.path).HasColumnName("path");
            entity.Property(e => e.price).HasColumnName("price");
            entity.Property(e => e.gearboxType).HasColumnName("gearboxType");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .HasColumnName("vin");
            entity.HasOne(d => d.Seller).WithMany(p => p.Car1s)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userid");
            entity.Property(e => e.condition);
        });

        modelBuilder.Entity<Carmodel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("carmodels_pkey");

            entity.ToTable("carmodels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brandid).HasColumnName("brandid");
            entity.Property(e => e.Model).HasColumnName("model");

            entity.HasOne(d => d.Brand).WithMany(p => p.Carmodels)
                .HasForeignKey(d => d.Brandid)
                .HasConstraintName("fk_brandid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.HasIndex(e => e.Id, "users_id_uindex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.IsVerified).HasColumnName("isVerified");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(15);
            entity.Property(e => e.PasswordHash).HasMaxLength(70);
            entity.Property(e => e.PhoneNumber).HasMaxLength(9);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
