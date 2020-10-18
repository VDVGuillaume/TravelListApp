using Microsoft.EntityFrameworkCore;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class TravelListContext : DbContext
    {

        public TravelListContext(DbContextOptions<TravelListContext> opt) : base(opt)
        {

        }
        public DbSet<TravelList> TravelLists { get; set; }
        public DbSet<Item> Items { get; set; }

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
