CREATE TABLE `adm_employee_education` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `year_id` INT NOT NULL,
  `employee_id` INT NOT NULL,
  `company_id` INT NOT NULL,
  `field` VARCHAR(500) NOT NULL,
  `field2` VARCHAR(500) NULL,
  `type` INT NOT NULL,
  `method` INT NOT NULL,
  `priority` INT NOT NULL,
  `execution_period` INT NOT NULL,
  `status` INT NOT NULL,
  `notes` VARCHAR(500) NULL,
  `emp_competency_id` INT NULL,
  `emp_obj_id` INT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


ALTER TABLE `adm_employee_education` 
ADD COLUMN `created_by` VARCHAR(45) NOT NULL AFTER `emp_obj_id`,
ADD COLUMN `created_date` DATETIME NOT NULL AFTER `created_by`,
ADD COLUMN `modified_by` VARCHAR(45) NULL AFTER `created_date`,
ADD COLUMN `modified_date` DATETIME NULL AFTER `modified_by`,
CHANGE COLUMN `field2` `field2` VARCHAR(500) CHARACTER SET 'binary' NOT NULL ;


INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('12', '0', 'None', '0', '1', 'admin', '2019-11-02', 'غير محدد');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('12', '1', 'Preventive', '1', '1', 'admin', '2019-11-02', 'وقائي');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('12', '2', 'Corrective', '1', '1', 'admin', '2019-11-02', 'تصحيحي');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('13', '1', 'None', '1', '1', 'admin', '2019-11-02', 'غير محدد');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('13', '2', 'Conference', '2', '1', 'admin', '2019-11-02', 'مؤتمر');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('13', '3', 'Workshop', '3', '1', 'admin', '2019-11-02', 'ورشة عمل');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('13', '4', 'Seminar', '4', '1', 'admin', '2019-11-02', 'ندوة');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('14', '0', 'None', '0', '1', 'admin', '2019-11-02', 'غير محدد');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('14', '1', 'Low', '1', '1', 'admin', '2019-11-02', 'منخفض');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('14', '2', 'Normal', '2', '1', 'admin', '2019-11-02', 'عادي');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('14', '3', 'High', '3', '1', 'admin', '2019-11-02', 'مرتفع');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('15', '1', 'Not Executed', '1', '1', 'admin', '2019-11-02', 'لم ينفذ');
INSERT INTO `adm_codes` (`MAJOR_NO`, `MINOR_NO`, `NAME`, `CODE`, `company_id`, `created_by`, `created_date`, `name2`) VALUES ('15', '2', 'Executed', '2', '1', 'admin', '2019-11-02', 'تم التنفيذ');

INSERT INTO `adm_menus` (`NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('Employee Education', '#/employeeeducationplan', 'fa-circle-o', '12', '1', 'ADMIN', '2019-10-25', 'تطوير الموظف', '5', 'HRMS', 'HOBJ');
INSERT INTO `adm_authorization` (`MENU_ID`, `ROLE_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`) VALUES ('25', '1', 'ADMIN', '2019-10-25', 'ADMIN', '2019-10-25');


INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblEmployeeEducation', 'Employee Education', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblEmployeeEducation', 'تطوير الموظف', '1', 'ar');

INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblSearch', 'Search', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblSearch', 'بحث', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblYear', 'Year', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblYear', 'السنة', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblEmployee', 'Employee', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblEmployee', 'الموظف', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'All', 'All', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'All', 'الكل', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'PleaseSelect', 'Please Select', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'PleaseSelect', 'الرجاء الاختيار', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblEducationField', 'Education Field', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblEducationField', 'مجال التطوير', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EmployeeObjective', 'Individual Objective', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EmployeeObjective', 'الهدف الفردي', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblDetails', 'Details', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblDetails', 'التفاصيل', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EmployeeCompetency', 'Competency', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EmployeeCompetency', 'الكفاءة', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EducationType', 'Type', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EducationType', 'التصنيف', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EducationMethod', 'Improvement Method', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EducationMethod', 'طريقة التطوير', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EducationPriority', 'Priority', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'EducationPriority', 'الأهمية', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'ExecutionPeriod', 'Execution Time', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'ExecutionPeriod', 'وقت التطوير', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'ExecutionStatus', 'Status', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'ExecutionStatus', 'الحالة', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblNotes', 'Notes', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblNotes', 'ملاحظات', '1', 'ar');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblSave', 'Save', '1', 'en');
INSERT INTO `hr_db4`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/EmployeeEducationPlan', 'lblSave', 'حفظ', '1', 'ar');
