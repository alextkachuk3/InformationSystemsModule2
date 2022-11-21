namespace JobService.Models
{
    public class HardSkillSalaryIndicatorDto
    {
        public HardSkillSalaryIndicatorDto(int year, int month, int? salaryAvg)
        {
            Year = year;
            Month = month;
            SalaryAvg = salaryAvg;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int? SalaryAvg { get; set; }
    }
}
