DELIMITER $$

CREATE TRIGGER jobservice.success_indicator_after_update_jobvacancies AFTER UPDATE ON jobvacancies
FOR EACH ROW
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE current_hardskillid INTEGER;   
    
	DECLARE hardskillsids_cursor CURSOR FOR 
		SELECT HardSkillsId
		FROM hardskilljobvacancy
		INNER JOIN jobvacancies
		ON hardskilljobvacancy.jobVacanciesid = jobvacancies.id
		WHERE jobVacancies.id = NEW.id;
        
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
            
	IF NEW.opened = 0 THEN
		OPEN hardskillsids_cursor;
    
		read_loop: LOOP
			FETCH hardskillsids_cursor INTO current_hardskillid;
			IF done THEN
				LEAVE read_loop;
			END IF;
        
			INSERT IGNORE INTO hardskillsuccessvacationsindicators
			(hardskillsuccessvacationsindicators.HardSkillId)
			VALUES
			(current_hardskillid);
            
            UPDATE hardskillsuccessvacationsindicators
				SET 
					hardskillsuccessvacationsindicators.ClosedVacationsCount = hardskillsuccessvacationsindicators.ClosedVacationsCount + 1,
					hardskillsuccessvacationsindicators.SuccessVacationsCount = hardskillsuccessvacationsindicators.SuccessVacationsCount + NEW.Success,
                    hardskillsuccessvacationsindicators.SuccessPercent = (hardskillsuccessvacationsindicators.SuccessVacationsCount / hardskillsuccessvacationsindicators.ClosedVacationsCount) * 100
				WHERE hardskillsuccessvacationsindicators.HardSkillId = current_hardskillid;
        
		END LOOP;
    
		CLOSE hardskillsids_cursor;
    
    END IF;

END$$

DELIMITER ;
