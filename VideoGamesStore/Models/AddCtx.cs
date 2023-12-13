using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoGamesStore.Models.Data;

namespace VideoGamesStore.Models
{
    public class AppCtx : IdentityDbContext<User>
    {
        public AppCtx(DbContextOptions<AppCtx> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<VideoGamesStore.Models.Data.Order> Order { get; set; } = default!;

        public DbSet<VideoGamesStore.Models.Data.Review> Review { get; set; } = default!;
    }
}