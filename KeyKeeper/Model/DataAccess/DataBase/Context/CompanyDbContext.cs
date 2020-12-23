using KeyKeeper.Model.DataAccess.DataBase.Domain;
using System.Data.Entity;

namespace KeyKeeper.Model.DataAccess.DataBase.Context
{
    class CompanyDbContext : DbContext
    {
        public CompanyDbContext() : base("name=CompanyDbContext_Test") { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RoomKey> RoomKeys { get; set; }
    }
}
