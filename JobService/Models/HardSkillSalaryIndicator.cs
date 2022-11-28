using Microsoft.EntityFrameworkCore;

namespace JobService.Models
{
    [Keyless]
    [Index(nameof(Year), nameof(Month), "HardSkillId", IsUnique = true)]
    public class HardSkillSalaryIndicator
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public HardSkill? HardSkill { get; set; }
        
        public int VacationsCount { get; set; }

        public int SalarySum { get; set; }

        public int SalaryAvg { get; set; }
    }
}
