ALTER TABLE `hr_db`.`adm_emp_comp_ass` 
ADD COLUMN `weight_result_without_round` FLOAT(18,3) NULL AFTER `target`,
ADD COLUMN `weight_result_after_round` FLOAT(18,3) NULL AFTER `weight_result_without_round`;
