using JobService.Data;
using JobService.Models;
using System.Linq;

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
                if (ind is null)
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

        public int GetHardSkillVacationsCount(int hardSkillId)
        {
            HardSkill? hardSkill = _dbContext.HardSkills!.Where(s => s.Id.Equals(hardSkillId)).FirstOrDefault();
            if (hardSkill == null)
            {
                return 0;
            }
            else
            {
                return _dbContext.JobVacancies!.Where(v => v.HardSkills!.Contains(hardSkill)).Count();
            }
        }

        public HardSkillSuccessVacationsIndicator GetHardSkillVacationsSuccessIndicators(int hardSkillId)
        {
            var ind = _dbContext.HardSkillSuccessVacationsIndicators!.Where(i => i.HardSkill!.Id.Equals(hardSkillId)).FirstOrDefault();
            if (ind == null)
            {
                ind = new HardSkillSuccessVacationsIndicator();
                ind.SuccessPercent = 0;
                ind.SuccessVacationsCount = 0;
                ind.ClosedVacationsCount = 0;
            }
            return ind;
        }
    }
}
