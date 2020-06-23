using EnergyCompanyDomain;
using Microsoft.EntityFrameworkCore;

namespace EnergyCompanySql
{
    public class EnergyCompanyDbContext : DbContext
    {
        public EnergyCompanyDbContext(DbContextOptions<EnergyCompanyDbContext> options) : base(options)
        {

        }

        public DbSet<MeterReading> MeterReadings { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EnergyCompanyDB;");
        }
    }
}
