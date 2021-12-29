using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class TravelContext : DbContext
    {
        public DbSet<Drzava> Drzave{get;set;}
        public DbSet<Pasos> Pasosi{get;set;}
        public DbSet<Test> Testovi{get;set;}
        public DbSet<Vakcina> Vakcine{get;set;}
       
        public TravelContext(DbContextOptions options) : base(options)
        {
            
        }

    }
}