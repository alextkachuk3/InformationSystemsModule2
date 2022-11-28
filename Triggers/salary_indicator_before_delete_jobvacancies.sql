DELIMITER $$

CREATE TRIGGER jobservice.salary_indicator_before_delete_jobvacancies BEFORE DELETE ON jobvacancies
FOR EACH ROW
BEGIN
	UPDATE hardskillsalaryindicators
    SET 
		hardskillsalaryindicators.VacationsCount = hardskillsalaryindicators.VacationsCount - 1,
		hardskillsalaryindicators.SalarySum = hardskillsalaryindicators.SalarySum - OLD.Salary,
		hardskillsalaryindicators.SalaryAvg = (hardskillsalaryindicators.SalarySum) / GREATEST(hardskillsalaryindicators.VacationsCount, 1)
	WHERE
		hardskillsalaryindicators.HardSkillId IN (SELECT HardSkillsId FROM hardskilljobvacancy WHERE hardskilljobvacancy.JobVacanciesId = OLD.ID) AND
		hardskillsalaryindicators.Year = YEAR(OLD.CreationTime) AND
		hardskillsalaryindicators.Month = MONTH(OLD.CreationTime);
END$$

DELIMITER ;
