DELIMITER $$

CREATE TRIGGER jobservice.suitablejobseeker_on_jobvacancy_insert AFTER INSERT ON jobvacancies
FOR EACH ROW
BEGIN

	DECLARE done INT DEFAULT FALSE;
	DECLARE current_userid INTEGER;

	DECLARE usersids_cursor CURSOR FOR
		SELECT DISTINCT UsersId
		FROM hardskilluser
		INNER JOIN users
		ON hardskilluser.usersid = users.Id
		WHERE hardskilluser.HardSkillsId IN (
			SELECT hardskilljobvacancy.hardskillsid
			FROM hardskilljobvacancy
			WHERE hardskilljobvacancy.JobVacanciesId = NEW.Id
			);	

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
    OPEN usersids_cursor;
    
    read_loop: LOOP
		FETCH usersids_cursor INTO current_userid;
        
        IF done THEN
			LEAVE read_loop;
		END IF;
    
		INSERT IGNORE INTO suitablejobseekers
        (JobVacancyId, UserId)
        VALUES
        (NEW.Id, current_userid);		
		
    END LOOP;
    
    CLOSE usersids_cursor;

END$$

DELIMITER ;
