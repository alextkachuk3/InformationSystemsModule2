using JobService.Models;

namespace JobService.Services.IndicatorsService
{
    public interface IIndicatorsService
    {
        public List<HardSkillSalaryIndicatorDto?> GetHardSkillSalaryIndicators(int hardSkillId, int monthCount);
    }
}
