INSERT INTO `hr1_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Company', 'lblValues', 'Values', '1', 'en');
INSERT INTO `hr1_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/Company', 'lblValues', 'القيم', '1', 'ar');


alter table adm_company add column company_values longtext null;

create table adm_project_actionPlans(
id bigint primary key auto_increment,
name varchar(2000),
description varchar(2000),
projectID bigint,
created_by	varchar(45),
created_date	date,
modified_by	varchar(45),
modified_date	date
);