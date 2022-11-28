DELIMITER $$

CREATE TRIGGER jobservice.salary_indicator_before_update_jobvacancies BEFORE UPDATE ON jobvacancies
FOR EACH ROW
BEGIN
	UPDATE hardskillsalaryindicators
    SET 
		hardskillsalaryindicators.SalarySum = (hardskillsalaryindicators.SalarySum - OLD.Salary + NEW.Salary),
		hardskillsalaryindicators.SalaryAvg = (hardskillsalaryindicators.SalarySum) / GREATEST(hardskillsalaryindicators.VacationsCount, 1)
	WHERE
		hardskillsalaryindicators.HardSkillId IN (SELECT HardSkillsId FROM hardskilljobvacancy WHERE hardskilljobvacancy.JobVacanciesId = OLD.ID) AND
		hardskillsalaryindicators.Year = YEAR(OLD.CreationTime) AND
		hardskillsalaryindicators.Month = MONTH(OLD.CreationTime);
END$$

DELIMITER ;
