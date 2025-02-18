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
  }
}
