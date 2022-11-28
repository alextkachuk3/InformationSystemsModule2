using Microsoft.EntityFrameworkCore;

namespace JobService.Models
{
    [Keyless]
    [Index("HardSkillId", IsUnique = true)]
    public class HardSkillSuccessVacationsIndicator
    {
        public HardSkill? HardSkill { get; set; }

        public int ClosedVacationsCount { get; set; }

        public int SuccessVacationsCount { get; set; }

        public int SuccessPercent { get; set; }
    }
}
