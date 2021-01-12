using Microsoft.EntityFrameworkCore;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class TravelListContext : DbContext
    {

        public TravelListContext(DbContextOptions<TravelListContext> opt) : base(opt)
        {

        }
        public DbSet<TravelListItem> TravelLists { get; set; }
        public DbSet<TravelCheckListItem> Items { get; set; }
        public DbSet<TravelTaskListItem> Tasks { get; set; }
        public DbSet<TravelPointOfInterest> Points { get; set; }
        public DbSet<TravelListItemImage> TravelListImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TravelRoute> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TravelPointOfInterest>()
            //.HasMany(p => p.ConnectedStartRoutes)
            //.WithOne(r => r.Start)
            //.OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<TravelPointOfInterest>()
            //.HasMany(p => p.ConnectedEndRoutes)
            //.WithOne(r => r.End)
            //.OnDelete(DeleteBehavior.SetNull);
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DotNetEnv.Env.Load();

                string TravelListConnection =
                "Server=" + System.Environment.GetEnvironmentVariable("TRAVEL_LIST_SERVER") +
                ";Initial Catalog=" + System.Environment.GetEnvironmentVariable("TRAVEL_LIST_INITIAL_CATALOG") +
                ";User ID=" + System.Environment.GetEnvironmentVariable("TRAVEL_LIST_USER_ID") +
                ";Password=" + System.Environment.GetEnvironmentVariable("TRAVEL_LIST_PASSWORD") +
                ";";

                optionsBuilder.UseSqlServer(TravelListConnection);
            }

        }
    }
}
