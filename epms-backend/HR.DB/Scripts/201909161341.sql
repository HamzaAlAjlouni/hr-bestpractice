CREATE TABLE `ADM_COMPANY` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`NAME`               varchar(100)  NOT NULL    ,
	`CODE`               varchar(50)      ,
	`ADDRESS`            varchar(1000)      ,
	`FAX`                varchar(50)      ,
	`PHONE1`             varchar(50)      ,
	`PHONE2`             varchar(50)      ,
	`EMAIL`              varchar(50)      ,
	`WEBSITE`            varchar(50)      ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE TABLE `ADM_MENUS` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`NAME`               varchar(100)  NOT NULL    ,
	`URL`                varchar(100)      ,
	`ICONE`              varchar(100)      ,
	`PARENT_ID`          int  NOT NULL    ,
	`COMPANY_ID`         int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_MENUS` ON `ADM_MENUS` ( `PARENT_ID` );

CREATE INDEX `idx_ADM_MENUS_0` ON `ADM_MENUS` ( `COMPANY_ID` );

CREATE TABLE `ADM_PERFORMANCE_LEVELS` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`CODE`               varchar(50)  NOT NULL    ,
	`NAME`               varchar(100)  NOT NULL    ,
	`LEVEL_NUMBER`       int  NOT NULL    ,
	`PERCENTAGE`         float(12,0)  NOT NULL    ,
	`COMPANY_ID`         int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_HRS_PERFORMANCE_LEVELS` ON `ADM_PERFORMANCE_LEVELS` ( `COMPANY_ID` );

CREATE TABLE `ADM_POSITIONS` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`CODE`               varchar(50)  NOT NULL    ,
	`NAME`               varchar(100)  NOT NULL    ,
	`IS_MANAGMENT`       int  NOT NULL DEFAULT 0   ,
	`COMPANY_ID`         int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_POSITIONS` ON `ADM_POSITIONS` ( `COMPANY_ID` );

CREATE TABLE `ADM_ROLES` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`NAME`               varchar(100)  NOT NULL    ,
	`COMPANY_ID`         int  NOT NULL    ,
	`IS_SYSTEM_ROLE`     int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_ROLES` ON `ADM_ROLES` ( `COMPANY_ID` );

CREATE TABLE `ADM_SCALES` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`SCALE_CODE`         varchar(50)  NOT NULL    ,
	`NAME`               varchar(100)  NOT NULL    ,
	`SCALE_NUMBER`       int  NOT NULL    ,
	`COMPANY_ID`         int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_SCALES` ON `ADM_SCALES` ( `SCALE_NUMBER` );

CREATE TABLE `ADM_USERS` ( 
	`USERNAME`           varchar(100)  NOT NULL    PRIMARY KEY,
	`NAME`               varchar(100)  NOT NULL    ,
	`COMPANY_ID`         int  NOT NULL    ,
	`PASSWORD`           varchar(100)  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_USERS` ON `ADM_USERS` ( `COMPANY_ID` );

CREATE TABLE `ADM_USERS_ROLES` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`USERNAME`           varchar(100)  NOT NULL    ,
	`ROLE_ID`            int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      
 );

CREATE INDEX `idx_ADM_USERS_ROLES` ON `ADM_USERS_ROLES` ( `ROLE_ID` );

CREATE INDEX `idx_ADM_USERS_ROLES` ON `ADM_USERS_ROLES` ( `USERNAME` );

CREATE TABLE adm_actions ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	name                 varchar(100)  NOT NULL    ,
	menu_id              int  NOT NULL    
 );

CREATE INDEX fk_adm_actions_adm_menus ON adm_actions ( menu_id );

CREATE TABLE adm_auth_actions_excluded ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	action_id            int  NOT NULL    ,
	is_readonly          int  NOT NULL DEFAULT 1   ,
	is_invisible         int  NOT NULL DEFAULT 1   ,
	role_id              int  NOT NULL    
 );

CREATE INDEX fk_adm_auth_actions_excluded_adm_actions ON adm_auth_actions_excluded ( action_id );

