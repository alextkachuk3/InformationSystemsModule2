using JobService.Models;

namespace JobService.Services.IndicatorsService
{
    public interface IIndicatorsService
    {
        public List<HardSkillSalaryIndicator?> GetHardSkillSalaryIndicators(int hardSkillId, int monthCount);
    }
}
