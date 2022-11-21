DELIMITER $$

CREATE TRIGGER jobservice.salary_indicator_before_delete_jobvacancies BEFORE DELETE ON jobvacancies
FOR EACH ROW
BEGIN
	UPDATE hardskillsalaryindicators
    SET hardskillsalaryindicators.VacationsCount = hardskillsalaryindicators.VacationsCount - 1,
			hardskillsalaryindicators.SalarySum = hardskillsalaryindicators.SalarySum - OLD.Salary,
			hardskillsalaryindicators.SalaryAvg = hardskillsalaryindicators.SalarySum / hardskillsalaryindicators.VacationsCount
	WHERE
	hardskillsalaryindicators.HardSkillId IN (SELECT HardSkillsId FROM hardskilljobvacancy WHERE hardskilljobvacancy.JobVacanciesId = OLD.ID) AND
	hardskillsalaryindicators.Year = YEAR(OLD.creationTime) AND
	hardskillsalaryindicators.Month = MONTH(OLD.creationTime);
END$$

DELIMITER ;
