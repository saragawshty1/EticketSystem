using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MOVIES.Models;
using static System.Net.Mime.MediaTypeNames;
using MOVIES.Models.ViewModel;


namespace MOVIES.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>

    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieCart> MovieCarts { get; set; }
        public DbSet<order> orders { get; set; }
        //public DbSet<ActorMovie> ActorMovie { get; set; }
         
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)

            : base(options)
        {
        }
        public ApplicationDbContext()

        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder = new ConfigurationBuilder().
                AddJsonFile("appsettings.json",true,reloadOnChange:true).Build();
            var Connection= builder.GetConnectionString("DefaultConnection");    
            optionsBuilder.UseSqlServer(Connection);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        ////    base.OnModelCreating(modelBuilder);
        ////    modelBuilder.Entity<ActorMovie>().HasKey(am => new {am.MoviesId,am.ActorsId});
        ////}
        public DbSet<MOVIES.Models.ViewModel.ApplicationUserVM> ApplicationUserVM { get; set; } = default!;

        public DbSet<MOVIES.Models.ViewModel.LoginVM> LoginVM { get; set; } = default!;

        public DbSet<MOVIES.Models.ViewModel.RoleVM> RoleVM { get; set; } = default!;
    }
}
