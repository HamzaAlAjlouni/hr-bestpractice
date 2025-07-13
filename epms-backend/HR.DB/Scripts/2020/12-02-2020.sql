INSERT INTO `hr1_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCALLOption', 'All', '1', 'en');
INSERT INTO `hr1_db`.`tbl_resources` (`url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES ('#/stratigicobjectives', 'lblKpiBSCALLOption', 'الكل', '1', 'ar');
INSERT INTO `hr1_db`.`tbl_resources` (`id`, `url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES (NULL, '#/employeeObjectve', 'lblKpiMeasurePercentageOption', 'Percentage', '1', 'en');
INSERT INTO `hr1_db`.`tbl_resources` (`id`, `url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES (NULL, '#/employeeObjectve', 'lblKpiMeasurePercentageOption', 'نسبة مئوية', '1', 'ar');
INSERT INTO `hr1_db`.`tbl_resources` (`id`, `url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES (NULL, '#/employeeObjectve', 'lblKpiMeasureValueOption', 'Value', '1', 'en');
INSERT INTO `hr1_db`.`tbl_resources` (`id`, `url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES (NULL, '#/employeeObjectve', 'lblKpiMeasureValueOption', 'قيمة', '1', 'ar');
INSERT INTO `hr1_db`.`tbl_resources` (`id`, `url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES (NULL, '#/employeeObjectve', 'lblKpiMeasure', 'Target Type', '1', 'en');
INSERT INTO `hr1_db`.`tbl_resources` (`id`, `url`, `resource_key`, `resource_value`, `org_id`, `culture_name`) VALUES (NULL, '#/employeeObjectve', 'lblKpiMeasure', 'القياس', '1', 'ar');

UPDATE `hr1_db`.`tbl_resources` SET `resource_value` = 'Please Select Unit' WHERE (`id` = '250');


alter table adm_project_evidence modify column project_id int(11) null;

alter table adm_project_evidence add column objective_id int(11) null;

alter table adm_Objective_kpi add is_obj_kpi  int not null default 1;


create table adm_action_plan (
id bigint auto_increment not null primary key,
project_id bigint not null,
project_kpi_id bigint not null,
emp_id bigint not null,
action_name nvarchar(2000) not null,
action_req nvarchar(2000) not null,
action_cost float null default 0,
action_date date not null,
action_weight float not null,
action_notes nvarchar(2000) null,
created_by	varchar(100) not null,
created_date	date not null,
modified_by	varchar(100),
modified_date	date
);


alter table adm_prj_results add column kpi_id int;


alter table  adm_Objective_kpi add column C_KPI_CYCLE_ID int(11) default 1;
alter table  adm_Objective_kpi add column C_KPI_TYPE_ID int(11) default 1;
alter table  adm_Objective_kpi add column C_RESULT_UNIT_ID int(11) default 1;

alter table adm_emp_objective add target_type int  not null default 1;

alter table adm_competencies add column notes nvarchar(3000) null;


create table adm_emp_perf_segments (
id bigint not null primary key auto_increment,
name nvarchar(500) not null,
description nvarchar(2000) null,
segment int not null,
percentage_from float null,
percentage_to float null,
year int not null,
company_id int not null

);


alter table adm_action_plan add column attachment nvarchar(2000) null;

create table adm_approval_setup
( 
id bigint not null auto_increment primary key,
name nvarchar(2000) not null,
page_url varchar(300) not null,
reviewing_user nvarchar(500) not null,
company_id bigint not null
);

alter table adm_projects add column planned_status int default 1;
alter table adm_projects add column assessment_status int default 1;

create table adm_traffic_light_indicator (
id bigint not null auto_increment primary key,
name nvarchar(2000) not null,
perc_from float not null,
perc_to float not null,
color nvarchar(2000) not null,
year int not null,
company_id bigint not null);
