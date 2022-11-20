namespace JobService.Models
{
    public class SuitableJobSeeker
    {
        public int Id { get; set; }

        public JobVacancy? JobVacancy { get; set; }

        public User? User { get; set; }
    }
}
