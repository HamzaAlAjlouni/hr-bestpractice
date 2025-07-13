CREATE TABLE `ADM_EMPLOYEE_POSITIONS` ( 
	ID                 		int  NOT NULL  AUTO_INCREMENT  PRIMARY KEY,
	YEAR         			int NOT NULL    ,
	POSITION_ID             int  NOT NULL    ,
	EMP_ID		            int  NOT NULL    ,
	created_by              varchar(100)  NOT NULL    ,
	created_date            date  NOT NULL    ,
	modified_by             varchar(100)      ,
	modified_date           date      ,
	name2                   varchar(100) ,
    FOREIGN KEY (EMP_ID) REFERENCES adm_employees(id),
    FOREIGN KEY (YEAR) REFERENCES adm_years(id),
    FOREIGN KEY (POSITION_ID) REFERENCES adm_positions(id)
 );
 

 ALTER TABLE `hrdb`.`adm_codes` 
ADD UNIQUE INDEX `uniqe_index` (`MAJOR_NO` ASC, `MINOR_NO` ASC, `company_id` ASC) VISIBLE;
;
