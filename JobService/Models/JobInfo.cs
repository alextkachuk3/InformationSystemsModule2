using System.ComponentModel.DataAnnotations;

namespace JobService.Models
{
    public class JobInfo
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public bool CurrentJob { get; set; }
    }
}
