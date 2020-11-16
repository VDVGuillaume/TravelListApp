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
        public DbSet<CheckListItem> Items { get; set; }
        public DbSet<TravelPointOfInterest> Points { get; set; }
        public DbSet<TravelListItemImage> TravelListImages { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TravelPointOfInterest>()
        //        .HasOne(p => p.TravelListItem)
        //        .WithMany(b => b.Points)
        //        .HasForeignKey(p => p.TravelListItemID);
        //}

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
