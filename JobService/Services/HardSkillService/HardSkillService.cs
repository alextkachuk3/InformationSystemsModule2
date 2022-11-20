using JobService.Data;
using JobService.Models;

namespace JobService.Services.HardSkillService
{
    public class HardSkillService : IHardSkillService
    {
        private readonly ApplicationDbContext _dbContext;

        public HardSkillService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<HardSkill> GetHardSkills()
        {
            return _dbContext.HardSkills!.ToList();
        }

        public List<HardSkill> GetHardSkills(string searchWord)
        {
            return _dbContext.HardSkills!.Where(s => s.Name!.Contains(searchWord)).ToList();
        }
    }
}
