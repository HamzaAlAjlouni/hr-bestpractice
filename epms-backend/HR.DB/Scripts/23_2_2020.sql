ALTER TABLE `adm_company` 
ADD COLUMN `objective_factor` DECIMAL(18,3) NOT NULL DEFAULT 0 AFTER `company_values`,
ADD COLUMN `competency_factor` DECIMAL(18,3) NOT NULL DEFAULT 0 AFTER `objective_factor`;

INSERT INTO `hr_demo`.`adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `company_id`, `created_by`, `created_date`, `modified_date`, `name2`) VALUES ('8', '3', 'Section', '1', 'ADMIN', '2019-11-09', '2019-11-09', 'Section');
INSERT INTO `hr_demo`.`adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `company_id`, `created_by`, `created_date`, `modified_date`, `name2`) VALUES ('8', '4', 'Department', '1', 'ADMIN', '2019-11-09', '2019-11-09', 'Department');
INSERT INTO `hr_demo`.`adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `company_id`, `created_by`, `created_date`, `modified_date`, `name2`) VALUES ('8', '5', 'Branch', '1', 'ADMIN', '2019-11-09', '2019-11-09', 'Branch');
INSERT INTO `hr_demo`.`adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `company_id`, `created_by`, `created_date`, `modified_date`, `name2`) VALUES ('8', '6', 'Office', '1', 'ADMIN', '2019-11-09', '2019-11-09', 'Office');
INSERT INTO `hr_demo`.`adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `company_id`, `created_by`, `created_date`, `modified_date`, `name2`) VALUES ('8', '7', 'Other', '1', 'ADMIN', '2019-11-09', '2019-11-09', 'Other');



UPDATE `hr_demo`.`adm_scales` SET `SCALE_CODE` = '1', `NAME` = '1', `name2` = '1' WHERE (`ID` = '2');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('2', '2', '2', '1', 'ADMIN', '2019-01-01', '2');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('3', '3', '3', '1', 'ADMIN', '2019-01-01', '3');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('4', '4', '4', '1', 'ADMIN', '2019-01-01', '4');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('5', '5', '5', '1', 'ADMIN', '2019-01-01', '5');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('6', '6', '6', '1', 'ADMIN', '2019-01-01', '6');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('7', '7', '7', '1', 'ADMIN', '2019-01-01', '7');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('8', '8', '8', '1', 'ADMIN', '2019-01-01', '8');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('9', '9', '9', '1', 'ADMIN', '2019-01-01', '9');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('10', '10', '10', '1', 'ADMIN', '2019-01-01', '10');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `modified_date`, `name2`) VALUES ('11', '11', '11', '1', 'ADMIN', '2019-01-01', NULL, '11');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('12', '12', '12', '1', 'ADMIN', '2019-01-01', '12');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('13', '13', '13', '1', 'ADMIN', '2019-01-01', '13');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('14', '14', '14', '1', 'ADMIN', '2019-01-01', '14');
INSERT INTO `hr_demo`.`adm_scales` (`SCALE_CODE`, `NAME`, `SCALE_NUMBER`, `COMPANY_ID`, `created_by`, `created_date`, `name2`) VALUES ('15', '15', '15', '1', 'ADMIN', '2019-01-01', '15');



UPDATE `hr_demo`.`tbl_resources` SET `resource_value` = 'Grade' WHERE (`id` = '622');
