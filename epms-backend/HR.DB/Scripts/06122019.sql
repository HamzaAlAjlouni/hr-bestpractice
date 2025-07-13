ALTER TABLE `adm_emp_assesment` 
ADD COLUMN `status` INT(11) NOT NULL DEFAULT 0 ,
ADD COLUMN `isQuarter1` INT(11) NOT NULL DEFAULT 0 ,
ADD COLUMN `isQuarter2` INT(11) NOT NULL DEFAULT 0 ,
ADD COLUMN `isQuarter3` INT(11) NOT NULL DEFAULT 0 ,
ADD COLUMN `isQuarter4` INT(11) NOT NULL DEFAULT 0 ,
ADD COLUMN `emp_manager_id` INT(11) NULL ,
ADD COLUMN `final_date` DATETIME NULL ;

ALTER TABLE `adm_emp_competency` 
ADD COLUMN `project_desc` VARCHAR(500) NULL;


ALTER TABLE `adm_emp_objective` 
ADD COLUMN `project_desc` VARCHAR(500) NULL ,
ADD COLUMN `final_point_result` INT(11) NULL ;


INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `modified_by`, `name2`) VALUES ('11', '0', 'Planning', '0', '1', 'admin', '2019-11-02', '', 'التخطيط');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('11', '1', 'Review', '1', '1', 'admin', '2019-11-02', 'المراجعة');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('11', '2', 'Final Evaluation', '2', '1', 'admin', '2019-11-02', 'النهائية');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('11', '3', 'Completed', '3', '1', 'admin', '2019-11-02', 'اكتمال التقييم');


INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/employeeObjectve', 'lblQ1Target', 'Q1 Target', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/employeeObjectve', 'lblQ1Target', 'المستهدف للربع 1', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/employeeObjectve', 'lblQ2Target', 'Q2 Target', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/employeeObjectve', 'lblQ2Target', 'المستهدف للربع 2', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/employeeObjectve', 'lblQ3Target', 'Q3 Target', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/employeeObjectve', 'lblQ3Target', 'المستهدف للربع 3', '1', 'ar');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/employeeObjectve', 'lblQ4Target', 'Q4 Target', '1', 'en');
INSERT INTO `tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/employeeObjectve', 'lblQ4Target', 'المستهدف للربع 4', '1', 'ar');
