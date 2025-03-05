using Microsoft.EntityFrameworkCore;

namespace SummerCampAPI.Models
{
  public class RegistrationsContext : DbContext
  {
    public RegistrationsContext(DbContextOptions<RegistrationsContext> options)
        : base(options)
    {
    }
    public DbSet<Summer_Camp_Registration> Summer_Camp_Registration { get; set; }
    public DbSet<Summer_Camp_Status_History> Summer_Camp_Status_History { get; set; }
    public DbSet<Summer_Camp_Status_Lookup> Summer_Camp_Status_Lookup { get; set; }
    public DbSet<Summer_Camp_Payments> Summer_Camp_Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<SC_Registration>()
      //    .ToTable("Summer_Camp_Registration")
      //    .HasKey(e => e.ID);
      //modelBuilder.Entity<Summer_Camp_Status_History>()
      //    .ToTable("Summer_Camp_Status_History")
      //    .HasKey(e => e.ID);
      //modelBuilder.Entity<Summer_Camp_Status_Lookup>()
      //    .ToTable("Summer_Camp_Status_Lookup")
      //    .HasKey(e => e.ID_Code);
      //modelBuilder.Entity<Summer_Camp_Payments>()
      //    .ToTable("Summer_Camp_Payments")
      //    .HasKey(e => e.ID);

      modelBuilder.Entity<Summer_Camp_Status_Lookup>()
          .HasData(
              new Summer_Camp_Status_Lookup { ID_Code = "A", Desc = "(A)pplied by Parent" },
              new Summer_Camp_Status_Lookup { ID_Code = "I", Desc = "(I)nvited - Seat Reserved" },
              new Summer_Camp_Status_Lookup { ID_Code = "C", Desc = "A(C)cepted by Parent" },
              new Summer_Camp_Status_Lookup { ID_Code = "R", Desc = "Invitation (R)ejected by Parent" },
              new Summer_Camp_Status_Lookup { ID_Code = "F", Desc = "Payment (F)ailed by Parent" },
              new Summer_Camp_Status_Lookup { ID_Code = "P", Desc = "(P)aid by Parent" }
          );
    }
  }
}
