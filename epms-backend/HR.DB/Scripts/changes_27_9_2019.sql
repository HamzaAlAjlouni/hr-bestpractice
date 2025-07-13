CREATE TABLE `adm_emp_competency` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `competency_id` INT NOT NULL,
  `competency_level_id` INT NULL,
  `weight` FLOAT(18,3) NULL,
  `result_without_round` FLOAT(18,3) NULL,
  `target` FLOAT(18,3) NOT NULL,
  `result_after_round` FLOAT(18,3) NULL,
  `emp_assessment_id` INT NOT NULL,
  `created_by` VARCHAR(45) NOT NULL,
  `created_date` DATE NOT NULL,
  `modified_by` VARCHAR(45) NULL,
  `modified_date` DATE NULL,
  PRIMARY KEY (`id`),
  INDEX `admin_emp_comp_adm_assessment_idx` (`emp_assessment_id` ASC) VISIBLE,
  INDEX `adm_emp_comp_adm_comp_idx` (`competency_id` ASC) VISIBLE,
  CONSTRAINT `adm_emp_comp_adm_comp`
    FOREIGN KEY (`competency_id`)
    REFERENCES `adm_competencies` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `admin_emp_comp_adm_assessment`
    FOREIGN KEY (`emp_assessment_id`)
    REFERENCES `adm_emp_assesment` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;
    
    
    
    /*                   *****    */
    
    
    CREATE TABLE `adm_emp_competency_kpi` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `emp_competency_id` INT NOT NULL,
  `competency_kpi_id` INT NOT NULL,
  `created_by` VARCHAR(45) NULL,
  `created_date` DATE NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `adm_emp_comp_kpi_adm_comp_id_idx` (`emp_competency_id` ASC) VISIBLE,
  INDEX `adm_emp_comp_kpi_adm_comp_kpi_idx` (`competency_kpi_id` ASC) VISIBLE,
  CONSTRAINT `adm_emp_comp_kpi_adm_comp`
    FOREIGN KEY (`emp_competency_id`)
    REFERENCES `adm_emp_competency` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `adm_emp_comp_kpi_adm_comp_kpi`
    FOREIGN KEY (`competency_kpi_id`)
    REFERENCES `competencies_kpi` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;
    
    /*************/
    
    /*******************/
    
    CREATE TABLE `adm_emp_comp_kpi_ass` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `period_no` INT NOT NULL,
  `emp_competency_kpi_id` INT NOT NULL,
  `result` FLOAT(18,3) NULL,
  `created_by` VARCHAR(45) NOT NULL,
  `created_date` DATE NOT NULL,
  `modified_by` VARCHAR(45) NULL,
  `modified_date` DATE NULL,
  `target` FLOAT(18,3) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `adm_emp_comp_kpi_ass_adm_emp_competency_kpi_idx` (`emp_competency_kpi_id` ASC) VISIBLE,
  CONSTRAINT `adm_emp_comp_kpi_ass_adm_emp_competency_kpi`
    FOREIGN KEY (`emp_competency_kpi_id`)
    REFERENCES `adm_emp_competency_kpi` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

/*******************/

CREATE TABLE `adm_emp_comp_ass` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `period_no` INT NOT NULL,
  `emp_competency_id` INT NOT NULL,
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
  INDEX `adm_emp_comp_ass_adm_emp_competency_idx` (`emp_competency_id` ASC) VISIBLE,
  CONSTRAINT `adm_emp_comp_ass_adm_emp_competency`
    FOREIGN KEY (`emp_competency_id`)
    REFERENCES `adm_emp_competency` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

/************************/


