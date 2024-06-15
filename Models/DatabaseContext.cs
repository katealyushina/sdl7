using Microsoft.EntityFrameworkCore;

namespace sdl7.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Material> Materials { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public int ProjectId { get; set; }
    }
}
