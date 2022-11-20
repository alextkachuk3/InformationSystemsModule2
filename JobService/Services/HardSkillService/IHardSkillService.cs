using JobService.Models;

namespace JobService.Services.HardSkillService
{
    public interface IHardSkillService
    {
        public List<HardSkill> GetHardSkills();

        public List<HardSkill> GetHardSkills(string searchWord);
    }
}
