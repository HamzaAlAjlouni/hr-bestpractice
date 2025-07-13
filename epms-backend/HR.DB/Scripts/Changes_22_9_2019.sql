/* Result Claculation per project */
ALTER TABLE `adm_projects` 
ADD COLUMN `Weight_From_Objective` FLOAT(12,3) NULL,
ADD COLUMN `Result` FLOAT(12,3) NULL ,
ADD COLUMN `Result_Percentage` FLOAT(12,3) NULL ,
ADD COLUMN `Result_Weight_Percentage` FLOAT(12,3) NULL ,
ADD COLUMN `Result_Weight_Perc_From_Obj` FLOAT(12,3) NULL;


/*  objective Result */
ALTER TABLE `adm_stratigic_objectives` 
ADD COLUMN `Result_Percentage` FLOAT(12,3) NULL AFTER `name2`,
ADD COLUMN `Result_Weight_Percentage` FLOAT(12,3) NULL AFTER `Result_Percentage`;

/* Year Performance */

CREATE TABLE `adm_company_obj_performance` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `year_id` INT NOT NULL,
  `company_id` INT NOT NULL,
  `total_employee` INT NULL,
  `Level1_Employee` FLOAT(12,3) NULL,
  `Level2_Employee` FLOAT(12,3) NULL,
  `Level3_Employee` FLOAT(12,3) NULL,
  `Level4_Employee` FLOAT(12,3) NULL,
  `Level5_Employee` FLOAT(12,3) NULL,
  `Level1_Result_Employee` FLOAT(12,3) NULL,
  `Level2_Result_Employee` FLOAT(12,3) NULL,
  `Level3_Result_Employee` FLOAT(12,3) NULL,
  `Level4_Result_Employee` FLOAT(12,3) NULL,
  `Level5_Result_Employee` FLOAT(12,3) NULL,
  `Result_Percentage` FLOAT(12,3) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

CREATE TABLE `adm_unit_projects_performance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `year_id` int(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  `branch_id` int(11) NOT NULL,
  `unit_id` int(11) NOT NULL,
  `total_employee` int(11) DEFAULT NULL,
  `Level1_Employee` float(12,3) DEFAULT NULL,
  `Level2_Employee` float(12,3) DEFAULT NULL,
  `Level3_Employee` float(12,3) DEFAULT NULL,
  `Level4_Employee` float(12,3) DEFAULT NULL,
  `Level5_Employee` float(12,3) DEFAULT NULL,
  `Level1_Result_Employee` float(12,3) DEFAULT NULL,
  `Level2_Result_Employee` float(12,3) DEFAULT NULL,
  `Level3_Result_Employee` float(12,3) DEFAULT NULL,
  `Level4_Result_Employee` float(12,3) DEFAULT NULL,
  `Level5_Result_Employee` float(12,3) DEFAULT NULL,
  `Result_Percentage` float(12,3) DEFAULT NULL,
  `Projects_Weight_Perc_From_Objs` float(12,3) DEFAULT NULL,
  `Result_Weight_Perc_From_Objs` float(12,3) DEFAULT NULL,
  `Employee_Percentage` float(12,3) DEFAULT NULL,
  `Prjs_Level1_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level2_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level3_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level4_Employee` float(12,3) DEFAULT NULL,
  `Prjs_Level5_Employee` float(12,3) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


