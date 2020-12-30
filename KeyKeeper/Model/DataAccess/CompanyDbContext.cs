using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyKeeper.Model.DataAccess
{
    /// <summary>
    /// Main db context class.
    /// </summary>
    public class CompanyDbContext : DbContext
    {
        // Constructor:
        public CompanyDbContext() : base("name = ContextOnLocalServer") 
        {
            //Database.SetInitializer<CompanyDbContext>(new DropCreateDatabaseAlways<CompanyDbContext>()); <- temporary
        }
        // Entities:
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RoomKey> RoomKeys { get; set; }
    }
}
