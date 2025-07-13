ALTER TABLE `adm_emp_competency_kpi` 
ADD COLUMN `result` FLOAT(18,3) NULL AFTER `target`,
ADD COLUMN `note` VARCHAR(500) NULL AFTER `result`,
ADD COLUMN `result_pref_segment_id` INT(11) NULL AFTER `note`;


ALTER TABLE `hr1_db`.`adm_emp_obj_kpi` 
ADD COLUMN `result` FLOAT(18,3) NULL DEFAULT NULL AFTER `target`,
ADD COLUMN `note` VARCHAR(500) NULL DEFAULT NULL AFTER `result`,
ADD COLUMN `result_pref_segment_id` INT(11) NULL DEFAULT NULL AFTER `note`;