CREATE INDEX fk_adm_auth_actions_excluded_adm_roles ON adm_auth_actions_excluded ( role_id );

CREATE TABLE adm_years ( 
	id                   int  NOT NULL  PRIMARY KEY,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      
 );

CREATE TABLE `ADM_AUTHORIZATION` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`MENU_ID`            int  NOT NULL    ,
	`ROLE_ID`            int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      
 );

CREATE INDEX `idx_ADM_AUTHORIZATION` ON `ADM_AUTHORIZATION` ( `MENU_ID` );

CREATE INDEX `idx_ADM_AUTHORIZATION` ON `ADM_AUTHORIZATION` ( `ROLE_ID` );

CREATE TABLE `ADM_CODES` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`MAJOR_NO`           int  NOT NULL    ,
	`MINOR_NO`           int  NOT NULL    ,
	`NAME`               varchar(1000)  NOT NULL    ,
	`CODE`               varchar(50)      ,
	company_id           int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX idx_adm_codes ON `ADM_CODES` ( company_id );

ALTER TABLE `ADM_CODES` COMMENT 'TITLE , SKILL';

CREATE TABLE `ADM_STRATIGIC_OBJECTIVES` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`COMPANY_ID`         int  NOT NULL    ,
	`CODE`               varchar(50)  NOT NULL    ,
	`NAME`               varchar(1000)  NOT NULL    ,
	`ORDER`              int  NOT NULL    ,
	`WEIGHT`             float(12,0)  NOT NULL    ,
	`DESCRIPTION`        varchar(1000)      ,
	year_id              int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_STRATIGIC_OBJECTIVES` ON `ADM_STRATIGIC_OBJECTIVES` ( `COMPANY_ID` );

CREATE INDEX fk_adm_stratigic_obj_adm_years ON `ADM_STRATIGIC_OBJECTIVES` ( year_id );

CREATE TABLE `ADM_UNITS` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`NAME`               varchar(1000)  NOT NULL    ,
	`CODE`               varchar(50)  NOT NULL    ,
	`ADDRESS`            varchar(1000)      ,
	`FAX`                varchar(50)      ,
	`PHONE1`             varchar(50)      ,
	`PHONE2`             varchar(50)      ,
	`C_UNIT_TYPE_ID`     int  NOT NULL    ,
	`COMPANY_ID`         int  NOT NULL    ,
	parent_id            int      ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_UNITS` ON `ADM_UNITS` ( `C_UNIT_TYPE_ID` );

CREATE INDEX `idx_ADM_UNITS_0` ON `ADM_UNITS` ( `COMPANY_ID` );

CREATE INDEX fk_adm_units_adm_units ON `ADM_UNITS` ( parent_id );

