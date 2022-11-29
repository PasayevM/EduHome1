using EduHome2.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHome2.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {
            
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Bio> Bios { get; set; }
    }
}
