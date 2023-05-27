using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoHDP.Models;

namespace ProyectoHDP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        //{

        //    builder.Properties<DateOnly>()
        //        .HaveConversion<DateOnlyConverter>()
        //        .HaveColumnType("date");

        //    base.ConfigureConventions(builder);

        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProyectoHDP.Models.Empleados>? Empleados { get; set; }
    }
}