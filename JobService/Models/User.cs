using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobService.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public User() { }

        public User(string username, string password, string firstName, string lastName)
        {
            Username = username;
            PasswordHash = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public bool CheckCredentials(string password)
        {
            return PasswordHash!.Equals(password);
        }

        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 4)]
        public string? Username { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string? LastName { get; set; }

        public Settlement? Settlement { get; set; }

        public List<HardSkill>? HardSkills { get; set; }

        public List<JobInfo>? JobHistory { get; set; }

        public List<JobVacancy>? JobVacancies { get; set; }
    }
}
