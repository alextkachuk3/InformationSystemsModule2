using JobService.Models;
using Microsoft.EntityFrameworkCore;

namespace JobService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options) { }

        public DbSet<User>? Users { get; set; }

        public DbSet<Region>? Regions { get; set; }

        public DbSet<Settlement>? Settlements { get; set; }

        public DbSet<HardSkill>? HardSkills { get; set; }

        public DbSet<JobVacancy>? JobVacancies { get; set; }

        public DbSet<SuitableJobSeeker>? SuitableJobSeekers { get; set; }

        public DbSet<HardSkillSalaryIndicator>? HardSkillSalaryIndicators { get; set; }

        public DbSet<HardSkillSuccessVacationsIndicator>? HardSkillSuccessVacationsIndicators { get; set; }

    }
}