CREATE TABLE adm_branches ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`NAME`               varchar(100)  NOT NULL    ,
	`CODE`               varchar(50)  NOT NULL    ,
	`ADDRESS`            varchar(1000)      ,
	`FAX`                varchar(50)      ,
	`PHONE1`             varchar(50)      ,
	`PHONE2`             varchar(50)      ,
	`EMAIL`              varchar(50)      ,
	`WEBSITE`            varchar(50)      ,
	`COMPANY_ID`         int  NOT NULL    ,
	`C_COUNTRY_ID`       int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_BRNACHES` ON adm_branches ( `COMPANY_ID` );

CREATE INDEX `idx_ADM_BRNACHES_0` ON adm_branches ( `C_COUNTRY_ID` );

CREATE TABLE adm_competencies ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	code                 varchar(50)  NOT NULL    ,
	name                 varchar(1000)  NOT NULL    ,
	c_nature_id          int  NOT NULL    ,
	company_id           int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX fk_adm_competencies_adm_codes ON adm_competencies ( c_nature_id );

CREATE INDEX fk_adm_competencies_adm_company ON adm_competencies ( company_id );

CREATE TABLE adm_competencies_kpi ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	name                 varchar(1000)  NOT NULL    ,
	c_kpi_type_id        int  NOT NULL    ,
	competence_id        int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX fk_adm_competencies_kpi_adm_competencies ON adm_competencies_kpi ( competence_id );

CREATE TABLE adm_position_competencies ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	position_id          int  NOT NULL    ,
	competence_id        int  NOT NULL    
 );

CREATE INDEX fk_adm_position_competencies_adm_competencies ON adm_position_competencies ( competence_id );

CREATE INDEX fk_adm_position_competencies_adm_positions ON adm_position_competencies ( position_id );

CREATE TABLE `ADM_EMPLOYEES` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	name1_1              varchar(100)  NOT NULL    ,
	name1_2              varchar(100)      ,
	name1_3              varchar(100)      ,
	name1_4              varchar(100)  NOT NULL    ,
	`UNIT_ID`            int  NOT NULL    ,
	`COMPANY_ID`         int  NOT NULL    ,
	`ADDRESS`            varchar(1000)      ,
	`PHONE1`             varchar(50)      ,
	`PHONE2`             varchar(50)      ,
	`PARENT_ID`          int      ,
	`IS_STATUS`          int  NOT NULL DEFAULT 0   ,
	`IMAGE`              varchar(1000)      ,
	`BRANCH_ID`          int  NOT NULL    ,
	`SCALE_ID`           int  NOT NULL    ,
	`START_DATE`         date  NOT NULL    ,
	`END_DATE`           date      ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2_1              varchar(100)      ,
	name2_2              varchar(100)      ,
	name2_3              varchar(100)      ,
	name2_4              varchar(100)      
 );

CREATE INDEX `idx_HRS_EMPLOYEES_0` ON `ADM_EMPLOYEES` ( `UNIT_ID` );

CREATE INDEX `idx_HRS_EMPLOYEES_1` ON `ADM_EMPLOYEES` ( `COMPANY_ID` );

CREATE INDEX `idx_HRS_EMPLOYEES_2` ON `ADM_EMPLOYEES` ( `PARENT_ID` );

CREATE INDEX `idx_ADM_EMPLOYEES` ON `ADM_EMPLOYEES` ( `BRANCH_ID` );

CREATE INDEX `idx_ADM_EMPLOYEES_0` ON `ADM_EMPLOYEES` ( `SCALE_ID` );

CREATE TABLE `ADM_PROJECTS` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`COMPANY_ID`         int  NOT NULL    ,
	`CODE`               varchar(50)  NOT NULL    ,
	`NAME`               varchar(1000)  NOT NULL    ,
	`PROJECT_ORDER`      int  NOT NULL    ,
	`WEIGHT`             float(12,0)  NOT NULL    ,
	`UNIT_ID`            int  NOT NULL    ,
	`TARGET`             int  NOT NULL    ,
	`C_KPI_CYCLE_ID`     int  NOT NULL    ,
	`C_KPI_TYPE_ID`      int  NOT NULL    ,
	`C_RESULT_UNIT_ID`   int  NOT NULL    ,
	`DESCRIPTION`        varchar(1000)      ,
	`KPI`                varchar(1000)      ,
	`STARG_OBJ_ID`       int  NOT NULL    ,
	`BRANCH_ID`          int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	name2                varchar(100)      
 );

CREATE INDEX `idx_ADM_PROJECTS_COM` ON `ADM_PROJECTS` ( `COMPANY_ID` );

CREATE INDEX `idx_ADM_PROJECTS_1` ON `ADM_PROJECTS` ( `C_KPI_CYCLE_ID` );

CREATE INDEX `idx_ADM_PROJECTS_2` ON `ADM_PROJECTS` ( `C_KPI_TYPE_ID` );

CREATE INDEX `idx_ADM_PROJECTS_3` ON `ADM_PROJECTS` ( `C_RESULT_UNIT_ID` );

CREATE INDEX `idx_ADM_PROJECTS_4` ON `ADM_PROJECTS` ( `UNIT_ID` );

CREATE INDEX `idx_ADM_PROJECTS` ON `ADM_PROJECTS` ( `STARG_OBJ_ID` );

CREATE INDEX `idx_ADM_PROJECTS_0` ON `ADM_PROJECTS` ( `BRANCH_ID` );

CREATE TABLE adm_prj_results ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	period_no            int  NOT NULL    ,
	plan_result          float(12,0)  NOT NULL    ,
	actual_result        float(12,0)      ,
	project_id           int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      
 );

CREATE INDEX fk_adm_prj_results_adm_projects ON adm_prj_results ( project_id );

CREATE TABLE `ADM_PRJ_STRAT_OBJ` ( 
	`ID`                 int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	`PROJECT_ID`         int  NOT NULL    ,
	`STARTG_OBJ_ID`      int  NOT NULL    ,
	`Q1`                 int  NOT NULL DEFAULT 0   ,
	`Q2`                 int  NOT NULL DEFAULT 0   ,
	`Q3`                 int  NOT NULL DEFAULT 0   ,
	`Q4`                 int  NOT NULL DEFAULT 0   ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      
 );

CREATE INDEX `idx_ADM_PRJ_STRAT_OBJ` ON `ADM_PRJ_STRAT_OBJ` ( `STARTG_OBJ_ID` );

CREATE INDEX `idx_ADM_PRJ_STRAT_OBJ_0` ON `ADM_PRJ_STRAT_OBJ` ( `PROJECT_ID` );

ALTER TABLE `ADM_AUTHORIZATION` ADD CONSTRAINT fk_adm_authorization_adm_menus FOREIGN KEY ( `MENU_ID` ) REFERENCES `ADM_MENUS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_AUTHORIZATION` ADD CONSTRAINT fk_adm_authorization_adm_roles FOREIGN KEY ( `ROLE_ID` ) REFERENCES `ADM_ROLES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_CODES` ADD CONSTRAINT fk_adm_codes FOREIGN KEY ( company_id ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_CODES` ADD CONSTRAINT fk_adm_codes_adm_company FOREIGN KEY ( company_id ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_EMPLOYEES` ADD CONSTRAINT `fk_EMPLOYEES_UNIT` FOREIGN KEY ( `UNIT_ID` ) REFERENCES `ADM_UNITS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_EMPLOYEES` ADD CONSTRAINT `fk_EMPLOYEES_PARENT` FOREIGN KEY ( `PARENT_ID` ) REFERENCES `ADM_EMPLOYEES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_EMPLOYEES` ADD CONSTRAINT `fk_ADM_EMPLOYEES_BRN` FOREIGN KEY ( `BRANCH_ID` ) REFERENCES adm_branches( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_EMPLOYEES` ADD CONSTRAINT `fk_ADM_EMPLOYEES` FOREIGN KEY ( `SCALE_ID` ) REFERENCES `ADM_SCALES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_EMPLOYEES` ADD CONSTRAINT fk_adm_employees_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_MENUS` ADD CONSTRAINT fk_adm_menus_adm_menus FOREIGN KEY ( `PARENT_ID` ) REFERENCES `ADM_MENUS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_MENUS` ADD CONSTRAINT fk_adm_menus_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_POSITIONS` ADD CONSTRAINT fk_adm_positions_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PRJ_STRAT_OBJ` ADD CONSTRAINT `fk_ADM_PRJ_STRAT_OBJ` FOREIGN KEY ( `STARTG_OBJ_ID` ) REFERENCES `ADM_STRATIGIC_OBJECTIVES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PRJ_STRAT_OBJ` ADD CONSTRAINT `fk_ADM_PRJ_STRAT_OBJ_0` FOREIGN KEY ( `PROJECT_ID` ) REFERENCES `ADM_PROJECTS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PROJECTS` ADD CONSTRAINT `fk_ADM_PROJECTS_CYC` FOREIGN KEY ( `C_KPI_CYCLE_ID` ) REFERENCES `ADM_CODES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PROJECTS` ADD CONSTRAINT `fk_ADM_PROJECTS_TYPE` FOREIGN KEY ( `C_KPI_TYPE_ID` ) REFERENCES `ADM_CODES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PROJECTS` ADD CONSTRAINT `fk_ADM_PROJECTS_R_UNIT` FOREIGN KEY ( `C_RESULT_UNIT_ID` ) REFERENCES `ADM_CODES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PROJECTS` ADD CONSTRAINT `fk_ADM_PROJECTS_UNIT` FOREIGN KEY ( `UNIT_ID` ) REFERENCES `ADM_UNITS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PROJECTS` ADD CONSTRAINT `fk_ADM_PROJECTS` FOREIGN KEY ( `STARG_OBJ_ID` ) REFERENCES `ADM_STRATIGIC_OBJECTIVES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PROJECTS` ADD CONSTRAINT `fk_ADM_PROJECTS_0` FOREIGN KEY ( `BRANCH_ID` ) REFERENCES adm_branches( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PROJECTS` ADD CONSTRAINT fk_adm_projects_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_PROJECTS` ADD CONSTRAINT fk_adm_projects_adm_company_0 FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_ROLES` ADD CONSTRAINT fk_adm_roles_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_SCALES` ADD CONSTRAINT fk_adm_scales_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_STRATIGIC_OBJECTIVES` ADD CONSTRAINT fk_adm_stratigic_objectives_adm_years FOREIGN KEY ( year_id ) REFERENCES adm_years( id ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_STRATIGIC_OBJECTIVES` ADD CONSTRAINT fk_adm_stratigic_objectives_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_UNITS` ADD CONSTRAINT `fk_ADM_UNITS` FOREIGN KEY ( `C_UNIT_TYPE_ID` ) REFERENCES `ADM_CODES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_UNITS` ADD CONSTRAINT `fk_ADM_UNITS_0` FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_UNITS` ADD CONSTRAINT fk_adm_units_adm_units FOREIGN KEY ( parent_id ) REFERENCES `ADM_UNITS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_UNITS` ADD CONSTRAINT fk_adm_units_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_USERS` ADD CONSTRAINT fk_adm_users_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_USERS_ROLES` ADD CONSTRAINT fk_adm_users_roles_adm_roles FOREIGN KEY ( `ROLE_ID` ) REFERENCES `ADM_ROLES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE `ADM_USERS_ROLES` ADD CONSTRAINT fk_adm_users_roles_adm_users FOREIGN KEY ( `USERNAME` ) REFERENCES `ADM_USERS`( `USERNAME` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_actions ADD CONSTRAINT fk_adm_actions_adm_menus FOREIGN KEY ( menu_id ) REFERENCES `ADM_MENUS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_auth_actions_excluded ADD CONSTRAINT fk_adm_auth_actions_excluded_adm_actions FOREIGN KEY ( action_id ) REFERENCES adm_actions( id ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_branches ADD CONSTRAINT `fk_ADM_BRNACHES_COUNTRY` FOREIGN KEY ( `C_COUNTRY_ID` ) REFERENCES `ADM_CODES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_branches ADD CONSTRAINT fk_adm_branches_adm_company FOREIGN KEY ( `COMPANY_ID` ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_competencies ADD CONSTRAINT fk_adm_competencies_adm_codes FOREIGN KEY ( c_nature_id ) REFERENCES `ADM_CODES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_competencies ADD CONSTRAINT fk_adm_competencies_adm_company_0 FOREIGN KEY ( company_id ) REFERENCES `ADM_COMPANY`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_competencies_kpi ADD CONSTRAINT fk_adm_competencies_kpi_adm_competencies FOREIGN KEY ( competence_id ) REFERENCES adm_competencies( id ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_position_competencies ADD CONSTRAINT fk_adm_position_competencies_adm_positions FOREIGN KEY ( position_id ) REFERENCES `ADM_POSITIONS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_position_competencies ADD CONSTRAINT fk_adm_position_competencies_adm_competencies FOREIGN KEY ( competence_id ) REFERENCES adm_competencies( id ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_prj_results ADD CONSTRAINT fk_adm_prj_results_adm_projects FOREIGN KEY ( project_id ) REFERENCES `ADM_PROJECTS`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;
