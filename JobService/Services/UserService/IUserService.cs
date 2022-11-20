using JobService.Models;

namespace JobService.Services.UserService
{
    public interface IUserService
    {
        public bool IsUserNameUsed(string username);

        public void AddUser(string username, string password, string firstName, string lastName);

        public User? GetUser(string username);

        public User? GetUserDetailed(string username);

        void UpdateProfile(string username, string firstName, string lastName, string? phoneNumber, string? email, int? settlementId, bool inSearch, List<int>? hardSkills);
    }
}
