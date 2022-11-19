using System.ComponentModel.DataAnnotations;

namespace JobService.Models
{
    public class UserDto
    {
        public UserDto(string? username, string? firstName, string? lastName, string? phoneNumber, string? email, bool inSearch, Settlement? settlement, List<HardSkill>? hardSkills)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            InSearch = inSearch;
            Settlement = settlement;
            HardSkills = hardSkills;
        }

        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public bool InSearch { get; set; }

        public Settlement? Settlement { get; set; }

        public List<HardSkill>? HardSkills { get; set; }
    }
}
