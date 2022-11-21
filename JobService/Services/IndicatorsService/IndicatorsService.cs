using JobService.Data;
using JobService.Models;

namespace JobService.Services.IndicatorsService
{
    public class IndicatorsService : IIndicatorsService
    {
        ApplicationDbContext _dbContext;

        public IndicatorsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<HardSkillSalaryIndicator?> GetHardSkillSalaryIndicators(int hardSkillId, int monthCount)
        {
            List<HardSkillSalaryIndicator?> result = new List<HardSkillSalaryIndicator?>();
            HardSkill? hardSkill = _dbContext.HardSkills!.FirstOrDefault(s => s.Id == hardSkillId);
            DateTime dateTime = DateTime.Now;

            for (int i = monthCount - 1; i >= 0; i--)
            {
                result.Add(_dbContext.HardSkillSalaryIndicators!.FirstOrDefault(s => s.HardSkill!.Equals(hardSkill) &&
                                                                                s.Year.Equals(dateTime.AddMonths(-i).Year) &&
                                                                                s.Month.Equals(dateTime.AddMonths(-i).Month)));
            }

            return result;
        }
    }
}
