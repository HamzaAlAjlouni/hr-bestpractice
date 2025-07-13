ALTER TABLE `adm_emp_objective` 
ADD COLUMN `target` FLOAT(18,3) NULL AFTER `pos_desc_id`,
ADD COLUMN `result_without_round` FLOAT(18,3) NULL AFTER `target`,
ADD COLUMN `result_after_round` FLOAT(18,3) NULL AFTER `result_without_round`;


/************************/

 CREATE TABLE `adm_emp_obj_kpi_ass` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `period_no` INT NOT NULL,
  `emp_obj_kpi_id` INT NOT NULL,
  `result` FLOAT(18,3) NULL,
  `created_by` VARCHAR(45) NOT NULL,
  `created_date` DATE NOT NULL,
  `modified_by` VARCHAR(45) NULL,
  `modified_date` DATE NULL,
  `target` FLOAT(18,3) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `adm_emp_obj_kpi_ass_adm_emp_obj_kpi_idx` (`emp_obj_kpi_id` ASC) VISIBLE,
  CONSTRAINT `adm_emp_obj_kpi_ass_adm_emp_obj_kpi`
    FOREIGN KEY (`emp_obj_kpi_id`)
    REFERENCES `adm_emp_obj_kpi` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

/*******************/

CREATE TABLE `adm_emp_obj_ass` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `period_no` INT NOT NULL,
  `emp_objective_id` INT NOT NULL,
  `result_before_round` FLOAT(18,3) NULL,
  `result_after_round` FLOAT(18,3) NULL,
  `created_by` VARCHAR(45) NOT NULL,
  `created_date` DATE NOT NULL,
  `modified_by` VARCHAR(45) NULL,
  `modified_date` DATE NULL,
  `target` FLOAT(18,3) NOT NULL,
  `weight_result_without_round` FLOAT(18,3) NULL,
  `weight_result_after_round` FLOAT(18,3) NULL,
  PRIMARY KEY (`id`),
  INDEX `adm_emp_obj_ass_adm_emp_obj_idx` (`emp_objective_id` ASC) VISIBLE,
  CONSTRAINT `adm_emp_obj_ass_adm_emp_objective`
    FOREIGN KEY (`emp_objective_id`)
    REFERENCES `adm_emp_objective` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

/************************/


ALTER TABLE `adm_emp_assesment` 
ADD COLUMN `target` FLOAT(18,3) NOT NULL AFTER `emp_position_id`,
ADD COLUMN `objectives_weight` FLOAT(18,3) NULL AFTER `target`,
ADD COLUMN `competencies_weight` FLOAT(18,3) NULL AFTER `objectives_weight`,
ADD COLUMN `objectives_result` FLOAT(18,3) NULL AFTER `competencies_weight`,
ADD COLUMN `competencies_result` FLOAT(18,3) NULL AFTER `objectives_result`,
ADD COLUMN `objectives_weight_result` FLOAT(18,3) NULL AFTER `competencies_result`,
ADD COLUMN `competencies_weight_result` FLOAT(18,3) NULL AFTER `objectives_weight_result`,
ADD COLUMN `result_before_round` FLOAT(18,3) NULL AFTER `competencies_weight_result`,
ADD COLUMN `result_after_round` FLOAT(18,3) NULL AFTER `result_before_round`;


ALTER TABLE `adm_emp_assesment` 
ADD COLUMN `objectives_result_after_round` FLOAT(18,3) NULL ,
ADD COLUMN `competencies_result_after_round` FLOAT(18,3) NULL ,
ADD COLUMN `objectives_weight_result_after_round` FLOAT(18,3) NULL ,
ADD COLUMN `competencies_weight_result_after_round` FLOAT(18,3) NULL ;
