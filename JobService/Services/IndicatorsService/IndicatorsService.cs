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

        public List<HardSkillSalaryIndicatorDto?> GetHardSkillSalaryIndicators(int hardSkillId, int monthCount)
        {
            List<HardSkillSalaryIndicatorDto?> result = new List<HardSkillSalaryIndicatorDto?>();
            HardSkill? hardSkill = _dbContext.HardSkills!.FirstOrDefault(s => s.Id == hardSkillId);
            DateTime dateTime = DateTime.Now;

            for (int i = monthCount - 1; i >= 0; i--)
            {
                var ind = _dbContext.HardSkillSalaryIndicators!.FirstOrDefault(s => s.HardSkill!.Equals(hardSkill) &&
                                                                                s.Year.Equals(dateTime.AddMonths(-i).Year) &&
                                                                                s.Month.Equals(dateTime.AddMonths(-i).Month));
                if(ind is null)
                {
                    result.Add(new HardSkillSalaryIndicatorDto(dateTime.AddMonths(-i).Year, dateTime.AddMonths(-i).Month, null));
                }
                else
                {
                    result.Add(new HardSkillSalaryIndicatorDto(dateTime.AddMonths(-i).Year, dateTime.AddMonths(-i).Month, ind.SalaryAvg));
                }                
            }

            return result;
        }
    }
}
