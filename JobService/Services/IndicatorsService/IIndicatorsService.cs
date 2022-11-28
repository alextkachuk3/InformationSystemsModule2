using JobService.Models;

namespace JobService.Services.IndicatorsService
{
    public interface IIndicatorsService
    {
        public List<HardSkillSalaryIndicatorDto?> GetHardSkillSalaryIndicators(int hardSkillId, int monthCount);

        public HardSkillSuccessVacationsIndicator GetHardSkillVacationsSuccessIndicators(int hardSkillId);

        public int GetHardSkillVacationsCount(int hardSkillId);
    }
}
