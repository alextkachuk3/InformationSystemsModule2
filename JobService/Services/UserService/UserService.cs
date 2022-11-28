﻿using JobService.Data;
using JobService.Models;
using Microsoft.EntityFrameworkCore;

namespace JobService.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(string username, string password, string firstName, string lastName)
        {
            try
            {
                _dbContext.Users?.Add(new User(username, password, firstName, lastName));
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

        public User? GetUser(string username)
        {
            return _dbContext.Users?.Where(u => u.Username!.Equals(username)).Include(u => u.HardSkills).FirstOrDefault();
        }

        public User? GetUserDetailed(string username)
        {
            return _dbContext.Users?.Where(u => u.Username!.Equals(username)).Include(u => u.HardSkills).Include(u => u.Settlement).ThenInclude(u => u!.Region).FirstOrDefault();
        }

        public bool IsUserNameUsed(string username)
        {
            return _dbContext.Users?.Where(u => u.Username!.Equals(username)).Count() > 0;
        }

        public void UpdateProfile(string username, string firstName, string lastName, string? phoneNumber, string? email, int? settlementId, bool inSearch, List<int>? hardSkills)
        {
            try
            {
                var user = GetUserDetailed(username);

                if (user == null)
                {
                    throw new Exception("User with username:" + username + "not exists!");
                }

                user.FirstName = firstName;
                user.LastName = lastName;
                user.PhoneNumber = phoneNumber;
                user.Email = email;
                user.InSearch = inSearch;
                user.Settlement = _dbContext.Settlements!.Where(s => s.Id.Equals(settlementId)).FirstOrDefault();
                if (user.HardSkills is not null)
                {
                    user.HardSkills.Clear();
                }

                if (hardSkills is not null)
                {
                    user.HardSkills = new List<HardSkill>(_dbContext.HardSkills!.Where(s => hardSkills.Contains(s.Id)));

                    List<JobVacancy>? suitableJobs = _dbContext.JobVacancies!.Where(v => v.User!.InSearch.Equals(true) && v.HardSkills!.Any(l => hardSkills.Contains(l.Id))).ToList();

                    if (suitableJobs != null)
                    {
                        foreach (JobVacancy suitableJob in suitableJobs)
                        {
                            SuitableJobSeeker suitableJobSeeker = new SuitableJobSeeker();
                            suitableJobSeeker.User = user;
                            suitableJobSeeker.JobVacancy = suitableJob;
                            _dbContext.SuitableJobSeekers!.Add(suitableJobSeeker);
                        }
                    }
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
    }
}
