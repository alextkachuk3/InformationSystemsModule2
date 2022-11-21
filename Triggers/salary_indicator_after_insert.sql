DELIMITER $$

CREATE TRIGGER jobservice.salary_indicator_after_insert AFTER INSERT ON jobservice.hardskilljobvacancy
FOR EACH ROW
BEGIN
	SET
	@vacancy_salary = (SELECT Salary FROM jobvacancies WHERE jobvacancies.Id = NEW.JobVacanciesId LIMIT 1);
	SET
	@vacancy_datetime = (SELECT creationTime FROM jobvacancies WHERE jobvacancies.Id = NEW.JobVacanciesId LIMIT 1);
	IF @vacancy_salary > 0 AND @vacancy_salary < 30000 THEN
		INSERT IGNORE INTO jobservice.hardskillsalaryindicators
		(hardskillsalaryindicators.HardSkillId, hardskillsalaryindicators.Year, hardskillsalaryindicators.Month, hardskillsalaryindicators.VacationsCount, hardskillsalaryindicators.SalarySum, hardskillsalaryindicators.SalaryAvg)
		VALUES
		(NEW.HardSkillsId, YEAR(@vacancy_datetime), MONTH(@vacancy_datetime), 0, 0, 0);
		UPDATE jobservice.hardskillsalaryindicators
		SET hardskillsalaryindicators.VacationsCount = hardskillsalaryindicators.VacationsCount + 1,
			hardskillsalaryindicators.SalarySum = hardskillsalaryindicators.SalarySum + @vacancy_salary,
			hardskillsalaryindicators.SalaryAvg = hardskillsalaryindicators.SalarySum / hardskillsalaryindicators.VacationsCount
		WHERE
		hardskillsalaryindicators.HardSkillId = NEW.HardSkillsId AND
		hardskillsalaryindicators.Year = YEAR(@vacancy_datetime) AND
		hardskillsalaryindicators.Month = MONTH(@vacancy_datetime);
	END IF;
END$$

DELIMITER ;
