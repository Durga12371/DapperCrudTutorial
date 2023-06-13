using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class SuperHeroViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string FirstName { get; set; } = String.Empty;
        [Required]
        public string LastName { get; set; } = String.Empty;
        [Required]
        public string Place { get; set; } = String.Empty;

    }
}
