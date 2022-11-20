using Microsoft.EntityFrameworkCore;

namespace JobService.Models
{
    [Keyless]
    public class HardSkillSuccessVacationsIndicator
    {
        public HardSkill? HardSkill { get; set; }

        public int VacationsCount { get; set; }

        public int SuccessVacationsCount { get; set; }

        public int SuccessPercent { get; set; }
    }
}
