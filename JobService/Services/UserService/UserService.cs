using JobService.Data;
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
            return _dbContext.Users?.FirstOrDefault(u => u.Username!.Equals(username));
        }

        public User? GetUserDetailed(string username)
        {
            return _dbContext.Users?.Where(u => u.Username!.Equals(username)).Include(u => u.HardSkills).Include(u => u.Settlement).ThenInclude(u => u!.Region).FirstOrDefault();
        }

        public bool IsUserNameUsed(string username)
        {
            return _dbContext.Users?.Where(u => u.Username!.Equals(username)).Count() > 0;
        }

        public void UpdateProfile(string username, string firstName, string lastName, string? phoneNumber, string? email, int? settlementId, List<int>? hardSkills)
        {
            try
            {
                var user = GetUser(username);

                if(user == null)
                {
                    throw new Exception("User with username:" + username + "not exists!");
                }

                user.FirstName = firstName;
                user.LastName = lastName;
                user.PhoneNumber = phoneNumber;
                user.Email = email;
                user.Settlement = _dbContext.Settlements!.Where(s => s.Id.Equals(settlementId)).FirstOrDefault();
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
