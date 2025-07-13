INSERT INTO `adm_users` (`USERNAME`, `NAME`, `COMPANY_ID`, `PASSWORD`, `created_by`, `created_date`) VALUES ('ADMIN', 'admin', '1', '123', 'ADMIN', '2019-02-02');

INSERT INTO `adm_roles` (`ID`, `NAME`, `COMPANY_ID`, `IS_SYSTEM_ROLE`, `created_by`, `created_date`) VALUES ('1', 'Administrator', '1', '1', 'ADMIN', '2019-01-01');

INSERT INTO `adm_users_roles` (`ID`, `USERNAME`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('1', 'ADMIN', '1', 'ADMIN', '2019-01-01');



ALTER TABLE `adm_menus` 

ADD COLUMN `order` INT(11) NOT NULL AFTER `name2`,

ADD COLUMN `application_code` VARCHAR(5) NOT NULL AFTER `order`,

ADD COLUMN `system_code` VARCHAR(5) NOT NULL AFTER `application_code`;


ALTER TABLE `adm_menus` 
DROP FOREIGN KEY `fk_adm_menus_adm_menus`;

ALTER TABLE `adm_menus` 
CHANGE COLUMN `PARENT_ID` `PARENT_ID` INT(11) NULL ;

ALTER TABLE `adm_menus` 
ADD CONSTRAINT `fk_adm_menus_adm_menus`
  FOREIGN KEY (`PARENT_ID`)
  REFERENCES `adm_menus` (`ID`);
ALTER TABLE `adm_menus` 
DROP FOREIGN KEY `fk_adm_menus_adm_menus`;
ALTER TABLE `adm_menus` 
DROP INDEX `idx_ADM_MENUS` ;
;
ALTER TABLE `adm_menus` 
ADD CONSTRAINT `fk_adm_menus_adm_menus`
  FOREIGN KEY ()
  REFERENCES `adm_menus` ();



INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('1', 'Setup', '/', 'fa fa-edit', '0', '1', 'ADMIN', '2019-10-01', '1', '2019-10-01', '?????????', '1', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('2', 'Skills Types', '#/skillsTypes', 'fa fa-circle-o', '1', '1', 'ADMIN', '2019-10-02', '1', '2019-10-02', '????????', '1', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('3', 'Positions', '#/positions', 'fa fa-circle-o', '1', '1', 'ADMIN', '2019-10-03', '1', '2019-10-03', '???????', '2', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('4', 'Competencies', '#/competence', 'fa fa-circle-o', '1', '1', 'ADMIN', '2019-10-04', '1', '2019-10-04', '????????', '3', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('5', 'Employees', '#/Employees', 'fa fa-circle-o', '1', '1', 'ADMIN', '2019-10-05', '1', '2019-10-05', '????????', '4', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('6', 'Planning', '/', 'fa fa-edit', '0', '1', 'ADMIN', '2019-10-06', '1', '2019-10-06', '?????', '2', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('7', 'Strategic Objectives', '#/stratigicobjectives', 'fa fa-circle-o', '6', '1', 'ADMIN', '2019-10-07', '1', '2019-10-07', '??????? ????????????', '1', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('8', 'Projects', '#/Projects', 'fa fa-circle-o', '6', '1', 'ADMIN', '2019-10-08', '1', '2019-10-08', '??????', '2', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('9', 'Projects Planner', '#/stratigicobjectivesChart', 'fa fa-circle-o', '6', '1', 'ADMIN', '2019-10-09', '1', '2019-10-09', '???? ????????', '3', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('10', 'Employee Structure', '#/EmpStructure', 'fa fa-circle-o', '6', '1', 'ADMIN', '2019-10-10', '1', '2019-10-10', '???? ??????', '4', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('11', 'Employee Performance Plan', '#/employeeObjectve', 'fa fa-circle-o', '6', '1', 'ADMIN', '2019-10-11', '1', '2019-10-11', '??? ???? ??????', '5', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('12', 'Operation Assessment', '/', 'fa fa-edit', '0', '1', 'ADMIN', '2019-10-12', '1', '2019-10-12', '??????? ????????', '3', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('13', 'Projects Assessment', '#/projectsAssessment', 'fa fa-circle-o', '12', '1', 'ADMIN', '2019-10-13', '1', '2019-10-13', '????? ?????????', '1', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('14', 'Projects Navigation', '#/stratigicobjectivesChart', 'fa fa-circle-o', '12', '1', 'ADMIN', '2019-10-14', '1', '2019-10-14', '?????? ?? ?????????', '2', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('15', 'Employees Performance Assessment', '#/employeeAssessment', 'fa fa-circle-o', '12', '1', 'ADMIN', '2019-10-15', '1', '2019-10-15', '????? ???? ????????', '3', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('16', 'Employee Navigation', '#/EmpStructure', 'fa fa-circle-o', '12', '1', 'ADMIN', '2019-10-16', '1', '2019-10-16', '???? ??????', '4', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('17', 'Dashboard Analysis', '/', 'fa fa-edit', '0', '1', 'ADMIN', '2019-10-17', '1', '2019-10-17', '???? ??????? ????????', '4', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('18', 'Organization Dashboard', '[\'/DashBoards\']', 'fa fa-circle-o', '17', '1', 'ADMIN', '2019-10-18', '1', '2019-10-18', '???? ??????? ???????', '1', 'HRMS', 'HOBJ');
INSERT INTO `adm_menus` (`ID`, `NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `modified_by`, `modified_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('19', 'Employee Dashboard', '[\'/EmpDashBoards\']', 'fa fa-circle-o', '17', '1', 'ADMIN', '2019-10-19', '1', '2019-10-19', '???? ??????? ??????', '2', 'HRMS', 'HOBJ');



update adm_menus set modified_by = null , modified_date = null;
update adm_menus set parent_id = null where parent_id  = 0;


INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('1', '1', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('2', '2', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('3', '3', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('4', '4', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('5', '5', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('6', '6', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('7', '7', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('8', '8', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('9', '9', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('10', '10', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('11', '11', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('12', '12', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('13', '13', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('14', '14', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('15', '15', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('16', '16', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('17', '17', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('18', '18', '1', 'ADMIN', '2019-01-01');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('19', '19', '1', 'ADMIN', '2019-01-01');


UPDATE `adm_menus` SET `URL` = '#/DashBoards' WHERE (`ID` = '18');

UPDATE `adm_menus` SET `URL` = '#/EmpDashBoards' WHERE (`ID` = '19');
update adm_menus set icone = 'fa-edit' where icone  = 'fa fa-edit';
update adm_menus set icone = 'fa-circle-o' where icone  = 'fa fa-circle-o';