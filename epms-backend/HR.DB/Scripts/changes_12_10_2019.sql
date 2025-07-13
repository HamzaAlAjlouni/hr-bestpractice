CREATE TABLE `adm_project_evidence` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `file_name` VARCHAR(150) NOT NULL,
  `file_url` VARCHAR(500) NOT NULL,
  `file_type` VARCHAR(45) NOT NULL,
  `project_id` INT NOT NULL,
  `created_by` VARCHAR(45) NOT NULL,
  `created_date` DATE NOT NULL,
  `modified_by` VARCHAR(45) NULL,
  `modified_date` DATE NULL,
  PRIMARY KEY (`id`),
  INDEX `adm_evid_adm_project_fk_idx` (`project_id` ASC),
  CONSTRAINT `adm_evid_adm_project_fk`
    FOREIGN KEY (`project_id`)
    REFERENCES `adm_projects` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


ALTER TABLE `adm_projects` 
ADD COLUMN `planned_cost` FLOAT(12,3) NULL ;

ALTER TABLE `adm_unit_projects_performance` 
ADD COLUMN `planned_cost` FLOAT(12,3) NULL ;

ALTER TABLE `adm_stratigic_objectives` 
ADD COLUMN `planned_cost` FLOAT(12,3) NULL ;

ALTER TABLE `adm_company_obj_performance` 
ADD COLUMN `planned_cost` FLOAT(12,3) NULL ;


ALTER TABLE `adm_projects` 
CHANGE COLUMN `name2` `name2` VARCHAR(1000) NULL DEFAULT NULL ;

