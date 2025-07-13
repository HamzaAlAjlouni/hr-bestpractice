
CREATE TABLE `tbl_perf_level_quota` (
  `id` INT NOT NULL,
  `year_id` INT NOT NULL,
  `from_percentage` FLOAT(12,3) NOT NULL,
  `to_percentage` FLOAT(12,3) NOT NULL,
  `lvl_number` INT NOT NULL,
  `quota_type` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `adm_level_quota_adm_year_idx` (`year_id` ASC) VISIBLE,
  CONSTRAINT `adm_level_quota_adm_year`
    FOREIGN KEY (`year_id`)
    REFERENCES `adm_years` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('9', '0', '', '1', 'ADMIN', '2019-01-10', '');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('9', '1', 'Planned Quota', '1', 'ADMIN', '2019-01-10', 'كوتا المخطط لها');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('9', '2', 'Remaning Employees', '1', 'ADMIN', '2019-01-10', 'باقي الموظفين');


ALTER TABLE .`tbl_perf_level_quota` 
ADD COLUMN `company_Id` INT NOT NULL AFTER `quota_type`;


ALTER TABLE `tbl_perf_level_quota` 
ADD INDEX `adm_level_quota_adm_company_idx` (`company_Id` ASC) VISIBLE;
;
ALTER TABLE `tbl_perf_level_quota` 
ADD CONSTRAINT `adm_level_quota_adm_company`
  FOREIGN KEY (`company_Id`)
  REFERENCES .`adm_company` (`ID`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  ALTER TABLE `hr_db2`.`tbl_performance_levels` 
ADD COLUMN `company_id` INT NOT NULL AFTER `lvl_year`;


UPDATE `tbl_performance_levels` SET `company_id` = '1' WHERE (`id` = '11');
UPDATE `tbl_performance_levels` SET `company_id` = '1' WHERE (`id` = '12');
UPDATE `tbl_performance_levels` SET `company_id` = '1' WHERE (`id` = '13');
UPDATE `tbl_performance_levels` SET `company_id` = '1' WHERE (`id` = '14');
UPDATE `tbl_performance_levels` SET `company_id` = '1' WHERE (`id` = '15');

ALTER TABLE `hr_db2`.`tbl_performance_levels` 
CHANGE COLUMN `lvl_year` `lvl_year` INT NOT NULL ,
ADD INDEX `adm_perf_level_adm_company_idx` (`company_id` ASC) VISIBLE,
ADD INDEX `adm_perf_level_adm_year_idx` (`lvl_year` ASC) VISIBLE;
;
ALTER TABLE `hr_db2`.`tbl_performance_levels` 
ADD CONSTRAINT `adm_per_level_adm_year`
  FOREIGN KEY (`lvl_year`)
  REFERENCES `hr_db2`.`adm_years` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `adm_per_level_adm_company`
  FOREIGN KEY (`company_id`)
  REFERENCES `hr_db2`.`adm_company` (`ID`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `hr_db2`.`tbl_perf_level_quota` 
CHANGE COLUMN `id` `id` INT(11) NOT NULL AUTO_INCREMENT ;

