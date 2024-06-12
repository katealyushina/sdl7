using System.ComponentModel.DataAnnotations;

namespace sdl7.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public string Class { get; set; }
        public int ProjectId { get; set; }
    }
}
