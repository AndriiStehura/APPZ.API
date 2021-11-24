using Microsoft.EntityFrameworkCore;

namespace APPZ.DAL
{
    public class APPZDBContext: DbContext
    {
        public APPZDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
