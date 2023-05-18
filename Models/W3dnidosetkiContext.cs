using w3dniDoSetki.Entities;
using Microsoft.EntityFrameworkCore;

namespace w3dniDoSetki;

public partial class W3dnidosetkiContext : DbContext
{
    public W3dnidosetkiContext()
    {
    }

    public W3dnidosetkiContext(DbContextOptions<W3dnidosetkiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
            entity.Property(e => e.PhoneNumber).HasMaxLength(9);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
