using Microsoft.EntityFrameworkCore;

namespace SummerCampAPI.Models
{
  public class RegistrationsContext : DbContext
  {
    public RegistrationsContext(DbContextOptions<RegistrationsContext> options)
        : base(options)
    {
    }
    public DbSet<SC_Registration> SC_Registration { get; set; }
    public DbSet<SC_Status_History> SC_Status_History { get; set; }
    public DbSet<SC_Status_Lookup> SC_Status_Lookup { get; set; }
    public DbSet<SC_Payments> SC_Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<SC_Registration>()
      //    .ToTable("SC_Registration")
      //    .HasKey(e => e.ID);
      //modelBuilder.Entity<SC_Status_History>()
      //    .ToTable("SC_Status_History")
      //    .HasKey(e => e.ID);
      //modelBuilder.Entity<SC_Status_Lookup>()
      //    .ToTable("SC_Status_Lookup")
      //    .HasKey(e => e.ID_Code);
      //modelBuilder.Entity<SC_Payments>()
      //    .ToTable("SC_Payments")
      //    .HasKey(e => e.ID);

      modelBuilder.Entity<SC_Status_Lookup>()
          .HasData(
              new SC_Status_Lookup { ID_Code = "A", Desc = "Applied by Parent" },
              new SC_Status_Lookup { ID_Code = "I", Desc = "Invited - Seat Reserved" },
              new SC_Status_Lookup { ID_Code = "C", Desc = "aCcepted by Parent" },
              new SC_Status_Lookup { ID_Code = "P", Desc = "Paid by Parent" }
          );
    }
  }
}
