using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobService.Models
{
    public class JobVacancy
    {
        public JobVacancy() { }

        public JobVacancy(User user, string title, int salary, string description)
        {
            User = user;
            Title = title;
            Salary = salary;
            Description = description;
            TitleLowerCase = title.ToLower();
            DescriptionLowerCase = description.ToLower();
            creationTime = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public User? User { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string? Title { get; set; }

        [Required]
        [StringLength(maximumLength: 3000)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(maximumLength: 100)]
        public string? TitleLowerCase { get; set; }
        
        [Required]
        [StringLength(maximumLength: 3000)]
        public string? DescriptionLowerCase { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public bool Remote { get; set; }

        public DateTime creationTime { get; set; }

        public Settlement? Settlement { get; set; }

        public List<HardSkill>? HardSkills { get; set; }
    }
}
