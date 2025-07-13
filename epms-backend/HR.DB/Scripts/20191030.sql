ALTER TABLE  `adm_company` 
ADD COLUMN `mission` LONGTEXT NULL AFTER `name2`,
ADD COLUMN `vision` LONGTEXT NULL AFTER `mission`,
ADD COLUMN `currency_c` INT(11) NOT NULL DEFAULT 1 AFTER `vision`,
ADD COLUMN `projects_link` INT(11) NOT NULL DEFAULT 1 AFTER `currency_c`,
ADD COLUMN `plan_link` INT(11) NOT NULL DEFAULT 1 AFTER `projects_link`;

INSERT INTO `adm_menus` (`NAME`, `URL`, `ICONE`, `PARENT_ID`, `COMPANY_ID`, `created_by`, `created_date`, `name2`, `order`, `application_code`, `system_code`) VALUES ('Company', '#/Company', 'fa-circle-o', '1', '1', 'ADMIN', '2019-10-30', 'Company', '6', 'HRMS', 'HOBJ');
INSERT INTO `adm_authorization` (`ID`, `MENU_ID`, `ROLE_ID`, `created_by`, `created_date`) VALUES ('22', '22', '1', 'ADMIN', '2019-10-30');
