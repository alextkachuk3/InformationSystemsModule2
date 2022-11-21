using JobService.Data;
using JobService.Models;
using Microsoft.EntityFrameworkCore;

namespace JobService.Services.VacancyService
{
    public class VacancyService : IVacancyService
    {
        private readonly ApplicationDbContext _dbContext;

        public VacancyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddVacancy(string username, string title, int? settlementId, int salary, string description, List<int>? hardSkills)
        {
            User? user = _dbContext.Users!.FirstOrDefault(u => u.Username == username);
            if (user is null)
                throw new InvalidOperationException("User with username: " + username + " not exists.");
            try
            {
                var jobVacancy = new JobVacancy(user, title, salary, description);
                if (settlementId is not null)
                {
                    jobVacancy.Settlement = _dbContext.Settlements!.Where(s => s.Id.Equals(settlementId)).FirstOrDefault();
                }
                if(hardSkills is not null)
                {
                    jobVacancy.HardSkills = new List<HardSkill>(_dbContext.HardSkills!.Where(v => hardSkills.Contains(v.Id)));
                }
                _dbContext.JobVacancies!.Add(jobVacancy);
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }

        public void DeleteVacancy(int vacancyId, string username)
        {
            User? user = _dbContext.Users!.FirstOrDefault(u => u.Username == username);
            var jobVacancy = _dbContext.JobVacancies!.Where(v => v.Id.Equals(vacancyId)).FirstOrDefault();
            if (user is null)
                throw new InvalidOperationException("User with username: " + username + " not exists.");
            else if (jobVacancy == null)
                throw new InvalidOperationException("Job vacancy with id: " + vacancyId + " not exists.");
            else if (jobVacancy.User != user)
                throw new InvalidOperationException("User with username: " + username + " is not owner of vacancy with id " + vacancyId + ".");
            try
            {
                _dbContext.JobVacancies!.Remove(jobVacancy);
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }

        public void EditVacancy(int vacancyId, string username, string title, int? settlementId, int salary, string description, List<int>? hardSkills)
        {
            User? user = _dbContext.Users!.FirstOrDefault(u => u.Username == username);
            JobVacancy? jobVacancy = _dbContext.JobVacancies!.Where(v => v.Id == vacancyId).Include(v => v.HardSkills).Include(v => v.Settlement).FirstOrDefault();
            if (user == null)
                throw new InvalidOperationException("User with username: " + username + " not exists.");
            if (jobVacancy == null)
                throw new InvalidOperationException("Vacancy with id: " + vacancyId + " not exists.");
            try
            {
                if (settlementId != null)
                {
                    jobVacancy.Settlement = _dbContext.Settlements!.Where(s => s.Id.Equals(settlementId)).FirstOrDefault();
                }
                else
                {
                    jobVacancy.Settlement = null;
                }

                jobVacancy.Title = title;
                jobVacancy.Salary = salary;
                if(description is not null)
                {
                    jobVacancy.Description = description;
                }
                else
                {
                    jobVacancy.Description = string.Empty;
                }
                jobVacancy.TitleLowerCase = title.ToLower();
                jobVacancy.DescriptionLowerCase = jobVacancy.Description.ToLower();

                if (jobVacancy.HardSkills is not null)
                {
                    jobVacancy.HardSkills.Clear();
                }

                if (hardSkills is not null)
                {
                    jobVacancy.HardSkills = new List<HardSkill>(_dbContext.HardSkills!.Where(v => hardSkills.Contains(v.Id)));
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }

        public JobVacancy? GetVacancy(int vacancyId)
        {
            return _dbContext.JobVacancies!.Where(v => v.Id.Equals(vacancyId)).Include(v => v.HardSkills).Include(v => v.Settlement).ThenInclude(v => v!.Region).FirstOrDefault();
        }

        public List<JobVacancy> SearchJobVacancies(string? searchInput, int? settlementId)
        {
            if (searchInput != null)
            {
                if (settlementId == null)
                {
                    return _dbContext.JobVacancies!.OrderBy(v => v.CreationTime).Where(v => v.TitleLowerCase!.Contains(searchInput) || v.DescriptionLowerCase!.Contains(searchInput)).ToList();
                }
                else
                {
                    return _dbContext.JobVacancies!.Include(v => v.Settlement).Where(v => v.Settlement!.Id.Equals(settlementId) && (v.TitleLowerCase!.Contains(searchInput) || v.DescriptionLowerCase!.Contains(searchInput))).ToList();
                }
            }
            else
            {
                if (settlementId == null)
                {
                    throw new InvalidOperationException("No search params!");
                }
                else
                {
                    return _dbContext.JobVacancies!.Include(v => v.Settlement).Where(v => v.Settlement!.Id.Equals(settlementId)).ToList();
                }
            }

        }

        public List<JobVacancy> UserJobVacancies(string username)
        {
            User? user = _dbContext.Users!.FirstOrDefault(u => u.Username == username);
            if (user is null)
                throw new InvalidOperationException("User with username: " + username + " not exists.");
            return _dbContext.JobVacancies!.Where(v => v.User!.Equals(user)).ToList();
        }
    }
}
