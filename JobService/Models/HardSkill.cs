using System.ComponentModel.DataAnnotations;

namespace JobService.Models
{
    public class HardSkill
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public List<User>? Users { get; set; }
    }
}
