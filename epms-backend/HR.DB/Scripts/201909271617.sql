CREATE TABLE adm_emp_assesment ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	year_id              int  NOT NULL    ,
	c_kpi_cycle          int  NOT NULL    ,
	agreement_date       date  NOT NULL    ,
	emp_reviewer_id      int  NOT NULL    ,
	created_by           varchar(50)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(50)      ,
	modified_date        date      ,
	attachment           varchar(1000)      ,
	emp_position_id      int  NOT NULL    ,
	employee_id          int  NOT NULL    
 ) engine=InnoDB;

CREATE INDEX fk_adm_emp_assesment_adm_employees ON adm_emp_assesment ( emp_reviewer_id );

CREATE INDEX fk_adm_emp_assesment_adm_years ON adm_emp_assesment ( year_id );

ALTER TABLE adm_emp_assesment ADD CONSTRAINT fk_adm_emp_assesment_adm_employees FOREIGN KEY ( emp_reviewer_id ) REFERENCES `ADM_EMPLOYEES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_emp_assesment ADD CONSTRAINT fk_adm_emp_assesment_adm_years FOREIGN KEY ( year_id ) REFERENCES adm_years( id ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE adm_emp_assesment ADD CONSTRAINT fk_adm_emp_assesment_adm_employees_0 FOREIGN KEY ( employee_id ) REFERENCES `ADM_EMPLOYEES`( `ID` ) ON DELETE NO ACTION ON UPDATE NO ACTION;


CREATE TABLE adm_emp_objective ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	name                 varchar(1000)  NOT NULL    ,
	name2                varchar(1000)      ,
	code                 varchar(50)  NOT NULL    ,
	weight               float(18,3)  NOT NULL    ,
	note                 varchar(1000)      ,
	emp_assesment_id     int  NOT NULL    ,
	created_by           varchar(100)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(100)      ,
	modified_date        date      ,
	project_id           int  NOT NULL    ,
	pos_desc_id          int  NOT NULL    
 ) engine=InnoDB;

CREATE INDEX fk_adm_emp_project_adm_employees ON adm_emp_objective ( emp_assesment_id );

ALTER TABLE adm_emp_objective ADD CONSTRAINT fk_adm_emp_objective_adm_emp_assesment FOREIGN KEY ( emp_assesment_id ) REFERENCES adm_emp_assesment( id ) ON DELETE NO ACTION ON UPDATE NO ACTION;


CREATE TABLE adm_emp_obj_kpi ( 
	id                   int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	name                 varchar(1000)  NOT NULL    ,
	name2                varchar(1000)      ,
	created_by           varchar(50)  NOT NULL    ,
	created_date         date  NOT NULL    ,
	modified_by          varchar(50)      ,
	modified_date        date      ,
	emp_obj_id           int  NOT NULL    ,
	target               float(18,3)  NOT NULL    
 ) engine=InnoDB;

CREATE INDEX fk_adm_emp_obj_kpi_adm_emp_objective ON adm_emp_obj_kpi ( emp_obj_id );

ALTER TABLE adm_emp_obj_kpi ADD CONSTRAINT fk_adm_emp_obj_kpi_adm_emp_objective FOREIGN KEY ( emp_obj_id ) REFERENCES adm_emp_objective( id ) ON DELETE NO ACTION ON UPDATE NO ACTION;
